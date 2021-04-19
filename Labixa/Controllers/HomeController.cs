using Labixa.Common;
using Labixa.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Outsourcing.Data.Models;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace Labixa.Controllers
{

    public class HomeController : Controller
    {
        ILog Log = log4net.LogManager.GetLogger(typeof(HomeController));
        private UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IWebsiteAttributeService _websiteAttributeService;
      

        public HomeController( UserManager<User> userManager, IUserService userSerice, IWebsiteAttributeService websiteAttributeService)
        {
            _websiteAttributeService = websiteAttributeService;
            _userManager = userManager;
            _userService = userSerice;
        }
        #region Home
        public ActionResult Index()
        {
            return View();
        }
        #endregion

       
        //public ActionResult About()
        //{
        //    try
        //    {
        //        ViewBag.Message = "Your application description page.";
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Info("Error exception action about _" + DateTime.Now + ": " + ex);
        //        return RedirectToAction("Index", "ErrorMessage");
        //    }
        //}

        public ActionResult Contact(string str)
        {
            //CaptchaResponse response = _captcha.ValidateCaptcha(Request["g-recaptcha-response"]);
            try
            {

                ViewBag.Message = "Thank you,";
                if (str != null)
                {
                    ViewBag.ScriptsMessage = "<script>alert('" + str + "')</script>";
                }

                return View();
            }
            catch (Exception ex)
            {
                Log.Info("Error exception action Contact _" + DateTime.Now + ": " + ex);
                return RedirectToAction("Index", "ErrorMessage");
            }

        }
        [HttpPost]
        public ActionResult Contactrespon(string email, string message)
        {
            WebsiteAttribute websiteAttribute = new WebsiteAttribute()
            {
                Name = User.Identity.Name,//UserName
                Description = email,//UserName
                Noted_1 = message,//UserName
                Noted_2 = DateTime.Now.ToString("dd/MM/yyyy/ HH:mm:ss"),
                //Noted_3 = InputFileName,
                IsPublic = true,
                Deleted = false
            };
            _websiteAttributeService.CreateWebsiteAttribute(websiteAttribute);
            ViewBag.Message = "Thank you for your comments, we will assist you as quickly as possible";
            return RedirectToAction("Contact", "Home", new { str = ViewBag.Message });
        }
       
        public ActionResult BLog()
        {
            return View();
        }
   
        public void SetSession()
        {
            var userId = User.Identity.GetUserId();
            Session["User"] = _userManager.FindById(userId);
        }
      
        [HttpPost]
        public JsonResult SearchName(string userName)
        {
            var name = _userManager.FindByName(userName).Email;
            return Json(name, JsonRequestBehavior.AllowGet);
        }
    }


   
   
}