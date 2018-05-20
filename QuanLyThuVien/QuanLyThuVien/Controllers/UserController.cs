using Model.DAO;
using Model.EF;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyThuVien.Controllers
{
    public class UserController : Controller
    {
        // id - EmployeeID
        public ActionResult Details(int id)
        {
            var dao = new EmployeeDAO();
            var employee = dao.GetEmployeeByID(id);

            if (employee == null)
                return HttpNotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID, FirstName, LastName, BirthDay, Address, Phone")]Employee employeeUpdate, HttpPostedFileBase UrlAvatar)
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
                var employeeUpdated = dao.EditByUser(employeeUpdate, user.Username);
                if (employeeUpdated == null)
                {
                    ModelState.AddModelError("", "Có lỗi xãy ra vui lòng thữ lại !!!");
                }
                else if (path != null)
                {
                    UrlAvatar.SaveAs(path);
                }
                return Content("<script>alert('Đã lưu'); window.location.href='/User/Details'</script>");
            }
            return View(employeeUpdate);      
        }

        [HttpGet]
        public ActionResult ChangePassword(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var dao = new EmployeeDAO();
            var employee = dao.GetEmployeeByID(id.Value);
            var accountModel = new AccountModel()
            {
                EmployeeID = employee.EmployeeID,
                Username = employee.Account.Username,
                UrlAvatar = employee.UrlAvatar
            };
            return View(accountModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "EmployeeID, Username, OldPassword, Password, ConfirmPassword, UrlAvatar")]AccountModel accountUpdate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new AccountDAO();
                    int result = dao.ChangePassword(accountUpdate.EmployeeID, accountUpdate.Username, accountUpdate.OldPassword, accountUpdate.Password);
                    if(result == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại. Vui lòng thữ lại hoặc tải lại trang");
                        return HttpNotFound();
                    }
                    else if(result == -2)
                    {
                        ModelState.AddModelError("", "Mật khẩu hiện tại không chính xác");
                    }
                    else if (result==1)
                        return Content("<script>alert('Đã lưu');window.location.href='/User/Details'</script>");
                    
                }
                catch
                {
                    ModelState.AddModelError("", "Thay đổi mật khẩu thất bại. Vui lòng thữ lại !!!");
                }
            }
            accountUpdate.Password = "";
            accountUpdate.ConfirmPassword = "";
            return View(accountUpdate);
        }

        public ActionResult Logout()
        {
            this.Session.Remove(Common.CommonSession.USER_SESSION);
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            if (user == null)
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "login", action = "index" }));
            else if(actionName == "details")
            {
                var paramID = filterContext.ActionParameters["id"];
                // set id if id = null
                if (paramID == null)
                {
                    filterContext.ActionParameters["id"] = paramID = user.EmployeeID;
                }
                if (user.EmployeeID != (int)paramID)
                    filterContext.Result = new HttpUnauthorizedResult();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}