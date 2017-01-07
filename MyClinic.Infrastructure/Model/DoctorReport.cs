﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyClinic.Infrastructure
{

    [Table("vDailyOperation")]
    public class DoctorReport
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public string Symptom { get; set; }
        public int DiagnoseCycle { get; set; }
        public int Status { get; set; }
        public DateTime DiagnoseDate { get; set; }
        public int DiagnoseBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ExpiredDate { get; set; }

        public string SymptomType { get; set; }
        public string Patient_Name { get; set; }
        public string Patient_Sex { get; set; }
        public int Patient_Age { get; set; }
        public string Patient_Tel { get; set; }
        public string Patient_Image { get; set; }
        public DateTime Patient_Dob { get; set; }

        public string Created_Name { get; set; }
    }
}