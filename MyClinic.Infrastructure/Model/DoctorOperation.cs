using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{

    [Table("vDoctorOperation")]
    public class vDoctorOperation
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Symptom { get; set; }
        public int DiagnoseCycle { get; set; }
        public DateTime DiagnoseDate { get; set; }
        public int DiagnoseBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int Status { get; set; }
        public int SymptomType { get; set; }
        public int DiseaseId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public string Image { get; set; }
        public string Dis_Name { get; set; }
        public string Sym_Type { get; set; }
    }
}
