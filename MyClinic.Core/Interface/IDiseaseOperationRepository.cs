using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IDiseaseOperationRepository
    {
        IEnumerable<vDiseaseOperation> Search(string id, string transDate, out int totalRecords);
    }
}
