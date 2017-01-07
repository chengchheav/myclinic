using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IDoctorOperationRepository
    {
        IEnumerable<vDoctorOperation> Search(string cboDoct, string searchBy, string keyWord, string startDate, string endDate, string orderBy, string order, out int totalRecords);
    }
}
