using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClinic.Resources;

namespace MyClinic.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            string layoutPage = "_Layout";
            bool isAjax = HttpContext.Request.IsAjaxRequest();
            if (isAjax == true)
            {
                layoutPage = "_LayoutBlank";
            }
            return View("Index", layoutPage);
        }

        public ActionResult Error403()
        {
            string layoutPage = "_Layout";
            bool isAjax = HttpContext.Request.IsAjaxRequest();
            if (isAjax == true)
            {
                layoutPage = "_LayoutBlank";
            }
            return View("Error403", layoutPage);
        }

        public ActionResult Error404()
        {
            string layoutPage = "_Layout";
            bool isAjax = HttpContext.Request.IsAjaxRequest();
            if (isAjax == true)
            {
                layoutPage = "_LayoutBlank";
            }
            return View("Error404", layoutPage);
        }

        public ActionResult LogInAgain()
        {
            string layoutPage = "_Layout";
            bool isAjax = HttpContext.Request.IsAjaxRequest();
            if (isAjax == true)
            {
                layoutPage = "_LayoutBlank";
            }
            return View("LogInAgain", layoutPage);
        }

        public ActionResult AjError403() {
            return View("AjError403", "_LayoutBlank");
        }

    }
}
