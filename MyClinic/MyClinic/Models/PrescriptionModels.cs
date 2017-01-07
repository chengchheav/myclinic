using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class PrescriptionModels
    {
        public IEnumerable<Prescription> records { get; set; }
        public Prescription record { get; set; }
        public PageUtilities pageUtilities { get; set; }
        public PrescriptionAdd validAdd { get; set; }
        public PrescriptionEdit validEdit { get; set; }
        public Boolean checkPost { get; set; }
        public Boolean checkError { get; set; }

        public IEnumerable<MedicineType> medicineTypeRecords { get; set; }
        public IEnumerable<Usage> usageRecords { get; set; }

        public DTODiagnosis dtoDiagnosis { get; set; }
        public IEnumerable<vPrescription> vRecords { get; set; }
        public IEnumerable<vMedicineDiagnosis> medicineRecords { get; set; }

        public Patient patient { get; set; }

        public Disease disease { get; set; }
    }
}