using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IAuditLogRepository
    {
        IEnumerable<DTOProcessType> GetProcessType();
        IEnumerable<DTOAuditLog> Search(string startDate, string endDate, string cboUser, string cboPro, string orderBy, string order, out int totalRecords);
    }
}
