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
                employeeUpdate.CreateBy = user.Username;
                var employeeUpdated = dao.Edit(employeeUpdate, user);
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

        public ActionResult Logout()
        {
            this.Session.Remove(Common.CommonSession.USER_SESSION);
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
            var actionName = filterContext.ActionDescriptor.ActionName;
            if (user == null)
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "login", action = "index" }));
            else if(actionName == "Details")
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