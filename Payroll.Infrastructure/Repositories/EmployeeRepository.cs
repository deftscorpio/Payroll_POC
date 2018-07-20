using Payroll.Core.Interfaces;
using Payroll.Infrastructure.Interfaces;
using System;
using System.Linq;

namespace Payroll.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public EmployeeRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEmployee Get(Guid id)
        {
            return _databaseContext.Employees.Where(e => e.Id == id).SingleOrDefault();
        }

        public IQueryable<IEmployee> GetAll()
        {
            return _databaseContext.Employees;
        }

        public void Insert(IEmployee employee)
        {
            throw new NotImplementedException();
        }
    }
}
