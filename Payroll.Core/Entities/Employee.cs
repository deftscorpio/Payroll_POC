using Payroll.Core.Interfaces;
using System;

namespace Payroll.Core.Entities
{
    public class Employee : IEmployee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public decimal SuperRate { get; set; }
        public ISalaryPeriod SalaryPeriod { get; set; }
    }
}
