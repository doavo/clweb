using System.Web.Mvc;

namespace Tempofme.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administrator_Home",
                "Administrator/Quantri-website",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );
            context.MapRoute(
                "News_manager",
                //"Administrator/Home/Newsmanager",
                "Administrator/Quantri-baiviet",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Newsmanager", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );

            context.MapRoute(
                "Giftcode_manager",
                //"Administrator/Home/Newsmanager",
                "Administrator/Quantri-giftcode",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Giftmanager", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );

            context.MapRoute(
                "Percen_manager",
                //"Administrator/Home/Newsmanager",
                "Administrator/Quantri-ty-le-nap-the",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Percenmanager", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );

            context.MapRoute(
                "User_manager",
                //"Administrator/Home/Newsmanager",
                "Administrator/Quantri-taikhoan",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Usermanager", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );

            context.MapRoute(
                "Ads_manager",
                //"Administrator/Home/Newsmanager",
                "Administrator/Quantri-quangcao",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Ads", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );
            context.MapRoute(
                "DeleteNews",
                //"Administrator/Home/Newsmanager",
                "Administrator/DeleteNews",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "DeleteNews", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );

            context.MapRoute(
                "Deleteshop",
                //"Administrator/Home/Newsmanager",
                "Administrator/Deleteshop",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Deleteshop", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );

            context.MapRoute(
                "Deletegift",
                //"Administrator/Home/Newsmanager",
                "Administrator/Deletegift",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Deletegift", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );

            context.MapRoute(
                "Addxu",
                //"Administrator/Home/Newsmanager",
                "Administrator/Them-xu-theo-username",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Addxu", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );
            context.MapRoute(
                "Shop_manager",
                //"Administrator/Home/Newsmanager",
                "Administrator/Quantri-shop",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Shopmanager", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );
            context.MapRoute(
                "News_post",
                //"Administrator/Home/Newsmanager",
                "Administrator/Vietbai-trangchu",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Newspost", id = UrlParameter.Optional, controller = "Home" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );
            context.MapRoute(
                "Administrator_Login",
                "Administrator/Quantri-website/Login.html",
                //"Administrator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional, controller = "Login" },
                new string[] { "Tempofme.Areas.Administrator.Controllers" }
            );
        }
    }
}
