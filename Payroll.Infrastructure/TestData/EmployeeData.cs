using Payroll.Core.Entities;
using Payroll.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payroll.Infrastructure.TestData
{
    public class EmployeeData
    {
        public static IQueryable<IEmployee> GenerateTestData()
        {
            var employees = new List<IEmployee>
            {
                new Employee
                {
                    Id = new Guid(),
                    FirstName = "Andrew",
                    LastName = "Smith",
                    AnnualSalary = 60050,
                    SuperRate = 9,
                    SalaryPeriod = new SalaryPeriod
                    {
                        StartDate = new DateTime(2017, 3, 1),
                        EndDate = new DateTime(2017, 3, 31)
                    }
                },

                new Employee
                {
                    Id = new Guid(),
                    FirstName = "Claire",
                    LastName = "Wong",
                    AnnualSalary = 120000,
                    SuperRate = 10,
                    SalaryPeriod = new SalaryPeriod
                    {
                        StartDate = new DateTime(2017, 3, 1),
                        EndDate = new DateTime(2017, 3, 31)
                    }
                }
            };
            return employees.AsQueryable();
        }
    }
}
