using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClinic.Infrastructure;
using MyClinic.Core;
using Utilities;
using MyClinic.Models;
using MyClinic.MVC.Extension;
using MyClinic.Resources;
using System.IO;
using Newtonsoft.Json;
using System.Transactions;
using BoatTEST.Classified.Core.Utilities;
using log4net;

namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU")]
    public class BackupController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        ILogRepository logRepository = new LogRepository();
        IBackupService backupService = new BackupService();
        string dbName = "MyClinicDB";
        // GET: /Home/
        public ActionResult Index()
        {

            var filePath = Utilities.Common.dbBackupPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "_" + dbName + ".bak";
            FileInfo fileInfo = new FileInfo(filePath);
            var createdDate = DateTime.Parse("1900-01-01");
            if (fileInfo.Exists)            
                createdDate = fileInfo.CreationTime;

            ViewData["CreatedDate"] = createdDate;
            return View(); 
        }
        [HttpPost]
        public ActionResult Backup()
        {
            try
            {
                var filePath = Utilities.Common.dbBackupPath + @"\" + DateTime.Now.ToString("yyyyMMdd") + "_" + dbName + ".bak";
                FileInfo fileInfo = new FileInfo(filePath);

                if (!fileInfo.Directory.Exists)
                    fileInfo.Directory.Create();

                if (fileInfo.Exists)
                    fileInfo.Delete();

                backupService.StartBackup(filePath, dbName, "Full backup " + dbName);
                backupService.RemoveOutOfPeriodBackup();

                var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                logTran.UserId = objSession.UserId;
                logTran.ProcessType = "Backup DB";
                logTran.Description = "Full backup " + dbName;
                logTran.LogDate = DateTime.Now;
                logRepository.Add(logTran);
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return Json(new { result = "success", proccessType = "Backup Database"});
        }
    }
}


