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
    public class PatientRepository :IPatientRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public PatientRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<Patient> Get()
        {
            IEnumerable<Patient> patients = null;
            try
            {
                patients = db.Patient.Where(p => p.Status != 0).ToList().OrderBy(p => p.Name);
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return patients;
        }

        
        public IEnumerable<Patient> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<Patient> patients = null;
            try
            {
                patients = (from p in db.Patient select p).Where(s=>s.Status !=0).AsEnumerable();
                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Name":
                            patients = patients.Where(p => p.Name.ToLower().Contains(keyWord.ToLower()));
                            break;
                        case "Email":
                            patients = patients.Where(p => p.Email.ToLower().Contains(keyWord.ToLower()));
                            break;
                    }
                }                

                totalRecords = patients.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Name":
                        if (order == "desc")
                            patients = patients.OrderByDescending(p => p.Name);
                        else
                            patients = patients.OrderBy(p => p.Name);
                        break;
                    case "Sex":
                        if (order == "desc")
                            patients = patients.OrderByDescending(p => p.Sex);
                        else
                            patients = patients.OrderBy(p => p.Sex);
                        break; 
                    case "Email":
                        if (order == "desc")
                            patients = patients.OrderByDescending(p => p.Email);
                        else
                            patients = patients.OrderBy(p => p.Email);
                        break;
                    case "Tel":
                        if (order == "desc")
                            patients = patients.OrderByDescending(p => p.Tel);
                        else
                            patients = patients.OrderBy(p => p.Tel);
                        break; 
                    case "Id":
                        if (order == "desc")
                            patients = patients.OrderByDescending(p => p.Id);
                        else
                            patients = patients.OrderBy(p => p.Id);
                        break;
                    case "Status":
                        if (order == "desc")
                            patients = patients.OrderByDescending(p => p.Status);
                        else
                            patients = patients.OrderBy(p => p.Status);
                        break;
                    case "Age":
                        if (order == "desc")
                            patients = patients.OrderByDescending(p => p.Age);
                        else
                            patients = patients.OrderBy(p => p.Age);
                        break;
                }

                var actualPageNo = pageNo - 1;
                patients = patients.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return patients;
        }

        public void Add(Patient patient)
        {
            try
            {
                db.Patient.Add(patient);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
                
            }
        }

        public void UpdateFieldChangedOnly(Patient originalPatient, Patient newPatient)
        {
            try
            {
                db.Entry(originalPatient).CurrentValues.SetValues(newPatient);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
        }

        /*For Get Patient Record by Id*/
        public Patient GetPatientById(int id)
        {
            Patient objReturn = new Patient();
            try
            {
                objReturn = db.Patient.Where(x => x.Id == id).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }


        /*For Get Patient Record by Id*/
        public DTOPatient GetDTOPatientById(int id)
        {
            DTOPatient objReturn = new DTOPatient();
            try
            {
                objReturn = db.DTOPatient.Where(x => x.Id == id).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }

        /*For Get Patient Lis Record By Name*/
        public IEnumerable<DTOPatient> GetPatientListByName(string name)
        {
            IEnumerable<DTOPatient> patients = null;
            try
            {
                patients = db.DTOPatient.Where(x => x.DisplayValue.Contains(name) && x.Status != 0).OrderBy(x => x.DisplayValue).ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return patients;
        }

    }
}