using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tempofme.Models;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
namespace Tempofme.Controllers
{
    public class AccountController : Controller
    {
        DataAccountContentDataContext AccountContent = new DataAccountContentDataContext();
        DataGMCISDataContext cisContext = new DataGMCISDataContext();

        private Common common = new Common();
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Changepass()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            string username = collection["name"].ToString();
            string password = Common.GetMD5(collection["password"].ToString());
            if (username == "" || collection["password"].ToString() == "")
            {
                TempData["errorlogin"] = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu và thử lại.";
                return RedirectToAction("Index");
            }
            else
            {
                


                //_9d_user users1 = AccountContent._9d_users.Where(c => c.user_name.Trim().ToString() != "a").FirstOrDefault();
                _9d_user users = AccountContent._9d_users.Where(c => c.user_name.Trim().ToString() == username.Trim()).FirstOrDefault();
                if (users != null)
                {
                    if (users.password == password)
                    {
                        Session["login"] = users.user_name;
                        Session["xu"] = users.balance;
                        if (users.status == true)
                        {
                            Session["status"] = true;
                            Session["message"] = "<script type='text/javascript'> alert('Quản trị game cảnh báo. \\n \\n" + users.message + "'); </script>";
                        }
                        else
                        {
                            Session["status"] = false;
                            Session["message"] = "";
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorlogin"] = "Bạn đã nhập sai mật khẩu, chúng tôi sẽ kiểm tra ip của bạn nếu nhập sai mật khẩu liên tục, vui lòng cẩn thận.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["errorlogin"] = "Tên đăng nhập không tồn tại, vui lòng kiểm tra lại. Hoặc chưa có tài khoản bạn có thể đăng ký tài khoản.";
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(FormCollection collection)
        {
            string username = collection["name"].ToString();
            string password = collection["password"].ToString();
            string passwordx = collection["passwordx"].ToString();
            string email = collection["email"].ToString();
            string telephone = collection["sodienthoai"].ToString();
            string sex = collection["option"].ToString();
            string address = collection["address"].ToString();

            if (username.Trim() == string.Empty || password.Trim() == string.Empty || telephone.Trim() == string.Empty || passwordx.Trim() == string.Empty || email.Trim() == string.Empty || address.Trim() == string.Empty)
            {
                TempData["errorsignup"] = "Vui lòng nhập đầy đủ thông tin đăng ký, password phải lớn hơn 6 ký tự.";
                return RedirectToAction("Signup", "Account");
            }

            if (username.Trim().Length < 5)
            {
                TempData["errorsignup"] = "Vui lòng nhập tài khoản đăng nhập phải lớn hơn 6 kí tự.";
                return RedirectToAction("Signup", "Account");
            }


            if (password.Trim().Length < 5)
            {
                TempData["errorsignup"] = "Vui lòng nhập password phải lớn hơn 6 kí tự.";
                return RedirectToAction("Signup", "Account");
            }
            if (password != passwordx)
            {
                TempData["errorsignup"] = "Vui lòng nhập mật khẩu và xác nhận mật khẩu phải trùng nhau.";
                return RedirectToAction("Signup", "Account");
            }

            if (!isMemberExist(username))
            {
                Tbl_Member_Password adduser = new Tbl_Member_Password();
                adduser.userid = username;
                adduser.userpassword = Common.GetMD5(password);

                AccountContent.Tbl_Member_Passwords.InsertOnSubmit(adduser);
                try
                {
                    AccountContent.SubmitChanges();
                }
                catch (Exception)
                {
                    TempData["errorsignup"] = "Có lỗi sảy ra vui lòng đăng ký lại.";
                    return RedirectToAction("Signup", "Account");
                }

                _9d_user user = new _9d_user();
                user.user_name = username.Trim();
                user.password = Common.GetMD5(password);
                user.email = email.Trim();
                user.created_at = DateTime.Now;
                user.telephone = telephone;
                user.balance = 0;
                user.address = address.Trim();
                user.created_by = "Admin";
                user.delete_flag = false;
                user.isActivate = true;
                user.ActivateCode = "FSGJTB45BDERT";
                user.level = 1;
                user.totalpost = 0;
                AccountContent._9d_users.InsertOnSubmit(user);
                try
                {
                    AccountContent.SubmitChanges();
                    string a = "";
                }
                catch (Exception)
                {
                    return RedirectToAction("Signup", "Account");
                }

                TempData["errorlogin"] = null;
                TempData["registerSuccess"] = "Đăng ký thành công, bạn hãy đăng nhập để vào trang chủ tham gia các sự kiện đặc biệt.";
                return RedirectToAction("Index", "Account");
            }
            else
            {
                TempData["errorsignup"] = "Tài khoản đã tồn tại, vui lòng đăng ký tài khoản mới.";
                return RedirectToAction("Signup", "Account");
            }
        }
        public ActionResult Logout()
        {
            Session["login"] = null;
            Session["status"] = false;
            Session["message"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Changepass(FormCollection collection)
        {
            string passwordo = collection["passwordo"].ToString();
            string password = collection["password"].ToString();
            string passwordx = collection["passwordx"].ToString();

            if (passwordo == "" || password == "" || passwordx == "")
            {
                TempData["erorprofile"] = "Vui lòng nhập đủ thông tin.";
                return View("Changepass");
            }
            if (password != passwordx)
            {
                TempData["erorprofile"] = "Mật khẩu mới phải nhập trùng nhau.";
                return View("Changepass");
            }
            else if (passwordx.Length < 6)
            {
                TempData["erorprofile"] = "Mật khẩu mới phải lớn hơn 6 kí tự.";
                return View("Changepass");
            }
            passwordo = Common.GetMD5(collection["passwordo"].ToString());
            password = Common.GetMD5(collection["password"].ToString());
            passwordx = Common.GetMD5(collection["passwordx"].ToString());

            _9d_user item = AccountContent._9d_users.Where(c => c.user_name == Session["login"].ToString()).FirstOrDefault();
            if (item.password != passwordo)
            {
                TempData["erorprofile"] = "Bạn đã nhập sai mật khẩu cũ, vui lòng nhập lại.";
                return View("Changepass");
            }

            item.password = passwordx;

            try
            {
                AccountContent.SubmitChanges();
                Tbl_Member_Password it = AccountContent.Tbl_Member_Passwords.Where(c => c.userid == Session["login"].ToString()).FirstOrDefault();
                it.userpassword = passwordx;
                TempData["erorprofile"] = null;
                TempData["successprofile"] = "Chúc mừng, mật khẩu của bạn đã được cập nhật.";
                AccountContent.SubmitChanges();
                return View("Changepass");
            }
            catch (Exception)
            {

                throw;
            }

            return View("Changepass");
        }

        public ActionResult Giftcode()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Giftcode(FormCollection collection)
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            string gift = collection["giftcode"].ToString();
            Tbl_giftcode gt = AccountContent.Tbl_giftcodes.Where(c => c.gift_code == gift).FirstOrDefault();
            if (gt == null)
            {
                TempData["errorgift"] = "Không tồn tại Gift code này";
                return RedirectToAction("Giftcode", "Account");
            }

            if (gt.active_id == 1)
            {
                if (!checkgroup_user(gt.group_user))
                {
                    TempData["errorgift"] = "Tài khoản đã kích hoạt Gift code này";
                    return RedirectToAction("Giftcode", "Account");
                }
                else
                {
                    string userId = Session["login"].ToString();
                    int? itemId = Convert.ToInt32(gt.item_code);
                    byte? server = Convert.ToByte(0);
                    int? orderId = 0;
                    byte? err = 0;
                    cisContext.Sp_Purchase_Using(userId, itemId, Convert.ToByte(0), 0, ref orderId, ref err);
                    //add vao transaction
                    try
                    {
                        cisContext.SubmitChanges();
                        gt.group_user = gt.group_user + "," + userId;
                        AccountContent.SubmitChanges();
                        TempData["successgift"] = "Tài khoản đã kích hoạt gift thành công, vào game để nhận quà.";
                        return View();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
            else if (gt.active_id == 2)
            {
                if (checkactive_user(gt.active_user))
                {
                    TempData["errorgift"] = "Đây là gift đặc biệt, tài khoản này không được sử dụng.";
                    return RedirectToAction("Giftcode", "Account");
                }
                else
                {
                    if (!checkgroup_user(gt.group_user))
                    {
                        TempData["errorgift"] = "Tài khoản đã kích hoạt Gift code này";
                        return RedirectToAction("Giftcode", "Account");
                    }
                    else
                    {
                        string userId = Session["login"].ToString();
                        int? itemId = Convert.ToInt32(gt.item_code);
                        byte? server = Convert.ToByte(0);
                        int? orderId = 0;
                        byte? err = 0;
                        cisContext.Sp_Purchase_Using(userId, itemId, Convert.ToByte(0), 0, ref orderId, ref err);
                        try
                        {
                            cisContext.SubmitChanges();
                            gt.group_user = gt.group_user + "," + userId;
                            AccountContent.SubmitChanges();
                            TempData["successgift"] = "Tài khoản đã kích hoạt gift thành công, vào game để nhận quà.";
                            return View();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult Profile()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Account");
            }

            _9d_user u = AccountContent._9d_users.Where(c => c.user_name == Session["login"].ToString() && c.delete_flag == false).FirstOrDefault();
            return View(u);
        }

        [HttpPost]
        public ActionResult Profile(FormCollection collection)
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Account");
            }
            if (collection["email"].ToString() == "" || collection["sodienthoai"].ToString() == "" || collection["address"].ToString() == "")
            {
                TempData["errorchange"] = "Vui lòng nhập thông tin chính xác.";
                TempData["successchange"] = null;
                _9d_user u = AccountContent._9d_users.Where(c => c.user_name == Session["login"].ToString() && c.delete_flag == false).FirstOrDefault();
                return View(u);
            }
            _9d_user i = AccountContent._9d_users.Where(c => c.user_name == Session["login"].ToString() && c.delete_flag == false).FirstOrDefault();
            i.email = collection["email"].ToString();
            i.telephone = collection["sodienthoai"].ToString();
            i.address = collection["address"].ToString();
            i.status = false;
            i.message = "";
            try
            {
                AccountContent.SubmitChanges();
                TempData["errorchange"] = null;
                TempData["successchange"] = "Dữ liệu mới đã được cập nhât.";
                Session["status"] = false;
                Session["message"] = null;
                _9d_user u = AccountContent._9d_users.Where(c => c.user_name == Session["login"].ToString() && c.delete_flag == false).FirstOrDefault();
                return View(u);

            }
            catch (Exception)
            {
                TempData["errorchange"] = "Có lỗi, liên hệ quản trị viên.";
                TempData["successchange"] = null;
                _9d_user u = AccountContent._9d_users.Where(c => c.user_name == Session["login"].ToString() && c.delete_flag == false).FirstOrDefault();
                return View(u);
            }
        }

        private bool isMemberExist(string username)
        {
            Tbl_Member_Password isExist = AccountContent.Tbl_Member_Passwords.Where(c => c.userid == username).FirstOrDefault();
            if (isExist != null)
                return true;
            return false;
        }

        public bool checkgroup_user(string group)
        {
            if (group == null)
            {
                return true;
            }
            string[] cat = group.Split(',');
            string checkus = Session["login"].ToString();
            foreach (var item in cat)
            {
                if (item == checkus)
                {
                    return false;
                }
            }
            return true;
        }

        public bool checkactive_user(string group)
        {
            if (group == null)
            {
                return true;
            }
            string[] cat = group.Split(',');
            string checkus = Session["login"].ToString();
            foreach (var item in cat)
            {
                if (item == checkus)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
