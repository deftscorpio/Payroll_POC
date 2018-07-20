
using Payroll.Core.Interfaces;
using System;
using System.Linq;

namespace Payroll.Infrastructure.Interfaces
{
    public interface ITaxSlabRepository
    {
        IQueryable<ITaxSlab> GetAll();
    }
}
