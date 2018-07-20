using System;

namespace Payroll.Core.Interfaces
{
    public interface ISalaryPeriod
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        int FinancialYear { get; set; }
    }
}
