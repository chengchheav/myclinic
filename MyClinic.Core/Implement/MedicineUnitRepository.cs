using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MyClinic.Infrastructure;
using System.Data.SqlClient;
using System.Data.Entity;

namespace MyClinic.Core
{
    public class MedicineUnitRepository : IMedicineUnitRepository
    {
        MyDbContext db;
        public MedicineUnitRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<MedicineUnit> Get()
        {
            IEnumerable<MedicineUnit> records = null;
            try
            {
                records = db.MedicineUnit.OrderByDescending(x => x.Id).ToList(); 
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return records;
        }
    } 
}
