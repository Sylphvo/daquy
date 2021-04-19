using Newtonsoft.Json;
using Outsourcing.Data.Models;
using Outsourcing.Data.Utils;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Areas.Admin.Controllers
{
    public class LibraryImageController : Controller
    {
        readonly ILibraryImageService _libraryImageService;
        public LibraryImageController(ILibraryImageService libraryImageService)
        {
            _libraryImageService = libraryImageService;
        }
        //
        // GET: /Admin/LibraryImage/
        public ActionResult Index(int type)
        {
            var model = _libraryImageService.GetByType(type);
            ViewBag.Type = type;
            return View(model);
        }
        // GET: /Admin/LibraryImage/DetailProduct
        public ActionResult DetailProduct(int id,int cateId)
        {
            var type = (int)Enums.LibraryImageType.Course;
            var model = _libraryImageService.GetCourse(type,id);
            ViewBag.Type = type;
            ViewBag.Id = id;
            ViewBag.CateId = cateId;
            return View(model);
        }
        // GET: /Admin/LibraryImage/CreateImage
        public ActionResult CreateImage()
        {
            ViewBag.Type = (int)Enums.LibraryImageType.Image;
            var selector = from Enums.ImageEnum d in Enum.GetValues(typeof(Enums.ImageEnum))
                           select new VideoSelector { Value = (int)d, Text = d.DisplayName() };
            ViewBag.Selector = selector;
            return View();
        }
        // POST: /Admin/LibraryImage/CreateImage
        [HttpPost]
        public ActionResult CreateImage(HttpPostedFileBase[] files)
        {
            string pathSv = Server.MapPath("~/");
            var type = Int32.Parse(Request.Form.GetValues("type").FirstOrDefault());
            var typeImage = Int32.Parse(Request.Form.GetValues("typeImage").FirstOrDefault());
            try
            {
                _libraryImageService.Save(files, pathSv, type,typeImage);
                return Json(new
                {
                    success = true,
                    url = Url.Action("Index", "LibraryImage", new { type = type })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        // GET: /Admin/LibraryImage/CreateVideo
        public ActionResult CreateVideo(int type,int? id,int? cateId)
        {
            ViewBag.Type = type;
            ViewBag.Id = id;
            ViewBag.CateId = cateId;
            var selector = from Enums.VideoEnum d in Enum.GetValues(typeof(Enums.VideoEnum))
                           select new VideoSelector { Value = (int)d, Text = d.DisplayName() };
            ViewBag.Selector = selector;
            return View();
        }

        public ActionResult EditImage(int id)
        {
            var image = _libraryImageService.GetLibraryImageById(id);
            ViewBag.Type = (int)Enums.LibraryImageType.Image;
            ViewBag.Id = id;
            var selector = from Enums.ImageEnum d in Enum.GetValues(typeof(Enums.ImageEnum))
                           select new VideoSelector { Value = (int)d, Text = d.DisplayName() };
            ViewBag.Selector = selector;
            return View(image);
        }

        [HttpPost]
        public ActionResult EditImage(HttpPostedFileBase[] files)
        {
            string pathSv = Server.MapPath("~/");
            var type = Int32.Parse(Request.Form.GetValues("type").FirstOrDefault());
            var id = Int32.Parse(Request.Form.GetValues("id").FirstOrDefault());
            var typeImage = Int32.Parse(Request.Form.GetValues("typeImage").FirstOrDefault());
            try
            {
                _libraryImageService.Update(files, pathSv, type,id,typeImage);
                return Json(new
                {
                    success = true,
                    url = Url.Action("Index", "LibraryImage", new { type = type })
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public ActionResult EditVideo(int id,int? productId,int? cateId)
        {
            var image = _libraryImageService.GetLibraryImageById(id);
            var selector = from Enums.VideoEnum d in Enum.GetValues(typeof(Enums.VideoEnum))
                           select new VideoSelector { Value = (int)d, Text = d.DisplayName() };
            ViewBag.Selector = selector;
            ViewBag.Id = id;
            ViewBag.Type = (int)Enums.LibraryImageType.Video;
            if (cateId != null)
            {
                ViewBag.Type = (int)Enums.LibraryImageType.Course;
                ViewBag.ProductId = productId;
                ViewBag.CateId = cateId;
            }
           
            return View(image);
        }
        [HttpPost]
        public ActionResult EditVideo(HttpPostedFileBase[] files)
        {
            string pathSv = Server.MapPath("~/");
            string iframe = null;
            if (Request.Unvalidated.Form["iframe"] != null)
            {
                iframe = Request.Unvalidated.Form.GetValues("iframe").FirstOrDefault();
                iframe = JsonConvert.DeserializeObject<string>(iframe);
            }
            var type = Int32.Parse(Request.Form.GetValues("type").FirstOrDefault());
            var id = Int32.Parse(Request.Form.GetValues("id").FirstOrDefault());

            var typeVideo = Int32.Parse(Request.Form.GetValues("typeVideo").FirstOrDefault());
            int? productId = null;
            int? cateId = null;
            if (Request.Form["productId"] != null)
            {
                productId = Int32.Parse(Request.Form.GetValues("productId").FirstOrDefault());
                cateId = Int32.Parse(Request.Form.GetValues("cateId").FirstOrDefault());
            }
            try
            {
                _libraryImageService.Update(files, pathSv, type,id,null, iframe, typeVideo,productId);
                switch (type)
                {
                    case (int)Enums.LibraryImageType.Course:
                        return Json(new
                        {
                            success = true,
                            url = Url.Action("DetailProduct", "LibraryImage", new { id = productId,cateId })
                        }, JsonRequestBehavior.AllowGet);
                    default:
                        return Json(new
                        {
                            success = true,
                            url = Url.Action("Index", "LibraryImage", new { type = type })
                        }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [HttpPost]
        // POST: /Admin/LibraryImage/CreateVideo
        public ActionResult CreateVideo(HttpPostedFileBase[] files)
        {
            string pathSv = Server.MapPath("~/");
            string iframe = null;
            if (Request.Unvalidated.Form["iframe"] != null)
            {
                iframe = Request.Unvalidated.Form.GetValues("iframe").FirstOrDefault();
                iframe = JsonConvert.DeserializeObject<string>(iframe);
            }
            var type = Int32.Parse(Request.Form.GetValues("type").FirstOrDefault());

            var typeVideo = Int32.Parse(Request.Form.GetValues("typeVideo").FirstOrDefault());
            int? productId = null;
            int? cateId = null;
            if(Request.Form["productId"] != null)
            {
                productId = Int32.Parse(Request.Form.GetValues("productId").FirstOrDefault());
                cateId = Int32.Parse(Request.Form.GetValues("cateId").FirstOrDefault());
            }
             
            try
            {
                _libraryImageService.Save(files, pathSv, type,null, iframe, typeVideo,productId);
                switch (type)
                {
                    case (int)Enums.LibraryImageType.Course:
                        return Json(new
                        {
                            success = true,
                            url = Url.Action("DetailProduct", "LibraryImage", new { id = productId, cateId = cateId })
                        }, JsonRequestBehavior.AllowGet);
                    default:
                        return Json(new
                        {
                            success = true,
                            url = Url.Action("Index", "LibraryImage", new { type = type })
                        }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public ActionResult Delete(int Id, int type,int? productId,int? cateId)
        {
            _libraryImageService.DeleteLibraryImage(Id);
            switch (type)
            {
                case (int)Enums.LibraryImageType.Course:
                    return RedirectToAction("DetailProduct", new { id = productId , cateId = cateId });
                default:
                    return RedirectToAction("Index", new { type = type });
            }
           
        }
        public class VideoSelector
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }
    }
}