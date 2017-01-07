using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClinic.Core;
using MyClinic.Infrastructure;
using System.Web.Security;
using MyClinic.Models;
using Utilities;
using log4net;
using System.Globalization;
using System.Configuration;

namespace MyClinic.Controllers
{
    public class LoginController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyClinic.Infrastructure.Log logTran = new MyClinic.Infrastructure.Log();
        ILogRepository logRepository = new LogRepository();
        IUserRepository userRepository = new UserRepository();
        UserModels userModels = new UserModels();
        TranslateSwitch translateSwitch = new TranslateSwitch();

        private void initParameter() {            
            Utilities.Common.ChangeLanguage(Request.Url.Segments);
            //Set default language cookies
            var lang = Request.Url.Segments[1];
            HttpCookie myCookie = new HttpCookie("MyShopCookie");
            DateTime now = DateTime.Now;
            myCookie.Values["lang"] = lang;            
            myCookie.Expires = now.AddYears(1);
            Response.Cookies.Add(myCookie);
        }

        public ActionResult Index()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (Request.Url.Segments.Length == 1)
            {
                var lang = "km";
                if (Request.Cookies["MyShopCookie"] != null)
                {
                    lang = Request.Cookies["MyShopCookie"]["lang"];
                    lang = lang.Contains("Content") ? "km" : lang;
                }
                return Redirect("/"+lang);
            }

