using Model.DAO;
using Model.EF;
using Model.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class EmployeeController : QuanLyPermissionController
    {
        //// GET: NhanVien
        //public ActionResult Index(int page = 1)
        //{
        //    var dao = new EmployeeDAO();
        //    var litEmployee = dao.GetAllEmployees().ToPagedList(page, 10);
        //    return View(litEmployee);
        //}

        // GET: NhanVien
        public ActionResult Index()
        {
            var dao = new EmployeeDAO();
            var litEmployee = dao.GetAllEmployees().ToList();
            return View(litEmployee);
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(int id)
        {
            var dao = new EmployeeDAO();
            var model = dao.GetEmployeeByID(id);
            if (model == null)
                return HttpNotFound();
            return PartialView("_Details", model);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            CreateSelectList();
            return View();
        }

        //// POST: NhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID, FirstName, LastName, Birthday, Sex, Address, " +
            "Phone, PermissionID")]Employee newEmployee, HttpPostedFileBase UrlAvatar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string path = null;
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    if (UrlAvatar != null && UrlAvatar.ContentLength > 0)
                    {
                        newEmployee.UrlAvatar = newEmployee.EmployeeID + Path.GetExtension(UrlAvatar.FileName);
                        path = Path.Combine(Server.MapPath("~/Upload/Images/Employee"), newEmployee.UrlAvatar);
                    }
                    else
                    {
                        newEmployee.UrlAvatar = "user.png";
                    }
                    var result = new EmployeeDAO().Insert(newEmployee, user.Username);
                    if (result > 0)
                    {
                        if(path!=null)
                            UrlAvatar.SaveAs(path);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm thất bại. Vui lòng thử lại!");
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Thêm thất bại. Vui lòng thử lại!"); ;
            }
            CreateSelectList();
            return View(newEmployee);
        }

        //// GET: NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var dao = new EmployeeDAO();
            var employeeEdit = dao.GetEmployeeByID(id.Value);

            CreateSelectList();
            return View(employeeEdit);
        }

        // POST: NhanVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID, FirstName, LastName, BirthDay, Address, Phone, Sex, PermissionID")]Employee employeeUpdate, HttpPostedFileBase UrlAvatar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dao = new EmployeeDAO();
                    string path = null;
                    if (UrlAvatar != null && UrlAvatar.ContentLength > 0)
                    {
                        employeeUpdate.UrlAvatar = employeeUpdate.EmployeeID + Path.GetExtension(UrlAvatar.FileName);
                        path = Path.Combine(Server.MapPath("~/Upload/Images/Employee"), employeeUpdate.UrlAvatar);
                    }
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    var employeeUpdated = dao.EditByQuanLy(employeeUpdate, user.Username);
                    if (employeeUpdated == null)
                    {
                        return HttpNotFound();
                    }
                    else if (path != null)
                    {
                        UrlAvatar.SaveAs(path);
                    }
                    return Content("<script>alert('Đã lưu'); window.location.href='/Employee/Edit/" + employeeUpdate.EmployeeID + "'</script>");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Có lỗi xãy ra vui lòng thữ lại !!!");
            }
            CreateSelectList();
            return View(employeeUpdate);
        }

        public int RestorePassword(int id)
        {
            try
            {
                var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                var dao = new AccountDAO();
                return dao.RestorePassword(id, user.Username);
            }
            catch
            {
                return -2;
            }
        }
        // POST: NhanVien/Delete/5
        [HttpPost]
        public int Delete(int id)
        {
            try
            {
                var dao = new EmployeeDAO();
                var resultDelete = dao.Delete(id);
                return resultDelete;
            }
            catch
            {
                return -2;
            }
        }

        private void CreateSelectList()
        {
            ViewBag.SeListSex = new SelectList(new SelectListItem[]
            {
                new SelectListItem()
                {
                    Text = "Nam",
                    Value = "true",

                },
                 new SelectListItem()
                {
                    Text = "Nữ",
                    Value = "false",
                }
            }, "Value", "Text");
            ViewBag.Permission = new SelectList(new PermissionDAO().GetAllPermission(), "PermissionID", "PermissionName");
        }
    }
}
