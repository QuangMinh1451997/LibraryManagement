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
            var actionName = filterContext.ActionDescriptor.ActionName;
            if(user == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else if (!user.ThuThu && actionName!="asd")
            {
                filterContext.Result = new HttpUnauthorizedResult(); 
            }
            base.OnActionExecuting(filterContext);
        }
    }
}