using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softvision.BL.Helpers
{
    public static class StringExtensions
    {
        public static int ToInt(this string Input)
        {
            int result = int.TryParse(Input, out result) ? result : 0;

            return result;
        }

        public static bool isNullOrEmpty(this string Input)
        {
            if (Input == null || Input == "")
            {
                return true;
            }

            return false;
        }

        public static string GetLiteralAndNumericalFromString(this string text)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                if ((c >= 'A' && c <= 'Z')
                    || (c >= 'a' && c <= 'z')
                    || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(' ');
                }
            }

            return sb.ToString();
        }

    }
}
