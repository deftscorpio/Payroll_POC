using Payroll.Core.Interfaces;
using Payroll.Infrastructure.Interfaces;
using Payroll.Service.Interfaces.Services;
using System.Linq;

namespace Payroll.Service.Services
{
    public class TaxConfigurationService : ITaxConfigurationService
    {
        private readonly ITaxConfigurationRepository _taxConfigurationRepository;

        public TaxConfigurationService(ITaxConfigurationRepository taxConfigurationRepository)
        {
            _taxConfigurationRepository = taxConfigurationRepository;
        }

        public ITaxConfiguration GetTaxConfiguration(int financialYear)
        {
            var taxConfigurations = _taxConfigurationRepository.GetAll();
            return taxConfigurations
                .Where(t =>t.FinancialYear == financialYear)
                .SingleOrDefault();
        }
    }
}
