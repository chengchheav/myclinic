using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using log4net;

namespace MyClinic.Controllers
{
    public class PhotoUploadController : Controller
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //
        // GET: /PhotoUpload/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadDiagnosisPhoto()
        {           
            var result = "";
            try
            {
                var sessionId = Session.SessionID;
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var PostedFile = Request.Files[i];
                    if (PostedFile.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(PostedFile.FileName);
                        string filePath = Server.MapPath("~") + "Uploads\\Temp\\" + sessionId + "\\" + fileName;
                        FileInfo fileInfo = new FileInfo(filePath);
                        if (!fileInfo.Directory.Exists)
                            fileInfo.Directory.Create();
                        PostedFile.SaveAs(filePath);

                        var link = baseUrl + "Uploads/Temp/" + sessionId + "/" + fileName;
                        result += link + "|";
                    }
                }

                if (result.Length > 0)
                    result = result.Substring(0, result.Length - 1);
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return Content(result);
        }

        [HttpPost]
        public ActionResult UploadSaveDiagnosisPhoto(int id)
        {
            var diagnosisId=id;
            var result = "";
            try
            {
                var sessionId =Session.SessionID;
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var PostedFile = Request.Files[i];
                    if (PostedFile.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(PostedFile.FileName);
                        string filePath = Server.MapPath("~") + "Uploads\\Temp\\" + sessionId + "\\" + fileName;
                        FileInfo fileInfo = new FileInfo(filePath);
                        if (!fileInfo.Directory.Exists)
                            fileInfo.Directory.Create();
                        PostedFile.SaveAs(filePath);
                    }
                }

                SaveUploadImge(sessionId,diagnosisId);
                string destinationPath = Server.MapPath("~") + "Uploads\\Diagnosis\\" + diagnosisId;

                DirectoryInfo destinationInfo = new DirectoryInfo(destinationPath);
                if (destinationInfo.Exists)
                {
                    foreach (FileInfo fileInfo in destinationInfo.GetFiles())
                    {
                        var link = baseUrl + "Uploads/Diagnosis/" + diagnosisId + "/" + fileInfo.Name;
                        result += link + "|";
                    }
                }

                if (result.Length > 0)
                    result = result.Substring(0, result.Length - 1);
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return Content(result);
        }

        private void SaveUploadImge(string sessionId,int diagnosisId)
        {
            try
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                string directoryPath = Server.MapPath("~") + "Uploads\\Temp\\" + sessionId + "\\";
                string destinationPath = Server.MapPath("~") + "Uploads\\Diagnosis\\" + diagnosisId;

                DirectoryInfo destinationInfo = new DirectoryInfo(destinationPath);
                if (!destinationInfo.Exists)
                    destinationInfo.Create();

                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
                if (directoryInfo.Exists)
                {
                    foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                    {
                        if (fileInfo.Exists)
                        {
                            FileInfo checkFileInfo = new FileInfo(destinationPath + "\\" + fileInfo.Name);
                            if (checkFileInfo.Exists)
                                checkFileInfo.Delete();
                            fileInfo.MoveTo(checkFileInfo.FullName);
                        }
                    }
                    directoryInfo.Delete();
                }
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
        }

        public ActionResult DeleteCreatePhoto()
        {
            string result = "";
            try
            {
                var sessionId = Session.SessionID;
                string fileName = Request.Form["fileName"];
                if (fileName.LastIndexOf("/") > -1)
                {
                    fileName = fileName.Substring(fileName.LastIndexOf("/") + 1, fileName.Length - (fileName.LastIndexOf("/") + 1));
                }

                string destinationPath = Server.MapPath("~") + "Uploads\\Temp\\" + sessionId + "\\" + fileName;
                FileInfo destFileInfo = new FileInfo(destinationPath);
                if (destFileInfo.Exists)
                {
                    destFileInfo.Delete();
                }

                if (destFileInfo.Directory.Exists)
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    foreach (FileInfo fileInfo in destFileInfo.Directory.GetFiles())
                    {
                        var link = baseUrl + "Uploads/Temp/" + sessionId + "/" + fileInfo.Name;
                        result += link + "|";
                    }

                    if (result.Length > 0)
                        result = result.Substring(0, result.Length - 1);
                }
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return Content(result);
        }

        public ActionResult DeleteEditPhoto()
        {
            string result = "";
            try
            {
                string fileName = Request.Form["fileName"];

                //string fileName = "http://localhost:12121/Uploads/Diagnosis/12/test.jpg";
                string strDiagnosisId = "";
                if (fileName.IndexOf("/Uploads/Diagnosis") > -1)
                {
                    fileName = fileName.Substring(fileName.IndexOf("/Uploads/Diagnosis"), fileName.Length - fileName.IndexOf("/Uploads/Diagnosis"));
                    strDiagnosisId = fileName.Replace("/Uploads/Diagnosis/", "");
                    strDiagnosisId=strDiagnosisId.Substring(0,strDiagnosisId.IndexOf("/"));
                }

                int diagnosisId = strDiagnosisId == "" ? 0 : int.Parse(strDiagnosisId);
                string destinationPath = Server.MapPath("~") + fileName;
                FileInfo destFileInfo = new FileInfo(destinationPath);
                if (destFileInfo.Exists)
                {
                    destFileInfo.Delete();
                }

                if (destFileInfo.Directory.Exists)
                {
                    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                    foreach (FileInfo fileInfo in destFileInfo.Directory.GetFiles())
                    {
                        var link = baseUrl + "Uploads/Diagnosis/" + diagnosisId + "/" + fileInfo.Name;
                        result += link + "|";
                        //result += "<div id=\"imgPhoto\" class=\"bx-pt\" style=\"margin:20px;\"><span onclick=\"$image.initRemoveImage(\'" + link + "\');\"></span><img src=\"" + link + "\" style=\"width:100%;height:100%;object-fit:cover;\" /></div>";
                    }
                    if (result.Length > 0)
                        result = result.Substring(0, result.Length - 1);
                }
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return Content(result);
        }
    }
}
