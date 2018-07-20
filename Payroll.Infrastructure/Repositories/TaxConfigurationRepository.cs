using Payroll.Core.Interfaces;
using Payroll.Infrastructure.Interfaces;
using System.Linq;

namespace Payroll.Infrastructure.Repositories
{
    public class TaxConfigurationRepository : ITaxConfigurationRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public TaxConfigurationRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<ITaxConfiguration> GetAll()
        {
            return _databaseContext.TaxConfigurations;
        }
    }
}
