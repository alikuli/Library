//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using AliKuli.Extentions;

//namespace AliKuli.Utilities
//{
//    public static class GuidHelper
//    {
//        public static bool IsNullOrEmpty(Guid? IdGuid)
//        {
//            return IdGuid == null || !IdGuid.HasValue || IdGuid.Equals(string.Empty) || IdGuid == Guid.Empty;
//        }

//        public static string GetStringValueOfNullable(Guid? IdGuid)
//        {
//            if (IdGuid.IsNullOrEmpty())
//                return null;

//            return IdGuid.ToString();
//        }
//        public static Guid? GetGuidValueOfNullable(Guid? IdGuid)
//        {

//            return IdGuid ?? null;
//        }
//        public static Guid GetGuid_NonNullable_ValueOfNullable(Guid? IdGuid)
//        {

//            return IdGuid ?? Guid.Empty;
//        }

//    }
//}