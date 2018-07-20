using Payroll.Core.Interfaces;

namespace Payroll.Core.Entities
{
    public class SuperRate : ISuperRate
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
    }
}
