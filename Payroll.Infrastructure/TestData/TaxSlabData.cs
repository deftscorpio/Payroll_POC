using Payroll.Core.Entities;
using Payroll.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Infrastructure.TestData
{
    public static class TaxSlabData
    {
        public static IQueryable<ITaxSlab> GenerateTestData()
        {
            var taxSlabs = new List<ITaxSlab>
            {
                new TaxSlab
                {
                    Id = new Guid(),
                    FinancialYear = 2017,
                    MinimumSalary = 0,
                    MaximumSalary = 18200,
                    FixedTax = null,
                    TaxRate = null
                },

                new TaxSlab
                {
                    Id = new Guid(),
                    FinancialYear = 2017,
                    MinimumSalary = 18201,
                    MaximumSalary = 37000,
                    FixedTax = null,
                    TaxRate = 19.0M
                },

                new TaxSlab
                {
                    Id = new Guid(),
                    FinancialYear = 2017,
                    MinimumSalary = 37001,
                    MaximumSalary = 87000,
                    FixedTax = 3572,
                    TaxRate = 32.5M
                },

                new TaxSlab
                {
                    Id = new Guid(),
                    FinancialYear = 2017,
                    MinimumSalary = 87001,
                    MaximumSalary = 180000,
                    FixedTax = 19822,
                    TaxRate = 37.0M
                },

                new TaxSlab
                {
                    Id = new Guid(),
                    FinancialYear = 2017,
                    MinimumSalary = 180001,
                    MaximumSalary = null,
                    FixedTax = 54232,
                    TaxRate = 45M
                }
            };

            return taxSlabs.AsQueryable();
        }
    }
}
