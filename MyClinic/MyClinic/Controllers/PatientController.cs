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
using Newtonsoft.Json.Linq;

namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU", "FUR")]
    public class PatientController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        IPatientRepository patientRepository = new PatientRepository();
        ILogRepository logRepository = new LogRepository();
        IDiagnosisRepository diagnosisReposityory = new DiagnosisRepository();
        PatientModels patientModel = null;
        public ActionResult Index()
        {
            try{
                string searchBy = "";
                string keyword = "";
                var orderBy = Common.defaultOrderBy;
                var order = Common.defaultListOrder;
                var _pageNo = 1;
                var _pageSize = 10;
                var _pageStatus = 1;
                var totalRecords = 0;
                var patients = patientRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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

                patientModel = new PatientModels
                {
                    patients = patients,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(patientModel);
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
                var patients = patientRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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

                patientModel = new PatientModels
                {
                    patients = patients,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Ajindex", "_LayoutBlank", patientModel);
        }

        [HttpGet]
        public ActionResult Add( string id){
            var patientAdd = new PatientAdd();
            var savePath = Server.MapPath("~/Uploads/");
            var fileName = "default.jpg";
            try
            {
                patientAdd.ImageStream = ImageHelper.BaseDateImage(savePath + "/" + fileName);
                var strValue = "1990-01-01";
                DateTime objDate;
                DateTime.TryParse(strValue, out objDate);
                patientAdd.Dob = objDate;
                patientModel = new PatientModels
                {
                    patientAdd = patientAdd,
                    checkPost = false
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, Translator.UnexpectedError);
            }
            ViewData["referrerId"] = id;
            return View(patientModel);
        }

        [HttpPost]
        public ActionResult Add(MyClinic.Infrastructure.PatientAdd patientAdd)
        {
            bool checkError = true;
            try
            {
                if (ModelState.IsValid)
                {
                    Patient patient = new Patient();
                    Dictionary<string, string> dictItem = TransactionHelper.processGenerateArray(patient);
                    patient = TransactionHelper.TransDict<Patient>(dictItem, patient);
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    patient.Image = patient.Id + "jpg";
                    patient.Status = 1;
                    patient.Age = 0;
                    patientRepository.Add(patient);

                    Session["patient"] = patient;
                    var fileName = patient.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/patient/") + patient.Id;
                    ImageHelper.SaveImage(savePath, patient.Id + ".jpg", patientAdd.ImageStream);
                    Patient newPatient = Utilities.ObjectCopier.Copy<Patient>(patient);
                    newPatient.Image = patient.Id + ".jpg";
                    patientRepository.UpdateFieldChangedOnly(patient, newPatient);

                    /*For Add New Record to LogTable*/
                    int userId = objSession.UserId;
                    logTran.UserId = userId;
                    logTran.ProcessType = "Add User";
                    logTran.Description = "Add User Name : " + patient.Name;
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
                patientAdd.Dob = patientAdd.DateOfBirth;
                patientModel = new PatientModels
                {
                    patientAdd = patientAdd,
                    checkPost = true
                };
                return View(patientModel);
            }
            else
            {
                var referrerId = Request.Form["referrerId"];
                if (referrerId == "1")/*Add Patient from patient Controller*/
                {
                    //Session["patient"] = null;
                    return RedirectToAction("Index", "Patient");
                }
                else/*Add Patient from patient Diagnosis*/
                {
                    return RedirectToAction("Add", "Diagnosis");
                }
            }

        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            PatientEdit patientEdit = new PatientEdit();
            var intId = 0;
            int.TryParse(id, out intId);
            try
            {
                Patient patient = patientRepository.GetPatientById(intId);
                if (patient == null )
                {
                    return RedirectToAction("Error404", "Error");
                }
                else
                {
                    var path = "";
                    var fileName = patient.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/patient/") + patient.Id;
                    // Check for Directory, If not exist, then create it 
                    if (Directory.Exists(savePath))
                    {
                        path = "/Uploads/patient/" + patient.Id + "/" + fileName;
                        patient.Image = path;
                    }
                    else
                    {
                        savePath = Server.MapPath("~/Uploads/");
                        fileName = "default.jpg";
                    }
                    patientEdit.ImageStream = ImageHelper.BaseDateImage(savePath + "/" + fileName);
                    patient.Image = Common.ConcatHost(path);
                }

                patientModel = new PatientModels
                {
                    patient = patient,
                    patientEdit = patientEdit,
                    checkPost = false
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(patientModel);
        }

        [HttpPost]
        public ActionResult Edit(MyClinic.Infrastructure.PatientEdit patientEdit)
        {
            bool checkError = true;
            try
            {
                Patient originPatient = patientRepository.GetPatientById(patientEdit.Id);
                if (ModelState.IsValid)
                {
                    Patient newPatient = Utilities.ObjectCopier.Copy<Patient>(originPatient);
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;

                    var savePathImage = Server.MapPath("~/Uploads/patient/") + originPatient.Id;
                    ImageHelper.SaveImage(savePathImage, originPatient.Id + ".jpg", patientEdit.ImageStream);
                    patientEdit.Age = 0;
                    newPatient.Name = patientEdit.Name;
                    newPatient.Email = patientEdit.Email;
                    newPatient.Sex = patientEdit.Sex;
                    newPatient.Tel = patientEdit.Tel;
                    newPatient.Dob = patientEdit.Dob;
                    newPatient.Image = newPatient.Id + ".jpg";

                    string diffString = originPatient.EnumeratePropertyDifferencesInString(newPatient);
                    patientRepository.UpdateFieldChangedOnly(originPatient, newPatient);

                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Edit User";
                    logTran.Description = "Edit User value as follow: " + diffString;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    checkError = false;
                }
                if (checkError == true)
                {
                    patientModel = new PatientModels
                    {
                        patient = originPatient,
                        patientEdit = patientEdit,
                        checkError = checkError,
                        checkPost = true
                    };
                    return View(patientModel);
                }
                else
                {
                    return RedirectToAction("Index", "Patient");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, Translator.UnexpectedError);
            }
            return View(patientModel);
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            IEnumerable<DTODiagnosis> listDiagnosis = null;
            try
            {
                IDiagnosisRepository diagnosisRepository = new DiagnosisRepository();
                Patient patient = patientRepository.GetPatientById(intId);
                if (patient == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                else
                {
                    listDiagnosis = diagnosisRepository.GetListDiagnosisByPatientId(intId);
                    var path = "/Uploads/default.jpg";
                    var fileName = patient.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/Patient/") + patient.Id;
                    // Check for Directory, If exist 
                    if (Directory.Exists(savePath))
                    {
                        path = "/Uploads/Patient/" + patient.Id + "/" + fileName;
                    }
                    patient.Image = Common.ConcatHost(path);

                    patientModel = new PatientModels
                    {
                        patient = patient,
                        listDiagnosis = listDiagnosis
                    };
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(patientModel);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            string resultTrans = "success";
            try
            {
                var diagnosisList = diagnosisReposityory.GetDiagnosisByPatientId(id);
                if (diagnosisList.Count() == 0)
                {
                    Patient originPatient = patientRepository.GetPatientById(id);
                    var newPatient = ObjectCopier.Copy<Patient>(originPatient);

                    newPatient.Status = 0;//For Delete
                    patientRepository.UpdateFieldChangedOnly(originPatient, newPatient);

                    /*For Add New Record to LogTable*/
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    int userId = objSession.UserId;

                    logTran.UserId = userId;
                    logTran.ProcessType = "Delete Patient";
                    logTran.Description = "Delete Patient Name :" + originPatient.Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                }
                else
                {
                    resultTrans = "failure";
                    return Json(new { result = resultTrans, proccessType = "Delete", Id = id, strName = Translator.ThisPatient, strUsed = Translator.MsgDiagnosis });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return Json(new { result = resultTrans, proccessType = "Delete", Id = id });
        }

        [HttpPost]
        public string GetPatientName()
        {
            string name = Request.Form["phrase"].ToString();
            IEnumerable<DTOPatient> objNames = null;
            try{
                name = name == null ? "" : name;
                objNames = patientRepository.GetPatientListByName(name);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            string objJson = JsonConvert.SerializeObject(objNames);
            return objJson;
        }
        [HttpPost]
        public string GetDateOfBirth()
        {
            var age = 0;
            var ageType = Request.Form["ageType"] == null ? string.Empty:Request.Form["ageType"];
            int.TryParse(Request.Form["strAge"], out age);
            var nativeAge = "-" + age;
            var Age = 0;
            int.TryParse(nativeAge, out Age);
            Patient patient = new Patient();
            if (ageType.Contains("y"))
            {
                patient.Dob = DateTime.Now.AddYears(Age);
            }
            else if (ageType.Contains("m"))
            {
                patient.Dob = DateTime.Now.AddMonths(Age);
            }
            else
            {
                patient.Dob = DateTime.Now.AddDays(Age);
            }
            string objJson = JsonConvert.SerializeObject(patient);
            return objJson;
        }

        [HttpPost]
        public string GetAge()
        {
            DateTime today = DateTime.Now;
            var dob = DateTime.Now;
            DateTime.TryParse(Request.Form["dob"], out dob);
            int Age = 0;
            var ageType = "";
            var dateDiff = today - dob;

            if (dateDiff.TotalDays < 30)
            {
                Age =(int) dateDiff.TotalDays;
                ageType = "d";
            }
            else if (dateDiff.TotalDays >= 30 && dateDiff.TotalDays < 365)
            {
                Age = (int)dateDiff.TotalDays / 30;                
                ageType = "m";
            }
            else
            {
                Age = (int)dateDiff.TotalDays / 365;                
                ageType = "y";
            }            

            Patient patient = new Patient();
            patient.Age = Age;
            JObject json = JObject.FromObject(patient);
            json.Add("ageType", ageType);
            string objJson = json.ToString();//JsonConvert.SerializeObject(patient);
            return objJson;
        }
        
    }
}
