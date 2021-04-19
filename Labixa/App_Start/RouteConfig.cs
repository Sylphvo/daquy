using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Labixa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Home", "Home", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("about-us", "about-us", new { controller = "Home", action = "AboutUs", id = UrlParameter.Optional });
            routes.MapRoute("ware-game", "ware-game", new { controller = "Home", action = "WareGame", id = UrlParameter.Optional });
            routes.MapRoute("contest", "contest", new { controller = "Home", action = "Contest", id = UrlParameter.Optional });
            routes.MapRoute("contact", "Contact", new { controller = "Home", action = "Contact", id = UrlParameter.Optional });
            routes.MapRoute("affilites", "affilites", new { controller = "Home", action = "AffiliateProgramming", id = UrlParameter.Optional });
            routes.MapRoute("account-dashboard", "account-dashboard", new { controller = "Home", action = "AccountDashboard", id=UrlParameter.Optional});
            routes.MapRoute("my-affilites", "my-affilites", new { controller = "Home", action = "Affilites", id = UrlParameter.Optional });
            routes.MapRoute("changePassword", "changePassword", new { controller = "Home", action = "ChangePassword", id = UrlParameter.Optional });
            routes.MapRoute("history-transaction", "history-transaction", new { controller = "Home", action = "History", id = UrlParameter.Optional });
            routes.MapRoute("register-account", "register-account", new { controller = "Account", action = "Register", affiliate = UrlParameter.Optional });
            routes.MapRoute("login", "login", new { controller = "Account", action = "Login", id = UrlParameter.Optional });
            routes.MapRoute("Logoff", "Logoff", new { controller = "Account", action = "Signout", id = UrlParameter.Optional });
            routes.MapRoute("change-password", "change-password", new { controller = "Account", action = "Manage", id = UrlParameter.Optional });
            routes.MapRoute("confirm-email", "confirm-email", new { controller = "EmailFunc", action = "ConfirmMail", id = UrlParameter.Optional });
            routes.MapRoute("confirm-transaction", "confirm-transaction", new { controller = "EmailFunc", action = "ConfirmTransactinMail", id = UrlParameter.Optional });
            routes.MapRoute("confirm-transaction-vip", "confirm-transaction-vip", new { controller = "EmailFunc", action = "ConfirmTransactionMail_VIP", id = UrlParameter.Optional });
            routes.MapRoute("forgot-password", "forgot-password", new { controller = "Account", action = "ResetPassword", id = UrlParameter.Optional });
            routes.MapRoute("ConfirmForgot-password", "ConfirmForgot-password", new { controller = "EmailFunc", action = "CheckMailTimeout", id = UrlParameter.Optional });
            routes.MapRoute("confirm-deposit", "confirm-deposit", new { controller = "EmailFunc", action = "ConfirmTrasactionDeposit", id = UrlParameter.Optional });
            routes.MapRoute("Blog", "Blog", new { controller = "Home", action = "Blog", id = UrlParameter.Optional });
            routes.MapRoute("faq", "faq", new { controller = "Home", action = "FAQ", id = UrlParameter.Optional });
            routes.MapRoute("Shop", "Shop", new { controller = "Shop", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Information", "Information", new { controller = "Shop", action = "Information", id = UrlParameter.Optional });
            routes.MapRoute("Deal", "Deal", new { controller = "Shop", action = "Deal", id = UrlParameter.Optional });
            routes.MapRoute("Service", "Service", new { controller = "Service", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Index2", "Index2", new { controller = "Service", action = "Index2", id = UrlParameter.Optional });
            routes.MapRoute("Index3", "Index3", new { controller = "Service", action = "Index3", id = UrlParameter.Optional });
            routes.MapRoute("Index4", "Index4", new { controller = "Service", action = "Index4", id = UrlParameter.Optional });
            routes.MapRoute("Pages", "Pages", new { controller = "Pages", action = "Index", id = UrlParameter.Optional });
            routes.MapRoute("Signin", "Signin", new { controller = "TaiKhoan", action = "Login", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
