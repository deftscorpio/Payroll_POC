using Payroll.Core.Entities;
using Payroll.Core.Interfaces;
using Payroll.Service.Helper;
using Payroll.Service.Interfaces.Calculators;
using Payroll.Service.Interfaces.Providers;
using Payroll.Service.Interfaces.Services;
using System;

namespace Payroll.Service.Providers
{
    public class PayslipProvider : IPayslipProvider
    {
        private readonly ITaxCalculator _taxCalculator;
        private readonly ITaxSlabService _taxSlabService;

        public PayslipProvider(ITaxCalculator taxCalculator, ITaxSlabService taxSlabService)
        {
            _taxCalculator = taxCalculator;
            _taxSlabService = taxSlabService;
        }

        public IPayslip GeneratePayslip(IEmployee employee)
        {
            var taxSlab = _taxSlabService.GetTaxSlab(employee.AnnualSalary, employee.SalaryPeriod.FinancialYear);
            var annualTax = _taxCalculator.CalculateTax(employee.AnnualSalary, taxSlab);
            var payslip = new Payslip
            {
                Name = string.Concat(employee.FirstName, ' ', employee.LastName),
                GrossIncome = Math.Round(employee.AnnualSalary / 12, MidpointRounding.AwayFromZero),
                IncomeTax = Math.Round(annualTax / 12, MidpointRounding.AwayFromZero),
                SalaryPeriod = employee.SalaryPeriod
            };

            payslip.SuperAmount = Math.Round((payslip.GrossIncome * employee.SuperRate) / 100, MidpointRounding.AwayFromZero);
            payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;

            return payslip;
        }
    }
}
