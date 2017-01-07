using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{

    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        IEnumerable<DTOEmployee> Search(string searchBy, string keyWord, string orderBy, string order, int pageNo, int pageSize, out int totalRecords);
        void Add(Employee employee);
        void UpdateFieldChangedOnly(Employee originalEmployee, Employee newEmployee);
        Employee GetEmployeeById(int id);
        List<Position> GetPosition();
        DTOEmployee GetDTOEmployeeById(int id);
        IEnumerable<Employee> GetEmployeeListByName(string name);
        IEnumerable<Diagnosis> GetDiagnosisByEmployeeId(int EmployeeId);
    }

}
