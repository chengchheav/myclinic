using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class MonthlyOperationModels
    {
        public IEnumerable<vMonthlyOperation> lstRecords { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string orderBy { get; set; }
        public string order { get; set; }
        public int totalRecords { get; set; }
    }
}