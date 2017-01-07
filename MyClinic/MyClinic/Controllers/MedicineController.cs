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
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU")]
    public class MedicineController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        IMedicineRepository medicineRepository = new MedicineRepository();
        ILogRepository logRepository = new LogRepository();
        MedicineModels medicineModel = null;
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
                var medicines = medicineRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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

                medicineModel = new MedicineModels
                {
                    medicines = medicines,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(medicineModel);
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
                var medicines = medicineRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords);
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

                medicineModel = new MedicineModels
                {
                    medicines = medicines,
                    pageUtilities = pageUtilities,
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Ajindex", "_LayoutBlank", medicineModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            IMedicineTypeRepository objMedicineType = new MedicineTypeRepository();
            IMedicineUnitRepository objMedicineUnit = new MedicineUnitRepository();
            IEnumerable<MedicineType> MedicineTypeRecords = null;
            IEnumerable<MedicineUnit> unitRecords = null;
            try
            {
                MedicineTypeRecords = objMedicineType.Get();
                unitRecords = objMedicineUnit.Get();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            medicineModel = new MedicineModels
            {
                medicineTypeRecords = MedicineTypeRecords,
                unitRecords = unitRecords
            };
            return View(medicineModel);
        }

        [HttpPost]
        public ActionResult Add(MyClinic.Infrastructure.Medicine medicine)
        {
            IMedicineTypeRepository objMedicineType = new MedicineTypeRepository();
            IMedicineUnitRepository objMedicineUnit = new MedicineUnitRepository();
            IEnumerable<MedicineType> MedicineTypeRecords = null;
            IEnumerable<MedicineUnit> unitRecords = null;
            try
            {
                MedicineTypeRecords = objMedicineType.Get();
                unitRecords = objMedicineUnit.Get();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }

            try
            {
                if (ModelState.IsValid)
                {
                   var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    medicine.CreatedBy = objSession.UserId;
                    medicine.CreatedDate = DateTime.Now;

                    medicineRepository.Add(medicine);
                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Add medicine";
                    logTran.Description = "Add New medicine Name :" + medicine.Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    return RedirectToAction("Index", "Medicine");
                }
                else
                {
                    medicineModel = new MedicineModels
                    {
                        medicine = medicine,
                        medicineTypeRecords = MedicineTypeRecords,
                        unitRecords = unitRecords
                    };
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            
            return View(medicineModel);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            Medicine medicine = null;
            try
            {
                medicine = medicineRepository.GetMedicineById(intId);
                if (medicine == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            } 

            IMedicineTypeRepository objMedicineType = new MedicineTypeRepository();
            IMedicineUnitRepository objMedicineUnit = new MedicineUnitRepository();
            IEnumerable<MedicineType> medicineTypeRecords = null;
            IEnumerable<MedicineUnit> unitRecords = null;
            try {
                medicineTypeRecords = objMedicineType.Get();
                unitRecords = objMedicineUnit.Get();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            medicineModel = new MedicineModels
            {
                medicine = medicine,
                medicineTypeRecords = medicineTypeRecords,
                unitRecords = unitRecords
            };
            return View(medicineModel);
        }

        [HttpPost]
        public ActionResult Edit(MyClinic.Infrastructure.Medicine medicine)
        {
            Medicine originMedicine = medicineRepository.GetMedicineById(medicine.Id);
            try
            {
                if (medicine == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                if (ModelState.IsValid)
                {
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;

                    medicine.CreatedBy = originMedicine.CreatedBy;
                    medicine.CreatedDate = originMedicine.CreatedDate;

                    string diffString = originMedicine.EnumeratePropertyDifferencesInString(medicine);
                    medicineRepository.UpdateFieldChangedOnly(originMedicine, medicine);

                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Edit Medicine";
                    logTran.Description = "Edit Medicine value as follow: " + diffString;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    return RedirectToAction("Index", "Medicine");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            } 
            
            IMedicineTypeRepository objMedicineType = new MedicineTypeRepository();
            IMedicineUnitRepository objMedicineUnit = new MedicineUnitRepository();
            IEnumerable<MedicineType> medicineTypeRecords = null;
            IEnumerable<MedicineUnit> unitRecords = null;
            try {
                medicineTypeRecords = objMedicineType.Get();
                unitRecords = objMedicineUnit.Get();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }

            medicineModel = new MedicineModels
            {
                medicine = medicine,
                medicineTypeRecords = medicineTypeRecords,
                unitRecords = unitRecords
            };
            return View(medicineModel);
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            try
            {
                DTOMedicine dtomedicine = medicineRepository.GetDTOMedicineById(intId);
                if (dtomedicine == null)
                {
                    return RedirectToAction("Error404", "Error");
                }
                medicineModel = new MedicineModels
                {
                    dtomedicine = dtomedicine,

                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(medicineModel);
        }


        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            string resultTrans = "success";
            try
            {
                var prescriptionList = medicineRepository.GetPrescriptionByMedicineId(id);
                if (prescriptionList.Count() == 0)
                {
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    Medicine medicine = medicineRepository.GetMedicineById(id);

                    var newMedicine = ObjectCopier.Copy<Medicine>(medicine);
                    newMedicine.Status = 0;

                    medicineRepository.UpdateFieldChangedOnly(medicine, newMedicine);
                    /*For Add New Record to LogTable*/
                    logTran.UserId = objSession.UserId;
                    logTran.ProcessType = "Delect medicine";
                    logTran.Description = "Delect medicine Name: " + medicine.Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                }
                else
                {
                    resultTrans = "failure";
                    return Json(new { result = resultTrans, proccessType = "Delete", Id = id, strName = Translator.ThisMedicine, strUsed = Translator.MsgPrescription });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                
            }
            return Json(new { result = resultTrans, proccessType = "Delete", Id = id });
        }

        [HttpPost]
        public ActionResult AutoMedicine()
        {
            IEnumerable<AutoMedicine> lstRecords = null;
            try
            {
                lstRecords = medicineRepository.AutoMedicine(1);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return Json(lstRecords);
        }
        

    }
}
