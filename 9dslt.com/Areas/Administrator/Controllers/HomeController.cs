using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tempofme.Models;

namespace Tempofme.Areas.Administrator.Controllers
{
    public class HomeController : Controller
    {
        private Common common = new Common();
        DataAccountContentDataContext accountContext = new DataAccountContentDataContext();
        //
        // GET: /Administrator/Home/

        public ActionResult Index(string actionIndex)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            if (actionIndex == "Quan-ly-bai-viet")
            {
                return RedirectToAction("Newsmanager");
            }
            return View();
        }

        public ActionResult Newsmanager()
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            List<_9d_new> items = accountContext._9d_news.Where(c => c.delete_flag == false).OrderByDescending(c => c.created_at).ToList();
            return View("Newsmanager", items);
        }

        public ActionResult DeleteNews(string actiondelete)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            _9d_new item = accountContext._9d_news.Where(c => c.news_id == Convert.ToInt32(actiondelete)).FirstOrDefault();
            item.delete_flag = true;
            try
            {
                accountContext.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Newsmanager", "Home");
        }

        public ActionResult Deleteshop(string actiondelete)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            _9d_KTCItem item = accountContext._9d_KTCItems.Where(c => c.id == Convert.ToInt32(actiondelete)).FirstOrDefault();
            item.delete_flag = true;
            try
            {
                accountContext.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Shopmanager", "Home");
        }

        public ActionResult Deletegift(string actiondelete)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            Tbl_giftcode t = accountContext.Tbl_giftcodes.Where(c => c.item_id == Convert.ToInt32(actiondelete)).FirstOrDefault();
            try
            {
                accountContext.Tbl_giftcodes.DeleteOnSubmit(t);
                accountContext.SubmitChanges();
                return RedirectToAction("Giftmanager", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Giftmanager", "Home");
                throw;
            }
            
        }

        public ActionResult Newspost()
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }

            return View("Newspost");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Newspost(FormCollection collection)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }

            string name = collection["newsName"].ToString();
            string description = collection["newsDescription"].ToString();
            string content = collection["newsContent"].ToString();
            string urlimage = collection["newsImages"].ToString();
            int type = Convert.ToInt32(collection["MovieType"].ToString());

            if (name == "" || description == "" || content == "" || urlimage == "")
            {
                TempData["errornews"] = "Vui lòng nhập đủ thông tin.";
                return View();
            }

            _9d_new news = new _9d_new();
            news.news_title = name;
            news.type = type;
            news.news_descriptions = description;
            news.news_content = content;
            news.created_at = DateTime.Now;
            news.created_by = "Admin";
            news.delete_flag = false;
            news.news_images = urlimage;
            news.news_ascii = Utility.RemoveAscii(Utility.RemoveSign4VietnameseString(name));
            accountContext._9d_news.InsertOnSubmit(news);
            try
            {
                accountContext.SubmitChanges();
                return RedirectToAction("Newsmanager");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Shopmanager()
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            List<_9d_KTCItem> items = accountContext._9d_KTCItems.Where(c => c.delete_flag == false).ToList();
            return View("Shopmanager", items);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Shopmanager(FormCollection collection)
        {
            try
            {
                string id = collection["ktcid"].ToString();
                string ktcname = collection["ktcname"].ToString();
                string description = collection["description"].ToString();
                string price = collection["price"].ToString();
                string imageitem = collection["iamgektc"].ToString();

                if (id == "" || ktcname == "" || description == "" || price == "")
                {
                    TempData["errorshop"] = "error";
                    return View("Shopmanager");
                }

                _9d_KTCItem item = new _9d_KTCItem();
                item.itemid = id;
                item.itemname = ktcname;
                item.itemdescription = description;
                item.itemprice = Convert.ToInt32(price);
                item.itemimages = imageitem;
                item.created_at = DateTime.Now;
                item.created_by = "Admin";
                item.updated_at = DateTime.Now;
                item.updated_by = "Admin";
                item.delete_flag = false;

                accountContext._9d_KTCItems.InsertOnSubmit(item);

                accountContext.SubmitChanges();
                TempData["successshop"] = "ok";
                List<_9d_KTCItem> items = accountContext._9d_KTCItems.Where(c => c.delete_flag == false).ToList();
                return View("Shopmanager", items);



            }
            catch (Exception)
            {

            }
            List<_9d_KTCItem> itemsa = accountContext._9d_KTCItems.Where(c => c.delete_flag == false).ToList();
            return View("Shopmanager", itemsa);
        }

        public ActionResult Addxu()
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Addxu(FormCollection collection)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            string username = collection["username"].ToString();
            string xu = collection["xu"].ToString();
            if (username == "" || xu == "")
            {
                TempData["erroraddxu"] = "Vui lòng nhập đủ thông tin.";
                TempData["successaddxu"] = null;
                return View();
            }
            if (Common.checkInt(xu))
            {
                TempData["erroraddxu"] = "Vui lòng nhập đúng số xu.";
                TempData["successaddxu"] = null;
                return View();
            }

            _9d_user us = accountContext._9d_users.Where(c => c.user_name == username && c.delete_flag == false).FirstOrDefault();
            if (us == null)
            {
                TempData["erroraddxu"] = "Vui lòng kiểm tra lại, tài khoản không tồn tại trong hệ thống.";
                TempData["successaddxu"] = null;
                return View();
            }

            us.balance = us.balance + Convert.ToInt32(xu);
            try
            {
                accountContext.SubmitChanges();
                TempData["successaddxu"] = "Tài khoản " + username + " đã thêm thành công " + xu + " xu.";
                return View();
            }
            catch (Exception)
            {
                TempData["erroraddxu"] = "Có lỗi từ hệ thống, vui lòng liên hệ quản trị viên.";
                return View();
            }
            
        }

        public ActionResult Ads()
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            return View("Ads");
        }

        [HttpPost]
        public ActionResult Ads(FormCollection collection)
        {
            _9d_ad ad = accountContext._9d_ads.Where(c => c.ads_type == Convert.ToInt32(collection["MovieType"].ToString())).FirstOrDefault();
            if (ad != null)
            {
                if (collection["adstitle"].ToString() != "")
                {
                    ad.ads_name = collection["adstitle"].ToString();
                }
                if (collection["adslink"].ToString() != "")
                {
                    ad.ads_link = collection["adslink"].ToString();
                }
                if (collection["adsimage"].ToString() != "")
                {
                    ad.ads_image = collection["adsimage"].ToString();
                }
                try
                {
                    accountContext.SubmitChanges();
                    TempData["successads"] = "Cập nhật quảng cáo thành công.";
                    return View();
                }
                catch (Exception)
                {
                    TempData["errorads"] = "Có lỗi vui lòng kiểm tra lại";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Usermanager([Bind(Prefix = "type")]string type, [Bind(Prefix = "userortelephone")]string userortelephone)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }

            if (userortelephone == null)
            {
                ViewBag.UserList = accountContext._9d_users.Where(c => c.user_id == 0).ToList();
                ViewBag.UserOrTelephone = userortelephone;
                ViewBag.Type = type;
                return View();
            }
            else if (Convert.ToInt32(type.ToString()) == 1)
            {
                ViewBag.UserList = accountContext._9d_users.Where(c => c.user_name == userortelephone.ToString()).ToList();
                ViewBag.UserOrTelephone = userortelephone;
                ViewBag.Type = type;
                return View();
            }
            else if (Convert.ToInt32(type.ToString()) == 2)
            {
                ViewBag.UserList = accountContext._9d_users.Where(c => c.telephone == userortelephone.ToString()).ToList();
                ViewBag.UserOrTelephone = userortelephone;
                ViewBag.Type = type;
                return View();
            }
            ViewBag.UserList = accountContext._9d_users.Where(c => c.user_id == 0).ToList();
            ViewBag.UserOrTelephone = "";
            ViewBag.Type = "0";
            return View();

        }



        [HttpPost]
        public ActionResult Usermanager(FormCollection collection)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            if (collection["warning"].ToString() == "")
            {
                TempData["erroruser"] = "Vui lòng nhập cảnh báo.";
                TempData["successuser"] = null;
                ViewBag.UserList = accountContext._9d_users.Where(c => c.user_id == 0).ToList();
                return View();
            }

            if (Convert.ToInt32(collection["usertype"].ToString()) == 2)
            {
                using (var db = new DataAccountContentDataContext())
                {
                    db._9d_users
                      .Where(x => x.telephone == collection["userid"].ToString())
                      .ToList()
                      .ForEach(a =>
                      {
                          a.status = true;
                          a.message = collection["warning"].ToString();
                      });

                    try
                    {
                        db.SubmitChanges();
                        TempData["erroruser"] = null;
                        TempData["successuser"] = "Nhập cảnh báo đến người dùng thành công.";
                        if (Convert.ToInt32(collection["usertype"].ToString()) == 1)
                        {
                            ViewBag.UserList = accountContext._9d_users.Where(c => c.user_name == collection["usertype"].ToString()).ToList();
                            return View();
                        }
                        else if (Convert.ToInt32(collection["usertype"].ToString()) == 2)
                        {
                            ViewBag.UserList = accountContext._9d_users.Where(c => c.telephone == collection["userid"].ToString()).ToList();
                        }
                        return View();
                    }
                    catch (Exception)
                    {
                        TempData["erroruser"] = "Có lỗi, vui lòng liên hệ người quản trị.";
                        TempData["successuser"] = null;
                        if (Convert.ToInt32(collection["usertype"].ToString()) == 1)
                        {
                            ViewBag.UserList = accountContext._9d_users.Where(c => c.user_name == collection["usertype"].ToString()).ToList();
                            return View();
                        }
                        else if (Convert.ToInt32(collection["usertype"].ToString()) == 2)
                        {
                            ViewBag.UserList = accountContext._9d_users.Where(c => c.telephone == collection["userid"].ToString()).ToList();
                        }
                        return View();
                    }
                }
            }
            else
            {
                _9d_user us = accountContext._9d_users.Where(c => c.user_name == collection["userid"].ToString()).FirstOrDefault();
                us.status = true;
                us.message = collection["warning"].ToString();
                try
                {
                    accountContext.SubmitChanges();
                    TempData["erroruser"] = null;
                    TempData["successuser"] = "Nhập cảnh báo đến người dùng thành công.";
                    if (Convert.ToInt32(collection["usertype"].ToString()) == 1)
                    {
                        ViewBag.UserList = accountContext._9d_users.Where(c => c.user_name == collection["usertype"].ToString()).ToList();
                        return View();
                    }
                    else if (Convert.ToInt32(collection["usertype"].ToString()) == 2)
                    {
                        ViewBag.UserList = accountContext._9d_users.Where(c => c.telephone == collection["userid"].ToString()).ToList();
                    }
                    return View();
                }
                catch (Exception)
                {
                    TempData["erroruser"] = "Có lỗi, vui lòng liên hệ người quản trị.";
                    TempData["successuser"] = null;
                    if (Convert.ToInt32(collection["usertype"].ToString()) == 1)
                    {
                        ViewBag.UserList = accountContext._9d_users.Where(c => c.user_name == collection["usertype"].ToString()).ToList();
                        return View();
                    }
                    else if (Convert.ToInt32(collection["usertype"].ToString()) == 2)
                    {
                        ViewBag.UserList = accountContext._9d_users.Where(c => c.telephone == collection["userid"].ToString()).ToList();
                    }
                    return View();
                }
            }
        }

        public ActionResult Giftmanager()
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            List<Tbl_giftcode> tg = accountContext.Tbl_giftcodes.ToList();
            return View(tg);
        }

        [HttpPost]
        public ActionResult Giftmanager(FormCollection collection)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            string giftcode = collection["giftcode"].ToString();
            string iditem = collection["iditem"].ToString();
            Tbl_giftcode t = accountContext.Tbl_giftcodes.Where(c => c.gift_code == giftcode).FirstOrDefault();
            if (t != null)
            {
                TempData["errorgift"] = "Mã quà tặng này đã tồn tại";
                TempData["successgift"] = null;
                List<Tbl_giftcode> tg = accountContext.Tbl_giftcodes.ToList();
                return View(tg);
            }

            Tbl_giftcode ta = new Tbl_giftcode();
            ta.gift_code = giftcode;
            ta.item_code = iditem;
            ta.group_user = ",";
            ta.active_user = "";
            ta.active_id = 1;
            try
            {
                accountContext.Tbl_giftcodes.InsertOnSubmit(ta);
                accountContext.SubmitChanges();
                TempData["errorgift"] = null;
                TempData["successgift"] = "Thêm mã mới thành công";
                List<Tbl_giftcode> tg = accountContext.Tbl_giftcodes.ToList();
                return View(tg);
            }
            catch (Exception)
            {
               TempData["errorgift"] = "Có lỗi, vui lòng liên hệ với quản trị.";
                TempData["successgift"] = null;
                List<Tbl_giftcode> tg = accountContext.Tbl_giftcodes.ToList();
                return View(tg);
            }
        }

        public ActionResult Percenmanager()
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            _9d_percen p = accountContext._9d_percens.FirstOrDefault();
            return View(p);
        }

        [HttpPost]
        public ActionResult Percenmanager(FormCollection collection)
        {
            if (Session["Quantri-website-Quantrivien"] == null)
            {
                Response.Redirect("/");
            }
            string percen = collection["tyle"].ToString() == "" ? "100" : collection["tyle"].ToString();
            bool status = true;
            if (collection["MovieType"].ToString() == "1")
            {
                status = true;
            }
            else {
                status = false;
            }


            _9d_percen p = accountContext._9d_percens.FirstOrDefault();
            p.percen = Convert.ToInt32(percen);
            p.status = status;
            try
            {
                accountContext.SubmitChanges();
                TempData["errorpercen"] = null;
                TempData["successpercen"] = "Cập nhật trạng thái thành công.";
                return View(p);
            }
            catch (Exception)
            {
                TempData["errorpercen"] = "Có lỗi, vui lòng liên hệ quản trị";
                TempData["successpercen"] = null;
                return View(p);
            }
            
        }

    }
}
