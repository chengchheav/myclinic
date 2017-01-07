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
    public class PrescriptionRepository : IPrescriptionRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public PrescriptionRepository()
        {
            this.db = new MyDbContext();
        }

        public void Add(Prescription record)
        {
            try
            {
                db.Prescription.Add(record);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
        }

        public void Update(Prescription record)
        {
            try
            {
                db.Prescription.Attach(record);
                var entry = db.Entry(record);
                entry.State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
        }

        public IEnumerable<Prescription> Get()
        {
            IEnumerable<Prescription> items = null;
            try
            {
                items = db.Prescription.OrderByDescending(x => x.Id).Where(x => x.Status != 0).ToList(); 
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return items;
        }

        public IEnumerable<vMedicineDiagnosis> GetPrescriptionDiagnosis(int DiagId)
        {
            IEnumerable<vMedicineDiagnosis> items = null;
            try
            {
                int intStatus = (int)Enums.RecordStatus.Active;
                items = (from u in db.vMedicineDiagnosis select u).AsEnumerable();
                items = items.Where(x => x.Status == intStatus && x.DiagnosisId == DiagId).ToList(); 
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return items;
        }

        
        public void Delete(int id)
        {
            try
            {
                var record = new Prescription { Id = id };
                db.Prescription.Attach(record);
                db.Prescription.Remove(record);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
        }

        public void DeleteAll(int id)
        {
            try
            {
                var record = new Prescription { DiagnosisId = id };
                db.Prescription.Attach(record);
                db.Prescription.Remove(record);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
        }

        public IEnumerable<Prescription> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<Prescription> items = null;
            try
            {
                items = (from u in db.Prescription select u).Where(x => x.Status != 0).AsEnumerable();

                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Qty":
                            items = items.Where(p => p.Qty.ToLower().Contains(keyWord.ToLower()));
                            break;
                        case "MedicineType":
                            items = items.Where(p => p.MedicineType.ToLower().Contains(keyWord.ToLower()));
                            break;
                    }
                }

                totalRecords = items.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Qty":
                        if (order == "desc")
                            items = items.OrderByDescending(p => p.Qty);
                        else
                            items = items.OrderBy(p => p.Qty);
                        break;
                    case "MedicineType":
                        if (order == "desc")
                            items = items.OrderByDescending(p => p.MedicineType);
                        else
                            items = items.OrderBy(p => p.MedicineType);
                        break;
                    

                }

                var actualPageNo = pageNo - 1;
                items = items.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return items;
        }

        public IEnumerable<vPrescription> VSearch(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<vPrescription> lstRecords = null;
            try
            {
                lstRecords = db.Database.SqlQuery<vPrescription>("Select * From vPrescription").ToList();

                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Med_Name":
                            lstRecords = lstRecords.Where(p => p.Med_Name.ToLower() == keyWord.ToLower());
                            break;
                        case "Name":
                            lstRecords = lstRecords.Where(p => p.Name.ToLower() == keyWord.ToLower());
                            break;
                        case "Emp_Name":
                            lstRecords = lstRecords.Where(p => p.Emp_Name.ToLower() == keyWord.ToLower());
                            break;
                    }
                }

                totalRecords = lstRecords.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Med_Name":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Med_Name);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Med_Name);
                        break;
                    case "Name":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Name);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Name);
                        break;
                    case "Emp_Name":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Emp_Name);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Emp_Name);
                        break;
                    case "MedicineType":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.MedicineType);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.MedicineType);
                        break;
                    case "Qty":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Qty);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Qty);
                        break;
                    case "Morning":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Morning);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Morning);
                        break;
                    case "Noon":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Noon);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Noon);
                        break;
                    case "Night":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Night);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Night);
                        break;


                }

                var actualPageNo = pageNo - 1;
                lstRecords = lstRecords.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstRecords;
        }

        public void UpdateFieldChangedOnly(Prescription originPawner, Prescription newPawner)
        {
            try
            {
                db.Entry(originPawner).CurrentValues.SetValues(newPawner);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
        }

        /*For Get Pawner Record by Id*/
        public Prescription GetById(int id)
        {
            Prescription objReturn = new Prescription();
            try
            {
                objReturn = db.Prescription.Where(x => x.Id == id && x.Status != 0).FirstOrDefault();
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }
    } 
}
