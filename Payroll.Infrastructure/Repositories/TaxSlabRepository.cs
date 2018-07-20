using Payroll.Core.Interfaces;
using Payroll.Infrastructure.Interfaces;
using System;
using System.Linq;

namespace Payroll.Infrastructure.Repositories
{
    public class TaxSlabRepository : ITaxSlabRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public TaxSlabRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IQueryable<ITaxSlab> GetAll()
        {
            return _databaseContext.TaxSlabs;
        }
    }
}
