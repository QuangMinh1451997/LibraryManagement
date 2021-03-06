﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyThuVien
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UserDetails",
                url: "User/{action}/{id}",
                defaults: new { controller = "User", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "UserChangePassword",
               url: "User/{action}/{id}",
               defaults: new { controller = "User", action = "ChangePassword", id = UrlParameter.Optional }
           );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}/{status}",
            //    defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional, status = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
