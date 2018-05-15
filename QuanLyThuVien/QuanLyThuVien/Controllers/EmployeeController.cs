using Model.DAO;
using Model.EF;
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
        // GET: NhanVien
        public  ActionResult Index(int page=1)
        {
            var dao = new EmployeeDAO();
            var litEmployee = dao.GetAllEmployees().ToPagedList(page, 10);
            return View(litEmployee);
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(int id)
        {
            var dao = new EmployeeDAO();
            var model = dao.GetEmployeeByID(id);
            if (model == null)
                return HttpNotFound();

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Details", model);
            }
            return View(model);
        }

        // GET: NhanVien/Create
        [HttpGet]
        public ActionResult Create()
        {
            CreateSelectList(null);
            return View();
        }

        //// POST: NhanVien/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaNhanVien, TenNhanVien, NgaySinh, DiaChi, SoDienThoai, PermissionID")]NhanVien newNhanVien, HttpPostedFileBase UrlHinhAnh)
        //{
        //    try
        //    {
        //        if(ModelState.IsValid)
        //        {
        //            if(UrlHinhAnh!=null && UrlHinhAnh.ContentLength > 0)
        //            {
        //                string fileName = newNhanVien.MaNhanVien + Path.GetExtension(UrlHinhAnh.FileName);
        //                string path = Path.Combine(Server.MapPath("~/Upload/Images/NhanVien"), fileName);
        //                UrlHinhAnh.SaveAs(path);
        //                newNhanVien.UrlHinhAnh = fileName;
        //            }
        //            else
        //            {
        //                newNhanVien.UrlHinhAnh = "user.png";
        //            }
        //            var result = new NhanVienDAO().Insert(newNhanVien);
        //            if (result > 0)
        //                return RedirectToAction("Index");
        //            else
        //            {
        //                ModelState.AddModelError("", "Thêm thất bại. Vui lòng thử lại!");
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        ModelState.AddModelError("", "Thêm thất bại. Vui lòng thử lại!"); ;
        //    }
        //    CreateSelectList(null);
        //    return View();
        //}

        //// GET: NhanVien/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    var nhanVienEdit = new NhanVienDAO().GetNhanVienById(id);
        //    CreateSelectList(id);
        //    return View(nhanVienEdit);
        //}

        // POST: NhanVien/Edit/5
        //[HttpPost]
        //[ActionName("Edit")]
        //public ActionResult EditPost(string id, HttpPostedFileBase UrlHinhAnh)
        //{
        //    var dao = new NhanVienDAO();
        //    if (id==null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    var nhanVienEdit = dao.GetNhanVienById(id);
        //    if (nhanVienEdit == null)
        //        return HttpNotFound();
        //    if (ModelState.IsValid)
        //    {
        //        if(TryUpdateModel(nhanVienEdit, "", new string[] { "TenNhanVien", "NgaySinh", "DiaChi", "SoDienThoai", "Username", "PermissionID" }))
        //        {
        //            try
        //            {
        //                if (UrlHinhAnh != null && UrlHinhAnh.ContentLength > 0)
        //                {
        //                    string fileName = nhanVienEdit.MaNhanVien + Path.GetExtension(UrlHinhAnh.FileName);
        //                    string path = Path.Combine(Server.MapPath("~/Upload/Images/NhanVien"), fileName);
        //                    UrlHinhAnh.SaveAs(path);
        //                    nhanVienEdit.UrlHinhAnh = fileName;
        //                }
        //                dao.Update();
        //                return RedirectToAction("Index");
        //            }
        //            catch 
        //            {
        //                ModelState.AddModelError("", "Lưu thất bại. Vui lòng thử lại");
        //            }
        //        }
        //    }
        //    CreateSelectList(id);
        //    return View(nhanVienEdit);
        //}

        // GET: NhanVien/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NhanVien/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void CreateSelectList(string selectedValue)
        {
            if (String.IsNullOrEmpty(selectedValue))
                ViewBag.Permission = new SelectList(new PermissionDAO().GetAllPermission(), "PermissionID", "PermissionName");
            else
                ViewBag.Permission = new SelectList(new PermissionDAO().GetAllPermission(), "PermissionID", "PermissionName", selectedValue);
        }
    }
}
