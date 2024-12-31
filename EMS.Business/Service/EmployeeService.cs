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
        public Task<EmployeeInformationApiModel> GetAllEmployeeInformation()
        {
            return _employeeRepository.GetAllEmployeeInformation();
        }
        public Task<bool> AddUpdateEmployee(EmployeeAddUpdateApiModel employeeAddUpdateApiModel)
        {
            return _employeeRepository.AddUpdateEmployee(employeeAddUpdateApiModel);
        }

        public Task<EmployeeInformationApiModel> GetEmployeeInformationById(int Id)
        {
            return _employeeRepository.GetAllEmployeeInformation(Id);
        }

        public Task<bool> DeleteEmployee(int Id)
        {
            return _employeeRepository.DeleteEmployee(Id);
        }
    }
}
