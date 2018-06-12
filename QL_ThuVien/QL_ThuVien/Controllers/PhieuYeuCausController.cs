using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;
using System.Data.Sql;

namespace QL_ThuVien.Controllers
{
    public class PhieuYeuCausController : Controller
    {
        private DataContext db = new DataContext();

        // GET: PhieuYeuCaus
        public ActionResult Index()
        {
            var phieuYeuCaus = db.PhieuYeuCaus.Include(p => p.BanDoc).Include(p => p.NhanVien).Include(p => p.TaiLieu);
            return View(phieuYeuCaus);
        }

        public ActionResult getList()
        {
            ViewData["SoThe"] = Session["SoThe"];
            ViewData["SDKCB"] = Session["SDKCB"];
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
            string SDKKB = Request["TL_SoDangKyCaBiet"];
            string SoThe = Request["BD_SoThe"];
            //string SDKCB = Request["TL_SoDangKyCaBiet"];
            if ((SoThe.Length == 0) || (SDKKB.Length == 0))
            {
                ModelState.AddModelError("", "Số thể và số đăng ký cá biệt không được rỗng");
            }
            else
            {
                string KTSoThe = db.Database.SqlQuery<string>("select BD_SoThe from BanDoc where BD_SoThe ='" + SoThe + "'").FirstOrDefault();
                string KTMaSach = db.Database.SqlQuery<string>("select TL_SoDangKyCaBiet from TaiLieu where TL_SoDangKyCaBiet ='" + SDKKB + "'").FirstOrDefault();
                if (KTMaSach != null && KTSoThe != null)
                {
                    ViewBag.min = DateTime.Now;
                    var sl = from p in db.PhieuYeuCaus select p;
                    if (ModelState.IsValid)
                    {
                        phieuYeuCau.PYC_IDPhieuYeuCau = autoMaPYC(sl.Count());
                        phieuYeuCau.BD_SoThe = SoThe;
                        phieuYeuCau.TL_SoDangKyCaBiet = SDKKB;
                        db.PhieuYeuCaus.Add(phieuYeuCau);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else ModelState.AddModelError("", "Số thẻ hoặc số đăng ký cá biệt sai !");
            }
            ViewBag.BD_SoThe = new SelectList(db.BanDocs, "BD_SoThe", "BD_HoVaTen", phieuYeuCau.BD_SoThe);
            ViewBag.NV_ID = new SelectList(db.NhanViens, "NV_ID", "NV_HOTEN", phieuYeuCau.NV_ID);
            ViewBag.TL_SoDangKyCaBiet = new SelectList(db.TaiLieux, "TL_SoDangKyCaBiet", "TL_TieuDe", phieuYeuCau.TL_SoDangKyCaBiet);
            return View(phieuYeuCau);
        }
        public ActionResult CreatePYC()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePYC([Bind(Include = "BD_SoThe,TL_SoDangKyCaBiet,NV_ID,PYC_NgayMuon,PYC_NgayTra")] PhieuYeuCau phieuYeuCau)
        {ViewBag.ST = Session["SoThe"];
            ViewBag.SDK = Session["SDKCB"];
            string SDKKB = ViewBag.SDK;
            string SoThe = ViewBag.ST;

            if (SDKKB == null || SoThe == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //string SDKCB = Request["TL_SoDangKyCaBiet"];
            if ((SoThe.Length == 0) || (SDKKB.Length == 0))
            {
                ModelState.AddModelError("", "Số thể và số đăng ký cá biệt không được rỗng");
            }
            else
            {
                
                    ViewBag.min = DateTime.Now;
                    var sl = from p in db.PhieuYeuCaus select p;
                    if (ModelState.IsValid)
                    {
                        phieuYeuCau.PYC_IDPhieuYeuCau = autoMaPYC(sl.Count());
                        phieuYeuCau.BD_SoThe = SoThe;
                        phieuYeuCau.TL_SoDangKyCaBiet = SDKKB;
                        db.PhieuYeuCaus.Add(phieuYeuCau);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    
                }
                else ModelState.AddModelError("", "Số thẻ hoặc số đăng ký cá biệt sai !");
            }
            ViewBag.BD_SoThe = new SelectList(db.BanDocs, "BD_SoThe", "BD_HoVaTen", phieuYeuCau.BD_SoThe);
            ViewBag.NV_ID = new SelectList(db.NhanViens, "NV_ID", "NV_HOTEN", phieuYeuCau.NV_ID);
            ViewBag.TL_SoDangKyCaBiet = new SelectList(db.TaiLieux, "TL_SoDangKyCaBiet", "TL_TieuDe", phieuYeuCau.TL_SoDangKyCaBiet);
            return View();
        }
        int autoMaPYC(int sl)
        {
            var i = from p in db.PhieuYeuCaus where p.PYC_IDPhieuYeuCau == sl select p;
            if (i.Count() >= 1)
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
            string id = Request["PYC_IDPhieuYeuCau"];
            string SoThe = Request["BD_SoThe"];
            string SDKCB = Request["TL_SoDangKyCaBiet"];
            if ((SoThe.Length == 0) || (SDKCB.Length == 0))
            {
                ModelState.AddModelError("", "Số thể và số đăng ký cá biệt không được rỗng");
            }
            else
            {
                string KTSoThe = db.Database.SqlQuery<string>("select BD_SoThe from BanDoc where BD_SoThe =" + SoThe + "").FirstOrDefault();
                string KTMaSach = db.Database.SqlQuery<string>("select BD_SoThe from BanDoc where BD_SoThe =" + SoThe + "").FirstOrDefault();
                if (KTMaSach != null && KTSoThe != null)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(phieuYeuCau).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else ModelState.AddModelError("", "Số thẻ hoặc số đăng ký cá biệt sai !");
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

        //POST: PhieuYeuCaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuYeuCau phieuYeuCau = db.PhieuYeuCaus.Find(id);
            db.PhieuYeuCaus.Remove(phieuYeuCau);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SearchPYC(int id)
        {
            var result = from p in db.PhieuYeuCaus where p.PYC_IDPhieuYeuCau == id select p;
            return View(result);
        }
        public ActionResult SearchSDKCB(string id)
        {
            ViewBag.SDKCB = Session["SDKCB"];
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiLieu result = db.TaiLieux.Find(id);
            if (result != null)
            {
                Session["SDKCB"] = id;
                return View(result);
            }
            else {
                ViewBag.info = "Không tìm thấy mã đăng ký cá biệt !";
                return View();
            }
        }
        public ActionResult SearchSoThe(string id)
        {
            ViewBag.SoThe = Session["SoThe"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BanDoc result = db.BanDocs.Find(id);
            if (result != null)
            {
                Session["SoThe"] = id;
                return View(result);
            }
            else {
                ViewBag.info = "Không tìm thấy dữ liệu bạn đọc !";
                return View();
            }
            
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public int SoNgayTre(int id)
        {
            int tre = db.Database.SqlQuery<int>("select datediff(day,PhieuYeuCau.PYC_NgayTra,GETDATE()) from PhieuYeuCau where PYC_IDPhieuYeuCau ='" + id + "'").FirstOrDefault();
            if (tre >= 1)
                return tre;
            else return 0;
        }

      
    }
}
