using Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payroll.Service.Services;
using Unity.Lifetime;
using Payroll.Service.Interfaces.Services;
using Payroll.Service.Interfaces.Providers;
using Payroll.Service.Providers;
using Payroll.Service.Calculators;
using Payroll.Core.Interfaces;
using Payroll.Core.Entities;
using System;
using Payroll.Service.Interfaces.Calculators;
using Payroll.Infrastructure.TestData;
using Payroll.Infrastructure;
using Payroll.Infrastructure.Interfaces;
using Payroll.Infrastructure.Repositories;
using Payroll.Infrastructure.Context;

namespace Payroll.Service.Tests
{
    [TestClass]
    public class PayslipServiceTests
    {
        UnityContainer container = new UnityContainer();

        [TestInitialize]
        public void Init()
        {
            container.RegisterType<ITaxSlabService, TaxSlabService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPayslipService, PayslipService>(new HierarchicalLifetimeManager());
            container.RegisterType<ITaxConfigurationService, TaxConfigurationService>(new HierarchicalLifetimeManager());

            container.RegisterType<IPayslipProvider, PayslipProvider>(new HierarchicalLifetimeManager());

            container.RegisterType<ITaxCalculator, TaxCalculator>(new HierarchicalLifetimeManager());

            container.RegisterType<IEmployee, Employee>(new HierarchicalLifetimeManager());
            container.RegisterType<ISalaryPeriod, SalaryPeriod>(new HierarchicalLifetimeManager());
            container.RegisterType<IPayslip, Payslip>(new HierarchicalLifetimeManager());

            container.RegisterType<IDatabaseContext, DatabaseContext>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<ITaxSlabRepository, TaxSlabRepository>();
            container.RegisterType<ITaxConfigurationRepository, TaxConfigurationRepository>();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Annual salary cannot be negative.")]
        public void GeneratePayslip_Null_Values()
        {
            var employee = container.Resolve<IEmployee>();
            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Annual salary cannot be negative.")]
        public void GeneratePayslip_NegativeIncome()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = -2500;

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);
        }

        [TestMethod]
        public void GeneratePayslip_BelowTaxableIncome()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 12000;
            employee.SuperRate = 9;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Andrew Smith", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 3, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 3, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(1000, payslip.GrossIncome);
            Assert.AreEqual(0, payslip.IncomeTax);
            Assert.AreEqual(1000, payslip.NetIncome);
            Assert.AreEqual(90, payslip.SuperAmount);
        }

        [TestMethod]
        public void GeneratePayslip_TaxableIncome()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 60050;
            employee.SuperRate = 9;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Andrew Smith", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 3, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 3, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(5004, payslip.GrossIncome);
            Assert.AreEqual(922, payslip.IncomeTax);
            Assert.AreEqual(4082, payslip.NetIncome);
            Assert.AreEqual(450, payslip.SuperAmount);
        }

        [TestMethod]
        public void GeneratePayslip_HigherIncome()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Claire";
            employee.LastName = "Wong";
            employee.AnnualSalary = 200000;
            employee.SuperRate = 10;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Claire Wong", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 3, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 3, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(16667, payslip.GrossIncome);
            Assert.AreEqual(5269, payslip.IncomeTax);
            Assert.AreEqual(11398, payslip.NetIncome);
            Assert.AreEqual(1667, payslip.SuperAmount);
        }

        [TestMethod]
        public void GeneratePayslip_ValidSuperRate()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 12000;
            employee.SuperRate = 5;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Andrew Smith", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 3, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 3, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(1000, payslip.GrossIncome);
            Assert.AreEqual(0, payslip.IncomeTax);
            Assert.AreEqual(1000, payslip.NetIncome);
            Assert.AreEqual(50, payslip.SuperAmount);
        }

        [TestMethod]
        public void GeneratePayslip_ValidSuperRate_DecimalPointValue()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 12000;
            employee.SuperRate = 7.25M;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Andrew Smith", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 3, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 3, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(1000, payslip.GrossIncome);
            Assert.AreEqual(0, payslip.IncomeTax);
            Assert.AreEqual(1000, payslip.NetIncome);
            Assert.AreEqual(73, payslip.SuperAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "SuperRate(%) Must Be Between 0 and 12.")]
        public void GeneratePayslip_InValidSuperRate()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 12000;
            employee.SuperRate = 15;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "SuperRate(%) Must Be Between 0 and 12.")]
        public void GeneratePayslip_NegativeSuperRate()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 12000;
            employee.SuperRate = -6;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);
        }

        [TestMethod]
        public void GeneratePayslip_Upto_18200_Slab()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 16500;
            employee.SuperRate = 9;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Andrew Smith", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 3, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 3, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(1375, payslip.GrossIncome);
            Assert.AreEqual(0, payslip.IncomeTax);
            Assert.AreEqual(1375, payslip.NetIncome);
            Assert.AreEqual(124, payslip.SuperAmount);
        }

        [TestMethod]
        public void GeneratePayslip_Upto_37000_Slab()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 32180;
            employee.SuperRate = 12;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 3, 1),
                EndDate = new DateTime(2017, 3, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Andrew Smith", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 3, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 3, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(2682, payslip.GrossIncome);
            Assert.AreEqual(221, payslip.IncomeTax);
            Assert.AreEqual(2461, payslip.NetIncome);
            Assert.AreEqual(322, payslip.SuperAmount);
        }

        [TestMethod]
        public void GeneratePayslip_Upto_87000_Slab()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 54280;
            employee.SuperRate = 12;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 7, 1),
                EndDate = new DateTime(2017, 7, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Andrew Smith", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 7, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 7, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(4523, payslip.GrossIncome);
            Assert.AreEqual(766, payslip.IncomeTax);
            Assert.AreEqual(3757, payslip.NetIncome);
            Assert.AreEqual(543, payslip.SuperAmount);
        }

        [TestMethod]
        public void GeneratePayslip_Upto_180000_Slab()
        {
            var employee = container.Resolve<IEmployee>();
            employee.FirstName = "Andrew";
            employee.LastName = "Smith";
            employee.AnnualSalary = 123000;
            employee.SuperRate = 11;
            employee.SalaryPeriod = new SalaryPeriod
            {
                StartDate = new DateTime(2017, 7, 1),
                EndDate = new DateTime(2017, 7, 31),
                FinancialYear = 2017
            };

            var payslipService = container.Resolve<IPayslipService>();

            var payslip = payslipService.GeneratePayslip(employee);

            Assert.AreEqual("Andrew Smith", payslip.Name);
            Assert.AreEqual(new DateTime(2017, 7, 1), payslip.SalaryPeriod.StartDate);
            Assert.AreEqual(new DateTime(2017, 7, 31), payslip.SalaryPeriod.EndDate);
            Assert.AreEqual(10250, payslip.GrossIncome);
            Assert.AreEqual(2762, payslip.IncomeTax);
            Assert.AreEqual(7488, payslip.NetIncome);
            Assert.AreEqual(1128, payslip.SuperAmount);
        }
    }
}
