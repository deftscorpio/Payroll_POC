
using System;

namespace Payroll.Core.Interfaces
{
    public interface ITaxSlab
    {
        Guid Id { get; set; }
        decimal? TaxRate { get; set; }
        decimal? FixedTax { get; set; }
        decimal MinimumSalary { get; set; }
        decimal? MaximumSalary { get; set; }
        int FinancialYear { get; set; }
    }
}
