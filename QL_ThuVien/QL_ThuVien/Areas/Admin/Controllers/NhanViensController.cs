using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;
using static QL_ThuVien.Areas.Admin.Controllers.LoginController;

namespace QL_ThuVien.Areas.Admin.Controllers
{
    public class NhanViensController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin/NhanViens
        public ActionResult Index()
        {
            return View(db.NhanViens.ToList());
        }

        // GET: Admin/NhanViens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                var dao = new LoginController();
                var encryptedMd5Pas = Encryptor.MD5Hash(nv.NV_MATKHAU);
                nv.NV_MATKHAU = encryptedMd5Pas;
                string id = dao.Insert(nv);
                if (id != null)
                {
                    return RedirectToAction("Index", "NhanViens");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user thành công");
                }
            }
            return View("Index");
        }

        // GET: Admin/NhanViens/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NV_ID,NV_HOTEN,NV_NGAYSINH,NV_GIOITINH,NV_EMAIL,NV_SDT,NV_QUEQUAN,NV_MATKHAU,NV_HINHANH")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanViens/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
