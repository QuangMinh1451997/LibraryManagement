using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Model.DAO;
using Model.EF;
using Model.ViewModels;
using QuanLyThuVien.Common;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            LibraryContext s = new LibraryContext();
            
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                var user = dao.GetUserLogin(loginModel.Username, loginModel.Password);
                if (user != null)
                {
                    Session.Add(CommonSession.USER_SESSION, user);
                    if (user.QuanLy)
                        return RedirectToAction("Index", "Employee");
                    if (user.ThuThu)
                        return RedirectToAction("Index", "Book");
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập không hợp lệ");
                }
            }
            loginModel.Password = "";
            return View("Index", loginModel);
        }

       

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
            if (user != null)
            {
                if (user.QuanLy)
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Employee", action = "Index" }));
                else if (user.ThuThu)
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Book", action = "Index" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
