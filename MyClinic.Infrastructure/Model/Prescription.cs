using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("Prescription")]
    public class Prescription
    {
        //[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DiagnosisId { get; set; }
        public int MedicineId { get; set; }
        public string Qty { get; set; }
        public string MedicineType { get; set; }
        public string Morning { get; set; }
        public string Noon { get; set; }
        public string Night { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public int UsageId { get; set; }
        
    }

    [Table("vMedicineDiagnosis")]
    public class vMedicineDiagnosis
    {
        public int Id { get; set; }
        public int DiagnosisId { get; set; }
        public int MedicineId { get; set; }
        public string Qty { get; set; }
        public string MedicineType { get; set; }
        public string Morning { get; set; }
        public string Noon { get; set; }
        public string Night { get; set; }
        public string Remark { get; set; }
        public int Status { get; set; }
        public string Med_Name { get; set; }
        public int UsageId { get; set; }
        public string Use_Name { get; set; }
        
    }
}
