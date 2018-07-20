using Payroll.Core.Interfaces;

namespace Payroll.Service.Interfaces.Services
{
    public interface ITaxConfigurationService
    {
        ITaxConfiguration GetTaxConfiguration(int financialYear);
    }
}