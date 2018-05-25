using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliKuli.Extentions
{
    public static class IntExtentions
    {
        public static bool IsDefault(this int i)
        {
            return i == default(int);
        }
        public static bool IsMaxValue(this int i)
        {
            return i == int.MaxValue;
        }
        public static bool IsMinValue(this int i)
        {
            return i == int.MinValue;
        }

        public static int SetMaxValue(this int i)
        {
            return int.MaxValue;
        }

        public static int SetMinValue(this int i)
        {
            return int.MinValue;
        }
    }
}
