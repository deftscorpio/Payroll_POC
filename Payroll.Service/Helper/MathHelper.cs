namespace Payroll.Service.Helper
{
    public static class MathHelper
    {
        public static bool Between(this decimal value, decimal minimum, decimal? maximum)
        {
            return maximum != null ? value >= minimum && value <= maximum : value >= minimum;
        }
    }
}
