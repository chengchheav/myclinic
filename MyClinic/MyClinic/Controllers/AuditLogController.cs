using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClinic.Core;
using MyClinic.Models;
using Utilities;
using MyClinic.MVC.Extension;
using MyClinic.Resources;
using log4net;
namespace MyClinic.Controllers
{
    /*Log For Specific User*/
    [CustomAuthorizeAttribute("RPV", "SPU", "ADM")]
    public class AuditLogController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IUserRepository userRepository = new UserRepository();
        IAuditLogRepository logRepository = new AuditLogRepository();
        TranslateSwitch translateSwitch = new TranslateSwitch();

        public ActionResult Index()
        {
            AuditLogModels viewerModels = null;
            try
            {
               MyClinic.Infrastructure.Log log = new MyClinic.Infrastructure.Log();
               var objCurDate       = DateTime.Today;
               var objPreDate       = objCurDate.AddDays(-1);
               string startDate     = Common.GetDatePickerDisplay(objPreDate);
               string endDate       = Common.GetDatePickerDisplay(objCurDate);
               string cboUser       = "";
               string cboPro        = "";
                var orderBy         = Common.defaultOrderBy;
                var order           = Common.defaultListOrder;
                var totalRecords    = 0;
                var lstProcessTypes = logRepository.GetProcessType();
                var lstUsers        = userRepository.Get();
                var lstRecords      = logRepository.Search(startDate, endDate, cboUser, cboPro, orderBy, order, out totalRecords);
                viewerModels = new AuditLogModels()
                {
                    lstRecords = lstRecords,
                    lstUsers = lstUsers,
                    lstProcessTypes = lstProcessTypes,
                    order = order,
                    orderBy = orderBy,
                    totalRecords = totalRecords,
                    startDate = startDate,
                    endDate = endDate,
                    cboPro = cboPro,
                    cboUser = cboUser
                };
            }
            catch (Exception ex) {
                log.Error(ex);
            }
            return View(viewerModels);
        }

        public ActionResult Ajindex(string orderBy, string order, string startDate, string endDate, string cboUser, string cboPro)
        {
            AuditLogModels viewerModels = null;
            try
            {
                MyClinic.Infrastructure.Log log = new MyClinic.Infrastructure.Log();
                orderBy = orderBy.Replace(" ", "") ?? Common.defaultOrderBy;
                order = order ?? Common.defaultListOrder;
                int totalRecords = 0;
                var lstRecords = logRepository.Search(startDate, endDate, cboUser, cboPro, orderBy, order, out totalRecords);
                viewerModels = new AuditLogModels()
                {
                    lstRecords = lstRecords,
                    order = order,
                    orderBy = orderBy,
                    totalRecords = totalRecords,
                    startDate = startDate,
                    endDate = endDate,
                    cboPro = cboPro,
                    cboUser = cboUser

                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Ajindex", "_LayoutBlank", viewerModels);
        }

    }
}
