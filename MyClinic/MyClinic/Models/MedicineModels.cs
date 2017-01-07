using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class MedicineModels
    {

        public IEnumerable<DTOMedicine> medicines { get; set; }
        public Medicine medicine { get; set; }
        public PageUtilities pageUtilities { get; set; }
        public DTOMedicine dtomedicine { get; set; }

        public PatientEdit patientEdit { get; set; }
        public PatientAdd patientAdd { get; set; }
        public Boolean checkPost { get; set; }
        public Boolean checkError { get; set; }
        public IEnumerable<MedicineType> medicineTypeRecords { get; set; }
        public IEnumerable<MedicineUnit> unitRecords { get; set; }
    }
}