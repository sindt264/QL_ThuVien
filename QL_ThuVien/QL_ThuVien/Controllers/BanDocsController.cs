﻿using System;
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

namespace QL_ThuVien.Controllers
{
    public class BanDocsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: BanDocs
        public ActionResult Index(string timkiem, int page = 1, int pagesize = 5)
        {
            var model = ListAllPage(timkiem, page, pagesize);
            ViewBag.TimKiem = timkiem;
            return View(model);
        }

        // GET: BanDocs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BanDoc banDoc = db.BanDocs.Find(id);
            if (banDoc == null)
            {
                return HttpNotFound();
            }
            return View(banDoc);
        }

        // GET: BanDocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BanDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BD_SoThe,BD_HoVaTen,BD_NgaySinh,BD_SoCMND,BD_CapNgay,BD_NoiCap,BD_TrinhDo,BD_NoiCongTacHocTap,BD_NgheNghiep,BD_ChucVu,BD_HopDongLaoDong,BD_DiaChiNoiLamViec,BD_DTCoQuan,BD_DTDIDong,BD_Email,BD_ChoOHienTai,BD_GioiHanMuon,BD_NgayCap,BD_HinhAnh")] BanDoc banDoc, HttpPostedFileBase fileUpload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fileimg = Request.Files["fileUpload"];
                    string sothe = Request["BD_SoThe"];
                    var listmathe = from s in db.BanDocs select s.BD_SoThe;
                    bool kq = listmathe.Contains(sothe);
                    if (kq == true)
                    {
                        ViewBag.kq = "<div class ='text-danger'> Số thẻ đã tồn tại! </div>";
                    }
                    else
                    {
                        if (fileimg.FileName.Length != 0)
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
                            banDoc.BD_HinhAnh = fileUpload.FileName;
                        }
                        db.BanDocs.Add(banDoc);
                        db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            var list = from s in db.BanDocs select s;
            return View();
        }

        // GET: BanDocs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BanDoc banDoc = db.BanDocs.Find(id);
            if (banDoc == null)
            {
                return HttpNotFound();
            }
            return View(banDoc);
        }

        // POST: BanDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BD_SoThe,BD_HoVaTen,BD_NgaySinh,BD_SoCMND,BD_CapNgay,BD_NoiCap,BD_TrinhDo,BD_NoiCongTacHocTap,BD_NgheNghiep,BD_ChucVu,BD_HopDongLaoDong,BD_DiaChiNoiLamViec,BD_DTCoQuan,BD_DTDIDong,BD_Email,BD_ChoOHienTai,BD_GioiHanMuon,BD_NgayCap,BD_HinhAnh")] BanDoc banDoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banDoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(banDoc);
        }

        // GET: BanDocs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BanDoc banDoc = db.BanDocs.Find(id);
            if (banDoc == null)
            {
                return HttpNotFound();
            }
            return View(banDoc);
        }

        // POST: BanDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BanDoc banDoc = db.BanDocs.Find(id);
            db.BanDocs.Remove(banDoc);
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

        public IEnumerable<BanDoc> ListAllPage(string timkiem, int page, int rowLimit)
        {
            IQueryable<BanDoc> model = db.BanDocs;
            if (!string.IsNullOrEmpty(timkiem))
            {
                model = model.Where(x => x.BD_HoVaTen.Contains(timkiem));
            }
            return model.OrderBy(b => b.BD_SoThe).ToPagedList(page, rowLimit);
        }
    }
}
