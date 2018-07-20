using Payroll.Core.Interfaces;
using System.Collections.Generic;

namespace Payroll.Service.Interfaces.Services
{
    public interface IPayslipService
    {
        IPayslip GeneratePayslip(IEmployee employees);
        IEnumerable<IPayslip> GeneratePayslips(IEnumerable<IEmployee> employees);
    }
}
