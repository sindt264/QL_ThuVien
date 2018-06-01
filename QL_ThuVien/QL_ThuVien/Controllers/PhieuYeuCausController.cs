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
    public class PhieuYeuCausController : Controller
    {
        private DataContext db = new DataContext();

        // GET: PhieuYeuCaus
        public ActionResult Index()
        {
            var phieuYeuCaus = db.PhieuYeuCaus.Include(p => p.BanDoc).Include(p => p.NhanVien).Include(p => p.TaiLieu);
            return View(phieuYeuCaus.ToList());
        }

        // GET: PhieuYeuCaus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuYeuCau phieuYeuCau = db.PhieuYeuCaus.Find(id);
            if (phieuYeuCau == null)
            {
                return HttpNotFound();
            }
            return View(phieuYeuCau);
        }

        // GET: PhieuYeuCaus/Create
        public ActionResult Create()
        {
            ViewBag.BD_SoThe = new SelectList(db.BanDocs, "BD_SoThe", "BD_HoVaTen");
            ViewBag.NV_ID = new SelectList(db.NhanViens, "NV_ID", "NV_HOTEN");
            ViewBag.TL_SoDangKyCaBiet = new SelectList(db.TaiLieux, "TL_SoDangKyCaBiet", "TL_TieuDe");
            return View();
        }

        // POST: PhieuYeuCaus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BD_SoThe,TL_SoDangKyCaBiet,NV_ID,PYC_NgayMuon,PYC_NgayTra")] PhieuYeuCau phieuYeuCau)
        {
            var sl = from p in db.PhieuYeuCaus select p;
            if (ModelState.IsValid)
            {
                phieuYeuCau.PYC_IDPhieuYeuCau = autoMaPYC(sl.Count());
                phieuYeuCau.PYC_Tre = 0;
                db.PhieuYeuCaus.Add(phieuYeuCau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BD_SoThe = new SelectList(db.BanDocs, "BD_SoThe", "BD_HoVaTen", phieuYeuCau.BD_SoThe);
            ViewBag.NV_ID = new SelectList(db.NhanViens, "NV_ID", "NV_HOTEN", phieuYeuCau.NV_ID);
            ViewBag.TL_SoDangKyCaBiet = new SelectList(db.TaiLieux, "TL_SoDangKyCaBiet", "TL_TieuDe", phieuYeuCau.TL_SoDangKyCaBiet);
            return View(phieuYeuCau);
        }

        int autoMaPYC(int sl)
        {
            var i = from p in db.PhieuYeuCaus where p.PYC_IDPhieuYeuCau == sl select p;
            if(i.Count() >= 1)
            {
                return autoMaPYC(sl + 1);
            }
            return sl;
        }

       
        // GET: PhieuYeuCaus/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuYeuCau phieuYeuCau = db.PhieuYeuCaus.Find(id);
            if (phieuYeuCau == null)
            {
                return HttpNotFound();
            }
            ViewBag.BD_SoThe = new SelectList(db.BanDocs, "BD_SoThe", "BD_HoVaTen", phieuYeuCau.BD_SoThe);
            ViewBag.NV_ID = new SelectList(db.NhanViens, "NV_ID", "NV_HOTEN", phieuYeuCau.NV_ID);
            ViewBag.TL_SoDangKyCaBiet = new SelectList(db.TaiLieux, "TL_SoDangKyCaBiet", "TL_TieuDe", phieuYeuCau.TL_SoDangKyCaBiet);
            return View(phieuYeuCau);
        }

        // POST: PhieuYeuCaus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PYC_IDPhieuYeuCau,BD_SoThe,TL_SoDangKyCaBiet,NV_ID,PYC_NgayMuon,PYC_NgayTra,PYC_Tre")] PhieuYeuCau phieuYeuCau)
        {
            //int id = Convert.ToInt32(Request["PYC_IDPhieuYeuCau"]);
            //var select = db.PhieuYeuCaus.Where(p => p.PYC_IDPhieuYeuCau == id);
            //foreach (var i in select)
            //{
            //    int kq = Convert.ToInt32(db.PhieuYeuCaus.Where(p => DbFunctions.DiffDays(i.PYC_NgayTra, DateTime.Now) == 0));
            //    phieuYeuCau.PYC_Tre = kq;
            //}
             
            if (ModelState.IsValid)
            {
               
                db.Entry(phieuYeuCau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BD_SoThe = new SelectList(db.BanDocs, "BD_SoThe", "BD_HoVaTen", phieuYeuCau.BD_SoThe);
            ViewBag.NV_ID = new SelectList(db.NhanViens, "NV_ID", "NV_HOTEN", phieuYeuCau.NV_ID);
            ViewBag.TL_SoDangKyCaBiet = new SelectList(db.TaiLieux, "TL_SoDangKyCaBiet", "TL_TieuDe", phieuYeuCau.TL_SoDangKyCaBiet);
            return View(phieuYeuCau);
        }

        // GET: PhieuYeuCaus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuYeuCau phieuYeuCau = db.PhieuYeuCaus.Find(id);
            if (phieuYeuCau == null)
            {
                return HttpNotFound();
            }
            return View(phieuYeuCau);
        }

        // POST: PhieuYeuCaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuYeuCau phieuYeuCau = db.PhieuYeuCaus.Find(id);
            db.PhieuYeuCaus.Remove(phieuYeuCau);
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
