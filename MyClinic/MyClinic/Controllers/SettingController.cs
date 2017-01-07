using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClinic.Core;
using MyClinic.Models;
using MyClinic.Infrastructure;
using Utilities;
using MyClinic.MVC.Extension;
using MyClinic.Resources;
using System.IO;
using Newtonsoft.Json;
using log4net;
namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "SPU")]
    public class SettingController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        ISettingRepository settingRepository = new SettingRepository();
        ILogRepository logRepository = new LogRepository();
        MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {
            SettingModels viewModels = new SettingModels();
            try
            {
                var settings = settingRepository.Get();
                viewModels = new SettingModels()
                {
                    settings = settings
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(viewModels);
        }

        [HttpPost]
        public ActionResult Ajindex(MyClinic.Infrastructure.SettingEdit settingEdit)
        {
            string resultReturn = String.Empty;
            SettingModels viewModels = null;
            try
            {
                var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                var setting = settingRepository.GetById(settingEdit.Id);
                if (ModelState.IsValid)
                {
                    string strValue = settingEdit.KeyValue;
                    MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
                    if (settingEdit.KeyType == "3")
                    {
                        strValue = "campany.jpg";
                        var savePathImage = Server.MapPath("~/Uploads/Logo/");
                        ImageHelper.SaveImage(savePathImage, strValue, settingEdit.KeyValue);
                    }
                    setting.KeyValue = strValue;
                    settingRepository.Update(setting);
                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Edit Setting";
                    logTran.Description = "Edit Setting " + setting.KeyDes + "=" + settingEdit.KeyValue;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    resultReturn = "success";
                }
                viewModels = new SettingModels()
                {
                    setting = setting
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                resultReturn = "failure";
            }
            return Json(new { result = resultReturn });
        }
    }
}
