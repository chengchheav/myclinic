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
    public class PatientOperationController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        ILogRepository logRepository = new LogRepository();
        IPatientOperationRepository objPatientOperationRepository = new PatientOperationRepository();
        IEmployeeRepository employeeRepository = new EmployeeRepository();
        
        /*For set the value of user login info*/
        SessUser objProfile = null;

        public void GetUserProfile()
        {
            this.objProfile = Session["user"] as MyClinic.Infrastructure.SessUser;
        }

        public ActionResult Report(string id, string cboDoct, string startDate, string endDate, string searchBy, string keyword)
        {
            PatientOperationModels objViewsModels = null;
            try
            {
                var totalRecords = 0;
                var lstRecords = objPatientOperationRepository.Search(id, cboDoct, searchBy, keyword, startDate, endDate, out totalRecords);

                objViewsModels = new PatientOperationModels()
                {
                    lstRecords = lstRecords,
                    totalRecords = totalRecords,
                    startDate = startDate,
                    endDate = endDate,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(objViewsModels);
        }

        public ActionResult AjReport(string id, string cboDoct, string startDate, string endDate, string searchBy, string keyword)
        {
            PatientOperationModels objViewsModels = null;
            try
            {
                var totalRecords = 0;
                var lstRecords = objPatientOperationRepository.Search(id, cboDoct, searchBy, keyword, startDate, endDate, out totalRecords);

                objViewsModels = new PatientOperationModels()
                {
                    lstRecords = lstRecords,
                    totalRecords = totalRecords,
                    startDate = startDate,
                    endDate = endDate,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }

            return View("Ajreport", "_LayoutBlank", objViewsModels);
        }

        public ActionResult ReportPrint(string id, string cboDoct, string startDate, string endDate, string searchBy, string keyword)
        {
            PatientOperationModels objViewsModels = null;
            try
            {
                var totalRecords = 0;
                var lstRecords = objPatientOperationRepository.Search(id, cboDoct, searchBy, keyword, startDate, endDate, out totalRecords);

                objViewsModels = new PatientOperationModels()
                {
                    lstRecords = lstRecords,
                    totalRecords = totalRecords,
                    startDate = startDate,
                    endDate = endDate,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }

            return View("ReportPrint", "_LayoutPrint", objViewsModels);
        }
    }
}
