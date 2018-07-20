using Payroll.Core.Interfaces;
using Payroll.Infrastructure.Interfaces;
using Payroll.Infrastructure.TestData;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Infrastructure.Context
{
    public class DatabaseContext : IDatabaseContext
    {
        public IQueryable<IEmployee> Employees { get; set; }
        public IQueryable<ITaxSlab> TaxSlabs { get; set; }
        public IQueryable<ITaxConfiguration> TaxConfigurations { get; set; }

        public DatabaseContext()
        {
            Employees = EmployeeData.GenerateTestData();
            TaxSlabs = TaxSlabData.GenerateTestData();
            TaxConfigurations = TaxConfigurationData.GenerateTaxConfigurationData();
        }
    }
}
