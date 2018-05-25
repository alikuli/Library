using System;
using AliKuli.Extentions;
using EnumLibrary.EnumNS;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class UomQuantityArray
    {
        public static string[] DataArray()
        {
            var uomArray = Enum.GetNames(typeof(UomQtyENUM));

            if (uomArray.IsNullOrEmpty())
                return null;


            for (int i = 0; i < uomArray.Length; i++)
            {
                uomArray[i] = uomArray[i].ToTitleSentance();
            }

            return uomArray;

        }
    }

}
