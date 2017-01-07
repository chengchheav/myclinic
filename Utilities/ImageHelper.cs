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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;

namespace Utilities
{
    public class ImageHelper
    {
        public static string BaseDateImage(string strPathImage) { 
            string strReturn = "";
            try
            {
                if (File.Exists(strPathImage))
                {
                    string extension = Path.GetExtension(strPathImage);
                    strReturn = "data:image/" + extension + ";base64," + Convert.ToBase64String(File.ReadAllBytes(strPathImage));
                }
            }
            catch(Exception ex){
            
            }
            return strReturn;
        }
        //For process array key from the submit form for boats class
        public static void SaveImage(string pathImage, string fileName, string baseData)
        {
            try
            {
                var base64Data = Regex.Match(baseData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                var imageBytes = Convert.FromBase64String(base64Data);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                if (!Directory.Exists(pathImage))
                {
                    Directory.CreateDirectory(pathImage);
                }
                string fullPathImage = pathImage + "/" + fileName;
                image.Save(fullPathImage);
             
            }catch(Exception ex){
                
            }
        }
    }
}
