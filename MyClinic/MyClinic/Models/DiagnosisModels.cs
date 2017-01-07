using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class DiagnosisModels
    {

        public IEnumerable<DTODiagnosis> dtodiagnosiss { get; set; }
        public Diagnosis diagnosis { get; set; }
        public DTODiagnosis dtodiagnosis { get; set; }
        public PageUtilities pageUtilities { get; set; }
        public IEnumerable<vMedicineDiagnosis> medicineRecords { get; set; }
        public IEnumerable<Employee> EmployeeLists { get; set; }
        public DTOPatient dtopatient { get; set; }
        public IEnumerable<SymptomType> symptomTypes { get; set; }
        public IEnumerable<Disease> diseases { get; set; }

        public DiagnosisAdd diagnosisAdd { get; set; }
        public DiagnosisEdit diagnosisEdit { get; set; }
        public Boolean checkPost { get; set; }
        public Boolean checkError { get; set; }
    }
}