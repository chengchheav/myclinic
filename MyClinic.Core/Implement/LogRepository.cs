using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public class LogRepository:ILogRepository
    {
        MyDbContext db;
        public LogRepository()
        {
            this.db = new MyDbContext();
        }
        public void Add(Log log) 
        {
            try
            {
                db.Log.Add(log);
                db.SaveChanges();
            }catch(Exception ex){
                
            }
        }

        public IEnumerable<Log> Get()
        {
            IEnumerable<Log> logs = null;
            try
            {
                logs = db.Log.ToList();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return logs;
        }


        public IEnumerable<DTOAuditLog> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<DTOAuditLog> Logs = null;
            try
            {
                Logs = (from l in db.DTOAuditLog select l).AsEnumerable();

                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "ProcessType":
                            Logs = Logs.Where(p => p.ProcessType == keyWord);
                            break;
                    }
                }
                totalRecords = Logs.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Id":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.Id);
                        else
                            Logs = Logs.OrderBy(p => p.Id);
                        break;
                    case "UserId":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.UserId);
                        else
                            Logs = Logs.OrderBy(p => p.UserId);
                        break;
                    case "ProcessType":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.ProcessType);
                        else
                            Logs = Logs.OrderBy(p => p.ProcessType);
                        break;
                    case "LogDate":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.LogDate);
                        else
                            Logs = Logs.OrderBy(p => p.LogDate);
                        break;
                    case "U_Name":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.U_Name);
                        else
                            Logs = Logs.OrderBy(p => p.U_Name);
                        break;
                    case "U_Email":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.U_Email);
                        else
                            Logs = Logs.OrderBy(p => p.U_Email);
                        break;
                    case "Description":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.Description);
                        else
                            Logs = Logs.OrderBy(p => p.Description);
                        break;
                }

                var actualPageNo = pageNo - 1;
                Logs = Logs.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return Logs;
        }



        /*Get Log Record For User Log In*/
        public IEnumerable<DTOAuditLog> SearchInLogUser(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords, int idLogUser)
        {
            IEnumerable<DTOAuditLog> Logs = null;
            try
            {
                Logs = (from l in db.DTOAuditLog select l).Where(x=>x.UserId == idLogUser).AsEnumerable();

                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "ProcessType":
                            Logs = Logs.Where(p => p.ProcessType == keyWord);
                            break;
                    }
                }
                totalRecords = Logs.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Id":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.Id);
                        else
                            Logs = Logs.OrderBy(p => p.Id);
                        break;
                    case "UserId":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.UserId);
                        else
                            Logs = Logs.OrderBy(p => p.UserId);
                        break;
                    case "ProcessType":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.ProcessType);
                        else
                            Logs = Logs.OrderBy(p => p.ProcessType);
                        break;
                }

                var actualPageNo = pageNo - 1;
                Logs = Logs.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return Logs;
        }
        

        /*For Search By Date*/
        public IEnumerable<DTOAuditLog> SearchByDate(string startDate, string endDate, string cboUser, string cboPro, string orderBy, string order, out int totalRecords,int pageNo, int pageSize) {
            IEnumerable<DTOAuditLog> Logs = null;
            try 
            {
                Logs = (from l in db.DTOAuditLog select l).AsEnumerable();
                if (startDate.Trim().Length > 0 && endDate.Trim().Length > 0)
                {
                    var objStartDate = System.DateTime.Parse(startDate);
                    var objEndDate = System.DateTime.Parse(endDate);
                    Logs = Logs.Where(p => p.LogDate.Date > objStartDate && p.LogDate.Date <= objEndDate);
                }
                int intUser = 0;
                if (int.TryParse(cboUser, out intUser))
                {
                    if (intUser > 0)
                    {
                        Logs = Logs.Where(p => p.UserId == intUser);
                    }
                }
                if (cboPro.Trim().Length > 0)
                {
                    Logs = Logs.Where(p => p.ProcessType == cboPro);
                }

                totalRecords = Logs.AsEnumerable().Count();
                switch (orderBy)
                {
                    case "Id":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.Id);
                        else
                            Logs = Logs.OrderBy(p => p.Id);
                        break;
                    case "UserId":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.UserId);
                        else
                            Logs = Logs.OrderBy(p => p.UserId);
                        break;
                    case "ProcessType":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.ProcessType);
                        else
                            Logs = Logs.OrderBy(p => p.ProcessType);
                        break;
                    case "Description":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.Description);
                        else
                            Logs = Logs.OrderBy(p => p.Description);
                        break;
                    case "LogDate":
                        if (order == "desc")
                            Logs = Logs.OrderByDescending(p => p.LogDate);
                        else
                            Logs = Logs.OrderBy(p => p.LogDate);
                        break;
                }
                var actualPageNo = pageNo - 1;
                Logs = Logs.Skip(actualPageNo * pageSize).Take(pageSize);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            return Logs;
        }
    }
}
