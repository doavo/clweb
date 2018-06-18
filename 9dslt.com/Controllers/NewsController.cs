using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tempofme.Models;

namespace Tempofme.Controllers
{
    public class NewsController : Controller
    {
        DataAccountContentDataContext accountContext = new DataAccountContentDataContext();
        //
        // GET: /News/

        public ActionResult Index(string nameascii)
        {
            if (nameascii == "")
            {
                return RedirectToAction("Index", "Home");
            }


            _9d_new news = accountContext._9d_news.Where(c =>c.news_ascii == nameascii && c.delete_flag == false).FirstOrDefault();
            ViewBag.newstitle = news.news_title;
            ViewBag.newsascii = news.news_ascii;
            ViewBag.newsimages = news.news_images;
            ViewBag.newsdescription = news.news_descriptions;
            ViewBag.newscontent = news.news_content;
            ViewBag.newscreateat = news.created_at;
            ViewBag.newscreateby = news.created_by;
            List<_9d_new> listNews = accountContext._9d_news.Where(c => c.delete_flag == false).OrderByDescending(c => c.created_at).ToList();
            return View(listNews);
        }

        public ActionResult Subnews()
        {
            //List<_9d_new> listNews = datanews._9d_news.Where(c => c.delete_flag == false).OrderByDescending(c => c.created_at).ToList();
            //_9d_user hj = accountContext._9d_users.
            return View();
        }

    }
}
