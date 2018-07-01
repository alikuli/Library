﻿using System;

namespace AliKuli.Extentions
{
    public static class ObjectExtentions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNullThrowException(this object obj, string errorMsg = "")
        {
            if (obj.IsNull())
            {
                if (errorMsg.IsNullOrWhiteSpace())
                    throw new Exception(string.Format("Obj '{0}' is null.",obj.GetType().Name));
                else
                    throw new Exception(errorMsg);

            }
            return obj.IsNull();
        }
        public static bool IsNullThrowExceptionArgument(this object obj, string errorMsg = "")
        {
            if (obj.IsNull())
            {
                if (errorMsg.IsNullOrWhiteSpace())
                    throw new ArgumentException(string.Format("Argument: '{0} is null.",obj.GetType().Name));
                else
                    throw new Exception(errorMsg);

            }
            return obj.IsNull();
        }
    }
}