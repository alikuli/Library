using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliKuli.Extentions
{
    public static class ObjectExtentions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}