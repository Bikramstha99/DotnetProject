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
        Task<EmployeeAddUpdateApiModel> AddUpdateEmployee(EmployeeAddUpdateApiModel employeeAddUpdateApiModel);

    }
}
