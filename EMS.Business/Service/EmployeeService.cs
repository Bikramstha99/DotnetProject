using EMS.Business.Interface;
using EMS.Entities.Dtos.Employee;
using EMS.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Business.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<EmployeeAddUpdateApiModel> AddUpdateEmployee(EmployeeAddUpdateApiModel employeeAddUpdateApiModel)
        {
            return _employeeRepository.AddUpdateEmployee(employeeAddUpdateApiModel);
        }
    }
}
