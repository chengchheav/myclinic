using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class PrintPrescriptionModels
    {
        public IEnumerable<vMedicineDiagnosis> medicineRecords { get; set; }
        public DTODiagnosis dtoDiagnosis { get; set; }
    }
}