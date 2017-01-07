using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class DiseaseOperationModels
    {
        public IEnumerable<vDiseaseOperation> lstRecords { get; set; }
        public string transDate { get; set; }
        public string diseaseId { get; set; }
        public int totalRecords { get; set; }
    }
}