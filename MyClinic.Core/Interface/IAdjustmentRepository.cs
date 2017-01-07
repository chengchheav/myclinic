using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface IAdjustmentRepository
    {
        IEnumerable<DTOAuditLog> Search(string startDate, string endDate, string orderBy, string order, int typeAction, out int totalRecords);
    }
}
