using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{

    public interface IMedicineRepository
    {
        IEnumerable<Medicine> Get();
        IEnumerable<AutoMedicine> AutoMedicine(int status);
        IEnumerable<DTOMedicine> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords);
        void Add(Medicine Medicine);
        void UpdateFieldChangedOnly(Medicine originalMedicine, Medicine newMedicine);
        Medicine GetMedicineById(int id);
        DTOMedicine GetDTOMedicineById(int id);
        IEnumerable<Prescription> GetPrescriptionByMedicineId(int medicineId);

    }

}
