using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class Sorting
    {
        public static string GetSortOrder(string displayField, string fieldName, string sortingField, string sortType)
        {
            var desc="desc";
            var asc = "asc";            
            StringBuilder stringBuilder = new StringBuilder();
            if (fieldName.Replace(" ", "").Equals(sortingField.Replace(" ", "")))
            {
                var newSortType = sortType == asc ? desc : asc;
                var imagePath = sortType == asc ? Common.host + "/Content/img/up.png" : Common.host + "/Content/img/down.png";
                //stringBuilder.AppendFormat("<a onclick=\"javacript:$common.orderByClick('{0}','{1}');\">{2} <br/><img src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$common.orderByClick('{0}','{1}');\">{2} <img style=\"width:10px;\"src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
            }
            else
            {
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$common.orderByClick('{0}','{1}');\">{2}</a>", fieldName, asc, displayField);
            }

            return stringBuilder.ToString();
        }

        /*For GetSortOrder By Status (use in Pawn Controller)*/
        public static string GetSortOrderByStatus(string displayField, string fieldName, string sortingField, string sortType)
        {
            var desc = "desc";
            var asc = "asc";
            StringBuilder stringBuilder = new StringBuilder();
            if (fieldName.Replace(" ", "").Equals(sortingField.Replace(" ", "")))
            {
                var newSortType = sortType == asc ? desc : asc;
                var imagePath = sortType == asc ? Common.host + "/Content/img/up.png" : Common.host + "/Content/img/down.png";
                //stringBuilder.AppendFormat("<a onclick=\"javacript:$common.orderByClick('{0}','{1}');\">{2} <br/><img src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$common.orderByClick_ByStatus('{0}','{1}');\">{2} <img style=\"width:10px;\"src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
            }
            else
            {
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$common.orderByClick_ByStatus('{0}','{1}');\">{2}</a>", fieldName, asc, displayField);
            }

            return stringBuilder.ToString();
        }

        /*For GetSortOrder By Date (use in Log Controller)*/
        public static string GetSortOrderByDate(string displayField, string fieldName, string sortingField, string sortType)
        {
            var desc = "desc";
            var asc = "asc";
            StringBuilder stringBuilder = new StringBuilder();
            if (fieldName.Replace(" ", "").Equals(sortingField.Replace(" ", "")))
            {
                var newSortType = sortType == asc ? desc : asc;
                var imagePath = sortType == asc ? Common.host + "/Content/img/up.png" : Common.host + "/Content/img/down.png";
                //stringBuilder.AppendFormat("<a onclick=\"javacript:$common.orderByClick('{0}','{1}');\">{2} <br/><img src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$managelog.orderByClickLogByDate('{0}','{1}');\">{2} <img style=\"width:10px;\"src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
            }
            else
            {
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$managelog.orderByClickLogByDate('{0}','{1}');\">{2}</a>", fieldName, asc, displayField);
            }

            return stringBuilder.ToString();
        }

        /*For GetSortOrder By Category (use in Item Controller)*/
        public static string GetSortOrderByCategory(string displayField, string fieldName, string sortingField, string sortType)
        {
            var desc = "desc";
            var asc = "asc";
            StringBuilder stringBuilder = new StringBuilder();
            if (fieldName.Replace(" ", "").Equals(sortingField.Replace(" ", "")))
            {
                var newSortType = sortType == asc ? desc : asc;
                var imagePath = sortType == asc ? Common.host + "/Content/img/up.png" : Common.host + "/Content/img/down.png";
                //stringBuilder.AppendFormat("<a onclick=\"javacript:$common.orderByClick('{0}','{1}');\">{2} <br/><img src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$common.orderByClick_ByCategory('{0}','{1}');\">{2} <img style=\"width:10px;\"src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
            }
            else
            {
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$common.orderByClick_ByCategory('{0}','{1}');\">{2}</a>", fieldName, asc, displayField);
            }

            return stringBuilder.ToString();
        }

        public static string GetSortOrderManageRecord(string displayField, string fieldName, string sortingField, string sortType)
        {
            var desc = "desc";
            var asc = "asc";
            StringBuilder stringBuilder = new StringBuilder();
            if (fieldName.Replace(" ", "").Equals(sortingField.Replace(" ", "")))
            {
                var newSortType = sortType == asc ? desc : asc;
                var imagePath = sortType == asc ? Common.host + "/Content/img/up.png" : Common.host + "/Content/img/down.png";
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$manage.orderManageRecords('{0}','{1}');\">{2} <img style=\"width:10px;\"src=\"{3}\" class=\"order\"/></a>", fieldName, newSortType, displayField, imagePath);
            }
            else
            {
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$manage.orderManageRecords('{0}','{1}');\">{2}</a>", fieldName, asc, displayField);
            }

            return stringBuilder.ToString();
        }

        public static string GetSortOrderLink(string displayField, string fieldName, string sortingField, string sortType, string strUrl="")
        {
            var desc = "desc";
            var asc = "asc";
            string strLinkGet = strUrl + "&orderBy=" + fieldName;
            StringBuilder stringBuilder = new StringBuilder();
            if (fieldName.Replace(" ", "").Equals(sortingField.Replace(" ", "")))
            {
                var newSortType = sortType == asc ? desc : asc;
                strLinkGet = strLinkGet + "&order=" + newSortType;
                var imagePath = sortType == asc ? Common.host + "/Content/img/up.png" : Common.host + "/Content/img/down.png";
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$manage.orderManageRecordsWithLink('{0}', '{1}', '{2}');\">{3} <img style=\"width:10px;\"src=\"{4}\" class=\"order\"/></a>", strLinkGet, fieldName, sortType, displayField, imagePath);
            }
            else
            {
                strLinkGet = strLinkGet + "&order=asc";
                stringBuilder.AppendFormat("<a style=\"text-decoration:none;\" onclick=\"javacript:$manage.orderManageRecordsWithLink('{0}','{1}', '{2}');\">{3}</a>", strLinkGet, fieldName, sortType, displayField);
            }

            return stringBuilder.ToString();
        }
    }
}
