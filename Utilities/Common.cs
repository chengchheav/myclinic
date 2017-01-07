using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;

namespace Utilities
{
    public class Common
    {
        public static string host = string.Empty;
        public static string hostWithLang = string.Empty;
        public static string currentCulture = string.Empty;
        public static string dbBackupPath = ConfigurationManager.AppSettings["Db:BackupPath"] == null ? "" : ConfigurationManager.AppSettings["Db:BackupPath"].ToString();
        public static string[] arrDbBackupTime = ConfigurationManager.AppSettings["Db:BackupTime"] == null ? new string[] { "00:00:00" } : ConfigurationManager.AppSettings["Db:BackupTime"].ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        public static int dbBackupPeriod = ConfigurationManager.AppSettings["Db:BackupPeriod"] == null ? 1 : int.Parse(ConfigurationManager.AppSettings["Db:BackupPeriod"].ToString().Replace(" Days", ""));
        public static bool showDoctorInfo = false;
        public static bool showDoctorTel = false;

        public static string GetConnectionString()
        {
            var encryptString = ConfigurationManager.ConnectionStrings["EntityFrameworkDB"].ToString();
            var normalString = DecryptString(encryptString);
            return normalString;
        }

        public static void ChangeLanguage(string[] urlSegment)
        {
            var culture = "en";
            if (urlSegment.Length > 1)
                if (urlSegment[1].Contains("km"))
                    culture = "km";

            currentCulture = culture;
            CultureInfo ci = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }








        public static string messagePath = string.Empty;
        public static string errorPath = string.Empty;
        public static string hostPathNoLanguage = string.Empty;
        public static string hostPath = string.Empty;
        public static int defaultPageSize = 10;
        public static string defaultOrderBy = "Id";
        public static string signCurrency = "USD";
        public static string signFormCurrency = "$";
        public static string defaultListOrder = "desc";
        public static int defaultNoOfPageLinkList = 5;
        public static string currentController = string.Empty;
        public static string systemCulture = ConfigurationManager.AppSettings["sysCulture"] == null ? "en-US" : ConfigurationManager.AppSettings["sysCulture"].ToString();
        public static string decimalFormat = ConfigurationManager.AppSettings["decimalFormat"] == null ? "N2" : ConfigurationManager.AppSettings["decimalFormat"].ToString();
        public static string defaultCurrency = "$";
        public static string jsDateFormat = systemCulture == "en-US" ? "m-d-Y" : "d-m-Y";
        public static string dateFormat = systemCulture == "en-US" ? "MM-dd-yyyy" : "dd-MM-yyyy";
        public static string dateTimeFormat = systemCulture == "en-US" ? "MM-dd-yyyy HH:mm:ss" : "dd-MM-yyyy HH:mm:ss";
        public static int decimalDigitAterPoint = ConfigurationManager.AppSettings["decimalFormat"] == null ? 0 : int.Parse(ConfigurationManager.AppSettings["decimalFormat"].ToString().Replace("N", ""));
        public static string backupPath = ConfigurationManager.AppSettings["backup_path"] == null ? "" : ConfigurationManager.AppSettings["backup_path"].ToString();
        public static int backupPeriod = ConfigurationManager.AppSettings["backup_period"] == null ? 0 : int.Parse(ConfigurationManager.AppSettings["backup_period"].ToString().Replace("days", ""));
        public static string backupTime = ConfigurationManager.AppSettings["backup_time"] == null ? "" : ConfigurationManager.AppSettings["backup_time"].ToString();
        public static DateTime defaultDate = DateTime.Parse("1900-01-01");
        public static string rielSign = "៛";
        public static string companyPath = ConfigurationManager.AppSettings["company_img_path"] == null ? "" : ConfigurationManager.AppSettings["company_img_path"].ToString();
        public static int threadCheck = ConfigurationManager.AppSettings["thread_check"] == null ? 5 : int.Parse(ConfigurationManager.AppSettings["thread_check"].ToString().Replace("mn", ""));
        public static string dollarSign = "$";
        public static string productLangDisplay = ConfigurationManager.AppSettings["product_lang_display"] == null ? "" : ConfigurationManager.AppSettings["product_lang_display"].ToString();

        public static string successMessage = "The proccess completed!";
        public static string failedMessage = "The process failed!";
        public static string errorMessage = "The Process error!";

        public static void setCultureInfo(string[] urlSegment, string host, int port, string urlSchem, string controller)
        {
            if (urlSegment.Length > 1)
            {
                currentCulture = urlSegment[1].Replace("/", "");
                messagePath = "/" + currentCulture + "/Message/?messageId=";
                errorPath = "/" + currentCulture + "/Error";
                var sport = port == 80 ? "" : ":" + port;
                hostPath = urlSchem + "://" + host + sport + "/" + currentCulture;
                hostPathNoLanguage = urlSchem + "://" + host + sport;
                currentController = hostPath + "/" + controller;
            }
        }

