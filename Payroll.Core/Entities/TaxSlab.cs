
using Payroll.Core.Interfaces;
using System;

namespace Payroll.Core.Entities
{
    public class TaxSlab : ITaxSlab
    {
        public Guid Id { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? FixedTax { get; set; }
        public decimal MinimumSalary { get; set; }
        public decimal? MaximumSalary { get; set; }
        public int FinancialYear { get; set; }
    }
}
