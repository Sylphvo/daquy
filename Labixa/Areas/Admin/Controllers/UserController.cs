using AutoMapper;
using Labixa.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Outsourcing.Data.Models;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        readonly IUserService _userService;
        readonly IProductService _productService;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        readonly IUserProductsMappingService _productUserService;
        public UserController(IUserService userService, RoleManager<IdentityRole> roleManager, IProductService productService, UserManager<User> userManager,
            IUserProductsMappingService productUserService)
        {
            _userService = userService;
            _roleManager = roleManager;
            _productService = productService;
            _userManager = userManager;
            _productUserService = productUserService;
        }
        //
        // GET: /Admin/User/
        public ActionResult Index()
        {
            var role = _roleManager.FindByName("Customer");
            var user = _userService.GetUsers().Where(p => p.Roles.Any(s => s.RoleId == role.Id));
            return View(user);
        }
        public ActionResult Create()
        {
            var model = new RegisterViewModel()
            {
                Products = _productService.GetProducts()
            };

            return View(model);
        }
        public ActionResult Edit(string id)
        {

            var user = _userService.GetUserById(id);
            var model = Mapper.Map<User, UserEditViewModel>(user);
            model.ProductId = _productUserService.GetByUserId(id).ProductID;
            model.Products = _productService.GetProducts();
            model.Name = user.DisplayName;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<UserEditViewModel, User>(model);
                user.FirstName = model.Name;
                _userService.EditUser(user);
                var mapping = new UserProductsMapping()
                {
                    UserId = user.Id,
                    ProductID = model.ProductId
                };
                _productUserService.EditUserProductsMapping(mapping);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };

                var result =  _userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, "Customer");
                    var mapping = new UserProductsMapping()
                    {
                        UserId = user.Id,
                        ProductID = model.ProductId
                    };
                    _productUserService.CreateUserProductsMapping(mapping);
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                }
            }
            model.Products = _productService.GetProducts();

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            _userService.DeleteUser(id);

            return RedirectToAction("Index");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}