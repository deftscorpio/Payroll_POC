using Payroll.Core.Interfaces;
using System.Linq;

namespace Payroll.Infrastructure.Interfaces
{
    public interface ITaxConfigurationRepository
    {
        IQueryable<ITaxConfiguration> GetAll();
    }
}
