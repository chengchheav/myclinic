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
    public class PatientOperationRepository : IPatientOperationRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public PatientOperationRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<vPatientOperation> Search(string id, string cboDoct, string searchBy, string keyWord, string startDate, string endDate, out int totalRecords)
        {
            IEnumerable<vPatientOperation> lstRecords = null;
            try
            {
                lstRecords = (from l in db.vPatientOperation select l).AsEnumerable();
                if (String.IsNullOrEmpty(startDate) == false && String.IsNullOrEmpty(endDate) == false)
                {
                    var objStartDate = System.DateTime.Parse(startDate);
                    var objEndDate = System.DateTime.Parse(endDate);
                    if (objStartDate == objEndDate)
                    {
                        lstRecords = lstRecords.Where(p => p.DiagnoseDate.Date == objStartDate);
                    }
                    else
                    {
                        lstRecords = lstRecords.Where(p => p.DiagnoseDate.Date >= objStartDate && p.DiagnoseDate.Date <= objEndDate);
                    }

                }
                else if (String.IsNullOrEmpty(startDate) == false)
                {
                    var objStartDate = System.DateTime.Parse(startDate);
                    lstRecords = lstRecords.Where(p => p.DiagnoseDate.Date == objStartDate);

                }
                else if (String.IsNullOrEmpty(endDate) == false)
                {
                    var objEndDate = System.DateTime.Parse(endDate);
                    lstRecords = lstRecords.Where(p => p.DiagnoseDate.Date == objEndDate);
                }

                int doctor = 0;
                if(int.TryParse(cboDoct, out doctor)){
                    if (doctor > 0)
                    {
                        lstRecords = lstRecords.Where(p => p.DiagnoseBy == doctor);
                    }
                }

                int intId = 0;
                if (int.TryParse(id, out intId))
                {
                    if (intId > 0)
                    {
                        lstRecords = lstRecords.Where(p => p.PatientId == intId);
                    }
                }

                if (String.IsNullOrEmpty(searchBy) == false && String.IsNullOrEmpty(keyWord) == false)
                {
                    if (keyWord.Trim().Length > 0)
                    {
                        switch (searchBy)
                        {
                            case "Name":
                                lstRecords = lstRecords.Where(p => p.Name.ToLower().Contains(keyWord.ToLower()));
                                break;

                            case "Doc_Name":
                                lstRecords = lstRecords.Where(p => p.Doc_Name.ToLower().Contains(keyWord.ToLower()));
                                break;

                            case "Dis_Name":
                                lstRecords = lstRecords.Where(p => p.Dis_Name.ToLower().Contains(keyWord.ToLower()));
                                break;

                            case "Sym_Type":
                                lstRecords = lstRecords.Where(p => p.Sym_Type.ToLower().Contains(keyWord.ToLower()));
                                break;

                            case "Tel":
                                lstRecords = lstRecords.Where(p => p.Tel.ToLower().Contains(keyWord.ToLower()));
                                break;
                        }
                    }
                }

                totalRecords = lstRecords.AsEnumerable().Count();
                lstRecords = lstRecords.OrderBy(p => p.PatientId).OrderBy(p => p.DiseaseId).OrderBy(p => p.DiagnoseDate);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstRecords;
        }
    } 
}
