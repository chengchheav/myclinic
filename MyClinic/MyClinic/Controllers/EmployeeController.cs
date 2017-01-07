using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClinic.Core;
using MyClinic.Models;
using MyClinic.Infrastructure;
using System.Web.UI.WebControls;
using Utilities;
using MyClinic.MVC.Extension;
using MyClinic.Resources;
using System.IO;
using log4net;
using BoatTEST.Classified.Core.Utilities;
using Newtonsoft.Json;

namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU")]
    public class EmployeeController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        IEmployeeRepository employeeRepository = new EmployeeRepository();
        ILogRepository logRepository = new LogRepository();
        EmployeeModels employeeModel = null;

        public ActionResult Index()
        {
            try
            {
                string searchBy = "";
                string keyword = "";
                var orderBy = Common.defaultOrderBy;
                var order = Common.defaultListOrder;
                var _pageNo = 1;
                var _pageSize = 10;
                var _pageStatus = 1;
                var totalRecords = 0;
                var dtoemployees = employeeRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
                var listResult = Paging.GetResultInfo(totalRecords, _pageNo, _pageSize);
                var paging = Paging.GetPaging(totalRecords, _pageNo, _pageSize, _pageStatus, Common.defaultNoOfPageLinkList, "$common.pagingClick", orderBy, order);
                var itemPerPage = Paging.getItemPerPage(totalRecords, _pageSize, orderBy, order);

                PageUtilities pageUtilities = new PageUtilities()
                {
                    listHeader = listResult,
                    listFooter = paging + itemPerPage,
                    order = order,
                    orderBy = orderBy
                };

                employeeModel = new EmployeeModels
                {
                    dtoemployees = dtoemployees,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(employeeModel);
        }

        public ActionResult Ajindex(int? pageNo, int? pageSize, int? pageStatus, string orderBy, string order, string searchBy, string keyword)
        {
            try
            {
                var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                string userType = objSession.UserType;
                MyClinic.Infrastructure.User user = new MyClinic.Infrastructure.User();
                var _pageNo = pageNo ?? 1;
                var _pageSize = pageSize ?? Common.defaultPageSize;
                var _pageStatus = pageStatus ?? 1;
                orderBy = orderBy.Replace(" ", "") ?? Common.defaultOrderBy;
                order = order ?? Common.defaultListOrder;
                int totalRecords = 0;
                var dtoemployees = employeeRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
                var listResult = Paging.GetResultInfo(totalRecords, _pageNo, _pageSize);
                var paging = Paging.GetPaging(totalRecords, _pageNo, _pageSize, _pageStatus, Common.defaultNoOfPageLinkList, "$common.pagingClick", orderBy, order);
                var itemPerPage = Paging.getItemPerPage(totalRecords, _pageSize, orderBy, order);

                PageUtilities pageUtilities = new PageUtilities()
                {
                    listHeader = listResult,
                    listFooter = paging + itemPerPage,
                    order = order,
                    orderBy = orderBy
                };

                employeeModel = new EmployeeModels
                {
                    dtoemployees = dtoemployees,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Ajindex", "_LayoutBlank", employeeModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                var objPosition = employeeRepository.GetPosition();
                EmployeeAdd employeeAdd = new EmployeeAdd();

                var savePath = Server.MapPath("~/Uploads/");
                var fileName = "default.jpg";
                employeeAdd.ImageStream = ImageHelper.BaseDateImage(savePath + "/" + fileName);

                var strValue = "1990-01-01";
                DateTime objDate;
                DateTime.TryParse(strValue, out objDate);
                employeeAdd.Dob = objDate;
                employeeModel = new EmployeeModels
                {
                    employeeAdd = employeeAdd,
                    checkPost = false,
                    positions = objPosition
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, Translator.UnexpectedError);
            }
            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult Add(MyClinic.Infrastructure.EmployeeAdd objFrm)
        {
            bool checkError = true;
            try
            {
                if (ModelState.IsValid)
                {
                    Employee employee = new Employee();
                    Dictionary<string, string> dictItem = TransactionHelper.processGenerateArray(employee);
                    employee = TransactionHelper.TransDict<Employee>(dictItem, employee);
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    employee.Image = employee.Id + "jpg";
                    employee.Status = 1;

                    employeeRepository.Add(employee);

                    var fileName = employee.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/employee/") + employee.Id;
                    ImageHelper.SaveImage(savePath, employee.Id + ".jpg", objFrm.ImageStream);
                    Employee newEmployee = Utilities.ObjectCopier.Copy<Employee>(employee);
                    newEmployee.Image = employee.Id + ".jpg";
                    employeeRepository.UpdateFieldChangedOnly(employee, newEmployee);

                    /*For Add New Record to LogTable*/
                    int userId = objSession.UserId;
                    logTran.UserId = userId;
                    logTran.ProcessType = "Add Employee";
                    logTran.Description = "Add Employee Name : " + employee.Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    checkError = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, Translator.UnexpectedError);
            }

            if (checkError == true)
            {
                var objPosition = employeeRepository.GetPosition();
                //objFrm.Dob = objFrm.DateOfBirth;
                employeeModel = new EmployeeModels
                {
                    employeeAdd = objFrm,
                    checkPost = true,
                    positions = objPosition
                };
                return View(employeeModel);
            }
            else
            {
                return RedirectToAction("Index", "Employee");
            }

        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            EmployeeEdit employeeEdit = new EmployeeEdit();
            var intId = 0;
            int.TryParse(id, out intId);
            try
            {
                Employee employee = employeeRepository.GetEmployeeById(intId);
                if (employee == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                else
                {
                    var path = "";
                    var fileName = employee.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/employee/") + employee.Id;
                    // Check for Directory, If not exist, then create it 
                    if (Directory.Exists(savePath))
                    {
                        path = "/Uploads/employee/" + employee.Id + "/" + fileName;
                        employee.Image = path;
                    }
                    else
                    {
                        savePath = Server.MapPath("~/Uploads/");
                        fileName = "default.jpg";
                    }
                    employeeEdit.ImageStream = ImageHelper.BaseDateImage(savePath + "/" + fileName);
                    employee.Image = Common.ConcatHost(path);
                }
                var objPosition = employeeRepository.GetPosition();
                employeeModel = new EmployeeModels
                {
                    employee = employee,
                    positions = objPosition,
                    employeeEdit = employeeEdit,
                    checkPost = false
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult Edit(MyClinic.Infrastructure.EmployeeEdit employeeEdit)
        {
            bool checkError = true;
            try
            {
                Employee originEmployee = employeeRepository.GetEmployeeById(employeeEdit.Id);
                if (ModelState.IsValid)
                {
                    Employee newEmployee = new Employee();
                    Dictionary<string, string> dictItem = TransactionHelper.processGenerateArray(newEmployee);
                    newEmployee = TransactionHelper.TransDict<Employee>(dictItem, newEmployee);

                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;

                    var savePathImage = Server.MapPath("~/Uploads/employee/") + originEmployee.Id;
                    ImageHelper.SaveImage(savePathImage, originEmployee.Id + ".jpg", employeeEdit.ImageStream);

                    string diffString = originEmployee.EnumeratePropertyDifferencesInString(newEmployee);
                    newEmployee.Image = newEmployee.Id + ".jpg";
                    employeeRepository.UpdateFieldChangedOnly(originEmployee, newEmployee);

                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Edit Employee";
                    logTran.Description = "Edit Employee value as follow: " + diffString;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    checkError = false;
                }
                if (checkError == true)
                {
                    var objPosition = employeeRepository.GetPosition();
                    employeeModel = new EmployeeModels
                    {
                        employee = originEmployee,
                        employeeEdit = employeeEdit,
                        checkError = checkError,
                        positions = objPosition,
                        checkPost = true
                    };
                    return View(employeeModel);
                }
                else
                {
                    return RedirectToAction("Index", "Employee");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, Translator.UnexpectedError);
            }
            return View(employeeModel);
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            try
            {
                DTOEmployee dtoemployee = employeeRepository.GetDTOEmployeeById(intId);
                if (dtoemployee == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                else
                {
                    var path = "/Uploads/default.jpg";
                    var fileName = dtoemployee.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/employee/") + dtoemployee.Id;
                    // Check for Directory, If exist 
                    if (Directory.Exists(savePath))
                    {
                        path = "/Uploads/employee/" + dtoemployee.Id + "/" + fileName;
                    }
                    dtoemployee.Image = Common.ConcatHost(path);
                }
                employeeModel = new EmployeeModels
                {
                    dtoemployee = dtoemployee,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(employeeModel);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            string resultTrans = "success";
            try
            {
                var diagnosisList = employeeRepository.GetDiagnosisByEmployeeId(id);
                if (diagnosisList.Count() == 0)
                {
                    Employee originEmployee = employeeRepository.GetEmployeeById(id);
                    var newEmployee = ObjectCopier.Copy<Employee>(originEmployee);

                    newEmployee.Status = 0;//For Delete
                    employeeRepository.UpdateFieldChangedOnly(originEmployee, newEmployee);

                    /*For Add New Record to LogTable*/
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    int userId = objSession.UserId;

                    logTran.UserId = userId;
                    logTran.ProcessType = "Delete Employee";
                    logTran.Description = "Delete Employee Name :" + originEmployee.Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                }
                else
                {
                    resultTrans = "failure";
                    return Json(new { result = resultTrans, proccessType = "Delete", Id = id, strName = Translator.ThisEmployee, strUsed = Translator.MsgDiagnosis });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                resultTrans = "failure";
            }
            return Json(new { result = resultTrans, proccessType = "Delete", Id = id });
        }

        [HttpPost]
        public string GetEmployeeName()
        {
            string name = Request.Form["phrase"].ToString();
            IEnumerable<Employee> objNames = null;
            try
            {
                name = name == null ? "" : name;
                objNames = employeeRepository.GetEmployeeListByName(name);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            string objJson = JsonConvert.SerializeObject(objNames);
            return objJson;
        }
    }
}
