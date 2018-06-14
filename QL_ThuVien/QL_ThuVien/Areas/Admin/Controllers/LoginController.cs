using QL_ThuVien.Areas.Admin.Models;
using QL_ThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QL_ThuVien.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private DataContext db = new DataContext();
        [HttpGet]
        // GET: Admin/Login
        public ActionResult Index()
        {
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
                    var userSession = new UserLogin();

                    userSession.NV_MATKHAU = NHANVIEN.NV_MATKHAU;

                    userSession.NV_EMAIL = NHANVIEN.NV_EMAIL;

                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "NhanViens");
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