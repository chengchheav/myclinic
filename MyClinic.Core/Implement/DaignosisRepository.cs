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
    public class DiagnosisRepository :IDiagnosisRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public DiagnosisRepository()
        {
            this.db = new MyDbContext();
        }
        public IEnumerable<DTODiagnosis> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<DTODiagnosis> dtoDiagnosiss = null;
            try
            {
                dtoDiagnosiss = (from p in db.DTODiagnosis select p).Where(s => s.Status != 0).AsEnumerable();

                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Name":
                            dtoDiagnosiss = dtoDiagnosiss.Where(p => p.Patient_Name.ToLower().Contains(keyWord.ToLower()));
                            break;
                        case "Symptom":
                            dtoDiagnosiss = dtoDiagnosiss.Where(p => p.Symptom.ToLower().Contains(keyWord.ToLower()));
                            break;
                        case "DiagnoseBy":
                            dtoDiagnosiss = dtoDiagnosiss.Where(p => p.Diagnose_Name.ToLower().Contains(keyWord.ToLower()));
                            break;
                    }
                }

                totalRecords = dtoDiagnosiss.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Id":
                        if (order == "desc")
                            dtoDiagnosiss = dtoDiagnosiss.OrderByDescending(p => p.Id);
                        else
                            dtoDiagnosiss = dtoDiagnosiss.OrderBy(p => p.Id);
                        break;
                    case "Name":
                        if (order == "desc")
                            dtoDiagnosiss = dtoDiagnosiss.OrderByDescending(p => p.Patient_Name);
                        else
                            dtoDiagnosiss = dtoDiagnosiss.OrderBy(p => p.Patient_Name);
                        break;
                    case "Symptom":
                        if (order == "desc")
                            dtoDiagnosiss = dtoDiagnosiss.OrderByDescending(p => p.Symptom);
                        else
                            dtoDiagnosiss = dtoDiagnosiss.OrderBy(p => p.Symptom);
                        break;
                    case "DiagnoseCycle":
                        if (order == "desc")
                            dtoDiagnosiss = dtoDiagnosiss.OrderByDescending(p => p.DiagnoseCycle);
                        else
                            dtoDiagnosiss = dtoDiagnosiss.OrderBy(p => p.DiagnoseCycle);
                        break;
                    case "DiagnoseBy":
                        if (order == "desc")
                            dtoDiagnosiss = dtoDiagnosiss.OrderByDescending(p => p.Diagnose_Name);
                        else
                            dtoDiagnosiss = dtoDiagnosiss.OrderBy(p => p.Diagnose_Name);
                        break;
                    case "Status":
                        if (order == "desc")
                            dtoDiagnosiss = dtoDiagnosiss.OrderByDescending(p => p.Status);
                        else
                            dtoDiagnosiss = dtoDiagnosiss.OrderBy(p => p.Status);
                        break;
                }

                var actualPageNo = pageNo - 1;
                dtoDiagnosiss = dtoDiagnosiss.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return dtoDiagnosiss;
        }

        public void Add(Diagnosis diagnosis)
        {
            try
            {
                db.Diagnosis.Add(diagnosis);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
                
            }
        }

        public void UpdateFieldChangedOnly(Diagnosis originalDiagnosis, Diagnosis newDiagnosis)
        {
            try
            {
                db.Entry(originalDiagnosis).CurrentValues.SetValues(newDiagnosis);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
        }

        /*For Get Diagnosis Record by Id*/
        public Diagnosis GetDiagnosisById(int id)
        {
            Diagnosis objReturn = new Diagnosis();
            try
            {
                objReturn = db.Diagnosis.Where(x => x.Id == id && x.Status !=0).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }

        /*For Get DTODiagnosis Record by Id*/
        public DTODiagnosis GetDTODiagnosisById(int id)
        {
            DTODiagnosis objReturn = new DTODiagnosis();
            try
            {
                objReturn = db.DTODiagnosis.Where(x => x.Id == id && x.Status != 0).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }
        /*Get Diagnosis List By PatientId*/
        public IEnumerable<Diagnosis> GetDiagnosisByPatientId(int patientId)
        {
            IEnumerable<Diagnosis> getDiagnosisByPawnerId = null;
            try
            {
                getDiagnosisByPawnerId = db.Diagnosis.Where(x => x.PatientId == patientId && x.Status != 0).ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return getDiagnosisByPawnerId;
        }

        /*Get Diagnosis List By PatientId*/
        public IEnumerable<DTODiagnosis> GetListDiagnosisByPatientId(int patientId)
        {
            IEnumerable<DTODiagnosis> getDiagnosisByPawnerId = null;
            try
            {
                getDiagnosisByPawnerId = db.DTODiagnosis.Where(x => x.PatientId == patientId && x.Status != 0).ToList().OrderByDescending(p => p.DiagnoseDate);
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return getDiagnosisByPawnerId;
        }

        /*Get Prescription List By DiagnosisId*/
        public IEnumerable<Prescription> GetPrescriptionByDiagnosisId(int DiagnosisId)
        {
            IEnumerable<Prescription> getPrescriptionByDiagnosisId = null;
            try
            {
                getPrescriptionByDiagnosisId = db.Prescription.Where(x => x.DiagnosisId == DiagnosisId && x.Status != 0).ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return getPrescriptionByDiagnosisId;
        }

        /*For Get All Record of Symptom Type*/
        public IEnumerable<SymptomType> GetSymptomType()
        {
            IEnumerable<SymptomType> symptomTypes = null;
            try
            {
                symptomTypes = db.SymptomType.ToList().OrderBy(p => p.Id);
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return symptomTypes;
        }

        /*For Get All Record of Disease Type*/
        public IEnumerable<Disease> GetDisease()
        {
            IEnumerable<Disease> disease = null;
            try
            {
                disease = db.Disease.ToList().OrderBy(p => p.Id);
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return disease;
        }

        /*For Get DTODisease By Id*/
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

        public int GetDiagnosisCycle(int PatientId)
        {
            Diagnosis diagnosis = new Diagnosis();
            int result = 0;
            try
            {
                  diagnosis = db.Diagnosis.Where(p => p.PatientId == PatientId).OrderByDescending(p=>p.Id).Take(1).FirstOrDefault();
                 if (diagnosis != null)
                 {
                     result = diagnosis.DiagnoseCycle;
                 }
            }
            catch (Exception exp)
            {
                log.Error(exp);
            }
            return result ;
        }

    }
}