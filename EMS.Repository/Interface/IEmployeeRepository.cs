using EMS.Entities.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Interface
{
    public interface IEmployeeRepository
    {
        Task<EmployeeInformationApiModel> GetAllEmployeeInformation();
        Task<bool> AddUpdateEmployee(EmployeeAddUpdateApiModel employeeAddUpdateApiModel);
        Task<EmployeeInformationApiModel> GetAllEmployeeInformation(int Id);
        Task<bool> DeleteEmployee(int Id);


    }
}
