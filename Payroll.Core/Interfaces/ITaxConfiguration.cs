
namespace Payroll.Core.Interfaces
{
    public interface ITaxConfiguration
    {
        ISuperRate SuperRate { get; set; }
        int FinancialYear { get; set; }
    }
}