            this.initParameter();            
            try
            {
                LicenseHelper.License license = new LicenseHelper.License();                
                var reponseMsg = license.CheckLicenseValidated();
                userModels.LicenseMessage = "";// reponseMsg.Message;

                //var connectionString = Common.DecryptString("fmEHidc034R6EfxrWeln6OeLoyyQPkpFZA413nAPMNHbkXIFQOTSv+aMgsPOQh/i0y3Q7/knFgzaQ3jsWDWu5LC/LTEXBaIvmZtEM3ENiaEOxLaW5ZgtwFNd0lA3TNwS9fMSrikgCyoAjjeiDiellY0yHWkECL4fGQbCa7rUn64=");
                //var connectionString = Common.EncryptString(@"Data Source=127.0.0.1\SQLEXPRESS; Initial Catalog=PawnDB; User Id=pawn_user; Password=Pawn@m!n0616;Connect Timeout=300;");//Instant Cash
                //var connectionString = Common.EncryptString(@"Data Source=192.168.3.5\SQLEXPRESS; Initial Catalog=MyClinicDB; User Id=pawn_user; Password=@m!n61;Connect Timeout=30;");
                //var connectionString = Common.EncryptString(@"Data Source=127.0.0.1\SQLEXPRESS; Initial Catalog=MyClinicDB; User Id=pawn_user; Password=@m!n61;Connect Timeout=30;");
                //var connectionString = Common.EncryptString(@"Data Source=127.0.0.1\SQLEXPRESS; Initial Catalog=MyClinicDB; User Id=apos_user; Password=rr@m!n3000;Connect Timeout=30;");
                //var connectionString = Common.EncryptString(@"Data Source=127.0.0.1\SQLEXPRESS; Initial Catalog=MyClinicDB; User Id=test_user; Password=@m!n136;Connect Timeout=30;");
                //userModels.LicenseMessage = connectionString;

                //reponseMsg.IsValid = true;//Comment When Live on Production
                if (reponseMsg.IsValid)
                {
                    if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                    {
                        MyClinic.Infrastructure.User user = new Infrastructure.User();
                        user.UserName = Request.Cookies["UserName"].Value;
                        user.Password = Common.DecryptString(Request.Cookies["Password"].Value);
                        userModels.user = user;
                        /*For LogIn Auto*/
                        var logInUser = userRepository.GetUserByUsernameAndPassword(user.UserName, user.Password);
                        if (logInUser != null)
                        {
                            if (!String.IsNullOrEmpty(logInUser.UserName) && !String.IsNullOrEmpty(logInUser.Password))
                            {
                                if (logInUser.IsActived.Equals(1))
                                {

                                    SessUser sessUser = new SessUser();
                                    sessUser.UserId = logInUser.Id;
                                    sessUser.Name = logInUser.Name;
                                    sessUser.Email = logInUser.Email;
                                    sessUser.Username = logInUser.UserName;
                                    sessUser.Password = logInUser.Password;
                                    sessUser.IsActived = logInUser.IsActived;
                                    sessUser.UserType = logInUser.UserType;
                                    sessUser.Image = logInUser.Image;
                                    Session["user"] = sessUser;

                                    /* For Add New Record to LogTable*/
                                    logTran.UserId = sessUser.UserId;
                                    logTran.ProcessType = "Auto Login";
                                    logTran.Description = "Auto Login UserName = " + sessUser.Username + " : Name = " + sessUser.Name;
                                    logTran.LogDate = DateTime.Now;
                                    logRepository.Add(logTran);


                                    Response.Cookies["userId"].Expires = DateTime.Now.AddDays(30);
                                    Response.Cookies["userId"].Value = sessUser.UserId.ToString();

                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, translateSwitch.Get("Invalid username or password."));
                        }
                    }
                }
                else
                {
                    userModels.LicenseMessage = reponseMsg.Message;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError("error", translateSwitch.Get("Unexpected Error"));
            }
            var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
            if (objSession != null)
            {
                if (Request.QueryString["rdr"] != null)
                {
                    string redirectUrl = Request.QueryString["rdr"];
                    redirectUrl = HttpUtility.UrlDecode(redirectUrl);
                    return Redirect(redirectUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Index", "_LayoutLogin", userModels);
        }

        [HttpPost]
        public ActionResult Index(MyClinic.Infrastructure.User user,bool Remember = false)
        {
            try
            {
                LicenseHelper.License license = new LicenseHelper.License();
                var reponseMsg = license.CheckLicenseValidated();
                userModels.LicenseMessage = "";
                //reponseMsg.IsValid = true;//Comment When Live on Production

                EntityFrameworkHelper efhelper = new EntityFrameworkHelper();
                var connectionWork = efhelper.TestConnection();

                if (connectionWork)
                {
                    //reponseMsg.IsValid = true;
                    if (reponseMsg.IsValid)
                    {
                        if (ModelState.IsValid)
                        {
                            /*For Remember Username and Pass*/
                            if (Remember)
                            {
                                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(30);
                                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
                            }
                            else
                            {
                                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                            }
                            if (!String.IsNullOrEmpty(user.UserName) && !String.IsNullOrEmpty(user.Password))
                            {
                                Response.Cookies["UserName"].Value = user.UserName.Trim();
                                Response.Cookies["Password"].Value = Common.EncryptString(user.Password.Trim());
                                /*======*/

                                var logInUser = userRepository.GetUserByUsernameAndPassword(user.UserName, user.Password);
                                if (logInUser.Id > 0)
                                {
                                    if (logInUser.IsActived.Equals(1))
                                    {
                                        SessUser sessUser = new SessUser();
                                        sessUser.UserId = logInUser.Id;
                                        sessUser.Name = logInUser.Name;
                                        sessUser.Email = logInUser.Email;
                                        sessUser.Username = logInUser.UserName;
                                        sessUser.Password = logInUser.Password;
                                        sessUser.IsActived = logInUser.IsActived;
                                        sessUser.UserType = logInUser.UserType;
                                        sessUser.Image = logInUser.Image;
                                        Session["user"] = sessUser;

                                        Response.Cookies["userId"].Expires = DateTime.Now.AddDays(30);
                                        Response.Cookies["userId"].Value = sessUser.UserId.ToString();

                                        /* For Add New Record to LogTable*/
                                        logTran.UserId = sessUser.UserId;
                                        logTran.ProcessType = "Login";
                                        logTran.Description = "Login UserName = " + sessUser.Username + " : Name = " + sessUser.Name;
                                        logTran.LogDate = DateTime.Now;
                                        logRepository.Add(logTran);
                                    }
                                    else
                                    {
                                        ModelState.AddModelError(string.Empty, translateSwitch.Get("You don't have authorized to access this system.​ Please contact to administrator."));
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError(string.Empty, translateSwitch.Get("Invalid username or password."));
                                }

                            }
                            else
                            {
                                ModelState.AddModelError("", translateSwitch.Get("Please enter username & password"));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, translateSwitch.Get("Login was unsuccessful. Please correct the errors and try again."));
                        }
                    }
                    else
                    {
                        userModels.LicenseMessage = reponseMsg.Message;
                    }
                }
                else
                {
                    userModels.LicenseMessage = translateSwitch.Get("Cannot connect to the Database, Please contact software provider for help!");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ModelState.AddModelError(string.Empty, translateSwitch.Get("Invalid username or password."));
            }
            var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
            if (objSession != null)
            {
                if (Request.QueryString["rdr"] != null)
                {
                    string redirectUrl = Request.QueryString["rdr"];
                    redirectUrl = HttpUtility.UrlDecode(redirectUrl);
                    return Redirect(redirectUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("Index", "_LayoutLogin", userModels);
        }

        /*====For Log out=====*/
        public ActionResult Logoff()
        {
            var objSession = Session["user"] as MyClinic.Infrastructure.SessUser;
            if (objSession == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Session.Clear();
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);

            /* For Add New Record to LogTable*/
            logTran.UserId = objSession.UserId;
            logTran.ProcessType = "LogOut";
            logTran.Description = "LogOut UserName = " + objSession.Username + " : Name = " + objSession.Name;
            logTran.LogDate = DateTime.Now;
            logRepository.Add(logTran);
            return RedirectToAction("Index", "Login");
        }


        public ActionResult CheckSession()
        {
            return View("CheckSession", "_LayoutBlank");
        }

    }
}