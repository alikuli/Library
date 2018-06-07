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

        public static bool IsNullThrowException(this object obj, string errorMsg="")
        {
            if(obj.IsNull())
            {
                if (errorMsg.IsNullOrWhiteSpace())
                    throw new Exception("Item is null.");
                else
                    throw new Exception(errorMsg);

            }
            return obj.IsNull();
        }
    }
}