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
    public class DiseaseRepository :IDiseaseRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public DiseaseRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<Disease> Get()
        {
            IEnumerable<Disease> Diseases = null;
            try
            {
                Diseases = db.Disease.Where(p => p.Status != 0).ToList().OrderBy(p => p.Name);
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return Diseases;
        }


        public IEnumerable<Disease> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<Disease> Diseases = null;
            try
            {
                Diseases = (from p in db.Disease select p).Where(s => s.Status != 0).AsEnumerable();
                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Name":
                            Diseases = Diseases.Where(p => p.Name.ToLower().Contains(keyWord.ToLower()));
                            break;
                    }
                }

                totalRecords = Diseases.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Name":
                        if (order == "desc")
                            Diseases = Diseases.OrderByDescending(p => p.Name);
                        else
                            Diseases = Diseases.OrderBy(p => p.Name);
                        break;
                    case "Description":
                        if (order == "desc")
                            Diseases = Diseases.OrderByDescending(p => p.Description);
                        else
                            Diseases = Diseases.OrderBy(p => p.Description);
                        break;
                    case "Id":
                        if (order == "desc")
                            Diseases = Diseases.OrderByDescending(p => p.Id);
                        else
                            Diseases = Diseases.OrderBy(p => p.Id);
                        break;
                    case "Status":
                        if (order == "desc")
                            Diseases = Diseases.OrderByDescending(p => p.Status);
                        else
                            Diseases = Diseases.OrderBy(p => p.Status);
                        break;
                }

                var actualPageNo = pageNo - 1;
                Diseases = Diseases.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return Diseases;
        }

        public void Add(Disease Disease)
        {
            try
            {
                db.Disease.Add(Disease);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
                
            }
        }

        public void UpdateFieldChangedOnly(Disease originalDisease, Disease newDisease)
        {
            try
            {
                db.Entry(originalDisease).CurrentValues.SetValues(newDisease);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
        }

        /*For Get Disease Record by Id*/
        public Disease GetDiseaseById(int id)
        {
            Disease objReturn = new Disease();
            try
            {
                objReturn = db.Disease.Where(x => x.Id == id && x.Status !=0).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }

        /*For Get DTODisease Record by Id*/
        public DTODisease GetDTODiseaseById(int id)
        {
            DTODisease objReturn = new DTODisease();
            try
            {
                objReturn = db.DTODisease.Where(x => x.Id == id && x.Status != 0).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }

        /*Get Diagnosis List By DiseaseId*/
        public IEnumerable<Diagnosis> GetDiagnosisByDiseaseId(int DiseaseId)
        {
            IEnumerable<Diagnosis> getDiagnosisByDiseaseId = null;
            try
            {
                getDiagnosisByDiseaseId = db.Diagnosis.Where(x => x.DiseaseId == DiseaseId && x.Status != 0).ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return getDiagnosisByDiseaseId;
        }

        /*For Get Disease Lis Record By Name*/
        public IEnumerable<Disease> GetDiseaseListByName(string name)
        {
            IEnumerable<Disease> Diseases = null;
            try
            {
                Diseases = db.Disease.Where(x => x.Name.Contains(name) && x.Status != 0).OrderBy(x => x.Name).ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return Diseases;
        }
    }
}