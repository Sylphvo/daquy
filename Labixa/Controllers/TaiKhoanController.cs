using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Labixa.Models;
using Outsourcing.Data.Models;

namespace Labixa.Controllers
{
    public class TaiKhoanController : BaseHomeController
    {
        private UserManager<User> _userManager;
        //public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public TaiKhoanController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //
        // GET: /TaiKhoan/
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var ip = IPClient.getIP();
                    ModelState.AddModelError("", message);
                    var user = await _userManager.FindAsync(model.UserName.ToLower(), model.Password);
                    if (user != null)
                    {
                        Session["User"] = user;
                        Session["UserName"] = user.UserName.ToLower();
                        await SignInAsync(user, model.RememberMe);
                        //lưu ip login vào db Website kiểm tra login
                        //user.IP_login = ip;
                        _userManager.Update(user);
                        return RedirectToAction("Index", "Home");
                    }
                    else ModelState.AddModelError("", "User name or Password incorrect");
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "ErrorMessage");
            }

        }
        public ActionResult SignOut()
        {
            Response.Cookies["mimosa"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["mamosi"].Expires = DateTime.Now.AddDays(-1);
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}