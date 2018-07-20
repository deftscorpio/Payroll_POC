using Payroll.Core.Interfaces;

namespace Payroll.Service.Interfaces.Services
{
    public interface ITaxSlabService
    {
        ITaxSlab GetTaxSlab(decimal salary, int financialYear);
    }
}
