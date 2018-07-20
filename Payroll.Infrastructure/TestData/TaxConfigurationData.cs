
using Payroll.Core.Entities;
using Payroll.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Infrastructure.TestData
{
    public static class TaxConfigurationData
    {
        public static IQueryable<ITaxConfiguration> GenerateTaxConfigurationData()
        {
            var taxConfigurations = new List<ITaxConfiguration>
            {
                new TaxConfiguration {
                    SuperRate = new SuperRate
                        {
                            Minimum = 0,
                            Maximum = 12
                        },
                    FinancialYear = 2017
                }
            };

            return taxConfigurations.AsQueryable();
        }
    }
}
