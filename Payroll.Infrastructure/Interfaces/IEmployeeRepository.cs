
using Payroll.Core.Interfaces;
using System;
using System.Linq;

namespace Payroll.Infrastructure.Interfaces
{
    public interface IEmployeeRepository
    {
        IEmployee Get(Guid id);
        IQueryable<IEmployee> GetAll();
        void Insert(IEmployee employee);
    }
}
