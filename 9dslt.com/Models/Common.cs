using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Configuration;

namespace Tempofme.Models
{
    public static class Utility
    {
        private static readonly string[] VietnameseSigns = new string[]

        {

        "aAeEoOuUiIdDyY",

        "áàạảãâấầậẩẫăắằặẳẵ",

        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

        "éèẹẻẽêếềệểễ",

        "ÉÈẸẺẼÊẾỀỆỂỄ",

        "óòọỏõôốồộổỗơớờợởỡ",

        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

        "úùụủũưứừựửữ",

        "ÚÙỤỦŨƯỨỪỰỬỮ",

        "íìịỉĩ",

        "ÍÌỊỈĨ",

        "đ",

        "Đ",

        "ýỳỵỷỹ",

        "ÝỲỴỶỸ"

        };

        public static string RemoveAscii(string id)
        {
            string str = "";
            str = id.Trim();
            str = str.Replace(" ", "-");
            str = str.Replace("/", "-");
            return str;
        }

        /// <summary>
        /// remote vietnam string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }
    }
    public class Common
    {

        DataAccountContentDataContext AccountContent = new DataAccountContentDataContext();

        public static string GetMD5(string str)
        {

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            StringBuilder sbHash = new StringBuilder();

            foreach (byte b in bHash)
            {
                sbHash.Append(String.Format("{0:x2}", b));
            }

            return sbHash.ToString();
        }
        public _9d_user getUserInfo(string username)
        {
            _9d_user user = AccountContent._9d_users.Where(c => c.user_name == username.Trim() && c.delete_flag == false).FirstOrDefault();

            return user;
        }
        public static string getBase44()
        {
            string base44 = ConfigurationManager.ConnectionStrings["NineDragons_AccountConnectionString"].ConnectionString;
            return base44;
        }

        /// <summary>
        ///  khoi tao toi uu seo web
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public static bool checkInt(string str)
        {
            int value;
            if (int.TryParse(str, out value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}