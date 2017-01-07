using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{
    public class DiagnosisAdd
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        [StringLength(500)]
        public string Symptom { get; set; }

        public int DiagnoseCycle { get; set; }

        public int Status { get; set; }

        public DateTime DiagnoseDate { get; set; }

        public int DiagnoseBy { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string Patient_Name { get; set; }

        public string Diagnose_Name { get; set; }

        public string Disease_Name { get; set; }

        public int SymptomType { get; set; }

        public int DiseaseId { get; set; }
    }

    public class DiagnosisEdit
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        [StringLength(500)]
        public string Symptom { get; set; }

        public int DiagnoseCycle { get; set; }

        public int Status { get; set; }

        public DateTime DiagnoseDate { get; set; }

        public int DiagnoseBy { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ExpiredDate { get; set; }

        
        public string Patient_Name { get; set; }

        
        public string Diagnose_Name { get; set; }

        public string Disease_Name { get; set; }

        public int SymptomType { get; set; }

        public int DiseaseId { get; set; }
    }
}
