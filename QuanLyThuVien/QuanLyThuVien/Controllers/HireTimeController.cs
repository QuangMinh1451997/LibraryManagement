using Model.DAO;
using Model.EF;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class HireTimeController : QuanLyPermissionController
    {
        HireTimeDAO dao = new HireTimeDAO();
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NumberOfWeek")]HireTime newHireTime)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    var result = dao.Insert(newHireTime, user.Username);
                    if (result)
                        return RedirectToAction("Index", "Setting");
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra vui lòng tải lại trang");
                }
            }
            return View(newHireTime);
        }

        [HttpPost]
        public int Edit(int id)
        {
            try
            {
                var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                var result = dao.Update(id, user.Username);
                return (result > 0) ? result : -1;
            }
            catch
            {
                return -2;
            }
        }
    }
}