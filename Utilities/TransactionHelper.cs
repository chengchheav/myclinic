using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace BoatTEST.Classified.Core.Utilities
{
    public class TransactionHelper
    {
        //For process array key from the submit form for boats class
        public static Dictionary<string, string> processGenerateArrayAllProperty<T>(T objItem)
        {
            var dictBoats = new Dictionary<string, string>();
            var properties = objItem.GetType().GetProperties();
            foreach (var p in properties)
            {
                string name = p.Name;
                var strValue = HttpContext.Current.Request.Form[name] == null ? String.Empty : HttpContext.Current.Request.Form[name].Trim();
                if (HttpContext.Current.Request.Form[name] != null)
                {
                    dictBoats[name] = strValue.Trim();
                }
                else {
                    dictBoats[name] = String.Empty;
                }
            }
            return dictBoats;
        }

        //For process array key from the submit form for boats class
        public static Dictionary<string, string> processGenerateArray<T>(T objItem)
        {
            var dictBoats = new Dictionary<string, string>();
            var properties = objItem.GetType().GetProperties();
            foreach (var p in properties)
            {
                string name = p.Name;
                var strValue = HttpContext.Current.Request.Form[name] == null ? String.Empty : HttpContext.Current.Request.Form[name].Trim();
                if (HttpContext.Current.Request.Form[name] != null)
                {
                    dictBoats[name] = strValue.Trim();
                }
            }
            return dictBoats;
        }

        public static T TransDict<T>(Dictionary<string, string> item, object objItem)
        {
            var properties = objItem.GetType().GetProperties();
            foreach (var p in properties)
            {
                string name = p.Name;
                if (item.ContainsKey(name))
                {
                    var strValue = item[name];
                    if (strValue == null)
                    {
                        p.SetValue(objItem, strValue, null); continue;
                    }

                    if (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                    {
                        DateTime objDate;
                        DateTime.TryParse(strValue, out objDate);
                        p.SetValue(objItem, objDate, null);
                    }
                    else if (p.PropertyType == typeof(String))
                    {
                        p.SetValue(objItem, strValue, null);
                    }
                    else if (p.PropertyType == typeof(int) || p.PropertyType == typeof(int?))
                    {
                        int number = 0;
                        int.TryParse(strValue, out number);
                        p.SetValue(objItem, number, null);
                    }
                    else if (p.PropertyType == typeof(Int64) || p.PropertyType == typeof(Int64?))
                    {
                        Int64 number = 0;
                        Int64.TryParse(strValue, out number);
                        p.SetValue(objItem, number, null);
                    }
                    else if (p.PropertyType == typeof(double) || p.PropertyType == typeof(double?))
                    {
                        double dblNumber = 0;
                        double.TryParse(strValue, out dblNumber);
                        p.SetValue(objItem, dblNumber, null);
                    }
                    else if (p.PropertyType == typeof(double) || p.PropertyType == typeof(double?))
                    {
                        double dblNumber = 0;
                        double.TryParse(strValue, out dblNumber);
                        p.SetValue(objItem, dblNumber, null);
                    }
                    else if (p.PropertyType == typeof(float) || p.PropertyType == typeof(float?))
                    {
                        float fltNumber = 0;
                        float.TryParse(strValue, out fltNumber);
                        p.SetValue(objItem, fltNumber, null);
                    }
                    else if (p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?))
                    {
                        decimal dclNumber = 0;
                        decimal.TryParse(strValue, out dclNumber);
                        p.SetValue(objItem, dclNumber, null);
                    }
                    else if (p.PropertyType == typeof(bool) || p.PropertyType == typeof(bool?))
                    {
                        bool bolNumber = false;
                        if (!String.IsNullOrEmpty(strValue))
                        {
                            if (strValue == "true" || strValue == "1")
                            {
                                bolNumber = true;
                            }
                            else
                            {
                                bolNumber = false;
                            }
                        }
                        else
                        {
                            bolNumber = false;
                        }

                        p.SetValue(objItem, bolNumber, null);
                    }
                }
            }
            return (T) objItem;
        }

        
        /*For check url valid or not*/
        public static bool urlExists(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.AllowAutoRedirect = false;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                if (res.StatusCode == HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
