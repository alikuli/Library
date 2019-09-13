using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AliKuli.Extentions
{
    public static class GuidExtentions
    {
        public static bool IsNullOrEmpty(this Guid? guid)
        {
            return isNullOrEmpty(guid);
        }
        public static bool IsNullOrEmpty(this Guid guid)
        {
            return isNullOrEmpty(guid);
        }

        public static Guid GetGuidValueOfNullable(this Guid? IdGuid)
        {

            return IdGuid ?? Guid.Empty;
        }


        #region Helpers
		private static bool isNullOrEmpty(Guid? IdGuid)
        {
            return IdGuid == null || !IdGuid.HasValue || IdGuid.Equals(string.Empty) || IdGuid == Guid.Empty;
        }

        private static string getStringValueOfNullable(Guid? IdGuid)
        {
            if (IdGuid.IsNullOrEmpty())
                return null;

            return IdGuid.ToString();
        }
        private static Guid? getGuidValueOfNullable(Guid? IdGuid)
        {

            return IdGuid ?? null;
        }
        private static Guid getGuid_NonNullable_ValueOfNullable(Guid? IdGuid)
        {

            return IdGuid ?? Guid.Empty;
        }
 
	    #endregion    

        public static string StringValueOrNull(this Guid? IdGuid)
        {
            if (IdGuid.IsNullOrEmpty())
                return null;

            return IdGuid.ToString();
        }
        public static Guid? GuidValueOrNull(this Guid? IdGuid)
        {

            return IdGuid ?? null;
        }
        public static Guid GuidValueOrEmpty(this Guid? IdGuid)
        {

            return IdGuid ?? Guid.Empty;
        }

    }
}