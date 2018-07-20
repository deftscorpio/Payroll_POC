
using Payroll.Core.Entities;
using Payroll.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Payroll.Service.Interfaces.Services
{
    public interface IEmployeeService
    {
        IEnumerable<IEmployee> GetAllEmployees();
        IEmployee GetEmployee(Guid employeeId);
        void SaveEmployee(Employee employee);
    }
}
