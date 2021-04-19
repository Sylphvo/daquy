using Labixa.Common;
using Labixa.Helpers;
using Microsoft.AspNet.Identity;
using Outsourcing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Labixa.Controllers
{
    public class BaseHomeController : Controller
    {
        //private readonly UserManager<User> _userManager;

        //public BaseHomeController(UserManager<User> userManager)
        //{
        //    _userManager = userManager;
        //}

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
            // Validate culture name
            HttpCookie cultureCookie = Request.Cookies["_culture"];
           
                //cultureName = "vi-VN";
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }
            else
            {
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                Request.UserLanguages[0] : // obtain it from HTTP header AcceptLanguages
                "vi";
                HttpCookie cookie = new HttpCookie("_culture", cultureName);
                Response.SetCookie(cookie);
            }
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
                                                                            //cultureName = "vi";
                                                                            //cultureName = "en";
                                                                            // Modify current thread's cultures
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            //var ip = IPClient.getIP();
            //var aaa = User.Identity.GetUserId();
            //if(aaa != "")
            //{
            //    var _user = User.Identity.F.FindById(aaa);
            //    if (!(ip.Equals(_user.IP_login))){
            //        Dispose();
            //    }
            //}

            return base.BeginExecuteCore(callback, state);
        }

    }
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Account" }));
            }
        }
    }
}