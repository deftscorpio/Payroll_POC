using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities
{
    public class TaxConfiguration : ITaxConfiguration
    {
        public ISuperRate SuperRate { get; set; }
        public int FinancialYear { get; set; }
    }
}
