using EMS.Entities.Dtos.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Business.Interface
{
    public interface IEmployeeService
    {
        Task<EmployeeInformationApiModel> GetAllEmployeeInformation();

        Task<bool> AddUpdateEmployee(EmployeeAddUpdateApiModel employeeAddUpdateApiModel);
        Task<EmployeeInformationApiModel> GetEmployeeInformationById(int Id);
        Task<bool> DeleteEmployee(int Id);



    }
}
