using EMS.Entities.Dtos.Employee;
using EMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<EmployeeAddUpdateApiModel> AddUpdateEmployee(EmployeeAddUpdateApiModel employeeAddUpdateApiModel)
        {
            throw new NotImplementedException();
        }
    }
}
