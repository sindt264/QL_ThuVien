﻿using System;
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
    public class HoatDongController : Controller
    {
        private DataContext db = new DataContext();

        // GET: HoatDong
        public ActionResult Index()
        {
            
            var data = (from p in db.HoatDongs select p).OrderByDescending(x => x.HD_NgayHoatDong).Take(3);
            ViewBag.data = data;

            var data1 = (from p in db.HoatDongs select p).OrderByDescending(x => x.HD_NgayHoatDong).Take(6);
            ViewBag.data1 = data1;

            var sachmoi = (from p in db.TaiLieux select p).OrderByDescending(s => s.TL_NgayNhap).Take(6);
            ViewBag.sachmoi = sachmoi;
            return View(db.HoatDongs.ToList());
        }

        public ActionResult getlist()
        {
            return View(db.HoatDongs.ToList());
        }
        public string getImage(int id)
        {
            string HA = db.Database.SqlQuery<string>("select top 1 HA_NoiDung from HinhAnhHoatDong where HD_IDHoatDong =" + id + "").FirstOrDefault();
            return HA;
        }
        // GET: HoatDong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatDong hoatDong = db.HoatDongs.Find(id);
            if (hoatDong == null)
            {
                return HttpNotFound();
            }
            return View(hoatDong);
        }

        // GET: HoatDong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HoatDong/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HD_IDHoatDong,HD_ChuDe,HD_NoiDung,HD_NgayHoatDong,HD_NgayKetThuc")] HoatDong hoatDong)
        {
            if (ModelState.IsValid)
            {
                db.HoatDongs.Add(hoatDong);
                db.SaveChanges();
                return RedirectToAction("getlist");
            }

            return View(hoatDong);
        }

        // GET: HoatDong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatDong hoatDong = db.HoatDongs.Find(id);
            if (hoatDong == null)
            {
                return HttpNotFound();
            }
            return View(hoatDong);
        }

        // POST: HoatDong/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HD_IDHoatDong,HD_ChuDe,HD_NoiDung,HD_NgayHoatDong,HD_NgayKetThuc")] HoatDong hoatDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoatDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("getlist");
            }
            return View(hoatDong);
        }

        // GET: HoatDong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatDong hoatDong = db.HoatDongs.Find(id);
            if (hoatDong == null)
            {
                return HttpNotFound();
            }
            return View(hoatDong);
        }

        // POST: HoatDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoatDong hoatDong = db.HoatDongs.Find(id);
            db.HoatDongs.Remove(hoatDong);
            db.SaveChanges();
            return RedirectToAction("getlist");
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
