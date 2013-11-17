using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using ChestClient.Models;
using System.Net;
using System.IO;
using System.Text;
using System.Windows;


namespace ChestClient
{
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
                   //Cookies
            CookieCollection cookies = new CookieCollection();

            // Создание cookie
            Cookie cookie = new Cookie("MyName", "Alex", "/", "localhost");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(
                   "http://localhost:2964/views/UserCookies.aspx");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cookie);

            HttpWebResponse responce = (HttpWebResponse)request.GetResponse();

            try
            {

                using (StreamReader stream = new StreamReader(
                        responce.GetResponseStream(), Encoding.UTF8))
                {

                    txb_text.Text = stream.ReadToEnd();

                }
            }
            catch { }
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}
