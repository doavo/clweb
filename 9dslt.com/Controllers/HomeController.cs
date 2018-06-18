using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tempofme.Models;

namespace Tempofme.Controllers
{
    public class HomeController : Controller
    {
        DataAccountContentDataContext accountContext = new DataAccountContentDataContext();
        DataNDGameDataContext ndgameContext = new DataNDGameDataContext();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            List<_9d_new> listNews = accountContext._9d_news.Where(c => c.delete_flag == false).OrderByDescending(c => c.created_at).ToList();
            _9d_ad indexleft = accountContext._9d_ads.Where(c => c.ads_type == 1).FirstOrDefault();
            if (indexleft != null)
            {
                ViewBag.indexleftlink = indexleft.ads_link;
                ViewBag.indexleftimage = indexleft.ads_image;
                ViewBag.indexleftname = indexleft.ads_name;
            }
            else
            {
                ViewBag.indexleftlink = "/";
                ViewBag.indexleftimage = "../Content/images/left-banner.png";
                ViewBag.indexleftname = "Không có sự kiện";
            }

            _9d_ad indexright = accountContext._9d_ads.Where(c => c.ads_type == 2).FirstOrDefault();
            if (indexleft != null)
            {
                ViewBag.indexrightlink = indexright.ads_link;
                ViewBag.indexrightimage = indexright.ads_image;
                ViewBag.indexrightname = indexright.ads_name;
            }
            else
            {
                ViewBag.indexleft = "/";
                ViewBag.indexleft = "../Content/images/right-banner.png";
                ViewBag.indexleft = "Không có sự kiện";
            }
            return View(listNews);
        }
        public ActionResult Checkaccloginandconfirmcheatfrominternet() {
            TempData["Checkaccloginandconfirmcheatfrominternet"] = Common.getBase44();
            List<_9d_new> listNews = accountContext._9d_news.Where(c => c.delete_flag == false).OrderByDescending(c => c.created_at).ToList();
            return View("Index", listNews);
        }
        public ActionResult Topserver()
        {
            IQueryable<VIEW_RANK_INFO> listtop = ndgameContext.VIEW_RANK_INFOs.OrderBy(c => c.chr_name).OrderByDescending(c => c.inner_level).Take(9);
            return PartialView("Topserver", listtop);
        }

        public ActionResult Downloadgame()
        {
            return View();
        }


        

    }
}
