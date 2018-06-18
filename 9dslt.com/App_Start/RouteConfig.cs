using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tempofme
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Trang-chu",
                url: "Trang-chu.html",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
            routes.MapRoute(
                name: "Tai-game",
                url: "Tai-game.html",
                defaults: new { controller = "Home", action = "Downloadgame" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
            routes.MapRoute(
                name: "Trang-chu22",
                url: "Checkaccloginandconfirmcheatfrominternet",
                defaults: new { controller = "Home", action = "Checkaccloginandconfirmcheatfrominternet" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );

            routes.MapRoute(
                name: "Dang-nhap",
                url: "Dang-nhap.html",
                defaults: new { controller = "Account", action = "Index" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
            routes.MapRoute(
                name: "Gift-code",
                url: "Ma-qua-tang.html",
                defaults: new { controller = "Account", action = "Giftcode" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
            routes.MapRoute(
                name: "Dang-ky",
                url: "Dang-ky.html",
                defaults: new { controller = "Account", action = "Signup" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
            routes.MapRoute(
                name: "Dich-vu-web",
                url: "Dich-vu-web.html",
                defaults: new { controller = "Service", action = "Index" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
            routes.MapRoute(
                name: "Buy",
                url: "Buy",
                defaults: new { controller = "Service", action = "Buy" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
            routes.MapRoute(
                name: "Bai-viet",
                url: "Bai-viet/{nameascii}",
                defaults: new { controller = "News", action = "Index" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
            routes.MapRoute(
                name: "Thay-doi-password",
                url: "Thay-doi-password.html",
                defaults: new { controller = "Account", action = "Changepass" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );

            routes.MapRoute(
                name: "Thong-tin-tai-khoan",
                url: "Thong-tin-tai-khoan.html",
                defaults: new { controller = "Account", action = "Profile" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );

            routes.MapRoute(
                name: "Nap-the",
                url: "Nap-the.html",
                defaults: new { controller = "Service", action = "Napthe" },
                namespaces: new string[] { "Tempofme.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Tempofme.Controllers" }
            );
        }
    }
}