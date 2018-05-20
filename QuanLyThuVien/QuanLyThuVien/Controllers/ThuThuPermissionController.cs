using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyThuVien.Controllers
{
    public abstract class ThuThuPermissionController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            if (user == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else if (!user.ThuThu && ((actionName != "index" && actionName != "details") || (controllerName != "book" && controllerName != "member")))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}