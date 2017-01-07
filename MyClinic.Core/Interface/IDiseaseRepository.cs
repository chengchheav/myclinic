using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{

    public interface IDiseaseRepository
    {
        IEnumerable<Disease> Get();
        IEnumerable<Disease> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords);
        void Add(Disease Disease);
        void UpdateFieldChangedOnly(Disease originalDisease, Disease newDisease);
        Disease GetDiseaseById(int id);
        IEnumerable<Diagnosis> GetDiagnosisByDiseaseId(int DiseaseId);

        DTODisease GetDTODiseaseById(int id);


        IEnumerable<Disease> GetDiseaseListByName(string name);
    }

}
