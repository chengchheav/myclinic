using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    public class PrescriptionAdd
    {
        public int DiagnosisId { get; set; }
        public List<string> Id { get; set; }
        public List<string> MedicineId { get; set; }
        public List<string> Medicine { get; set; }
        public List<string> Qty { get; set; }
        public List<string> MedicineType { get; set; }
        public List<string> Morning { get; set; }
        public List<string> Noon { get; set; }
        public List<string> Night { get; set; }
        public List<string> Remark { get; set; }
        public List<string> UsageId { get; set; }
    }

    public class PrescriptionEdit
    {
        public int DiagnosisId { get; set; }
        public List<string> Id { get; set; }
        public List<string> MedicineId { get; set; }
        public List<string> Medicine { get; set; }
        public List<string> Qty { get; set; }
        public List<string> MedicineType { get; set; }
        public List<string> Morning { get; set; }
        public List<string> Noon { get; set; }
        public List<string> Night { get; set; }
        public List<string> Remark { get; set; }
        public List<string> UsageId { get; set; }
    }
}
