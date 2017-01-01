using System;
using System.Globalization;

namespace MusicRandomizer3000.Core.Utilities
{
    public static class CommonHelper
    {
        public static double GetDouble(string value, CultureInfo currentCulture)
        {
            double result = 0;

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    if (currentCulture != null)
                    {
                        if (currentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            result = Convert.ToDouble(value.Replace(".", ","), CultureInfo.CurrentCulture);
                        }
                        else if (currentCulture.NumberFormat.NumberDecimalSeparator == ".")
                        {
                            result = Convert.ToDouble(value.Replace(",", "."), CultureInfo.CurrentCulture);
                        }
                    }
                }
                catch (FormatException)
                {
                    result = 0;
                }
                catch (OverflowException)
                {
                    result = 0;
                }
            }

            return result;
        }
    }
}