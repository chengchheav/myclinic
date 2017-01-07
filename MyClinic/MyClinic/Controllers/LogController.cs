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
    [CustomAuthorizeAttribute("ADM","SPU")]
    public class LogController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ILogRepository logRepository = new LogRepository();
        LogModels logModels          = new LogModels();
        IUserRepository userRepository = new UserRepository();
        IAuditLogRepository auditLogRepository = new AuditLogRepository();
        public ActionResult Index()
        {
            try
            {
               MyClinic.Infrastructure.Log log = new MyClinic.Infrastructure.Log();
                string searchBy = "";
                string keyword = "";
                var objCurDate = DateTime.Today;
                var objPreDate = objCurDate.AddDays(-1);
                string startDate = Common.GetDatePickerDisplay(objPreDate);
                string endDate = Common.GetDatePickerDisplay(objCurDate);
                var orderBy = Common.defaultOrderBy;
                var order = Common.defaultListOrder;
                var _pageNo = 1;
                var _pageSize = 10;
                var _pageStatus = 1;
                var totalRecords = 0;
                var logs = logRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
                var listResult = Paging.GetResultInfo(totalRecords, _pageNo, _pageSize);
                var paging = Paging.GetPaging(totalRecords, _pageNo, _pageSize, _pageStatus, Common.defaultNoOfPageLinkList, "$managelog.pagingManageRecords",orderBy,order);
                var itemPerPage = Paging.getItemPerPage(totalRecords, _pageSize, orderBy, order, "$managelog.itemPerPageChangeLogByDate");

                var lstProcessTypes = auditLogRepository.GetProcessType();
                var lstUsers = userRepository.Get();
                string cboPro = "";
                PageUtilities pageUtilities = new PageUtilities()
                {
                    listHeader = listResult,
                    listFooter = paging + itemPerPage,
                    order = order,
                    orderBy = orderBy,
                };
                logModels = new LogModels
                {
                    logRecords = logs,
                    pageUtilities = pageUtilities,
                    lstProcessTypes = lstProcessTypes,
                    lstUsers = lstUsers,
                    //startDate = startDate,
                    //endDate = endDate,
                    cboPro = cboPro,
                };
            }
            catch (Exception ex) {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(logModels);
        }

        public ActionResult AjindexSearchByDate(string orderBy, string order, string startDate, string endDate, string cboUser, string cboPro, int? pageNo, int? pageSize, int? pageStatus)
        {
            try
            {
                var _pageNo = pageNo ?? 1;
                var _pageSize = pageSize ?? Common.defaultPageSize;
                var _pageStatus = pageStatus ?? 1;
                MyClinic.Infrastructure.Log log = new MyClinic.Infrastructure.Log();
                orderBy = orderBy.Replace(" ", "") ?? Common.defaultOrderBy;
                order = order ?? Common.defaultListOrder;
                int totalRecords = 0;
                var lstRecords = logRepository.SearchByDate(startDate, endDate, cboUser, cboPro, orderBy, order, out totalRecords,_pageNo,_pageSize);
                var listResult = Paging.GetResultInfo(totalRecords, _pageNo, _pageSize);
                var paging = Paging.GetPaging(totalRecords, _pageNo, _pageSize, _pageStatus, Common.defaultNoOfPageLinkList, "$managelog.pagingManageRecords", orderBy, order);
                var itemPerPage = Paging.getItemPerPage(totalRecords, _pageSize, orderBy, order, "$managelog.itemPerPageChangeLogByDate");
                PageUtilities pageUtilities = new PageUtilities()
                {
                    listHeader = listResult,
                    listFooter = paging + itemPerPage,
                    order = order,
                    orderBy = orderBy
                };
                logModels = new LogModels
                {
                    logRecords = lstRecords,
                    order = order,
                    orderBy = orderBy,
                    totalRecords = totalRecords,
                    startDate = startDate,
                    endDate = endDate,
                    cboPro = cboPro,
                    cboUser = cboUser,
                    pageUtilities = pageUtilities,

                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("AjindexSearchByDate", "_LayoutBlank", logModels);
        }

    }
}
