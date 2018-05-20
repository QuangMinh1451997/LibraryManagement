using Model.ViewModels;
using QuanLyThuVien.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyThuVien.Controllers
{
    public abstract class QuanLyPermissionController: Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName.ToLower();
            
            var userLogin = (EmployeeLogin)Session[Common.CommonSession.USER_SESSION];
            if (userLogin == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Login", action = "Index" })); 
            }
            else if(!userLogin.QuanLy )
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

            base.OnActionExecuting(filterContext);
        }
    }
}