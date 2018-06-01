using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Controllers
{
    public class HinhAnhHoatDongsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: HinhAnhHoatDongs
        public ActionResult Index()
        {
            var hinhAnhHoatDongs = db.HinhAnhHoatDongs.Include(h => h.HoatDong);
            return View(hinhAnhHoatDongs.ToList());
        }

        // GET: HinhAnhHoatDongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnhHoatDong hinhAnhHoatDong = db.HinhAnhHoatDongs.Find(id);
            if (hinhAnhHoatDong == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnhHoatDong);
        }

        // GET: HinhAnhHoatDongs/Create
        public ActionResult Create()
        {
            ViewBag.HD_IDHoatDong = new SelectList(db.HoatDongs, "HD_IDHoatDong", "HD_ChuDe");
            return View();
        }

        // POST: HinhAnhHoatDongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HA_IDHinhAnh,HD_IDHoatDong,HA_ChuThich,HA_NoiDung")] HinhAnhHoatDong hinhAnhHoatDong)
        {
            int datalength = (int)Request.Files["image"].InputStream.Length;
            byte[] _byteArr = new byte[datalength];
            Request.Files[0].InputStream.Read(_byteArr, 0, datalength);

            if (ModelState.IsValid)
            {
                hinhAnhHoatDong.HA_NoiDung = _byteArr;
                db.HinhAnhHoatDongs.Add(hinhAnhHoatDong);
                db.SaveChanges();
                //ModelState.AddModelError("", "Xong");
                return RedirectToAction("Index");
            }

            ViewBag.HD_IDHoatDong = new SelectList(db.HoatDongs, "HD_IDHoatDong", "HD_ChuDe", hinhAnhHoatDong.HD_IDHoatDong);
            return View(hinhAnhHoatDong);
        }

        public ActionResult getImage(string id)
        {
            string strID = Request.QueryString["ID"];
            int ID = -1;
            if (int.TryParse(id, out ID))
            {
                //var ha = db.HinhAnhHoatDongs.Where(h => h.HD_IDHoatDong == ID).FirstOrDefault();
                var ha = from p in db.HinhAnhHoatDongs where p.HA_IDHinhAnh == ID select p;
                foreach(var i in ha) { 
                if (i == null || i.HA_NoiDung == null)
                {
                    ModelState.AddModelError("", "Loi");

                }
                    ViewBag.ha = i.HA_NoiDung;
                //Response.ContentType = "image/jpeg";
                Response.OutputStream.Write(i.HA_NoiDung.ToArray(), 0, i.HA_NoiDung.Length);
                Response.Flush();}
            }

            return View();
        }
        // GET: HinhAnhHoatDongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnhHoatDong hinhAnhHoatDong = db.HinhAnhHoatDongs.Find(id);
            if (hinhAnhHoatDong == null)
            {
                return HttpNotFound();
            }
            ViewBag.HD_IDHoatDong = new SelectList(db.HoatDongs, "HD_IDHoatDong", "HD_ChuDe", hinhAnhHoatDong.HD_IDHoatDong);
            return View(hinhAnhHoatDong);
        }

        // POST: HinhAnhHoatDongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HA_IDHinhAnh,HD_IDHoatDong,HA_ChuThich,HA_NoiDung")] HinhAnhHoatDong hinhAnhHoatDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhAnhHoatDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HD_IDHoatDong = new SelectList(db.HoatDongs, "HD_IDHoatDong", "HD_ChuDe", hinhAnhHoatDong.HD_IDHoatDong);
            return View(hinhAnhHoatDong);
        }

        // GET: HinhAnhHoatDongs/Delete/5
        public ActionResult Delete(int? id)
        {
            HinhAnhHoatDong hinhAnhHoatDong = db.HinhAnhHoatDongs.Find(id);
            db.HinhAnhHoatDongs.Remove(hinhAnhHoatDong);
            db.SaveChanges();
            return RedirectToAction("Index");
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //HinhAnhHoatDong hinhAnhHoatDong = db.HinhAnhHoatDongs.Find(id);
            //if (hinhAnhHoatDong == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(hinhAnhHoatDong);
        }

        // POST: HinhAnhHoatDongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HinhAnhHoatDong hinhAnhHoatDong = db.HinhAnhHoatDongs.Find(id);
            db.HinhAnhHoatDongs.Remove(hinhAnhHoatDong);
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
