using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClinic.Infrastructure;

namespace MyClinic.Models
{
    public class EmployeeModels
    {

        public IEnumerable<DTOEmployee> dtoemployees { get; set; }
        public Employee employee { get; set; }
        public DTOEmployee dtoemployee { get; set; }
        public PageUtilities pageUtilities { get; set; }
        public IEnumerable<Position> positions { get; set; }

        public EmployeeAdd employeeAdd { get; set; }
        public EmployeeEdit employeeEdit { get; set; }
        public Boolean checkPost { get; set; }
        public Boolean checkError { get; set; }
    }
}