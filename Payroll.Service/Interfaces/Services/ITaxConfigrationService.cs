
using Payroll.Core.Interfaces;

namespace Payroll.Service.Interfaces.Services
{
    public interface ITaxConfigrationService
    {
        ITaxConfiguration GetTaxConfiguration(int financialYear);
    }
}
