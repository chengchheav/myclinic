using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IPatientOperationRepository
    {
        IEnumerable<vPatientOperation> Search(string id, string cboDoct, string searchBy, string keyWord, string startDate, string endDate, out int totalRecords);
    }
}
