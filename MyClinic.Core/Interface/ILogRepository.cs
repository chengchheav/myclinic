using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface ILogRepository
    {
        void Add(Log log);
        IEnumerable<Log> Get();
        IEnumerable<DTOAuditLog> Search(string searchBy, string keyWord, string orderBy, string order, int _pageNo, int _pageSize, out int totalRecords);
        IEnumerable<DTOAuditLog> SearchInLogUser(string searchBy, string keyWord, string orderBy, string order, int _pageNo, int _pageSize, out int totalRecords, int idLogUser);
        IEnumerable<DTOAuditLog> SearchByDate(string startDate, string endDate, string cboUser, string cboPro, string orderBy, string order, out int totalRecords, int pageNo, int pageSize);
        //IEnumerable<DTOAuditLog> SearchInLogUserByDate(string startDate, string endDate, string cboUser, string cboPro, string orderBy, string order, out int totalRecords, int _pageNo, int _pageSize, int idLogUser);
    }
}
