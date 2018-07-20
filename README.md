# Tools:
	Microsoft Visual Studio 2017
	.NET Framework 4.6.1
	Unity Framework 5.8.6
	Unit Test Framework
	
# Assumptions:
	1.	As this application does not require to save any records into the database so there is not any database implementation exist.
		1.1.	FirstName, LastName and StartDate & EndDate of SalaryPeriod are not required fields as these are not being used in Tax calculations but to display in Payslip.
		1.2.	AnnualSalary, SuperRate and FinancialYear of SalaryPeriod are required for Tax Calculations and to genearte Payslip.
		1.3.	As Salary months fall between 2 years (1st July to 30th June of next year), so accepting FinacialYear separatly. 
				It can also be extracted from the SalaryPeriod dates if required.
	2.	To save time and efforrt, the system is using hard coded data for Tax Slabs and Employees (as Repositories).
		2.1.	Tax Slabs are for 2017 by default.
	3.	Emphasis is given on the architecture, code quality, coding standards and implementation.
	4.	Application can be validated by running Test cases.
	5.	No UI implemented for any testing.
	6.	No API implemented for any testing.

# How To Run:
	Pleases make sure you have Visual Studio 2017 installed on your machine.

	1.	Open "Payroll" Solution in Visual Studio.
	2.	Build the solution clicking CTRL + Shift + B.
	3.	To Install NuGet Packages: Right click on Solution and select "Restore Nuget packages"
	4.	Go to Test > Windows > Test Explorer
	5.	This command will open the "Test Explorer" on the left pane.
	6.	Expand the "Test Explorer".
	7.	Under the "Test Explorer", there will be 3 test cases with parent class (PayslipServiceTest) and project (Payroll.Service.Test).
	8.	Right click on the project (Payroll.Service.Test) and click on "Run Selected Tests". It should run the tests.
	9.	If all test cases passes, it will show up with GREEN icon for the project, class and test methods.
	10.	If any of the test case(s) fails, it will show the RED icon over test method.
# Payroll_POC
