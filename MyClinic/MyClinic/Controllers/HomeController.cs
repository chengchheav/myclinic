using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClinic.Infrastructure;
using MyClinic.MVC.Extension;
using MyClinic.Resources;
using MyClinic.Core;
using log4net;


namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU","FUR")]
    public class HomeController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /Home/
        public ActionResult Index()
        {            
            return View(); 
        }        
    }
}


