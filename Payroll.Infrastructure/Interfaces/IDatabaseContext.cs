using Payroll.Core.Interfaces;
using System.Linq;

namespace Payroll.Infrastructure.Interfaces
{
    public interface IDatabaseContext
    {
        IQueryable<IEmployee> Employees { get; set; }
        IQueryable<ITaxSlab> TaxSlabs { get; set; }
        IQueryable<ITaxConfiguration> TaxConfigurations { get; set; }
    }
}
