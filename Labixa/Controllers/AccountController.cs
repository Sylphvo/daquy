using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Labixa.Models;
using Outsourcing.Data.Models;
using log4net;
using System.Reflection;

namespace Labixa.Controllers
{
    //[Authorize]
    public class AccountController : BaseHomeController
    {
       
        private UserManager<User> _userManager;
  
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        // GET: /Account/Login
        //[AllowAnonymous]
        public ActionResult Login(/*string returnUrl*/)
        {
            //AuthenticationManager.SignOut();
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("AccountDashboard", "Home");
            //}
            //log.Info("tui là tui");
            ////_userManager.ChangePassword("c21a1169-1e5d-4aa7-9604-1595514608bc", "123456", "labixa@123");
            //ViewBag.ReturnUrl = returnUrl;
            ////HttpCookie cookie1 = Request.Cookies["mimosa"];
            //Response.Cookies["mimosa"].Expires = DateTime.Now.AddDays(-1);
            //Response.Cookies["mamosi"].Expires = DateTime.Now.AddDays(-1);
            //Request.Cookies.Remove("mimosa");//QRIMage
            //Request.Cookies.Remove("mamosi");//QRText
            //                                 //Response.Cookies.Clear()
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.AddModelError("", message);
                    var user = await _userManager.FindAsync(model.UserName.ToLower(), model.Password);
                    if (user != null)
                    {
                        if (user.Activated == true)
                        {
                            //kiểm tra xác thực email
                            if (user.EmailConfirmed == true)
                            {
                                //Nếu chứa xác thực thì login bình thường
                                if (user.TwoFactorEnabled == false)
                                {
                                    Session["User"] = user;
                                    Session["UserName"] = user.UserName.ToLower();
                                    await SignInAsync(user, model.RememberMe);
                                    //lưu ip login vào db Website kiểm tra login
                                    _userManager.Update(user);
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    return RedirectToAction("Confirm2FA", "EmailFunc", new { username = user.UserName.ToLower() });
                                }
                            }
                            else ModelState.AddModelError("", "Please confirm email before login");
                        }
                        else
                        {
                            ModelState.AddModelError("", "User name or Password incorrect");
                        }
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

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string affiliate)
        {
          
            Session["User"] = null;
            Session["UserName"] = null;
            Request.Cookies.Remove("mimosa");//QRIMage
            Request.Cookies.Remove("mamosi");//QRText
            Response.Cookies["mimosa"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["mamosi"].Expires = DateTime.Now.AddDays(-1);
            AuthenticationManager.SignOut();
            if (affiliate != null && affiliate != "")
            {
                ViewBag.Affiliate = affiliate;
            }
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //kiểm tra user name khi được nhập vào
                    var checkUser = _userManager.FindByName(model.UserName.ToLower());
                    //check mail
                    var checkEmailAddress = _userManager.FindByEmail(model.Email);
                    if (checkEmailAddress != null)// mail tồn tại không cho tạo tk
                    {
                        ModelState.AddModelError("", "Email existed");
                    }
                    else
                    {
                        //kiểm tra username có tồn tại trong db hay chưa, null là username chưa có -> có thể đăng ký được
                        if (checkUser == null)
                        {
                            User userParent = new User()
                            {
                                UserName = model.UserName.ToLower().Replace(" ", string.Empty),
                                Email = model.Email,
                                PasswordNotHash = model.Password,
                                Temp2 = model.Password,// để lưu khi đổi pass
                                Activated = true//tạo mặc định để có thể lock hoặc unclock trong admin
                            };
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Error exception function register accont_" + DateTime.Now + ": " + ex);
                ModelState.AddModelError("", "**Error register");
            };
            return View();
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await _userManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //Change Password
        // GET: /Account/Manage
        [Authorize]
        public ActionResult Manage(ManageMessageId? message, string messagechange)
        {
            ViewBag.ReturnUrl = Url.Action("SignOut");
            if (messagechange != null)
            {
                ViewBag.messagechange = messagechange;
            }
            return View();
        }

        //Change Password
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = _userManager.FindById(userId);

            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            //string message = string.Empty;
            ManageMessageId? message = null;


            if (hasPassword)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        IdentityResult result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.CurrentPassword, model.Password);
                        if (result.Succeeded)
                        {
                            var findUserName = _userManager.FindByName(User.Identity.Name);
                            findUserName.Temp2 = model.Password;
                            //findUserName.PasswordNotHash = model.NewPassword;
                            _userManager.Update(findUserName);
                            return RedirectToAction("Manage", new { message = ManageMessageId.ChangePasswordSuccess, messagechange = "Change Password Success" }); ;
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //message = "Change Password Failed";
                    log.Info("Function change password account_" + DateTime.Now + ": " + ex);
                    return RedirectToAction("Index", "ErrorMessage");
                }

            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }
                try
                {
                    if (ModelState.IsValid)
                    {
                        IdentityResult result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Info("Function change password account: " + ex);
                    return RedirectToAction("Index", "ErrorMessage");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await _userManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

       

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["User"] = null;
            Session["UserName"] = null;
            Request.Cookies.Remove("mimosa");//QRIMage
            Request.Cookies.Remove("mamosi");//QRText
            Response.Cookies["mimosa"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["mamosi"].Expires = DateTime.Now.AddDays(-1);
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult SignOut()
        {
            Response.Cookies["mimosa"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["mamosi"].Expires = DateTime.Now.AddDays(-1);
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

      
        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = _userManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }
        [Authorize]
        public ActionResult GetSession(string username)
        {
            var userId = User.Identity.GetUserId();
            Session["User"] = _userManager.FindById(userId);
            return null;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
        #region reset password
        /// <summary>
        /// Form nhập email để reset password
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }
       
        #endregion
    }

}