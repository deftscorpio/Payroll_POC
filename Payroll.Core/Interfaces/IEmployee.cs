
using System;

namespace Payroll.Core.Interfaces
{
    public interface IEmployee
    {
        Guid Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        decimal AnnualSalary { get; set; }
        decimal SuperRate { get; set; }
        ISalaryPeriod SalaryPeriod { get; set; }
    }
}
