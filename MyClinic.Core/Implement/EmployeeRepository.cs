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
    public class EmployeeRepository :IEmployeeRepository
    {
        ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MyDbContext db;
        public EmployeeRepository()
        {
            this.db = new MyDbContext();
        }

        public IEnumerable<Employee> Get()
        {
            IEnumerable<Employee> Employees = null;
            try
            {
                Employees = db.Employee.Where(p => p.Status != 0).ToList().OrderBy(p => p.Id);
                //Employees = db.Employee.Where(p=>p.Status !=0).ToList().OrderBy(p => p.Name);
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return Employees;
        }


        public IEnumerable<DTOEmployee> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords)
        {
            IEnumerable<DTOEmployee> dtoemployees = null;
            try
            {
                dtoemployees = (from p in db.DTOEmployee select p).Where(s => s.Status != 0).AsEnumerable();
                if (keyWord.Trim().Length > 0)
                {
                    switch (searchBy)
                    {
                        case "Name":
                            dtoemployees = dtoemployees.Where(p => p.Name.ToLower().Contains(keyWord.ToLower()));
                            break;
                        case "Email":
                            dtoemployees = dtoemployees.Where(p => p.Email.ToLower().Contains(keyWord.ToLower()));
                            break;
                    }
                }

                totalRecords = dtoemployees.AsEnumerable().Count();

                switch (orderBy)
                {
                    case "Name":
                        if (order == "desc")
                            dtoemployees = dtoemployees.OrderByDescending(p => p.Name);
                        else
                            dtoemployees = dtoemployees.OrderBy(p => p.Name);
                        break;
                    case "Sex":
                        if (order == "desc")
                            dtoemployees = dtoemployees.OrderByDescending(p => p.Sex);
                        else
                            dtoemployees = dtoemployees.OrderBy(p => p.Sex);
                        break;
                    case "Email":
                        if (order == "desc")
                            dtoemployees = dtoemployees.OrderByDescending(p => p.Email);
                        else
                            dtoemployees = dtoemployees.OrderBy(p => p.Email);
                        break;
                    case "Tel":
                        if (order == "desc")
                            dtoemployees = dtoemployees.OrderByDescending(p => p.Tel);
                        else
                            dtoemployees = dtoemployees.OrderBy(p => p.Tel);
                        break;
                    case "Id":
                        if (order == "desc")
                            dtoemployees = dtoemployees.OrderByDescending(p => p.Id);
                        else
                            dtoemployees = dtoemployees.OrderBy(p => p.Id);
                        break;
                    case "Status":
                        if (order == "desc")
                            dtoemployees = dtoemployees.OrderByDescending(p => p.Status);
                        else
                            dtoemployees = dtoemployees.OrderBy(p => p.Status);
                        break;
                    case "Position":
                        if (order == "desc")
                            dtoemployees = dtoemployees.OrderByDescending(p => p.PositionName);
                        else
                            dtoemployees = dtoemployees.OrderBy(p => p.PositionName);
                        break;
                }

                var actualPageNo = pageNo - 1;
                dtoemployees = dtoemployees.Skip(actualPageNo * pageSize).Take(pageSize);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return dtoemployees;
        }

        public void Add(Employee Employee)
        {
            try
            {
                db.Employee.Add(Employee);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
                
            }
        }

        public void UpdateFieldChangedOnly(Employee originalEmployee, Employee newEmployee)
        {
            try
            {
                db.Entry(originalEmployee).CurrentValues.SetValues(newEmployee);
                db.SaveChanges();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
        }

        /*For Get Employee Record by Id*/
        public Employee GetEmployeeById(int id)
        {
            Employee objReturn = new Employee();
            try
            {
                objReturn = db.Employee.Where(x => x.Id == id && x.Status !=0).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }

        /*For Get DTOEmployee Record by Id*/
        public DTOEmployee GetDTOEmployeeById(int id)
        {
            DTOEmployee objReturn = new DTOEmployee();
            try
            {
                objReturn = db.DTOEmployee.Where(x => x.Id == id && x.Status != 0).FirstOrDefault();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return objReturn;
        }

        /*For Get All Record in Position Table*/
        public List<Position> GetPosition()
        {
            List<Position> positions = null;
            positions = db.Database.SqlQuery<Position>("select distinct Id,Name from Position").OrderBy(x => x.Id).ToList();
            return positions;
        }

        /*For Get Employee Lis Record By Name*/
        public IEnumerable<Employee> GetEmployeeListByName(string name)
        {
            IEnumerable<Employee> employees = null;
            try
            {
                employees = db.Employee.Where(x => x.Name.Contains(name) && x.Status != 0).OrderBy(x => x.Name).ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return employees;
        }

        /*Fir Get Employee List By EmployeeId*/
        public IEnumerable<Diagnosis> GetDiagnosisByEmployeeId(int EmployeeId)
        {
            IEnumerable<Diagnosis> getDiagnosisByEmployeeId = null;
            try
            {
                getDiagnosisByEmployeeId = db.Diagnosis.Where(x => x.DiagnoseBy == EmployeeId && x.Status != 0).ToList();
            }
            catch (DbEntityValidationException exp)
            {
                log.Error(exp);
            }
            return getDiagnosisByEmployeeId;
        }
    }
}