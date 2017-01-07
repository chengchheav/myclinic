﻿using System;
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
    public class DoctorOperationController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        ILogRepository logRepository = new LogRepository();
        IDoctorOperationRepository objDoctorOperationRepository = new DoctorOperationRepository();
        IEmployeeRepository employeeRepository = new EmployeeRepository();
        
        /*For set the value of user login info*/
        SessUser objProfile = null;

        public void GetUserProfile()
        {
            this.objProfile = Session["user"] as MyClinic.Infrastructure.SessUser;
        }

        public ActionResult Report()
        {
            DoctorOperationModels objViewsModels = null;
            try
            {
                string searchBy = "";
                string keyword = "";
                var cboDoct = "";
                var orderBy = Common.defaultOrderBy;
                var order = Common.defaultListOrder;
                var objCurDate = DateTime.Today;
                var objPreDate = objCurDate.AddDays(-1);
                string startDate = Common.GetDatePickerDisplay(objPreDate);
                string endDate = Common.GetDatePickerDisplay(objCurDate);
                var totalRecords = 0;
                IEnumerable<Employee> doctorRecords = employeeRepository.Get();
                var lstRecords = objDoctorOperationRepository.Search(cboDoct, searchBy, keyword, startDate, endDate, orderBy, order, out totalRecords);

                objViewsModels = new DoctorOperationModels()
                {
                    lstRecords = lstRecords,
                    doctorRecords = doctorRecords,
                    order = order,
                    orderBy = orderBy,
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

        public ActionResult AjReport(string cboDoct, string startDate, string endDate, string orderBy, string order, string searchBy, string keyword)
        {
            DoctorOperationModels objViewsModels = null;
            try
            {
                orderBy = orderBy.Replace(" ", "") ?? Common.defaultOrderBy;
                order = order ?? Common.defaultListOrder;
             
                var totalRecords = 0;
                var lstRecords = objDoctorOperationRepository.Search(cboDoct, searchBy, keyword, startDate, endDate, orderBy, order, out totalRecords);

                objViewsModels = new DoctorOperationModels()
                {
                    lstRecords = lstRecords,
                    order = order,
                    orderBy = orderBy,
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

        public ActionResult ReportPrint(string cboDoct, string startDate, string endDate, string orderBy, string order, string searchBy, string keyword)
        {
            DoctorOperationModels objViewsModels = null;
            try
            {
                orderBy = orderBy.Replace(" ", "") ?? Common.defaultOrderBy;
                order = order ?? Common.defaultListOrder;

                var totalRecords = 0;
                var lstRecords = objDoctorOperationRepository.Search(cboDoct, searchBy, keyword, startDate, endDate, orderBy, order, out totalRecords);

                objViewsModels = new DoctorOperationModels()
                {
                    lstRecords = lstRecords,
                    order = order,
                    orderBy = orderBy,
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

        public ActionResult ReportCsv(string cboDoct, string startDate, string endDate, string orderBy, string order, string searchBy, string keyword)
        {
            DoctorOperationModels objViewsModels = null;
            try
            {
                orderBy = orderBy.Replace(" ", "") ?? Common.defaultOrderBy;
                order = order ?? Common.defaultListOrder;

                var totalRecords = 0;
                var lstRecords = objDoctorOperationRepository.Search(cboDoct, searchBy, keyword, startDate, endDate, orderBy, order, out totalRecords);

                objViewsModels = new DoctorOperationModels()
                {
                    lstRecords = lstRecords,
                    order = order,
                    orderBy = orderBy,
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

            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("content-disposition", "attachment;filename=doctor-report.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            return View("ReportCsv", "_LayoutCsv", objViewsModels);
        }
    }
}