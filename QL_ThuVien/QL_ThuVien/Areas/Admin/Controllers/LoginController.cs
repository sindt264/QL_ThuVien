using QL_ThuVien.Areas.Admin.Models;
using QL_ThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using QL_ThuVien.Models;

namespace QL_ThuVien.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private DataContext db = new DataContext();
        [HttpGet]
        // GET: Admin/Login
        public ActionResult Index()
        {
            Comand.NV_Level = 0;
            
            return View();
        }
        public string Insert(NhanVien entity)
        {
            db.NhanViens.Add(entity);
            db.SaveChanges();
            return entity.NV_ID;
        }
        public int Login(string userName, string password)
        {

            var result = db.NhanViens.SingleOrDefault(x => x.NV_EMAIL == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                
                //    if (result.Status == false)
                //    {
                //        return -1;
                //    }
                //    else
                {
                    if (result.NV_MATKHAU == password) {
                       
                    return 1;
                    }
                    else
                        return -2;
                }

            }
        }
        public static class Encryptor
        {
            public static string MD5Hash(string text)
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                //compute hash from the bytes of text
                md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

                //get hash result after compute it
                byte[] result = md5.Hash;

                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    //change it into 2 hexadecimal digits
                    //for each byte
                    strBuilder.Append(result[i].ToString("x2"));
                }

                return strBuilder.ToString();
            }
        }
        public NhanVien GetById(string userName)
        {
            //return db.NHANVIENs.Find(userName);
            return db.NhanViens.SingleOrDefault(x => x.NV_EMAIL == userName);
        }

        [Serializable]
        public class UserLogin
        {

            public string NV_EMAIL { get; set; }
            public int? NV_Level { get; set; }
            public string NV_MATKHAU { get; set; }
        }
        public static class CommonConstants
        {
            public static string USER_SESSION = "USER_SESSION";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new LoginController();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var NHANVIEN = dao.GetById(model.UserName);
                    Session["MaNV"] = model.UserName;
                    var getlevel = (from p in db.NhanViens where p.NV_EMAIL == model.UserName select p).SingleOrDefault();
                    Session["QuyenNV"] = getlevel.NV_Level;
                    var userSession = new UserLogin();

                    userSession.NV_MATKHAU = NHANVIEN.NV_MATKHAU;

                    userSession.NV_EMAIL = NHANVIEN.NV_EMAIL;

                    userSession.NV_Level = NHANVIEN.NV_Level;

                     Comand.NV_MATKHAU = NHANVIEN.NV_MATKHAU;

                    Comand.NV_EMAIL = NHANVIEN.NV_EMAIL;

                    Comand.NV_ID = NHANVIEN.NV_ID;

                    Comand.NV_Level = NHANVIEN.NV_Level;

                    int? level = NHANVIEN.NV_Level;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    if (level == 1)
                    {
                        return RedirectToAction("Index", "NhanViens");
                    }
                    else if (level == 2) {return Redirect("~/PhieuYeuCaus/"); }
                    else if (level == 3) {return RedirectToAction("","AdminTaiLieux"); }
                    else if (level == 4) {return Redirect("~/BanDoc/"); }
                        
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không đúng!");
                }
                //else if (result == -1)
                //{
                //    ModelState.AddModelError("", "Tài khoản đang bị khoá!");
                //}
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng!");
                }
            }
            return View("Index");
        }
    }
}