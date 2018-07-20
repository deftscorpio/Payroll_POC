using Payroll.Core.Interfaces;

namespace Payroll.Service.Interfaces.Calculators
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal annualSalary, ITaxSlab taxSlab);
    }
}
