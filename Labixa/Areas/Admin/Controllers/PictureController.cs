using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    public class PictureController : Controller
    {
        readonly IPictureService _pictureService;
        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }
        //
        // GET: /Admin/Picture/
        public ActionResult Index()
        {
            
            return View(_pictureService.GetPictures());
        }
        public ActionResult Delete(int Id)
        {
            _pictureService.DeletePicture(Id);
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            //Get the list category
            //var listProductCategory = _productCategoryService.GetProductCategories().ToSelectListItems(-1);
            //var product = new ProductFormModel { ListProductCategory = listProductCategory };
            return View();
        }
    }
}