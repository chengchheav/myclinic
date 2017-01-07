using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{

    public interface IPatientRepository
    {
        IEnumerable<Patient> Get();
        IEnumerable<Patient> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords);
        void Add(Patient patient);
        void UpdateFieldChangedOnly(Patient originalPatient, Patient newPatient);
        Patient GetPatientById(int id);
        IEnumerable<DTOPatient> GetPatientListByName(string name);
        DTOPatient GetDTOPatientById(int id);
    }

}
