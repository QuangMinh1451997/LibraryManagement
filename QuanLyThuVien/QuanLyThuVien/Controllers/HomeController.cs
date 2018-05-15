using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class HomeController : ThuThuPermissionController
    {
        public ActionResult Index()
        {
            //return new HttpUnauthorizedResult();
            return View();
        }
        public ActionResult Asd()
        {
            return View();
        }
    }
}