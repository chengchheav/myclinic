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
    public class MedicineTypeRepository : IMedicineTypeRepository
    {
        MyDbContext db;
        public MedicineTypeRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<MedicineType> Get()
        {
            IEnumerable<MedicineType> records = null;
            try
            {
                records = db.MedicineType.OrderByDescending(x => x.Id).ToList(); 
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return records;
        }
        /*For Get Usage*/
        public IEnumerable<Usage> GetUsage()
        {
            IEnumerable<Usage> records = null;
            try
            {
                records = db.Usage.OrderBy(x => x.Id).ToList();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return records;
        }

        public string GetMedicineTypeSelectBox()
        {
            string result = "";
            var medicineTypeList = Get();
            result="<select id=\"MedicineType\" name=\"MedicineType\">";
            foreach (var medicineType in medicineTypeList)
            {
                result += "<option value=\"" + medicineType.Type + "\">" + medicineType.Type + "</option>";
            }
            result+="</select>";
            return result;
        }

        public string GetUsageSelectBox()
        {
            string result = "";
            var UsageList = GetUsage();
            result = "<select id=\"UsageId\" name=\"UsageId\" >";
            foreach (var usage in UsageList)
            {
                result += "<option value=\"" + usage.Id + "\">" + usage.Name + "</option>";
            }
            result += "</select>";
            return result;
        }
    } 
}
