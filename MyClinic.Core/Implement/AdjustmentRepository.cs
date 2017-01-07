﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;
using System.Data.Entity.Validation;
using log4net;

namespace MyClinic.Core
{
    public class AdjustmentRepository : IAdjustmentRepository
    {
        MyDbContext db;
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public AdjustmentRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<DTOAuditLog> Search(string startDate, string endDate, string orderBy, string order, int typeAction, out int totalRecords)
        {
            IEnumerable<DTOAuditLog> lstRecords = null;
            try
            {
                lstRecords = (from l in db.DTOAuditLog select l).AsEnumerable();
                if (String.IsNullOrEmpty(startDate) == false && String.IsNullOrEmpty(endDate) == false)
                {
                    var objStartDate = System.DateTime.Parse(startDate);
                    var objEndDate = System.DateTime.Parse(endDate);
                    if (objStartDate == objEndDate)
                    {
                        lstRecords = lstRecords.Where(p => p.LogDate.Date == objStartDate);
                    }else{
                        lstRecords = lstRecords.Where(p => p.LogDate.Date >= objStartDate && p.LogDate.Date <= objEndDate);
                    }
                }else if (startDate != ""){
                    var objStartDate = System.DateTime.Parse(startDate);
                    lstRecords = lstRecords.Where(p => p.LogDate.Date == objStartDate);

                }
                else if (endDate != "")
                {
                    var objEndDate = System.DateTime.Parse(endDate);
                    lstRecords = lstRecords.Where(p => p.LogDate.Date == objEndDate);
                }
                else
                {
                    if (typeAction == 0)
                    {
                        DateTime preDate = DateTime.Today.AddDays(-1);
                        lstRecords = lstRecords.Where(p => p.LogDate.Date == preDate);
                    }
                }

                lstRecords = lstRecords.Where(p => p.ProcessType == "Edit Pawn");

                totalRecords = lstRecords.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Id":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Id);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Id);
                        break;
                    case "UserId":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.UserId);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.UserId);
                        break;
                    case "ProcessType":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.ProcessType);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.ProcessType);
                        break;
                    case "Description":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.Description);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.Description);
                        break;
                    case "LogDate":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.LogDate);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.LogDate);
                        break;
                    case "U_Name":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.U_Name);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.U_Name);
                        break;
                    case "U_Email":
                        if (order == "desc")
                            lstRecords = lstRecords.OrderByDescending(p => p.U_Email);
                        else
                            lstRecords = lstRecords.OrderBy(p => p.U_Email);
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
