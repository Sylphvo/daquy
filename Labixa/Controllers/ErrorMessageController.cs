using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labixa.Controllers
{
    public class ErrorMessageController : Controller
    {
        //
        // GET: /ErrorMessage/
        #region message exception
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        #region message 400 404 403 500
        public ActionResult Message400()
        {
            return View();
        }
        public ActionResult Message404()
        {
            return View();
        }
        public ActionResult Message403()
        {
            return View();
        }
        public ActionResult Message500()
        {
            return View();
        }
        #endregion
    }
}