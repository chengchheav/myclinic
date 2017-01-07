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
    public class DoctorOperationRepository : IDoctorOperationRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public DoctorOperationRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<vDoctorOperation> Search(string cboDoct, string searchBy, string keyWord, string startDate, string endDate, string orderBy, string order, out int totalRecords)
        {
            IEnumerable<vDoctorOperation> lstRecords = null;
            try
            {
                lstRecords = (from l in db.vDoctorOperation select l).AsEnumerable();
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

                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Name":
                            lstRecords = lstRecords.Where(p => p.Name.ToLower().Contains(keyWord.ToLower()));
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

                totalRecords = lstRecords.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "DiagnoseDate":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.DiagnoseDate);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.DiagnoseDate);
                        break;
                    case "ExpiredDate":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.ExpiredDate);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.ExpiredDate);
                        break;

                    case "SymptomType":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.SymptomType);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.SymptomType);
                        break;
                    case "Name":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Name);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Name);
                        break;
                    case "Sex":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Sex);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Sex);
                        break;
                    case "Age":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Age);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Age);
                        break;
                    
                    case "Sym_Type":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Sym_Type);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Sym_Type);
                        break;
                    case "Dis_Name":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Dis_Name);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Dis_Name);
                        break;
                    

                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return lstRecords;
        }
    } 
}
