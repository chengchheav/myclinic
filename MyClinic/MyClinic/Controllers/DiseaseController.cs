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
    public class DiseaseController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        IUserRepository userRepository = new UserRepository();
        IDiseaseRepository diseaseRepository = new DiseaseRepository();
        ILogRepository logRepository = new LogRepository();
        DiseaseModels diseaseModel = null;
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
                var diseases = diseaseRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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

                diseaseModel = new DiseaseModels
                {
                    diseases = diseases,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(diseaseModel);
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
                var diseases = diseaseRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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

                diseaseModel = new DiseaseModels
                {
                    diseases = diseases,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Ajindex", "_LayoutBlank", diseaseModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Add(MyClinic.Infrastructure.Disease disease)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    /*disease disease = new disease();
                    Dictionary<string, string> dictItem = TransactionHelper.processGenerateArray(disease);
                    disease = TransactionHelper.TransDict<disease>(dictItem, disease);*/

                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    disease.CreateBy = objSession.UserId;
                    disease.CreateDate = DateTime.Now;

                    //disease.ModifiedBy = 0;
                    var strValue = "1990-01-01";
                    DateTime objDate;
                    DateTime.TryParse(strValue, out objDate);
                    disease.ModifiedDate = objDate;
                    disease.Status = 1;
                    diseaseRepository.Add(disease);
                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Add disease";
                    logTran.Description = "Add New disease Name :" + disease.Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    return RedirectToAction("Index", "disease");
                }
                else
                {
                    diseaseModel = new DiseaseModels
                    {
                        disease = disease
                    };
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(diseaseModel);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            Disease disease = null;
            try
            {
                disease = diseaseRepository.GetDiseaseById(intId);
                if (disease == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                diseaseModel = new DiseaseModels
                {
                    disease = disease,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(diseaseModel);
        }

        [HttpPost]
        public ActionResult Edit(MyClinic.Infrastructure.Disease disease)
        {
            Disease originDisease = diseaseRepository.GetDiseaseById(disease.Id);
            try
            {
                if (ModelState.IsValid)
                {
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    disease.ModifiedBy = objSession.UserId;
                    disease.ModifiedDate = DateTime.Now;
                    disease.CreateBy = originDisease.CreateBy;
                    disease.CreateDate = originDisease.CreateDate;
                    disease.Status = originDisease.Status;
                    string diffString = originDisease.EnumeratePropertyDifferencesInString(disease);

                    diseaseRepository.UpdateFieldChangedOnly(originDisease, disease);
                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Edit Disease";
                    logTran.Description = "Edit Disease value as follow: " + diffString;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    return RedirectToAction("Index", "Disease");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            diseaseModel = new DiseaseModels
            {
                disease = disease,
            };
            return View(diseaseModel);
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            DiseaseModels objDisease = null;
            try
            {
                
                DTODisease dtoDisease = diseaseRepository.GetDTODiseaseById(intId);
                var user = userRepository.GetUserById(dtoDisease.ModifiedBy);
                if (dtoDisease == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                objDisease = new DiseaseModels
                {
                    dtoDisease = dtoDisease,
                    user = user
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(objDisease);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            string resultTrans = "success";
            try
            {
                var DiagnosisList = diseaseRepository.GetDiagnosisByDiseaseId(id);
                if (DiagnosisList.Count() == 0)
                {
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    Disease Disease = diseaseRepository.GetDiseaseById(id);
                    var newDisease = ObjectCopier.Copy<Disease>(Disease);
                    newDisease.Status = 0;
                    diseaseRepository.UpdateFieldChangedOnly(Disease, newDisease);
                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Delect Disease";
                    logTran.Description = "Delect Disease Name: " + Disease.Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                }
                else
                {
                    resultTrans = "failure";
                    return Json(new { result = resultTrans, proccessType = "Delete", Id = id, strName = Translator.ThisDisease, strUsed = Translator.MsgDiagnosis });
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
        public string GetDiseaseName()
        {
            string name = Request.Form["phrase"].ToString();
            IEnumerable<Disease> objNames = null;
            try
            {
                name = name == null ? "" : name;
                objNames = diseaseRepository.GetDiseaseListByName(name);
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
