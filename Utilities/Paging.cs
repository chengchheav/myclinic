using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// Summary description for Paging
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// Get Paging
        /// </summary>
        /// <param name="totalRecords"></param>
        /// <param name="pageNo"></param>
        /// <param name="noOfPageLinkList"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageStatus"></param>
        /// <param name="pageLinkClickFunction"></param>
        /// <returns></returns>
        public static string GetPaging(int totalRecords, int pageNo, int pageSize, int pageStatus, int noOfPageLinkList, string pageLinkClickFunction,string orderByField, string orderType)        
        {            
            string strResult = "";
            if (totalRecords > 0)
            {
                string lang = Common.currentCulture;
                string next = "Next";
                string last = "Last";
                string previous = "Previous";
                string first = "First";
                if (lang.Equals("km"))
                {
                    next = "បន្ទាប់";
                    last = "ចុងក្រោយបង្អស់";
                    previous = "ខាងមុខ";
                    first = "មុខគេបង្អស់";
                }

                var fullPageList = noOfPageLinkList;
                StringBuilder objStringBuilder = new StringBuilder();
                StringBuilder objSBNoSelectedLink = new StringBuilder();
                int intSelectedPageNo = pageNo;
                                
                //Find total of page
                int intTotalNoOfPage = totalRecords > pageSize ? totalRecords / pageSize : 1;
                int intRemainRecord = totalRecords > pageSize ? totalRecords % pageSize : 0;
                intTotalNoOfPage = intRemainRecord > 0 ? intTotalNoOfPage + 1 : intTotalNoOfPage;

                if (intTotalNoOfPage > 1)
                {

                    noOfPageLinkList = (intTotalNoOfPage - pageNo) < noOfPageLinkList ? (intTotalNoOfPage - pageNo) + 1 : noOfPageLinkList;
                    noOfPageLinkList = noOfPageLinkList > intTotalNoOfPage ? intTotalNoOfPage : noOfPageLinkList;

                    if (pageStatus == 0)//if previous click
                        noOfPageLinkList = fullPageList > intTotalNoOfPage ? intTotalNoOfPage : fullPageList;

                    
                    objStringBuilder.Append("<div class=\"Pagination number-pagin\"><ul>");
                    int j = 0;
                    if (intTotalNoOfPage > fullPageList)//if total no of page bigger than default page list
                    {
                        if (pageNo > noOfPageLinkList)
                        {
                            j += pageNo - noOfPageLinkList;
                            objStringBuilder.AppendFormat("<li><a href=\"javascript:{2}({0},0,'" + orderByField + "','" + orderType + "');\">{1}</a></li> | ", 1, first, pageLinkClickFunction);
                            objStringBuilder.AppendFormat("<li><a href=\"javascript:{2}({0},0,'" + orderByField + "','" + orderType + "');\">< {1}</a></li> | ", pageNo - 1, previous, pageLinkClickFunction);
                        }
                        int n = 0;
                        if (pageStatus == 2)
                        {
                            for (int i = 1; i <= noOfPageLinkList; i++)
                            {
                                if (pageNo + i - 1 == intSelectedPageNo)
                                    objStringBuilder.AppendFormat("<li style=\"background:#333333;\"><a><span style=\"color:white;\">{0}</span></a></li> | ", pageNo);
                                else
                                    objStringBuilder.AppendFormat("<li><a href=\"javascript:{1}({0},1,'" + orderByField + "','" + orderType + "');\"><span>{0}</span></a></li> | ", pageNo + i - 1, pageLinkClickFunction);

                                n = intSelectedPageNo + 1;
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= noOfPageLinkList; i++)
                            {
                                if (i + j == intSelectedPageNo)
                                    objStringBuilder.AppendFormat("<li style=\"background-color:#333333;\"><a><span style=\"color:white;\">{0}</span></a></li> | ", i + j);
                                else
                                    objStringBuilder.AppendFormat("<li><a href=\"javascript:{1}({0},1,'" + orderByField + "','" + orderType + "');\"><span>{0}</span></a></li> | ", i + j, pageLinkClickFunction);

                                //n = i + j + 1;
                                n = intSelectedPageNo + 1;
                            }
                        }

                        if (pageNo < intTotalNoOfPage)
                        {
                            strResult = objStringBuilder.AppendFormat("<li><a href=\"javascript:{2}({0},2,'" + orderByField + "','" + orderType + "');\">{1} ></a></li> | ", n, next, pageLinkClickFunction).ToString();
                            strResult = objStringBuilder.AppendFormat("<li><a href=\"javascript:{2}({0},2,'" + orderByField + "','" + orderType + "');\">{1}</a></li>", intTotalNoOfPage, last, pageLinkClickFunction).ToString();
                        }
                        else
                        {
                            strResult = objStringBuilder.ToString();
                            strResult = strResult.Substring(0, strResult.Length - 3);
                        }
                    }
                    else
                    {
                        noOfPageLinkList = intTotalNoOfPage;
                        for (int i = 1; i <= noOfPageLinkList; i++)
                        {
                            if (i + j == intSelectedPageNo)
                                objStringBuilder.AppendFormat("<li style=\"background-color:#333333;\"><a><span style=\"color:white;\">{0}</span></a></li> | ", i + j);
                            else
                                objStringBuilder.AppendFormat("<li><a href=\"javascript:{1}({0},1,'" + orderByField + "','" + orderType + "');\"><span>{0}</span></a></li> | ", i + j, pageLinkClickFunction);
                        }

                        strResult = objStringBuilder.ToString();
                        strResult = strResult.Substring(0, strResult.Length - 3);
                    }
                    strResult = strResult + "</ul></div>";
                }
            }

            return strResult;
        }
        /// <summary>
        /// Get Line Per Page
        /// </summary>
        /// <param name="totalRecord"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /*public static string getItemPerPage(int totalRecord, int pageSize, string requestAction = "$common.itemPerPageChange();")
        {
            StringBuilder strItemPerPage = new StringBuilder();

            if (totalRecord > 0)
            {
                int[] pageRange = new int[] { 10, 20, 50, 100 };
                strItemPerPage.AppendFormat("            <div style=\"color: #666666; font-size: 12px; line-height: 18px; position: absolute; right: 58px; top: 0;  width: 120px;margin-right:10px;\"class=\"items\">{0} ", "<span style=\"position: absolute;font-size:13px; margin-left:-7px;\">Items Per Page:</span>");
                strItemPerPage.AppendLine("          <select style=\"margin-left: 94px; margin-top: 10px;  position: relative;  top: -17px;  width: 80px;\" id=\"cmbItemPerPage\" onchange=\"" + requestAction + "\">");
                for (int i = 0; i < pageRange.Length; i++)
                {
                    int value = pageRange[i];
                    if (value == pageSize)
                        strItemPerPage.AppendFormat("<option value=\"{0}\" selected=\"true\">{0}</option>", value);
                    else
                        strItemPerPage.AppendFormat("<option value=\"{0}\">{0}</option>", value);
                }
                strItemPerPage.AppendLine("            </select></div>");
            }
            return strItemPerPage.ToString();
        }*/

        public static string getItemPerPage(int totalRecord, int pageSize, string orderByField, string orderType, string requestAction = "$common.itemPerPageChange")
        {
            StringBuilder strItemPerPage = new StringBuilder();

            if (totalRecord > 0)
            {
                string lang = Common.currentCulture;
                string itemPerpage = "Item per page";
                
                if (lang.Equals("km"))
                {
                    itemPerpage = "ចំនួនជួរក្នុងមួយទំព័រ";
                }

                int[] pageRange = new int[] { 10, 20, 50, 100 };
                strItemPerPage.AppendFormat("            <div style=\"color: #666666; font-size: 12px; line-height: 18px; position: absolute; right: 58px; top: 0;  width: 120px;margin-right:10px;\"class=\"items\">{0} ", "<span style=\"position: absolute;font-size:13px; margin-left:-7px;\">" + itemPerpage + ":</span>");
                strItemPerPage.AppendLine("          <select style=\"margin-left: 94px; margin-top: 10px;  position: relative;  top: -17px;  width: 80px;\" id=\"cmbItemPerPage\" onchange=\""+ requestAction +"('" + orderByField + "','" + orderType + "');\">");
                for (int i = 0; i < pageRange.Length; i++)
                {
                    int value = pageRange[i];
                    if (value == pageSize)
                        strItemPerPage.AppendFormat("<option value=\"{0}\" selected=\"true\">{0}</option>", value);
                    else
                        strItemPerPage.AppendFormat("<option value=\"{0}\">{0}</option>", value);
                }
                strItemPerPage.AppendLine("            </select></div>");
            }
            return strItemPerPage.ToString();
        }

        /*For Get ItemPerPage By Status (use in Pawn Controller)*/
        public static string getItemPerPageByStatus(int totalRecord, int pageSize, string orderByField, string orderType)
        {
            StringBuilder strItemPerPage = new StringBuilder();

            if (totalRecord > 0)
            {
                string lang = Common.currentCulture;
                string itemPerpage = "Item per page";

                if (lang.Equals("km"))
                {
                    itemPerpage = "ចំនួនជួរក្នុងមួយទំព័រ";
                }

                int[] pageRange = new int[] { 10, 20, 50, 100 };
                strItemPerPage.AppendFormat("            <div style=\"color: #666666; font-size: 12px; line-height: 18px; position: absolute; right: 58px; top: 0;  width: 120px;margin-right:10px;\"class=\"items\">{0} ", "<span style=\"position: absolute;font-size:13px; margin-left:-7px;\">" + itemPerpage + ":</span>");
                strItemPerPage.AppendLine("          <select style=\"margin-left: 94px; margin-top: 10px;  position: relative;  top: -17px;  width: 80px;\" id=\"cmbItemPerPage\" onchange=\"$common.itemPerPageChangeByStatus('"+orderByField+"','"+orderType+"');\">");
                for (int i = 0; i < pageRange.Length; i++)
                {
                    int value = pageRange[i];
                    if (value == pageSize)
                        strItemPerPage.AppendFormat("<option value=\"{0}\" selected=\"true\">{0}</option>", value);
                    else
                        strItemPerPage.AppendFormat("<option value=\"{0}\">{0}</option>", value);
                }
                strItemPerPage.AppendLine("            </select></div>");
            }
            return strItemPerPage.ToString();
        }

        /*For Get ItemPerPage By Category (use in Item Controller)*/
        public static string getItemPerPageByCategory(int totalRecord, int pageSize, string orderByField, string orderType)
        {
            StringBuilder strItemPerPage = new StringBuilder();

            if (totalRecord > 0)
            {
                string lang = Common.currentCulture;
                string itemPerpage = "Item per page";

                if (lang.Equals("km"))
                {
                    itemPerpage = "ចំនួនជួរក្នុងមួយទំព័រ";
                }

                int[] pageRange = new int[] { 10, 20, 50, 100 };
                strItemPerPage.AppendFormat("            <div style=\"color: #666666; font-size: 12px; line-height: 18px; position: absolute; right: 58px; top: 0;  width: 120px;margin-right:10px;\"class=\"items\">{0} ", "<span style=\"position: absolute;font-size:13px; margin-left:-7px;\">"+itemPerpage+":</span>");
                strItemPerPage.AppendLine("          <select style=\"margin-left: 94px; margin-top: 10px;  position: relative;  top: -17px;  width: 80px;\" id=\"cmbItemPerPage\" onchange=\"$common.itemPerPageChangeByCategory('" + orderByField + "','" + orderType + "');\">");
                for (int i = 0; i < pageRange.Length; i++)
                {
                    int value = pageRange[i];
                    if (value == pageSize)
                        strItemPerPage.AppendFormat("<option value=\"{0}\" selected=\"true\">{0}</option>", value);
                    else
                        strItemPerPage.AppendFormat("<option value=\"{0}\">{0}</option>", value);
                }
                strItemPerPage.AppendLine("            </select></div>");
            }
            return strItemPerPage.ToString();
        }

        /// <summary>
        /// Get Result Information
        /// </summary>
        /// <param name="totalRecord"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static string GetResultInfo(int totalRecord, int pageNo, int pageSize)
        {
            string strResult = "";
            string lang = Common.currentCulture;
            string resultMsg = "No Record Found.";
            string result = "Result";
            string of = "of";
            if (lang.Equals("km"))
            {
                resultMsg = "អត់មានទិន្ន័យ។";
                result = "លទ្ធផល";
                of = "នៃ";
            }
            if (totalRecord > 0)
            {                

                int intFrom = ((pageNo - 1) * pageSize) + 1;
                int intTo = pageNo * pageSize > totalRecord ? totalRecord : pageNo * pageSize; 

                strResult = "<div style=\"color: #666666;float: right;line-height: 18px;position: absolute;right: 195px;text-align: right;margin-right:10px;\"class=\"pageResult\"> " + result + " <strong>" + intFrom + "</strong> - <strong>" + intTo + "</strong> " + of + " <strong>" + totalRecord.ToString() + "</strong> |</div>";
            }
            else
            {
                strResult = "<span class=\"red\">" + resultMsg + "</span>";
            }
            return strResult;
        }
        /// <summary>
        /// Get Hidden Infomation for oder
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static string GetHiddenInfo(string orderBy, string order)
        {
            string result = "";
            result = "<input type=\"hidden\" value=\"" + orderBy + "\" id=\"orderBy\" /><input type=\"hidden\" value=\"" + order + "\" id=\"order\" />";
            return result;
        }
    }
}