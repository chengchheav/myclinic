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
using MyClinic.Resources;
using BoatTEST.Classified.Core.Utilities;
namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU")]
    public class PrescriptionController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        ILogRepository logRepository = new LogRepository();
        IPatientRepository patientRepository = new PatientRepository();
        IPrescriptionRepository objPrescription = new PrescriptionRepository();
        IDiseaseRepository diseaseRepository = new DiseaseRepository();
        
        /*For set the value of user login info*/
        SessUser objProfile = null;

        public void GetUserProfile()
        {
            this.objProfile = Session["user"] as MyClinic.Infrastructure.SessUser;
        }

        public ActionResult Index()
        {
            PrescriptionModels objViewsModels = null;
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
                var records = objPrescription.VSearch(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
                var listResult = Paging.GetResultInfo(totalRecords, _pageNo, _pageSize);
                var paging = Paging.GetPaging(totalRecords, _pageNo, _pageSize, _pageStatus, Common.defaultNoOfPageLinkList, "$common.pagingClick", orderBy, order);
                var itemPerPage = Paging.getItemPerPage(totalRecords, _pageSize,orderBy,order);

                PageUtilities pageUtilities = new PageUtilities()
                {
                    listHeader = listResult,
                    listFooter = paging + itemPerPage,
                    order = order,
                    orderBy = orderBy
                };
                objViewsModels = new PrescriptionModels()
                {
                    vRecords = records,
                    pageUtilities = pageUtilities
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(objViewsModels);
        }

        public ActionResult Ajindex(int? pageNo, int? pageSize, int? pageStatus, string orderBy, string order, string searchBy, string keyword)
        {
            PrescriptionModels objViewsModels = null;
            try
            {
                var _pageNo = pageNo ?? 1;
                var _pageSize = pageSize ?? Common.defaultPageSize;
                var _pageStatus = pageStatus ?? 1;
                orderBy = orderBy.Replace(" ", "") ?? Common.defaultOrderBy;
                order = order ?? Common.defaultListOrder;
                var totalRecords = 0;
                var records = objPrescription.VSearch(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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
                objViewsModels = new PrescriptionModels()
                {
                    vRecords = records,
                    pageUtilities = pageUtilities
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Ajindex", "_LayoutBlank", objViewsModels);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var objSessionDiagnosis = Session["diagnosis"] as MyClinic.Infrastructure.Diagnosis;
            if (objSessionDiagnosis == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Patient patient = patientRepository.GetPatientById(objSessionDiagnosis.PatientId);
            Disease disease = diseaseRepository.GetDiseaseById(objSessionDiagnosis.DiseaseId);
            var itemAdd = new PrescriptionAdd();
            IDiagnosisRepository diagnosisRepository = new DiagnosisRepository();
            IMedicineTypeRepository objMedicineType = new MedicineTypeRepository();

            //IEnumerable<MedicineType> medicineTypeRecords = null;
            IMedicineTypeRepository medicineTypeRepository = new MedicineTypeRepository();
            var usageSelectBox = medicineTypeRepository.GetUsageSelectBox();
            ViewData["usageSelectBox"] = usageSelectBox;


            PrescriptionModels viewModel = null;
            try
            {
                IEnumerable<MedicineType> medicineTypeRecords = objMedicineType.Get();
                viewModel = new PrescriptionModels
                {
                    validAdd = itemAdd,
                    checkPost = false,
                    patient = patient,
                    disease = disease,
                    medicineTypeRecords = medicineTypeRecords
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Add(MyClinic.Infrastructure.PrescriptionAdd itemAdd)
        {

            Prescription itemGet = new Prescription();
            var objSessionDiagnosis = Session["diagnosis"] as MyClinic.Infrastructure.Diagnosis;

            this.GetUserProfile();
            bool checkError = true;
            PrescriptionModels viewModel = null;
            DTODiagnosis dtoDiagnosis = null;
            IEnumerable<MedicineType> medicineTypeRecords = null;
            try
            {
                IMedicineTypeRepository objMedicineType = new MedicineTypeRepository();
                IDiagnosisRepository diagnosisRepository = new DiagnosisRepository();
                dtoDiagnosis = diagnosisRepository.GetDTODiagnosisById(itemAdd.DiagnosisId);

                /*For Add Diagnosis to Table */
                diagnosisRepository.Add(objSessionDiagnosis);
                itemGet.DiagnosisId = objSessionDiagnosis.Id;
                //Save Image
                SaveUploadImge(Session.SessionID,objSessionDiagnosis.Id);
                if (ModelState.IsValid)
                {
                    var intTotal = 0;
                    if (itemAdd.MedicineId != null)
                    {
                        intTotal = itemAdd.MedicineId.Count();
                    }
                    for (int intIndex = 0; intIndex < intTotal; intIndex++) {
                        int MedicineId = 0;
                        int.TryParse(itemAdd.MedicineId[intIndex], out MedicineId);
                        itemGet.MedicineId  = MedicineId;
                        itemGet.Qty         = itemAdd.Qty[intIndex];
                        //itemGet.MedicineType       = itemAdd.MedicineType[intIndex];
                        itemGet.Morning     = itemAdd.Morning[intIndex];
                        itemGet.Noon        = itemAdd.Noon[intIndex];
                        itemGet.Night       = itemAdd.Night[intIndex];
                        itemGet.Remark      = itemAdd.Remark[intIndex];
                        itemGet.UsageId     = int.Parse(itemAdd.UsageId[intIndex]);
                        itemGet.Status      = (int)MyClinic.Core.Enums.RecordStatus.Active;

                        objPrescription.Add(itemGet);
                    }

                    Session["diagnosis"] = null;
                    /*For Add New Record to LogTable*/
                    logTran.UserId = this.objProfile.UserId;
                    logTran.ProcessType = "Add Prescription";
                    logTran.Description = "Add Prescription";
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    /*this.GetUserProfile();
                    var test = this.objProfile;*/
                    return RedirectToAction("Detail", "Prescription", new { id = itemGet.DiagnosisId });
                    //checkError = false;
                }
                medicineTypeRecords = objMedicineType.Get();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            if (checkError == true)
            {
                viewModel = new PrescriptionModels
                {
                    validAdd = itemAdd,
                    checkPost = true,
                    checkError = checkError,
                    dtoDiagnosis = dtoDiagnosis,
                    medicineTypeRecords = medicineTypeRecords
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        private void SaveUploadImge(string sessionId,int diagnosisId)
        {
            try
            {
                //string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                
                string destinationPath = Server.MapPath("~") + "Uploads\\Diagnosis\\" + diagnosisId;

                DirectoryInfo destinationInfo = new DirectoryInfo(destinationPath);
                if(!destinationInfo.Exists)
                    destinationInfo.Create();

                string directoryPath = Server.MapPath("~") + "Uploads\\Temp\\" + sessionId + "\\";
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                if (directoryInfo.Exists)
                {
                    foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                    {
                        if (fileInfo.Exists)
                        {
                            fileInfo.MoveTo(destinationPath + "\\" + fileInfo.Name);
                        }
                    }
                    //directoryInfo.Delete();
                }   
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            var itemAdd = new PrescriptionAdd();
            IDiagnosisRepository diagnosisRepository = new DiagnosisRepository();
            IMedicineTypeRepository objMedicineType = new MedicineTypeRepository();
            PrescriptionModels viewModel = null;
            try
            {
                int intId = 0;
                int.TryParse(id, out intId);
                IEnumerable<MedicineType> medicineTypeRecords = objMedicineType.Get();

                IEnumerable<Usage> usageRecords = objMedicineType.GetUsage();

                DTODiagnosis dtoDiagnosis = diagnosisRepository.GetDTODiagnosisById(intId);
                IEnumerable<vMedicineDiagnosis> medicineRecords = objPrescription.GetPrescriptionDiagnosis(intId);

                IMedicineTypeRepository medicineTypeRepository = new MedicineTypeRepository();
                var usageSelectBox = medicineTypeRepository.GetUsageSelectBox();
                ViewData["usageSelectBox"] = usageSelectBox;

                viewModel = new PrescriptionModels
                {
                    validAdd = itemAdd,
                    checkPost = false,
                    dtoDiagnosis = dtoDiagnosis,
                    medicineTypeRecords = medicineTypeRecords,
                    medicineRecords = medicineRecords,
                    usageRecords = usageRecords,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(MyClinic.Infrastructure.PrescriptionAdd itemAdd)
        {
            this.GetUserProfile();
            bool checkError = true;
            PrescriptionModels viewModel = null;
            DTODiagnosis dtoDiagnosis = null;
            IEnumerable<MedicineType> medicineTypeRecords = null;
            IEnumerable<vMedicineDiagnosis> medicineRecords = null;
            string diffString = "";
            try
            {
                IMedicineTypeRepository objMedicineType = new MedicineTypeRepository();
                IDiagnosisRepository diagnosisRepository = new DiagnosisRepository();
                dtoDiagnosis = diagnosisRepository.GetDTODiagnosisById(itemAdd.DiagnosisId);
                medicineRecords = objPrescription.GetPrescriptionDiagnosis(itemAdd.DiagnosisId);
                if (ModelState.IsValid)
                {
                    var intTotal = 0;
                    if (itemAdd.MedicineId != null)
                    {
                        intTotal = itemAdd.MedicineId.Count();
                    }
                    
                    foreach (var record in medicineRecords)
                    {
                        bool blnFound = false;
                        for (int intIndex = 0; intIndex < intTotal; intIndex++)
                        {
                            var intId = 0;
                            if (int.TryParse(itemAdd.Id[intIndex], out intId))
                            {
                                if (intId == record.Id)
                                {
                                    blnFound = true;
                                    Prescription itemOld = new Prescription();
                                    itemOld.DiagnosisId = record.DiagnosisId;
                                    itemOld.Id = record.Id;
                                    itemOld.MedicineId = record.MedicineId;
                                    itemOld.Qty = record.Qty;
                                    //itemOld.MedicineType = record.MedicineType;
                                    itemOld.Morning = record.Morning;
                                    itemOld.Noon = record.Noon;
                                    itemOld.Night = record.Night;
                                    itemOld.Remark = record.Remark;
                                    itemOld.UsageId = record.UsageId;

                                    Prescription itemNew = new Prescription();
                                    int itemId = 0;
                                    int.TryParse(itemAdd.Id[intIndex], out itemId);
                                    itemNew.DiagnosisId = record.DiagnosisId;
                                    itemNew.Id = itemId;
                                    int MedicineId = 0;
                                    int.TryParse(itemAdd.MedicineId[intIndex], out MedicineId);
                                    itemNew.MedicineId = MedicineId;
                                    itemNew.Qty = itemAdd.Qty[intIndex];
                                    //itemNew.MedicineType = itemAdd.MedicineType[intIndex];
                                    itemNew.Morning = itemAdd.Morning[intIndex];
                                    itemNew.Noon = itemAdd.Noon[intIndex];
                                    itemNew.Night = itemAdd.Night[intIndex];
                                    itemNew.Remark = itemAdd.Remark[intIndex];
                                    itemNew.UsageId = int.Parse(itemAdd.UsageId[intIndex]);
                                    itemNew.Status = record.Status;
                                    diffString = diffString + itemOld.EnumeratePropertyDifferencesInString(itemNew);
                                    objPrescription.Update(itemNew);
                                }
                            }
                        }
                        if (blnFound == false)
                        {
                            objPrescription.Delete(record.Id);
                        }
                    }
                    for (int intIndex = 0; intIndex < intTotal; intIndex++)
                    {
                        int itemId = 0;
                        int.TryParse(itemAdd.Id[intIndex], out itemId);
                        if (itemId == 0)
                        {
                            Prescription itemGet = new Prescription();
                            itemGet.DiagnosisId = itemAdd.DiagnosisId;
                            int MedicineId = 0;
                            int.TryParse(itemAdd.MedicineId[intIndex], out MedicineId);
                            itemGet.MedicineId = MedicineId;
                            itemGet.Qty = itemAdd.Qty[intIndex];
                            //itemGet.MedicineType = itemAdd.MedicineType[intIndex];
                            itemGet.Morning = itemAdd.Morning[intIndex];
                            itemGet.Noon = itemAdd.Noon[intIndex];
                            itemGet.Night = itemAdd.Night[intIndex];
                            itemGet.Remark = itemAdd.Remark[intIndex];
                            itemGet.UsageId = int.Parse(itemAdd.UsageId[intIndex]);
                            itemGet.Status = (int)MyClinic.Core.Enums.RecordStatus.Active;
                            objPrescription.Add(itemGet);
                        }
                    }

                    /*For Add New Record to LogTable*/
                    logTran.UserId = this.objProfile.UserId;
                    logTran.ProcessType = "Edit Prescription";
                    logTran.Description = "Edit Prescription value as follow: " + diffString;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    return RedirectToAction("Detail", "Prescription", new { id = itemAdd.DiagnosisId });
                    checkError = false;
                }
                medicineTypeRecords = objMedicineType.Get();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            if (checkError == true)
            {
                viewModel = new PrescriptionModels
                {
                    validAdd = itemAdd,
                    checkPost = true,
                    checkError = checkError,
                    dtoDiagnosis = dtoDiagnosis,
                    medicineTypeRecords = medicineTypeRecords,
                    medicineRecords = medicineRecords
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Diagnosis");
            }
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            PrintPrescriptionModels viewModel = null;
            try
            {
                IDiagnosisRepository diagnosisRepository = new DiagnosisRepository();
                DTODiagnosis dtoDiagnosis = diagnosisRepository.GetDTODiagnosisById(intId);
                IEnumerable<vMedicineDiagnosis> medicineRecords = objPrescription.GetPrescriptionDiagnosis(intId);
                if (dtoDiagnosis == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                viewModel = new PrintPrescriptionModels
                {
                    dtoDiagnosis = dtoDiagnosis,
                    medicineRecords = medicineRecords
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Print(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            PrintPrescriptionModels viewModel = null;
            try
            {                
                IDiagnosisRepository diagnosisRepository = new DiagnosisRepository();                
                DTODiagnosis dtoDiagnosis = diagnosisRepository.GetDTODiagnosisById(intId);
                ViewData["PatientTel"] = dtoDiagnosis.Patient_Tel;
                IEnumerable<vMedicineDiagnosis> medicineRecords = objPrescription.GetPrescriptionDiagnosis(intId);
                if (dtoDiagnosis == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                viewModel = new PrintPrescriptionModels
                {
                    dtoDiagnosis = dtoDiagnosis,
                    medicineRecords = medicineRecords
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Print", "_LayoutPrint", viewModel);
        }

        /*public ActionResult Print()
        {
            PrescriptionModels objViewsModels = null;
            objViewsModels = new PrescriptionModels{
            
            };
            return View("Print", "_LayoutPrint", objViewsModels);
        }*/
    }
}
