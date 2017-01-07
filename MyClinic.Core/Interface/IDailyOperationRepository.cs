using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IDailyOperationRepository
    {
        IEnumerable<vDailyOperation> SearchByDisease(string diseaseId, string orderBy, string order, out int totalRecords);
        IEnumerable<vDailyOperation> Search(string searchBy, string keyWord, string startDate, string endDate, string orderBy, string order, out int totalRecords);
    }
}
