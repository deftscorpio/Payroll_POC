using Payroll.Core.Interfaces;
using Payroll.Service.Interfaces.Calculators;
using System;

namespace Payroll.Service.Calculators
{
    public class TaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(decimal annualSalary, ITaxSlab taxSlab)
        {
            decimal annualTax = 0;

            if(taxSlab.TaxRate != null)
                annualTax = Convert.ToDecimal(((annualSalary - (taxSlab.MinimumSalary - 1)) * taxSlab.TaxRate) / 100);

            return annualTax + (taxSlab.FixedTax ?? 0);
        }
    }
}
