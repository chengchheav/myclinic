using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IPrescriptionRepository
    {
        void Add(Prescription pawner);
        void Update(Prescription pawner);
        IEnumerable<Prescription> Get();
        void Delete(int id);
        void DeleteAll(int id);
        IEnumerable<vMedicineDiagnosis> GetPrescriptionDiagnosis(int DiagId);
        IEnumerable<vPrescription> VSearch(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords);
        IEnumerable<Prescription> Search(string searchBy, string keyWord, string orderBy, string order, int _pageNo, int _pageSize, out int totalRecords);
        void UpdateFieldChangedOnly(Prescription originPawner, Prescription newPawner);
        Prescription GetById(int id);
    }
}
