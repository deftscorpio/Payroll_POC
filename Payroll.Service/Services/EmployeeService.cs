using System;
using System.Collections.Generic;
using System.Linq;
using Payroll.Core.Entities;
using Payroll.Core.Interfaces;
using Payroll.Infrastructure.Interfaces;
using Payroll.Service.Interfaces.Services;

namespace Payroll.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<IEmployee> GetAllEmployees()
        {
            return _employeeRepository.GetAll().AsEnumerable();
        }

        public IEmployee GetEmployee(Guid employeeId)
        {
            return _employeeRepository.Get(employeeId);
        }

        public void SaveEmployee(Employee employee)
        {
            _employeeRepository.Insert(employee);
        }
    }
}
