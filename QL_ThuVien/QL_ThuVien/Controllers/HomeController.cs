using QL_ThuVien.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_ThuVien.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataContext db = new DataContext();
            var i = from p in db.BanDocs select p;
            ViewBag.sothenew = i.Count();
            int sothe = i.Count() + 1; 
            string KTSoThe = db.Database.SqlQuery<string>("select BD_SoThe from BanDoc").LastOrDefault();
            ViewBag.sothe = KTSoThe;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}