using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{

    public interface IDiagnosisRepository
    {
        IEnumerable<DTODiagnosis> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords);
        void Add(Diagnosis diagnosis);
        void UpdateFieldChangedOnly(Diagnosis originalDiagnosis, Diagnosis newDiagnosis);
        Diagnosis GetDiagnosisById(int id);
        DTODiagnosis GetDTODiagnosisById(int id);
        IEnumerable<Diagnosis> GetDiagnosisByPatientId(int patientId);
        IEnumerable<DTODiagnosis> GetListDiagnosisByPatientId(int patientId);
        IEnumerable<Prescription> GetPrescriptionByDiagnosisId(int DiagnosisId);
        IEnumerable<SymptomType> GetSymptomType();
        IEnumerable<Disease> GetDisease();

        int GetDiagnosisCycle(int PatientId);
    }

}
