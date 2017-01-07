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
using BoatTEST.Classified.Core.Utilities;
namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU")]
    public class DiseaseOperationController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        ILogRepository logRepository = new LogRepository();
        IDiseaseOperationRepository objDiseaseOperationRepository = new DiseaseOperationRepository();
        IEmployeeRepository employeeRepository = new EmployeeRepository();
        
        /*For set the value of user login info*/
        SessUser objProfile = null;

        public void GetUserProfile()
        {
            this.objProfile = Session["user"] as MyClinic.Infrastructure.SessUser;
        }

        public ActionResult Report(string id, string transDate)
        {
            DiseaseOperationModels objViewsModels = null;
            IEnumerable<vDiseaseOperation> lstRecords = null;
            try
            {
                var totalRecords = 0;
                lstRecords = objDiseaseOperationRepository.Search(id, transDate, out totalRecords);

                objViewsModels = new DiseaseOperationModels()
                {
                    lstRecords = lstRecords,
                    transDate = transDate,
                    totalRecords = totalRecords
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(objViewsModels);
        }


        public ActionResult ReportPrint(string id, string transDate)
        {
            DiseaseOperationModels objViewsModels = null;
            IEnumerable<vDiseaseOperation> lstRecords = null;
            try
            {
                var totalRecords = 0;
                lstRecords = objDiseaseOperationRepository.Search(id, transDate, out totalRecords);

                objViewsModels = new DiseaseOperationModels()
                {
                    lstRecords = lstRecords,
                    transDate = transDate,
                    totalRecords = totalRecords
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }

            return View("ReportPrint", "_LayoutPrint", objViewsModels);
        }

        public ActionResult ReportDetail(string diseaseId)
        {
            DailyOperationModels objViewsModels = null;
            IEnumerable<vDailyOperation> lstRecords = null;
            IDailyOperationRepository objDailyOperationRepository = new DailyOperationRepository();
            try
            {
                var orderBy = Common.defaultOrderBy;
                var order = Common.defaultListOrder;
                var totalRecords = 0;
                lstRecords = objDailyOperationRepository.SearchByDisease(diseaseId, orderBy, order, out totalRecords);

                objViewsModels = new DailyOperationModels()
                {
                    lstRecords = lstRecords,
                    totalRecords = totalRecords,
                    diseaseId = diseaseId
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(objViewsModels);
        }

        public ActionResult ReportDetailPrint(string diseaseId)
        {
            DailyOperationModels objViewsModels = null;
            IEnumerable<vDailyOperation> lstRecords = null;
            IDailyOperationRepository objDailyOperationRepository = new DailyOperationRepository();
            try
            {
                var orderBy = Common.defaultOrderBy;
                var order = Common.defaultListOrder;
                var totalRecords = 0;
                lstRecords = objDailyOperationRepository.SearchByDisease(diseaseId, orderBy, order, out totalRecords);

                objViewsModels = new DailyOperationModels()
                {
                    lstRecords = lstRecords,
                    totalRecords = totalRecords,
                    diseaseId = diseaseId
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("ReportDetailPrint", "_LayoutPrint", objViewsModels);
        }
    }
}
