using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    [Table("vPrescription")]
    public class vPrescription
    {
        public int Id { get; set; }
        public int DiagnosisId { get; set; }
        public int MedicineId { get; set; }
        public string Qty { get; set; }
        public string MedicineType { get; set; }
        public string Morning { get; set; }
        public string Noon { get; set; }
        public string Night { get; set; }
        public int UsageId { get; set; }
        public string Usage_Name { get; set; }
        public int Status { get; set; }
        public int PatientId { get; set; }
        public string Symptom { get; set; }
        public int DiagnoseCycle { get; set; }
        public DateTime DiagnoseDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string Image { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Sex { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Tel { get; set; }
        public string Emp_Skill { get; set; }
        public string Med_Name { get; set; }
    }
}
