using Model.DAO;
using Model.EF;
using Model.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class BookController : ThuThuPermissionController
    {
        BookDAO dao = new BookDAO();
        // GET: Book
        public ActionResult Index()
        {
            var listBook = dao.GetAllBook().ToList();
            return View(listBook);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var book = dao.GetBookByID(id.Value);
            if (book == null)
                return HttpNotFound();
            if (Request.IsAjaxRequest())
                return PartialView("_Details", book);
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateSelList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookCode, BookTypeID")]Book newBook)
        {
            var bookCodeIsExisted = dao.CheckBookCodeIsExisted(newBook.BookCode.ToLower());
            if (bookCodeIsExisted == true)
                ModelState.AddModelError("", "mã sách đã tồn tại");
            if (ModelState.IsValid)
            {
                try
                {
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    var result = dao.Insert(newBook, user.Username);
                    if (result == true)
                        return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra. Vui lòng tải lại trang");
                }
            }
            CreateSelList();
            return View(newBook);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var book = dao.GetBookByID(id.Value);
            if (book == null)
                return Redirect("/Error/Error404");
            CreateSelList();
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID, BookCode, BookTypeID")]Book bookUpdate)
        {
            var bookCodeIsExisted = dao.CheckBookCodeIsExisted(bookUpdate.BookID, bookUpdate.BookCode.ToLower());
            if (bookCodeIsExisted == true)
                ModelState.AddModelError("", "mã sách đã tồn tại");
            if (ModelState.IsValid)
            {
                try
                {
                    var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                    var result = dao.Update(bookUpdate, user.Username);
                    if (result)
                    {
                        return Content("<script>alert('Đã lưu'); window.location.href='/Book/Index';</script>");
                    }
                    return HttpNotFound();
                }
                catch
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra vui lòng tải lại trang");
                }
            }
            CreateSelList();
            return View(bookUpdate);
        }

        [HttpGet]
        public ActionResult HireBook(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var book = dao.GetBookByID(id.Value);
            if (book == null)
                return Redirect("/Error/Error404");
            return View(book);
        }

        [HttpPost]
        public int Delete(int? id)
        {
            if (id == null)
                return -2;
            try
            {
                var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
                var result = dao.Delete(id.Value, user.Username);
                return result;
            }
            catch
            {
                return -2;
            }
            
        }

        private void CreateSelList()
        {
            var daoBookType = new BookTypeDAO();
            ViewBag.SelListBookType = new SelectList(daoBookType.GetAllBookType(), "BookTypeID", "BookTypeName");
        }
    }
}