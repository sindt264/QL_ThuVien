using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QL_ThuVien.Models;

namespace QL_ThuVien.Areas.Admin.Controllers
{
    public class AdminTaiLieuxController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Admin/AdminTaiLieux
        public ActionResult Index()
        {
            return View(db.TaiLieux.ToList());
        }

        // GET: Admin/AdminTaiLieux/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiLieu taiLieu = db.TaiLieux.Find(id);
            if (taiLieu == null)
            {
                return HttpNotFound();
            }
            return View(taiLieu);
        }

        // GET: Admin/AdminTaiLieux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTaiLieux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost, ActionName("Create")]
        [ValidateInput(false)]
        public ActionResult Create(TaiLieu tailieu, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Upload file
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Lưu đường dẫn file ảnh 
                    var path = Path.Combine(Server.MapPath("~/Content/Image"), fileName);
                    //Kiểm tra file đã tồn tại
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    //Them Sach Moi
                    tailieu.TL_HinhAnh = fileUpload.FileName;
                    db.TaiLieux.Add(tailieu);
                    db.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            //Cập nhật lại danh sách hiển thị
            var listBook = from s in db.TaiLieux select s;
            return View("Index", listBook);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "TL_SoDangKyCaBiet,TL_ChuDe,TL_TieuDe,TL_TacGia,TL_NhaXuatBan,TL_NamSanXuat,TL_SoTrang,TL_TomTat,TL_KhoSach,TL_TrangThai,TL_HinhAnh,TL_NgayNhap")] TaiLieu taiLieu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TaiLieux.Add(taiLieu);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(taiLieu);
        //}

        // GET: Admin/AdminTaiLieux/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiLieu taiLieu = db.TaiLieux.Find(id);
            if (taiLieu == null)
            {
                return HttpNotFound();
            }
            return View(taiLieu);
        }

        // POST: Admin/AdminTaiLieux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TL_SoDangKyCaBiet,TL_ChuDe,TL_TieuDe,TL_TacGia,TL_NhaXuatBan,TL_NamSanXuat,TL_SoTrang,TL_TomTat,TL_KhoSach,TL_TrangThai,TL_HinhAnh,TL_NgayNhap")] TaiLieu taiLieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiLieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taiLieu);
        }

        // GET: Admin/AdminTaiLieux/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiLieu taiLieu = db.TaiLieux.Find(id);
            if (taiLieu == null)
            {
                return HttpNotFound();
            }
            return View(taiLieu);
        }

        // POST: Admin/AdminTaiLieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TaiLieu taiLieu = db.TaiLieux.Find(id);
            db.TaiLieux.Remove(taiLieu);
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
