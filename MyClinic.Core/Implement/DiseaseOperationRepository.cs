using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MyClinic.Infrastructure;
using System.Data.SqlClient;
using log4net;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace MyClinic.Core
{
    public class DiseaseOperationRepository : IDiseaseOperationRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public DiseaseOperationRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<vDiseaseOperation> Search(string id, string transDate, out int totalRecords)
        {
            IEnumerable<vDiseaseOperation> lstRecords = null;
            try
            {
                int diseaseId = 0;
                int.TryParse(id, out diseaseId);
                int withDate = 0;
                var objtransDate = System.DateTime.Now;
                if (string.IsNullOrEmpty(transDate) == false)
                {
                    withDate = 1;
                    objtransDate = System.DateTime.Parse(transDate);
                }
                lstRecords = db.Database.SqlQuery<vDiseaseOperation>("GetDiseaseSummary @diseaseId, @transDate, @withDate", new SqlParameter("@diseaseId", diseaseId), new SqlParameter("@transDate", objtransDate), new SqlParameter("@withDate", withDate)).ToList();
                totalRecords = lstRecords.AsEnumerable().Count();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstRecords;
        }
    } 
}
