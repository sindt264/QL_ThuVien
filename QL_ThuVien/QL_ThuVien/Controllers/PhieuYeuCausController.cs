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
using PagedList;
using PagedList.Mvc;
using QL_ThuVien.Areas.Admin.Controllers;

namespace QL_ThuVien.Controllers
{
    public class PhieuYeuCausController : Controller
    {
        private DataContext db = new DataContext();

        // GET: PhieuYeuCaus
        public ActionResult getList(int? page)
        {
            var phieuYeuCaus = db.PhieuYeuCaus.OrderByDescending(n => n.PYC_NgayMuon).ToPagedList(page ?? 1, 9);
            return View(phieuYeuCaus);
        }

        public ActionResult getListDaMuon(string id)
        {
            var phieuYeuCaus = from p in db.PhieuYeuCaus where p.BD_SoThe == id && p.PYC_TrangThai == 1 select p;
            return View(phieuYeuCaus);
        }
        public ActionResult Index()
        {
            Session["SoThe"] = "";
            Session["SDKCB"] = "";
             var phieuYeuCaus = db.PhieuYeuCaus.Include(p => p.BanDoc).Include(p => p.NhanVien).Include(p => p.TaiLieu);
            return View(phieuYeuCaus.ToList());
        }

        [HttpPost]
        public ActionResult Index(string id)
        {

            //if (Session["SoThe"].ToString() !="" && Session["SDKCB"].ToString() !="")
            //{
            //    short MaTT = db.Database.SqlQuery<short>("select PYC_TrangThai from PhieuYeuCau where TL_SoDangKyCaBiet = '" + Session["SDKCB"] + "'").FirstOrDefault();
            //    if (MaTT == 1)
            //    {
            //        ModelState.AddModelError("", "Sách "+ Session["SDKCB"] +" đã được mượn trước !");
            //    }
            //    else
            //    return RedirectToAction("CreatePYC");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Mã thẻ hoặc mã sách chưa được điền");
            //}
            return View();
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

        public ActionResult XacNhanTra(int? id)
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
            //string masach = db.Database.SqlQuery<string>("select TL_SoDangKyCaBiet from PhieuYeuCau where PYC_IDPhieuYeuCau ='" + id + "'").SingleOrDefault();
            ChuyenTrangThai(phieuYeuCau.PYC_IDPhieuYeuCau, 0);
            db.SaveChanges();
            return RedirectToAction("getList");
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
                string selectIDNV = db.Database.SqlQuery<string>("select NV_ID FROM NHANVIEN WHERE NV_EMAIL = '" + Session["MaNV"] + "'").FirstOrDefault();
                if (KTMaSach != null && KTSoThe != null)
                {
                    ViewBag.min = DateTime.Now;
                    var sl = from p in db.PhieuYeuCaus select p;
                    if (ModelState.IsValid)
                    {
                        phieuYeuCau.NV_ID = selectIDNV;
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
        //public ActionResult CreatePYC()
        //{
        //    return View();
        //}

        //[HttpPost]
        public ActionResult CreatePYC([Bind(Include = "BD_SoThe,TL_SoDangKyCaBiet,NV_ID,PYC_NgayMuon,PYC_NgayTra")] PhieuYeuCau phieuYeuCau)
        {

            ViewBag.ST = Session["SoThe"];
            ViewBag.SDK = Session["SDKCB"];
            string SDKKB = ViewBag.SDK;
            string SoThe = ViewBag.ST;

            if (SDKKB.Length > 0 && SoThe.Length > 0)
            {
                short MaTT = db.Database.SqlQuery<short>("select PYC_TrangThai from PhieuYeuCau where TL_SoDangKyCaBiet = '" + Session["SDKCB"] + "'").FirstOrDefault();
                if (MaTT == 1)
                {
                    ModelState.AddModelError("", "Sách " + Session["SDKCB"] + " đã được mượn trước !");
                }
                short SLCoTheMuon = db.Database.SqlQuery<short>("select BD_GioiHanMuon from BanDoc where BD_SoThe ='" + SoThe+"'").SingleOrDefault();

                int SLDaMuon = db.Database.SqlQuery<int>("select count(*) from PhieuYeuCau where BD_SoThe ='"+SoThe+"' and PYC_TrangThai = 1").SingleOrDefault();

                if(SLDaMuon >= SLCoTheMuon)
                {
                    ModelState.AddModelError("", "Bạn đã đạt giới hạn mượn");
                }
                string selectIDNV = db.Database.SqlQuery<string>("select NV_ID FROM NHANVIEN WHERE NV_EMAIL = '" + Session["MaNV"] + "'").FirstOrDefault();



                ViewBag.min = DateTime.Now;
                var sl = from p in db.PhieuYeuCaus select p;
                if (ModelState.IsValid)
                {
                    phieuYeuCau.NV_ID = selectIDNV;
                    phieuYeuCau.PYC_NgayMuon = DateTime.Now;
                    phieuYeuCau.PYC_NgayTra = DateTime.Now.AddDays(+7);
                    phieuYeuCau.PYC_IDPhieuYeuCau = autoMaPYC(sl.Count());
                    phieuYeuCau.BD_SoThe = SoThe;
                    phieuYeuCau.TL_SoDangKyCaBiet = SDKKB;
                    phieuYeuCau.PYC_TrangThai = 1;
                    db.PhieuYeuCaus.Add(phieuYeuCau);
                    db.SaveChanges();
                    return RedirectToAction("getListDaMuon", new { id = SoThe });


                }
                //else ModelState.AddModelError("", "Số thẻ hoặc số đăng ký cá biệt sai !");
            }
            else ModelState.AddModelError("", "Số thể và mã sách không được rỗng !");
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
            string selectIDNV = db.Database.SqlQuery<string>("select NV_ID FROM NHANVIEN WHERE NV_EMAIL = '" + Session["MaNV"] + "'").FirstOrDefault();

            if ((SoThe.Length == 0) || (SDKCB.Length == 0))
            {
                ModelState.AddModelError("", "Số thể và số đăng ký cá biệt không được rỗng");
            }
            else
            {
                string KTSoThe = db.Database.SqlQuery<string>("select BD_SoThe from BanDoc where BD_SoThe ='" + SoThe + "'").FirstOrDefault();
                string KTMaSach = db.Database.SqlQuery<string>("select TL_SoDangKyCaBiet from TaiLieu where TL_SoDangKyCaBiet ='" + SDKCB + "'").FirstOrDefault();
                if (KTMaSach != null && KTSoThe != null)
                {
                    if (ModelState.IsValid)
                    {
                        phieuYeuCau.NV_ID = selectIDNV;
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
            //ChuyenTrangThai(Request["TL_SoDangKyCaBiet"], 0);
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

        public short GetTrangThai(int id)
        {
            return db.Database.SqlQuery<short>("select PYC_TrangThai from PhieuYeuCau where PYC_IDPhieuYeuCau = '" + id + "'").SingleOrDefault();
        }
        public RedirectToRouteResult ChuyenTrangThai(int id,short id2)
        {
            PhieuYeuCau phieuYeuCau = db.PhieuYeuCaus.FirstOrDefault(m => m.PYC_IDPhieuYeuCau == id);
            if (phieuYeuCau != null)
            {
                phieuYeuCau.PYC_TrangThai = id2;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }




        public ActionResult TimKiemSearch(string id, int id2)
        {
            if (id2 == 1)
            {
                var a = (from p in db.PhieuYeuCaus where p.TL_SoDangKyCaBiet == id select p).OrderByDescending(m => m.PYC_NgayMuon);
                return View(a);
            }
            if (id2 == 2)
            {
                var a = (from p in db.PhieuYeuCaus where p.BD_SoThe == id select p).OrderByDescending(m =>m.PYC_NgayMuon);
                return View(a);
            }
            else
            {
                return View(db.PhieuYeuCaus.ToList().OrderByDescending(m => m.PYC_NgayMuon));
            }
        }

    }
}
