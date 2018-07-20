
namespace Payroll.Core.Interfaces
{
    public interface IPayslip
    {
        string Name { get; set; }
        ISalaryPeriod SalaryPeriod { get; set; }
        decimal GrossIncome { get; set; }
        decimal IncomeTax { get; set; }
        decimal NetIncome { get; set; }
        decimal SuperAmount { get; set; }
    }
}
