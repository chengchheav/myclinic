using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class LogModels
    {
        public IEnumerable<DTOAuditLog> logs { get; set; }
        public PageUtilities pageUtilities { get; set; }
        public IEnumerable<DTOAuditLog> logRecords { get; set; }



        public IEnumerable<DTOAuditLog> lstRecords { set; get; }
        public IEnumerable<User> lstUsers { set; get; }
        public IEnumerable<DTOProcessType> lstProcessTypes { set; get; }
        public IEnumerable<User> users { get; set; }

        public string startDate { get; set; }
        public string endDate { get; set; }
        public string orderBy { get; set; }
        public string order { get; set; }
        public int totalRecords { get; set; }
        public string cboUser { get; set; }
        public string cboPro { get; set; }
    }
}