using Model.DAO;
using Model.EF;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class SpecializedController : ThuThuPermissionController
    {
        SpecializedDAO dao = new SpecializedDAO();
        // GET: Specialized
        public ActionResult Index()
        {
            var listSpecialized = dao.GetAllSpecialized().ToList();
            return View(listSpecialized);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "SpecializedName, Descripstion")]Specialized newSpecialized)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    var result = dao.Insert(newSpecialized, user.Username);
                    if (result)
                        return Content("<script>alert('Lưu thành công');window.location.href='/Specialized/Index';</script>");
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra. vui lòng thử lại.");
                }
            }
            return View(newSpecialized);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var employee = dao.GetSpecializedByID(id.Value);
            if (employee == null)
                return HttpNotFound();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "SpecializedID, SpecializedName, Descripstion")]Specialized specialUpdate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    var result = dao.Update(specialUpdate, user.Username);
                    if (result == true)
                        return Content("<script>alert('Đã lưu');window.location.href='/Specialized/Index';</script>");
                    else
                        return HttpNotFound();
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra vui lòng thữ lại.");
                }
            }
            return View(specialUpdate);
        }

        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                return dao.Delete(id, user.Username);
            }
            catch
            {
                return -2;
            }
        }
    }
}