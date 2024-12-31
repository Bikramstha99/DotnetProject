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
        public Task<EmployeeInformationApiModel> GetAllEmployeeInformation()
        {
            throw new NotImplementedException();
        }
        public Task<bool> AddUpdateEmployee(EmployeeAddUpdateApiModel employeeAddUpdateApiModel)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeInformationApiModel> GetAllEmployeeInformation(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployee(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
