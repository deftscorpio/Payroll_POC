using Payroll.Core.Interfaces;
using Payroll.Service.Helper;
using Payroll.Service.Interfaces.Providers;
using Payroll.Service.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace Payroll.Service.Services
{
    public class PayslipService : IPayslipService
    {
        private readonly IPayslipProvider _payslipProvider;
        private readonly ITaxConfigurationService _taxConfigurationService;

        public PayslipService(IPayslipProvider payslipProvider, ITaxConfigurationService taxConfigurationService)
        {
            _payslipProvider = payslipProvider;
            _taxConfigurationService = taxConfigurationService;
        }

        public IPayslip GeneratePayslip(IEmployee employee)
        {
            if (employee.AnnualSalary <= 0) { throw new Exception("Annual salary cannot be negative."); };

            var taxConfiguration = _taxConfigurationService
                .GetTaxConfiguration(employee.SalaryPeriod.FinancialYear);

            if (!MathHelper.Between(employee.SuperRate, 
                taxConfiguration.SuperRate.Minimum, 
                taxConfiguration.SuperRate.Maximum))
            {
                throw new ArgumentOutOfRangeException(string.Format("SuperRate(%) Must Be Between {0} and {1}.",
                    taxConfiguration.SuperRate.Minimum, 
                    taxConfiguration.SuperRate.Maximum));
            }

            return _payslipProvider.GeneratePayslip(employee);
        }

        public IEnumerable<IPayslip> GeneratePayslips(IEnumerable<IEmployee> employees)
        {
            foreach (var employee in employees)
            {
                yield return _payslipProvider.GeneratePayslip(employee);
            }
        }
    }
}
