using Model.DAO;
using Model.EF;
using Model.ViewModels;
using Newtonsoft.Json;
using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class BookTypeController : ThuThuPermissionController
    {
        BookTypeDAO dao = new BookTypeDAO();
        // GET: BookType
        public ActionResult Index()
        {
            var listBookType = dao.GetAllBookType().ToList();
            return View(listBookType);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var bookType = dao.GetBookTypeByID(id.Value);
            if (bookType == null) return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView("_Details", bookType);
            return HttpNotFound();          
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateSelectList();
            return View();
        }

        public ActionResult Create([Bind(Include = "BookTypeName, PublishingHouse, NumberOfPage, Size, Author, SpecializedID, Description")]BookTypeModel newBookTypeModel, HttpPostedFileBase UrlImage)
        {
            if ( UrlImage!=null && Helper.IsImage(Path.GetExtension(UrlImage.FileName)) == false)
                ModelState.AddModelError("", "Tệp tin hình ảnh phải có phần mở rộng là : .jpeg, .png, .jpg");
            if (ModelState.IsValid)
            {
                try
                {
                    string path = null;
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    if (UrlImage != null && UrlImage.ContentLength > 0)
                    {
                        var id = dao.GetIDMax();
                        newBookTypeModel.UrlImage = id + Path.GetExtension(UrlImage.FileName);
                        path = Path.Combine(Server.MapPath("~/Upload/Images/BookType"), newBookTypeModel.UrlImage);
                    }
                    else
                    {
                        newBookTypeModel.UrlImage = "default.png";
                    }
                    var newBookType = CopyBookTypeModelToBookType(newBookTypeModel);
                    var result = dao.Insert(newBookType, user.Username);
                    if (result > 0)
                    {
                        
                        if (path != null)
                            UrlImage.SaveAs(path);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm thất bại. Vui lòng thử lại!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra vui lòng thữ lại");
                }
            }
            CreateSelectList();
            return View(newBookTypeModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var bookType = dao.GetBookTypeByID(id.Value);
            if (bookType == null)
                return Redirect("/Error/Error404");
            var bookTypeModel = new BookTypeModel
            {
                BookTypeID = bookType.BookTypeID,
                BookTypeName = bookType.BookTypeName,
                PublishingHouse = bookType.PublishingHouse,
                NumberOfPage = bookType.NumberOfPage,
                Size = bookType.Size,
                Author = bookType.Author,
                SpecializedID = bookType.SpecializedID,
                Description = bookType.Description,
                UrlImage = bookType.UrlImage
            };
            CreateSelectList();
            return View(bookTypeModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookTypeID, BookTypeName, PublishingHouse, NumberOfPage, Size, Author, SpecializedID, Description, UrlImage")]BookTypeModel newBookTypeModel, HttpPostedFileBase UrlImage)
        {
            if (UrlImage != null && Helper.IsImage(Path.GetExtension(UrlImage.FileName)) == false)
                ModelState.AddModelError("", "Tệp tin hình ảnh phải có phần mở rộng là : .jpeg, .png, .jpg");
            if (ModelState.IsValid)
            {
                try
                {
                    string path = null;
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    if (UrlImage != null && UrlImage.ContentLength > 0)
                    {
                        newBookTypeModel.UrlImage = newBookTypeModel.BookTypeID + Path.GetExtension(UrlImage.FileName);
                        path = Path.Combine(Server.MapPath("~/Upload/Images/BookType"), newBookTypeModel.UrlImage);
                    }
                    var bookTypeUpdate = CopyBookTypeModelToBookType(newBookTypeModel);
                    var result = dao.Update(bookTypeUpdate, user.Username);
                    if (result == true)
                    {

                        if (path != null)
                            UrlImage.SaveAs(path);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không tồn tại đầu sách này!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra vui lòng thữ lại");
                }
            }
            CreateSelectList();
            return View(newBookTypeModel);
        }

        private void CreateSelectList()
        {
            var dao = new SpecializedDAO();
            ViewBag.SeListSpecialized = new SelectList(dao.GetAllSpecialized(), "SpecializedID", "SpecializedName");
        }

        private BookType CopyBookTypeModelToBookType(BookTypeModel newBookTypeModel)
        {
            var newBookType = new BookType()
            {
                BookTypeID = newBookTypeModel.BookTypeID,
                BookTypeName = newBookTypeModel.BookTypeName,
                PublishingHouse = newBookTypeModel.PublishingHouse,
                NumberOfPage = newBookTypeModel.NumberOfPage,
                Size = newBookTypeModel.Size,
                Author = newBookTypeModel.Author,
                SpecializedID = newBookTypeModel.SpecializedID,
                Description = newBookTypeModel.Description,
                UrlImage = newBookTypeModel.UrlImage
            };
            return newBookType;
        }

        public string GetData(int id)
        {
            var data = dao.GetData(id);
            if (data == null)
            {
                data = new
                {
                    SpecializedName = "",
                    Quantity = "",
                    UrlImage = "default.png"
                };
            }
            return JsonConvert.SerializeObject(data);
        }

        [HttpPost]
        public int Delete(int? id)
        {
            if (id == null)
                return -2;
            try
            {
                var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                return dao.Delete(id.Value, user.Username);
            }
            catch
            {
                return -2;
            }
        }
    }
}