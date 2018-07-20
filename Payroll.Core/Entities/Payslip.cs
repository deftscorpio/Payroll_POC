using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities
{
    public class Payslip : IPayslip
    {
        public string Name { get; set; }
        public ISalaryPeriod SalaryPeriod { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal IncomeTax { get; set; }
        public decimal NetIncome { get; set; }
        public decimal SuperAmount { get; set; }
    }
}
