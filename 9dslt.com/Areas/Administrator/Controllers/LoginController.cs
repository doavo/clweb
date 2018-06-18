using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tempofme.Areas.Administrator.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Administrator/Login/

        public ActionResult Index(string userlogin)
        {

            if (userlogin == "Quantriwebsite")
            {
            }
            else if (userlogin == "logout")
            {
                Session["Quantri-website-Quantrivien"] = null;
            }
            else {
                Response.Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            if (collection["username"].ToString() == "admin" && collection["password"].ToString() == "17032017")
            {
                Session["Quantri-website-Quantrivien"] = collection["username"];
                return RedirectToAction("Index", "Home");
            }
            else {
                Response.Redirect("/Administrator/Login.html?userlogin=Quantriwebsite");
            }
            return View();
        }

    }
}
