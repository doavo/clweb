using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tempofme.Models;

namespace Tempofme.Controllers
{
    public class ServiceController : Controller
    {
        DataAccountContentDataContext accountContext = new DataAccountContentDataContext();
        DataGMCISDataContext cisContext = new DataGMCISDataContext();
        Common common = new Common();
        //
        // GET: /Service/

        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            List<_9d_KTCItem> items = accountContext._9d_KTCItems.Where(c => c.delete_flag == false).ToList();
            return View(items);
        }

        public ActionResult Napthe()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            _9d_percen p = accountContext._9d_percens.FirstOrDefault();
            TempData["status"] = p.status;
            return View();
        }

        [HttpPost]
        public ActionResult Napthe(FormCollection collection)
        {
            if (Session["login"] == null)
            {
                HttpContext.Application["_controler"] = "Service";
                HttpContext.Application["_action"] = "Napthe";
                return RedirectToAction("Index", "Account");
                //return RedirectToAction("Index", HttpContext.Application["_controler"] as string);
            }

            if (collection["seri"].ToString() == "" || collection["pin"].ToString() == "")
            {
                TempData["errorcard"] = "Vui lòng nhập đầy đủ thông tin thẻ cào.";
                TempData["successcard"] = null;
                _9d_percen p = accountContext._9d_percens.FirstOrDefault();
                TempData["status"] = p.status;
                return View();
            }

            try
            {
                _9d_percen p = accountContext._9d_percens.FirstOrDefault();
                TempData["status"] = p.status;

                RequestInfo info = new RequestInfo();
                info.Merchant_id = "36680";
                info.Merchant_acount = "demo@nganluong.vn";
                info.Merchant_password = "matkhauketnoi";

                //Nhà mạng
                info.CardType = collection["MovieType"].ToString();
                info.Pincard = collection["pin"].ToString();

                //Mã đơn hàng
                info.Refcode = (new Random().Next(0, 10000)).ToString();
                info.SerialCard = collection["seri"].ToString();

                ResponseInfo resutl = NLCardLib.CardChage(info);

                if (resutl.Errorcode.Equals("00"))
                {
                    _9d_user user = accountContext._9d_users.Where(c => c.user_name == Session["login"].ToString() && c.delete_flag == false).FirstOrDefault();
                    int coutncar = (Convert.ToInt32(resutl.Card_amount) * p.percen) / 100;
                    user.balance = user.balance + coutncar;
                    accountContext.SubmitChanges();

                    User_History addhistory = new User_History();
                    addhistory.user_name = Session["login"].ToString();
                    addhistory.time_into = DateTime.Now;
                    addhistory.car_info = coutncar.ToString();
                    addhistory.car_type = collection["MovieType"].ToString();
                    accountContext.User_Histories.InsertOnSubmit(addhistory);
                    accountContext.SubmitChanges();
                    _9d_user u = common.getUserInfo(Session["login"].ToString());
                    ViewBag.balance = u != null ? u.balance.ToString() : "0";
                    TempData["errorcard"] = null;
                    TempData["successcard"] = "Chúc mừng, bạn đã nạp thành công thẻ mệnh giá " + coutncar.ToString() + ".";
                }
                else
                {
                    TempData["errorcard"] = resutl.Message.ToString();
                    TempData["successcard"] = null;
                }

                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Buy(int id)
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            _9d_user u = common.getUserInfo(Session["login"].ToString());
            _9d_KTCItem item = accountContext._9d_KTCItems.Where(c => c.id == id).FirstOrDefault();

            if (item == null)
            {
                TempData["ktcFailed"] = "Item này không tồn tại";
                return RedirectToAction("Index", "Service");
            }

            if (u.balance < item.itemprice)
            {
                TempData["ktcFailed"] = "Bạn không đủ tiền để mua vật phẩm này";
                return RedirectToAction("Index", "Service");
            }

            try
            {
                //chuyen item vao tk nguoi choi
                string userId = u.user_name;
                int? itemId = Convert.ToInt32(item.itemid);
                byte? server = Convert.ToByte(0);
                int? orderId = 0;
                byte? err = 0;

                cisContext.Sp_Purchase_Using(userId, Convert.ToInt32(item.itemid), server, 0, ref orderId, ref err);
                //add vao transaction
                try
                {
                    cisContext.SubmitChanges();
                }
                catch (Exception)
                {

                    throw;
                }


                int curr = u.balance;
                //tru tien
                //accountContext._9d_transactions.InsertOnSubmit(trans);
                //accountContext.SubmitChanges();
                _9d_user user = accountContext._9d_users.Where(c => c.user_name == userId).FirstOrDefault();
                user.balance = curr - item.itemprice;
                Session["xu"] = null;
                Session["xu"] = user.balance;

                //accountContext._9d_transactions.InsertOnSubmit(trans);
                try
                {
                    accountContext.SubmitChanges();
                    TempData["ktcOK"] = "Mua hàng thành công !!!";
                    return RedirectToAction("Index", "Service");
                }
                catch (Exception)
                {

                    throw;
                }


            }
            catch (Exception)
            {

            }

            return RedirectToAction("Index", "Service");
        }

        public ActionResult Banner()
        {
            _9d_ad right = accountContext._9d_ads.Where(c => c.ads_type == 3).FirstOrDefault();
            if (right != null)
            {
                ViewBag.rightlink = right.ads_link;
                ViewBag.rightimage = right.ads_image;
                ViewBag.rightname = right.ads_name;
            }
            else
            {
                ViewBag.rightlink = "/";
                ViewBag.rightimage = "../Content/images/bottom-banner.png";
                ViewBag.rightname = "Không có sự kiện";
            }
            return PartialView("Banner");
        }

    }
}
