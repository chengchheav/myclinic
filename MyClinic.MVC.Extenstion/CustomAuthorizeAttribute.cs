using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using MyClinic.Core;
using MyClinic.Infrastructure;

namespace MyClinic.MVC.Extension
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }


        public override void OnAuthorization(AuthorizationContext context)
        {
            bool authorized = false;
            bool haveLogin = false;
            var cUser = (SessUser)HttpContext.Current.Session["user"];
            if (cUser == null)
            {
                cUser = new SessUser();
                cUser.Name = "";
                //cUser.Id = 0;
            }
            var userName = cUser.Username;//HttpContext.Current.User.Identity.Name;

            if (userName != null)
            {
                if (userName.Trim().Length > 0)
                {
                    haveLogin = true;
                    IUserRepository userRepository = new UserRepository();
                    var userAuth = new User();
                    userAuth.UserName = userName;
                    userAuth.Password = Utilities.Common.DecryptString(cUser.Password);
                    var user = userRepository.GetUserByUsernameAndPassword(userAuth.UserName, userAuth.Password);
                    if (user != null)
                    {
                        if (user.UserType != null)
                        {
                            foreach (var role in allowedroles)
                            {
                                if (user.UserType.Contains(role))
                                {
                                    authorized = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            var isAjax = context.HttpContext.Request.IsAjaxRequest();
            if (!authorized && haveLogin == true)
            {

                var logonUrl = "";
                var url = new UrlHelper(context.RequestContext);
                if (isAjax)
                {
                    logonUrl = url.Action("AjError403", "Error");
                }
                else
                {
                    logonUrl = url.Action("Error403", "Error");
                }
                context.Result = new RedirectResult(logonUrl);
                return;
            }
            else if (haveLogin == false)
            {
                var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
                string CurrentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                var controllerName = "";
                if (routeValues.ContainsKey("controller"))
                {
                    controllerName = (string)routeValues["controller"];
                }
                var actionName = "";
                if (routeValues.ContainsKey("action"))
                {
                    actionName = (string)routeValues["action"];
                }
                var url = new UrlHelper(context.RequestContext);
                var logonUrl = "";
                if (isAjax)
                {
                    logonUrl = url.Action("LogInAgain", "Error", new { rdr = HttpUtility.UrlEncode(CurrentUrl) });
                }
                else
                {
                    logonUrl = url.Action("Index", "Login", new { rdr = HttpUtility.UrlEncode(CurrentUrl) });
                }
                context.Result = new RedirectResult(logonUrl);
                return;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}
