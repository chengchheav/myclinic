using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IMonthlyOperationRepository
    {
        IEnumerable<vMonthlyOperation> Search(string searchBy, string keyWord, string startDate, string endDate, string orderBy, string order, out int totalRecords);
    }
}
