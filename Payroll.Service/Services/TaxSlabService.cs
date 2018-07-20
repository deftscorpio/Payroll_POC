
using Payroll.Core.Interfaces;
using Payroll.Infrastructure.Interfaces;
using Payroll.Service.Helper;
using Payroll.Service.Interfaces.Services;
using System.Linq;

namespace Payroll.Service.Providers
{
    public class TaxSlabService : ITaxSlabService
    {
        private readonly ITaxSlabRepository _taxSlabRepository;

        public TaxSlabService(ITaxSlabRepository taxSlabRepository)
        {
            _taxSlabRepository = taxSlabRepository;
        }

        public ITaxSlab GetTaxSlab(decimal annualSalary, int financialYear)
        {
            var taxSlabs = _taxSlabRepository.GetAll();
            return taxSlabs
                .Where(t => 
                    t.FinancialYear == financialYear &&
                    MathHelper.Between(annualSalary, t.MinimumSalary, t.MaximumSalary))
                .SingleOrDefault();
        }
    }
}
