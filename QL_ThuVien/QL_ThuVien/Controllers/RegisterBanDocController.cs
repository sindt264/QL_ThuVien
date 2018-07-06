using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Controllers
{
    public class RegisterBanDocController : Controller
    {
        private DataContext db = new DataContext();
        // GET: RegisterBanDoc
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(Register model)
        {
            var i = from p in db.BanDocs select p;
            int sothe = i.Count() + 1;
            if (ModelState.IsValid)
            {
                var bandoc = new BanDoc();
                if(sothe <= 10) { bandoc.BD_SoThe = "TV0" + sothe; }
                else { bandoc.BD_SoThe = "TV" + sothe; }
                bandoc.BD_HoVaTen = model.BD_HoVaTen;
                bandoc.BD_NgaySinh = model.BD_NgaySinh;
                bandoc.BD_TrinhDo = model.BD_TrinhDo;
                bandoc.BD_NgheNghiep = model.BD_NgheNghiep;
                bandoc.BD_DiaChiNoiLamViec = model.BD_NoiCongTacHocTap;
                bandoc.BD_DTDIDong = model.BD_DTDIDong;
                bandoc.BD_ChoOHienTai = model.BD_ChoOHienTai;
                bandoc.BD_Email = model.BD_Email;
                db.BanDocs.Add(bandoc);
                db.SaveChanges();

                return Redirect("/RegisterBanDoc/ThanhCong");
            }
            else
            {
                ModelState.AddModelError("", "Đăng ký không thành công.");
            }
            return View(model);
        }

        public ActionResult ThanhCong()
        {
            return View();
        }
    }
}