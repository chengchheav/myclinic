using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;
using System.Data;
using Utilities;
using System.Data.Entity.Validation;
using log4net;
using System.Data.Entity;

namespace MyClinic.Core
{
    public class MedicineRepository :IMedicineRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public MedicineRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<Medicine> Get()
        {
            IEnumerable<Medicine> medicines = null;
            try
            {
                medicines = db.Medicine.ToList().OrderBy(p => p.Name);
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return medicines;
        }


        public IEnumerable<DTOMedicine> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<DTOMedicine> medicines = null;
            try
            {
                medicines = (from p in db.DTOMedicine select p).Where(s => s.Status != 0).AsEnumerable();
                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Name":
                            medicines = medicines.Where(p => p.Name.ToLower().Contains(keyWord.ToLower()));
                            break;
                    }
                }

                totalRecords = medicines.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Id":
                        if (order == "desc")
                            medicines = medicines.OrderByDescending(p => p.Id);
                        else
                            medicines = medicines.OrderBy(p => p.Id);
                        break;
                    case "Name":
                        if (order == "desc")
                            medicines = medicines.OrderByDescending(p => p.Name);
                        else
                            medicines = medicines.OrderBy(p => p.Name);
                        break;
                    case "Description":
                        if (order == "desc")
                            medicines = medicines.OrderByDescending(p => p.Description);
                        else
                            medicines = medicines.OrderBy(p => p.Description);
                        break;
                    case "Unit":
                        if (order == "desc")
                            medicines = medicines.OrderByDescending(p => p.Unit);
                        else
                            medicines = medicines.OrderBy(p => p.Unit);
                        break;
                    case "MedicineType":
                        if (order == "desc")
                            medicines = medicines.OrderByDescending(p => p.MedicineType);
                        else
                            medicines = medicines.OrderBy(p => p.MedicineType);
                        break;
                    case "Status":
                        if (order == "desc")
                            medicines = medicines.OrderByDescending(p => p.Status);
                        else
                            medicines = medicines.OrderBy(p => p.Status);
                        break;
                }

                var actualPageNo = pageNo - 1;
                medicines = medicines.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return medicines;
        }

        public void Add(Medicine medicine)
        {
            try
            {
                db.Medicine.Add(medicine);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
                
            }
        }

        public void UpdateFieldChangedOnly(Medicine originalMedicine, Medicine newMedicine)
        {
            try
            {
                db.Entry(originalMedicine).CurrentValues.SetValues(newMedicine);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
        }

        /*For Get Medicine Record by Id*/
        public Medicine GetMedicineById(int id)
        {
            Medicine objReturn = new Medicine();
            try
            {
                objReturn = db.Medicine.Where(x => x.Id == id && x.Status !=0).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }

        /*For Get vMedicine Record by Id*/
        public DTOMedicine GetDTOMedicineById(int id)
        {
            DTOMedicine objReturn = new DTOMedicine();
            try
            {
                objReturn = db.DTOMedicine.Where(x => x.Id == id && x.Status != 0).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }

        public IEnumerable<AutoMedicine> AutoMedicine(int status)
        {
            IEnumerable<AutoMedicine> lstRecords = null;
            try
            {
                lstRecords = db.Database.SqlQuery<AutoMedicine>("SELECT DISTINCT m.Id AS Id, m.Name AS Name, m.MedicineType AS MedicineType FROM dbo.vMedicine AS m WHERE Status = " + status + " ORDER BY m.Name").ToList();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstRecords;
        }

        /*For Get Prescription List By MedicineId*/
        public IEnumerable<Prescription> GetPrescriptionByMedicineId(int medicineId)
        {
            IEnumerable<Prescription> getPrescriptionByMedicineId = null;
            try
            {
                getPrescriptionByMedicineId = db.Prescription.Where(x => x.MedicineId == medicineId && x.Status != 0).ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return getPrescriptionByMedicineId;
        }

    }
}