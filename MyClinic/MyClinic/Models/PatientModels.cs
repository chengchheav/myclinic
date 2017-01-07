using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class PatientModels
    {

        public IEnumerable<Patient> patients { get; set; }
        public IEnumerable<DTODiagnosis> listDiagnosis { get; set; }
        public Patient patient { get; set; }
        public PageUtilities pageUtilities { get; set; }

        public PatientEdit patientEdit { get; set; }
        public PatientAdd patientAdd { get; set; }
        public Boolean checkPost { get; set; }
        public Boolean checkError { get; set; }
    }
}