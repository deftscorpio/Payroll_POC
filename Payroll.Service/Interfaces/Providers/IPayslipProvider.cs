
using Payroll.Core.Interfaces;

namespace Payroll.Service.Interfaces.Providers
{
    public interface IPayslipProvider
    {
        IPayslip GeneratePayslip(IEmployee employee);
    }
}
