using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class SettingController : QuanLyPermissionController
    {
        PermissionDAO daoPer = new PermissionDAO();
        HireTimeDAO daoHireTime = new HireTimeDAO();
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult IndexPermission()
        {
            return PartialView("_ListPermission", daoPer.GetAllPermission());
        }

        public PartialViewResult IndexHireTime()
        {
            return PartialView("_ListHireTime", daoHireTime.GetAllHireTime());
        }
    }
}