        public static void ChangeLanguage(string culture)
        {
            currentCulture = culture;
            CultureInfo ci = new CultureInfo(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
        }

        public static string GetLanguageChangeUrl(string[] urlSegment)
        {
            string result = string.Empty;
            if (urlSegment.Length > 1)
            {
                var changeToLang = "";
                var lang = urlSegment[1].Replace("/", "");
                if (lang.Equals("km"))
                    changeToLang = "en";
                else
                    changeToLang = "km";

                result = "/" + changeToLang + "/";
                var i = 0;
                foreach (var path in urlSegment)
                {
                    if (i > 1)
                        result += path;
                    i++;
                }

            }
            result = result.Length == 4 ? result.Substring(0, 3) : result;
            return result;
        }

        public static string GetLanguage(string[] urlSegment)
        {
            string result = string.Empty;
            if (urlSegment.Length > 1)
            {
                result = urlSegment[1].Replace("/", "");
            }
            return result;
        }

        public static string AddZeroBeforeId(int id)
        {
            return id.ToString().PadLeft(6, '0');
        }

        public static string convertMonthToString(int month)
        {
            string[] arrEnMonth = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
            return arrEnMonth[month - 1];
        }

        public static string RemoveDefaultCurrency(string amount)
        {
            return amount.Replace(Common.defaultCurrency, "");
        }

        public static string ProductName(string nameEn, String nameKh)
        {
            if (productLangDisplay == "KH")
                return nameKh;
            else if (productLangDisplay == "KH-EN")
                return nameKh + "-" + nameEn;
            else if (productLangDisplay == "EN-KH")
                return nameEn + "-" + nameKh;
            else
                return nameEn;
        }

        public static DateTime CleanUpDateTime(string dateTime)
        {
            DateTime dtResult = DateTime.Now;
            try
            {
                DateTime.Parse(dateTime);
            }
            catch
            {
                string[] arrayDate = dateTime.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                string strA = arrayDate[0];
                string strB = arrayDate[1];
                string strYear = arrayDate[2];
                string newDate = string.Format("{0}-{1}-{2}", strB, strA, strYear);
                dtResult = DateTime.Parse(newDate);
            }
            return dtResult;
        }

        public static String GetCurrentFullDate()
        {
            DateTime d = DateTime.Now;
            string dateString = String.Format("{0:dddd, MMMM d, yyyy}", d);
            return dateString;
        }

        public static String ConcatHost(string url)
        {
            return host + url;
        }

        public static bool IsNumber(string strNumber)
        {
            try
            {
                decimal.Parse(strNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*public static string GetGender(string val)
        {
            string strReturn = "male";
            if (productLangDisplay == "f")
            {
                strReturn = "female";
            }
            return strReturn;
        }*/

        public static string GetGender(string val)
        {
            string strReturn = "male";
            if (val == "F")
            {
                strReturn = "female";
            }
            return strReturn;
        }

        public static string GetPawnStatus(int val)
        {
            string strReturn = "";
            switch (val)
            {
                case 1: strReturn = "New"; break;
                case 2: strReturn = "Renew"; break;
                case 3: strReturn = "Redeem"; break;
                case 4: strReturn = "Void"; break;
                case 5: strReturn = "Expired"; break;
                case 6: strReturn = "Closed"; break;
            }
            return strReturn;
        }

        /*For get value to display*/
        public static string GetValueDisplay(string strValue, string strDefault = "N/A")
        {
            string strReturn = strDefault;
            if (!String.IsNullOrEmpty(strValue))
            {
                strReturn = strValue.ToString().Trim();
            }
            return strReturn;
        }

        /*For get the formate of currency*/
        public static string GetCurrencyFormate(decimal value, decimal strDefault = 0)
        {
            string strValue = "";
            if (value > 0)
                strValue = "$" + String.Format("{0:0.##}", value);
            return strValue;
        }
        /*For get the formate of USD*/
        public static string GetCurrency(decimal strValue, decimal strDefault = 0)
        {
            return String.Format("{0:0.##}", strValue) + signCurrency;
        }
        /*For get the formate of percent*/

        /*For get the formate of percent*/
        public static string GetPercentFormate(decimal strValue)
        {
            var strReturn = (strValue * 1);
            return string.Format("{0:0.##}%", strReturn);
        }

        /*For check datetime for display if null or equal to 01/01/1900 it will show empty*/
        public static string GetDateTimeFormate(DateTime dateTime, string strDefault = "00-00-0000")
        {
            String strReturn = strDefault;
            if (dateTime!=null)
            {
                if (currentCulture.Contains("en"))
                    strReturn = dateTime.ToString("dd-MMM-yyyy");
                else
                    strReturn = dateTime.ToString("dd-MMMM-yyyy");
            }
            return strReturn;
        }

        public static string FormatDateForList(DateTime dateTime)
        {
            String strReturn = "";
            if (dateTime != null)
            {
                if (dateTime.Year != 2099)
                {
                    if (currentCulture.Contains("en"))
                        strReturn = dateTime.ToString("dd-MMM-yyyy");
                    else
                        strReturn = dateTime.ToString("dd-MMMM-yyyy");
                }
            }
            return strReturn;
        }

        /*For check datetime for display if null or equal to 01/01/1900 it will show empty*/
        public static string GetDateTimeFormateWithTimeAndMinute(DateTime dateTime, string strDefault = "00-00-0000")
        {
            String strReturn = strDefault;
            if (!String.IsNullOrEmpty(dateTime.ToString()))
            {                
                if (currentCulture.Contains("en"))
                    strReturn = dateTime.ToString("dd-MMM-yyyy HH:mm");
                else
                    strReturn = dateTime.ToString("dd-MMMM-yyyy HH:mm");
            }
            return strReturn;
        }

        /*For check datetime for display if null or equal to 01/01/1900 it will show empty*/
        public static string GetSearchDateTimeFormate(String dateTime, string strDefault = "00-00-0000")
        {
            String strReturn = strDefault;
            if (!String.IsNullOrEmpty(dateTime))
            {
                var objDate = System.DateTime.Parse(dateTime);
                strReturn = objDate.ToString("dd-MMMM-yyyy");
            }
            return strReturn;
        }

        public static string GetNameMonth(string month)
        {
            string strReturn = "";
            int intM = 0;
            if (int.TryParse(month, out intM) == true)
            {
                System.Globalization.DateTimeFormatInfo mfi = new
                System.Globalization.DateTimeFormatInfo();
                strReturn = mfi.GetMonthName(intM).ToString();
            }
            return strReturn;
        }

        /*For check datetime for display if null or equal to 01/01/1900 it will show empty*/
        public static string GetDateTimeFormateFull(DateTime dateTime, string strDefault = "00-00-0000")
        {
            String strReturn = strDefault;
            if (!String.IsNullOrEmpty(dateTime.ToString()))
            {
                strReturn = dateTime.ToString("dd-MMMM-yyyy HH:mm:ss");
            }
            return strReturn;
        }

        /*For check datetime for display if null or equal to 01/01/1900 it will show empty*/
        public static string GetDatePickerDisplay(DateTime dateTime, string strDefault = "")
        {
            String strReturn = strDefault;
            if (!String.IsNullOrEmpty(dateTime.ToString()))
            {
                if (dateTime.Year != 2099)
                {
                    bool blnChange = false;
                    if (currentCulture == "km")
                    {
                        Common.ChangeLanguage("en");
                        blnChange = true;
                    }
                    strReturn = dateTime.ToString("dd/MMM/yyyy");
                    if (blnChange == true)
                    {
                        Common.ChangeLanguage("km");
                    }
                }
                else
                {
                    strReturn = "";
                }
            }
            return strReturn;
        }

        /*For check datetime for display if null or equal to 01/01/1900 it will show empty*/
        public static string GetDateDetectLanguage(DateTime dateTime, string strDefault = "")
        {
            String strReturn = strDefault;
            if (!String.IsNullOrEmpty(dateTime.ToString()))
            {
                bool blnChange = false;
                if (currentCulture == "km")
                {
                    Common.ChangeLanguage("en");
                    blnChange = true;
                }
                strReturn = dateTime.ToString("dd-MMMM-yyyy");
                if (blnChange == true)
                {
                    Common.ChangeLanguage("km");
                }
            }
            return strReturn;
        }

        public static string GetValueFromDict(Dictionary<string, string> dict, string key)
        {
            string strReturn = String.Empty;
            if (dict.ContainsKey(key))
            {
                strReturn = dict[key];
            }

            return strReturn;
        }

        public static int GetUniqueRandomNumber(int from, int to, List<int> intList)
        {
            int result = 0;
            Random random = new Random();
        Restart:
            int newNumber = random.Next(from, to);
            if (!intList.Contains(newNumber))
            {
                result = newNumber;
                intList.Add(result);
            }
            else
            {
                goto Restart;
            }

            return result;
        }






        #region Encrypt String
        private static string passPhrase = "PaS5$R@sU][x<X#@XDMEW<MADoxbf@$GFRoogRog";        // can be any string
        private static string saltValue = "W@StVXuUYkLI#199$Umr&Mnt$#@5241xSZMLuKQ<&>XzR";   // can be any string
        private static string hashAlgorithm = "SHA1";          // can be "MD5"
        private static int passwordIterations = 2;             // can be any number
        private static string vector = "#$&*910GajLp7q0r";         // must be 16 bytes
        private static int keySize = 256;                      // can be 192 or 128
        /// <summary>
        /// Encrypt String
        /// </summary>
        /// <param name="regularString"></param>
        /// <returns></returns>
        public static string EncryptString(string regularString)
        {
            var result = Cryption.Encrypt(regularString, passPhrase, saltValue, hashAlgorithm, passwordIterations, vector, keySize);
            return result;
        }
        /// <summary>
        /// Decrypt String
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string DecryptString(string encryptString)
        {
            var result = Cryption.Decrypt(encryptString, passPhrase, saltValue, hashAlgorithm, passwordIterations, vector, keySize);
            return result;
        }
        #endregion Encrypt String
    }
}
