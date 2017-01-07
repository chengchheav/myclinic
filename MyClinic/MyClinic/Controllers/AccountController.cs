using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MyClinic.Models;
using System.IO;
using Utilities;
using MyClinic.Infrastructure;
using MyClinic.Core;
using log4net;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using Utilities;
using MyClinic.MVC.Extension;
using MyClinic.Resources;

namespace MyClinic.Controllers
{
    [CustomAuthorizeAttribute("ADM", "USR", "RPV", "SPU", "FUR")]
    public class AccountController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        IUserRepository userRepository = new UserRepository();
        ILogRepository logRepository = new LogRepository();
        MyDbContext db = new MyDbContext();
        TranslateSwitch translateSwitch = new TranslateSwitch();

        private void initParameter()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("",​ "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        /*For Edit Profile userLogin*/
        [HttpGet]
        public ActionResult EditUserProfile()
        {
            UserEdit userEdit = new UserEdit();
            UserModels viewModel = null;
            User user = null;
            try
            {
                var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
                if (objSession == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    user = userRepository.GetUserById(objSession.UserId);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    var path = "/Uploads/default.jpg";
                    var fileName = user.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/User/") + user.Id;
                    // Check for Directory, If not exist, then create it 
                    if (Directory.Exists(savePath))
                    {
                        path = "/Uploads/User/" + user.Id + "/" + fileName;
                    }
                    userEdit.ImageStream = ImageHelper.BaseDateImage(savePath + "/" + fileName);
                    user.Image = Common.ConcatHost(path);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty,  Translator.UnexpectedError);
            }

            viewModel = new UserModels
            {
                user = user,
                userEdit = userEdit,
                checkPost = false
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditUserProfile(MyClinic.Infrastructure.UserEdit userEdit)
        {
            bool checkError = true;
            UserModels viewModel = null;
            var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
            if (objSession == null)
            {
                return HttpNotFound();
            }
            else {
                userEdit.Id = objSession.UserId;
            }
            User user = userRepository.GetUserById(userEdit.Id);
            User checkUser = userRepository.GetUserByUsername(userEdit.UserName);
            try
            {
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
                    ModelState.AddModelError("ConfirmPassword", "Confirm Password is not match.");
                }

                if (ModelState.IsValid)
                {
                    User oldUser = userRepository.GetUserById(userEdit.Id);
                    var newUser = ObjectCopier.Copy<User>(oldUser);
                    var savePathImage = Server.MapPath("~/Uploads/User/") + user.Id;
                    ImageHelper.SaveImage(savePathImage, user.Id + ".jpg", userEdit.ImageStream);
                    if (String.IsNullOrEmpty(userEdit.Password) == false)
                    {
                        newUser.Password = Common.EncryptString(userEdit.Password);
                    }
                    newUser.Name = userEdit.Name;
                    newUser.UserName = userEdit.UserName;
                    newUser.Email = userEdit.Email;
                    newUser.Name = userEdit.Name;
                    newUser.ModifiedBy = objSession.UserId;
                    newUser.ModifiedDate = DateTime.Now;
                    string diffString = oldUser.EnumeratePropertyDifferencesInString(newUser);
                    objSession.Password = newUser.Password;
                    userRepository.UpdateFieldChangedOnly(oldUser, newUser);
                    /*For Add New Record to LogTable*/
                    int userId = objSession.UserId;
                    logTran.UserId = userId;
                    logTran.ProcessType = "Update Profile";
                    logTran.Description = "Update Profile User " + user.Name + " value as follow: " + diffString;
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
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user = null;
            try
            {
                user = userRepository.GetUserById(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    var path = "/Uploads/default.jpg";
                    var fileName = user.Id + ".jpg";
                    var savePath = Server.MapPath("~/Uploads/User/") + user.Id;
                    // Check for Directory, If not exist, then create it 
                    if (Directory.Exists(savePath))
                    {
                        path = "/Uploads/User/" + user.Id + "/" + fileName;
                    }
                    user.Image = Common.ConcatHost(path);
                }
            }catch(Exception ex){
                log.Error(ex);
            }
            return View(user);
        }
        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception ex)
                {
                    changePasswordSucceeded = false;
                    log.Error(ex);
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        
    }
}
