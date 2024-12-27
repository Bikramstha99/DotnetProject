using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Entities.Dtos.Employee
{
    public class EmployeeAddUpdateApiModel
    {
        public string EmployeeName {  get; set; }
        public string DOB {  get; set; }
        public decimal Salary {  get; set; }

    }
}
