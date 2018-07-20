using Payroll.Core.Interfaces;
using System;

namespace Payroll.Core.Entities
{
    public class SalaryPeriod : ISalaryPeriod
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FinancialYear { get; set; }
    }
}
