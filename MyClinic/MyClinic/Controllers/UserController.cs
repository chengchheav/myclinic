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
    [CustomAuthorizeAttribute("ADM", "SPU")]
    public class UserController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        IUserRepository userRepository = new UserRepository();
        ILogRepository logRepository = new LogRepository();
        UserModels userModels = new UserModels();
        public void initParameter()
        {

        }
        public ActionResult Index()
        {
            this.initParameter();
            try
            {
                MyClinic.Infrastructure.User user = new MyClinic.Infrastructure.User();
                var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                string userType = objSession.UserType;

                string searchBy = "";
                string keyword = "";
                var orderBy = Common.defaultOrderBy;
                var order = Common.defaultListOrder;
                var _pageNo = 1;
                var _pageSize = 10;
                var _pageStatus = 1;
                var totalRecords = 0;
                var users = userRepository.Search(searchBy, keyword, orderBy, order, _pageNo, _pageSize, out totalRecords, userType);
                var listResult = Paging.GetResultInfo(totalRecords, _pageNo, _pageSize);
                var paging = Paging.GetPaging(totalRecords, _pageNo, _pageSize, _pageStatus, Common.defaultNoOfPageLinkList, "$common.pagingClick", orderBy, order);
                var itemPerPage = Paging.getItemPerPage(totalRecords, _pageSize, orderBy, order);

                userModels.users = users;
                PageUtilities pageUtilities = new PageUtilities()
                {
                    listHeader = listResult,
                    listFooter = paging + itemPerPage,
                    order = order,
                    orderBy = orderBy
                };
                userModels.pageUtilities = pageUtilities;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            return View(userModels);
        }

        public ActionResult Ajindex(int? pageNo, int? pageSize, int? pageStatus, string orderBy, string order, string searchBy, string keyWord)
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
                var users = userRepository.Search(searchBy, keyWord, orderBy, order, _pageNo, _pageSize, out totalRecords, userType);//Just create new class
                var listResult = Paging.GetResultInfo(totalRecords, _pageNo, _pageSize);
                var paging = Paging.GetPaging(totalRecords, _pageNo, _pageSize, _pageStatus, Common.defaultNoOfPageLinkList, "$common.pagingClick", orderBy, order);
                var itemPerPage = Paging.getItemPerPage(totalRecords, _pageSize, orderBy, order);

                userModels.users = users;
                PageUtilities pageUtilities = new PageUtilities()
                {
                    listHeader = listResult,
                    listFooter = paging + itemPerPage,
                    order = order,
                    orderBy = orderBy
                };
                userModels.pageUtilities = pageUtilities;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View("Ajindex", "_LayoutBlank", userModels);
        }

        [HttpGet]
        public ActionResult Add() 
        {
            var userAdd = new UserAdd();
            var savePath = Server.MapPath("~/Uploads/");
            var fileName = "default.jpg";
            UserModels viewModel = null;
            try{
                userAdd.ImageStream = ImageHelper.BaseDateImage(savePath + "/" + fileName);
                viewModel = new UserModels
                {
                    userAdd = userAdd,
                    checkPost = false
                };
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, Translator.UnexpectedError);
            }
            return View(viewModel);

        }
        [HttpPost]
        public ActionResult Add(MyClinic.Infrastructure.UserAdd userAdd)
        {
            bool checkError = true;
            UserModels viewModel = null;
            User checkUser = null;
            try
            {
                if (String.IsNullOrEmpty(userAdd.UserName))
                {
                    userAdd.UserName = String.Empty;
                }
                checkUser = userRepository.GetUserByUsername(userAdd.UserName);
                if (String.IsNullOrEmpty(checkUser.UserName) == false)
                {
                    ModelState.AddModelError("UserName", "UserName Exist");
                }

                if (String.IsNullOrEmpty(userAdd.ConfirmPassword))
                {
                    userAdd.ConfirmPassword = String.Empty;
                }

                if (String.IsNullOrEmpty(userAdd.Password))
                {
                    userAdd.Password = String.Empty;
                }

                if (userAdd.ConfirmPassword.Trim() != userAdd.Password.Trim())
                {
                    ModelState.AddModelError("ConfirmPassword", "The Confirm Password is not match.");
                }


                var userType = "";
                if (Request.Form["ADM"] != null) userType = userType + Request.Form["ADM"] + ",";
                if (Request.Form["SPU"] != null) userType = userType + Request.Form["SPU"] + ",";
                if (Request.Form["RPV"] != null) userType = userType + Request.Form["RPV"] + ",";
                if (Request.Form["USR"] != null) userType = userType + Request.Form["USR"] + ",";
                if (Request.Form["FUR"] != null) userType = userType + Request.Form["FUR"] + ",";
                if (userType == "")
                {
                    userType = "USR";
                }
                else
                {
                    userType = userType.Substring(0, userType.Count() - 1);
                }

                userAdd.UserType = userType;

                if (ModelState.IsValid)
                {
                    User user = new User();
                    Dictionary<string, string> dictItem = TransactionHelper.processGenerateArray(user);
                    user = TransactionHelper.TransDict<User>(dictItem, user);
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    user.UserType = userType;
                    user.Image = user.Id + "jpg";
                    user.ModifiedBy = 0;
                    var strValue = "1990-01-01 00:00:00.000";
                    DateTime objDate;
                    DateTime.TryParse(strValue, out objDate);
                    user.ModifiedDate = objDate;
                    user.CreatedBy = objSession.UserId;
                    user.CreatedDate = DateTime.Now;

                    userRepository.Add(user);

                    var fileName = user.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/User/") + user.Id;
                    ImageHelper.SaveImage(savePath, user.Id + ".jpg", userAdd.ImageStream);
                    User newUser = Utilities.ObjectCopier.Copy<User>(user);
                    newUser.Image = user.Id + ".jpg";
                    userRepository.UpdateFieldChangedOnly(user, newUser);

                    /*For Add New Record to LogTable*/
                    int userId = objSession.UserId;
                    logTran.UserId = userId;
                    logTran.ProcessType = "Add User";
                    logTran.Description = "Add User Name : " + user.Name;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    checkError = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty,  Translator.UnexpectedError);
            }

            if (checkError == true)
            {
                viewModel = new UserModels
                {
                    userAdd = userAdd,
                    checkPost = true
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            UserModels viewModel = null;
            UserEdit userEdit = new UserEdit();
            var intId = 0;
            int.TryParse(id, out intId);
            try{
                User user = userRepository.GetUserById(intId);
                if (user == null || user.Name == null || user.Name == "")
                {
                    return RedirectToAction("Error404", "Error");
                }
                else
                {
                    var path = "";
                    var fileName = user.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/User/") + user.Id;
                    // Check for Directory, If not exist, then create it 
                    if (Directory.Exists(savePath))
                    {
                        path = "/Uploads/User/" + user.Id + "/" + fileName;
                        user.Image = path;
                    }
                    else {
                        savePath = Server.MapPath("~/Uploads/");
                        fileName = "default.jpg";
                    }
                    userEdit.ImageStream = ImageHelper.BaseDateImage(savePath + "/" + fileName);
                    user.Image = Common.ConcatHost(path);
                } 
            
                viewModel = new UserModels
                {
                    user = user,
                    userEdit = userEdit,
                    checkPost = false
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
        public ActionResult Edit(MyClinic.Infrastructure.UserEdit userEdit)
        {
            bool checkError = true;
            UserModels viewModel = null;
            User user = null;
            User checkUser = null;
            try
            {
                user = userRepository.GetUserById(userEdit.Id);
                checkUser = userRepository.GetUserByUsername(userEdit.UserName);
                if (String.IsNullOrEmpty(checkUser.UserName) == false)
                {
                    if (user.UserName != userEdit.UserName)
                    {
                        ModelState.AddModelError("UserName", "UserName Exist");
                    }
                }

                if (String.IsNullOrEmpty(userEdit.ConfirmPassword))
                {
                    userEdit.ConfirmPassword = String.Empty;
                }

                if (String.IsNullOrEmpty(userEdit.Password))
                {
                    userEdit.Password = String.Empty;
                }

                if (userEdit.ConfirmPassword.Trim() != userEdit.Password.Trim())
                {
                    ModelState.AddModelError("ConfirmPassword", "The Confirm Password is not match.");
                }

                if (ModelState.IsValid)
                {
                    User userOld = userRepository.GetUserById(userEdit.Id);
                    User userDiff = Utilities.ObjectCopier.Copy<User>(userOld);
                    var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                    if (objSession.UserId == user.Id)
                    {
                        objSession.Password = Common.EncryptString(userEdit.Password);
                    }
                    var userType = "";
                    if (Request.Form["ADM"] != null) userType = userType + Request.Form["ADM"] + ",";
                    if (Request.Form["SPU"] != null) userType = userType + Request.Form["SPU"] + ",";
                    if (Request.Form["RPV"] != null) userType = userType + Request.Form["RPV"] + ",";
                    if (Request.Form["USR"] != null) userType = userType + Request.Form["USR"] + ",";
                    if (Request.Form["FUR"] != null) userType = userType + Request.Form["FUR"] + ",";
                    if (userType == "")
                    {
                        userType = "USR";
                    }
                    else
                    {
                        userType = userType.Substring(0, userType.Count() - 1);
                    }
                    user.UserType = userType;
                    var savePathImage = Server.MapPath("~/Uploads/User/") + user.Id;
                    ImageHelper.SaveImage(savePathImage, user.Id + ".jpg", userEdit.ImageStream);

                    if (String.IsNullOrEmpty(userEdit.Password) == false)
                    {
                        user.Password = Common.EncryptString(userEdit.Password);
                    }
                    user.Name           = userEdit.Name;
                    user.UserName       = userEdit.UserName;
                    user.Email          = userEdit.Email;
                    user.IsActived      = userEdit.IsActived;
                    user.Tel            = userEdit.Tel;
                    user.Image          = user.Id + ".jpg";
                    string diffString = userDiff.EnumeratePropertyDifferencesInString(user);
                    user.ModifiedBy = objSession.UserId;
                    user.ModifiedDate = DateTime.Now;

                    userRepository.UpdateFieldChangedOnly(userOld, user);
                    /*For Add New Record to LogTable*/
                    int userId = objSession.UserId;
                    logTran.UserId = userId;
                    logTran.ProcessType = "Edit User";
                    logTran.Description = "Edit User value as follow: " + diffString;
                    logTran.LogDate = DateTime.Now;
                    logRepository.Add(logTran);
                    checkError = false;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty,  Translator.UnexpectedError);
            }

            if (checkError == true)
            {
                viewModel = new UserModels
                {
                    user = user,
                    userEdit = userEdit,
                    checkError = checkError,
                    checkPost = true
                };
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            User originUser = null;
            string resultTrans = "success";
            try
            {
                originUser = userRepository.GetUserById(id);
                var newUser = ObjectCopier.Copy<User>(originUser);

                newUser.IsActived = 2;//For Delete
                userRepository.UpdateFieldChangedOnly(originUser, newUser);

                /*For Add New Record to LogTable*/
                var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                int userId = objSession.UserId;

                logTran.UserId = userId;
                logTran.ProcessType = "Delete User";
                logTran.Description = "Delete User Name :" + originUser.Name;
                logTran.LogDate = DateTime.Now;
                logRepository.Add(logTran);
            }catch(Exception ex){
                log.Error(ex);
                resultTrans = "failure";
            }
            return Json(new { result = resultTrans, proccessType = "Delete", Id = id });
        }
        [HttpGet]
        public ActionResult Detail(string id)
        {
            var intId = 0;
            int.TryParse(id, out intId);
            try{
                var usertypes = userRepository.getUserType();
                User user = userRepository.GetUserById(intId);
                if (user == null || user.Name == null || user.Name == "")
                {
                    return RedirectToAction("Error404", "Error");
                }
                else
                {
                    User objUserCreate = userRepository.GetUserById(user.CreatedBy);
                    User objUserModified = userRepository.GetUserById(user.ModifiedBy);
                    var path = "/Uploads/default.jpg";
                    var fileName = user.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/User/") + user.Id;
                    // Check for Directory, If exist 
                    if (Directory.Exists(savePath))
                    {
                        path = "/Uploads/User/" + user.Id + "/" + fileName;
                    }
                    user.Image = Common.ConcatHost(path);

                    userModels = new UserModels
                    {
                        user = user,
                        userCreate = objUserCreate,
                        userModified = objUserModified,
                        usertypes = usertypes
                    };
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", Translator.UnexpectedError);
            }
            return View(userModels);
        }
    }
}
