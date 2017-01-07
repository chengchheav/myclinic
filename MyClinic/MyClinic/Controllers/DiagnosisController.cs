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

namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU", "FUR")]
    public class DiagnosisController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        IPrescriptionRepository objPrescription = new PrescriptionRepository();
        IDiagnosisRepository diagnosisRepository = new DiagnosisRepository();
        IPatientRepository patientRepository = new PatientRepository();
        IEmployeeRepository employeeRepository = new EmployeeRepository();
        IDiseaseRepository diseaseRepository = new DiseaseRepository();
        ILogRepository logRepository = new LogRepository();
        DiagnosisModels diagnosisModel = null;

        public ActionResult Index(string id)
        {
            try
            {
                int Id = 0;
                int.TryParse(id, out Id);
                Id = Id == 0 ? Id : 1;
                if (Id == 0)
                {
                    Session["keyword"] = "";
                }
                string searchBy = "";
                string keyword = "";
                var orderBy = Common.defaultOrderBy;
                var order = Common.defaultListOrder;
                var _pageNo = 1;
                var _pageSize = 10;
                var _pageStatus = 1;
                var totalRecords = 0;
                var dtodiagnosiss = diagnosisRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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

                diagnosisModel = new DiagnosisModels
                {
                    dtodiagnosiss = dtodiagnosiss,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(diagnosisModel);
        }

        public ActionResult Ajindex(int? pageNo, int? pageSize, int? pageStatus, string orderBy, string order, string searchBy, string keyword)
        {
            try
            {
                Session["keyword"] = keyword == "" ? "":keyword;
                var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                string userType = objSession.UserType;
                MyClinic.Infrastructure.User user = new MyClinic.Infrastructure.User();
                var _pageNo = pageNo ?? 1;
                var _pageSize = pageSize ?? Common.defaultPageSize;
                var _pageStatus = pageStatus ?? 1;
                orderBy = orderBy.Replace(" ", "") ?? Common.defaultOrderBy;
                order = order ?? Common.defaultListOrder;
                int totalRecords = 0;
                var dtodiagnosiss = diagnosisRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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

                diagnosisModel = new DiagnosisModels
                {
                    dtodiagnosiss = dtodiagnosiss,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Ajindex", "_LayoutBlank", diagnosisModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            DiagnosisAdd objFrm = new DiagnosisAdd();
            try
            {
                var symptomTypes = diagnosisRepository.GetSymptomType();
                var employeeLists = employeeRepository.Get();
                var strValue = "1990-01-01";
                DateTime objDate;
                DateTime.TryParse(strValue, out objDate);
                objFrm.ExpiredDate = objDate;
                diagnosisModel = new DiagnosisModels
                {
                    diagnosisAdd = objFrm,
                    symptomTypes = symptomTypes,
                    EmployeeLists = employeeLists,
                    checkPost = false,
                    checkError = false
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, Translator.UnexpectedError);
            }
            return View(diagnosisModel);
        }

        [HttpPost]
        public ActionResult Add(DiagnosisAdd objFrm) 
        {

            bool checkError = true;
            Diagnosis diagnosis = new Diagnosis();
            if (objFrm.ExpiredDate.Year < 2001)
                objFrm.ExpiredDate = DateTime.Parse("01-01-2099");
            /* start check  Patient_Name  is correct*/
            var objectDTOPatient = patientRepository.GetDTOPatientById(objFrm.PatientId);
            if (objectDTOPatient != null)
            {
                if (string.IsNullOrEmpty(objFrm.Patient_Name) == false && string.IsNullOrEmpty(objectDTOPatient.Name) == false)
                {
                    var checkPawerName = objectDTOPatient.Id + " - " + objectDTOPatient.Name;
                    if (objFrm.Patient_Name.Trim() != checkPawerName.Trim())
                    {
                        ModelState.AddModelError("Patient_Name", Translator.MsgInvalidPatientName);
                    }
                }
                else
                {
                    ModelState.AddModelError("Patient_Name", Translator.MsgInvalidPatientName);
                }
            }
            else
            {
                ModelState.AddModelError("Patient_Name", Translator.MsgInvalidPatientName);
            }

            var objectDisease = diseaseRepository.GetDiseaseById(objFrm.DiseaseId);
            if (objectDisease != null)
            {
                if (string.IsNullOrEmpty(objFrm.Disease_Name) == false && string.IsNullOrEmpty(objectDisease.Name) == false)
                {
                    if (objFrm.Disease_Name.Trim() != objectDisease.Name.Trim())
                    {
                        ModelState.AddModelError("Disease_Name", Translator.MsgInvalidDiseaseName);
                    }
                }
                else
                {
                    ModelState.AddModelError("Disease_Name", Translator.MsgInvalidDiseaseName);
                }
            }
            else
            {
                ModelState.AddModelError("Disease_Name", Translator.MsgInvalidDiseaseName);
            }
            /*End check Patient_Name,Disease_Name */
            try
            {
                if (ModelState.IsValid)
                {
                    Dictionary<string, string> dictItem = TransactionHelper.processGenerateArray(diagnosis);
                    diagnosis = TransactionHelper.TransDict<Diagnosis>(dictItem, diagnosis);

                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    diagnosis.CreatedBy = objSession.UserId;
                    diagnosis.DiagnoseDate = DateTime.Now;
                    diagnosis.Status = 1;
                    diagnosis.ExpiredDate = objFrm.ExpiredDate;

                    Session["diagnosis"] = diagnosis;
                    /*For Add New Record to LogTable*/
                    int userId = objSession.UserId;
                    logTran.UserId = userId;
                    logTran.ProcessType = "Add Diagnosis";
                    logTran.Description = "Add Diagnosis";
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    checkError = false;
                    
                    Session["patient"] = null;
                    return RedirectToAction("UploadPhoto", "Diagnosis");
                }

            }
            catch (Exception ex) {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, Translator.UnexpectedError);
            }

            if (checkError == true)
            {
                var symptomTypes = diagnosisRepository.GetSymptomType();
                var employeeLists = employeeRepository.Get();
                diagnosisModel = new DiagnosisModels
                {
                    diagnosisAdd = objFrm,
                    dtopatient = objectDTOPatient,
                    symptomTypes = symptomTypes,
                    EmployeeLists = employeeLists,
                    checkPost = true,
                    checkError = false
                };
            }

            return View(diagnosisModel);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            DiagnosisEdit objFrm = new DiagnosisEdit();
            var intId = 0;
            int.TryParse(id, out intId);
            try 
            {
                DTODiagnosis dtodiagnosis = diagnosisRepository.GetDTODiagnosisById(intId);
                if (dtodiagnosis == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                else
                {
                    var symptomTypes = diagnosisRepository.GetSymptomType();
                    var employeeLists = employeeRepository.Get();
                    diagnosisModel = new DiagnosisModels
                    {
                        dtodiagnosis = dtodiagnosis,
                        diagnosisEdit = objFrm,
                        symptomTypes = symptomTypes,
                        EmployeeLists = employeeLists,
                        checkPost = false
                    };
                }
            }
            catch(Exception ex){
                log.Error(ex);
                ModelState.AddModelError("", Translator.UnexpectedError);
            }
            return View(diagnosisModel);
        }

        [HttpPost]
        public ActionResult Edit(DiagnosisEdit objFrm)
        {
            bool checkError = true;
            /* start check  Disease_Name  is correct*/
            var objectDisease = diseaseRepository.GetDiseaseById(objFrm.DiseaseId);
            if (objectDisease != null)
            {
                if (string.IsNullOrEmpty(objFrm.Disease_Name) == false && string.IsNullOrEmpty(objectDisease.Name) == false)
                {
                    if (objFrm.Disease_Name.Trim() != objectDisease.Name.Trim())
                    {
                        ModelState.AddModelError("Disease_Name", Translator.MsgInvalidDiseaseName);
                    }
                }
                else
                {
                    ModelState.AddModelError("Disease_Name", Translator.MsgInvalidDiseaseName);
                }
            }
            else
            {
                ModelState.AddModelError("Disease_Name", Translator.MsgInvalidDiseaseName);
            }
            /*End check Disease_Name */
            try
            {
                Diagnosis originDiagnosis = diagnosisRepository.GetDiagnosisById(objFrm.Id);
                if (ModelState.IsValid)
                {
                    Diagnosis newDiagnosis = Utilities.ObjectCopier.Copy<Diagnosis>(originDiagnosis);
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;

                    //newDiagnosis.Status = objFrm.Status;
                    newDiagnosis.DiagnoseCycle = objFrm.DiagnoseCycle;
                    newDiagnosis.ExpiredDate = objFrm.ExpiredDate;
                    newDiagnosis.Symptom = objFrm.Symptom;
                    newDiagnosis.SymptomType = objFrm.SymptomType;
                    newDiagnosis.DiseaseId = objFrm.DiseaseId;
                    newDiagnosis.DiagnoseBy = objFrm.DiagnoseBy;
                    string diffString = originDiagnosis.EnumeratePropertyDifferencesInString(newDiagnosis);
                    diagnosisRepository.UpdateFieldChangedOnly(originDiagnosis, newDiagnosis);

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
                    DTODiagnosis dtodiagnosis = diagnosisRepository.GetDTODiagnosisById(objFrm.Id);

                    dtodiagnosis.Status = objFrm.Status;
                    dtodiagnosis.DiagnoseCycle = objFrm.DiagnoseCycle;
                    dtodiagnosis.ExpiredDate = objFrm.ExpiredDate;
                    dtodiagnosis.Symptom = objFrm.Symptom;

                    var symptomTypes = diagnosisRepository.GetSymptomType();
                    var employeeLists = employeeRepository.Get();
                    diagnosisModel = new DiagnosisModels
                    {
                        symptomTypes = symptomTypes,
                        dtodiagnosis = dtodiagnosis,
                        diagnosisEdit = objFrm,
                        EmployeeLists = employeeLists,
                        checkPost = true
                    };
                    return View(diagnosisModel);
                }
                else
                {
                    return RedirectToAction("Index", "Diagnosis");
                }
            }
            catch(Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("", Translator.UnexpectedError);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            try
            {
                DTODiagnosis dtodiagnosis = diagnosisRepository.GetDTODiagnosisById(intId);

                IEnumerable<vMedicineDiagnosis> medicineRecords = objPrescription.GetPrescriptionDiagnosis(intId);
                if (dtodiagnosis == null)
                {
                    return RedirectToAction("Error404", "Error");
                }

                diagnosisModel = new DiagnosisModels
                {
                    dtodiagnosis = dtodiagnosis,
                    medicineRecords = medicineRecords
                };

            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(diagnosisModel);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            string resultTrans = "success";
            try
            {
                var prescriptionList = diagnosisRepository.GetPrescriptionByDiagnosisId(id);
                if (prescriptionList.Count() == 0)
                {
                    DTODiagnosis dtodiagnosis = diagnosisRepository.GetDTODiagnosisById(id);

                    Diagnosis originDiagnosis = diagnosisRepository.GetDiagnosisById(id);
                    var newDiagnosis = ObjectCopier.Copy<Diagnosis>(originDiagnosis);

                    newDiagnosis.Status = 0;//For Delete
                    diagnosisRepository.UpdateFieldChangedOnly(originDiagnosis, newDiagnosis);

                    /*For Add New Record to LogTable*/
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    int userId = objSession.UserId;

                    logTran.UserId = userId;
                    logTran.ProcessType = "Delete Diagnosis";
                    logTran.Description = "Delete Diagnosis, Patient Name :" + dtodiagnosis.Patient_Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                }
                else
                {
                    resultTrans = "failure";
                    return Json(new { result = resultTrans, proccessType = "Delete", Id = id, strName = Translator.ThisDiagnosis, strUsed = Translator.MsgPrescription });
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
        public ActionResult GetDiagnosisCycle()
        {
            int diagnosisCycle = 1;
            try
            {
                int PatientId = int.Parse(Request.Form["id"]);
                diagnosisCycle = diagnosisCycle + diagnosisRepository.GetDiagnosisCycle(PatientId);

            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return Json(new { result = diagnosisCycle});
        }

        [HttpGet]
        public ActionResult UploadPhoto()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadPhotoViewer(int id)
        {
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            string result = "";
            int diagnosisId = id;
            ViewData["id"] = diagnosisId;
            var index = 0;
            string destinationPath = Server.MapPath("~") + "Uploads\\Diagnosis\\" + diagnosisId;

            DirectoryInfo destinationInfo = new DirectoryInfo(destinationPath);
            if (destinationInfo.Exists)
            {
                foreach (FileInfo fileInfo in destinationInfo.GetFiles())
                {
                    index++;
                    var link = baseUrl + "Uploads/Diagnosis/" + diagnosisId + "/" + fileInfo.Name;
                    result += "<div id=\"imgPhoto\" class=\"bx-pt\" style=\"margin:20px;\"><span onclick=\"$image.confirmDeleteImage('','','" + Translator.MsgDeleteImage + "',0,'" + link + "');\"></span><a href='" + link + "'><img src=\"" + link + "\" style=\"width:100%;height:100%;object-fit:cover;\" /></a></div>";
                    
                }
            }

            ViewData["imgListBox"] = result;

            return View();
        }
    }
}
