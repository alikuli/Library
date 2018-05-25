using System;
using AliKuli.Extentions;
using EnumLibrary.EnumNS;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of languages that the worker can speak or understand.
    /// </summary>
    public class CustomerCategoryData
    {
        public static string[] DataArray()
        {
            var custCatArray = Enum.GetNames(typeof(CustomerCategoryENUM));

            if (custCatArray.IsNullOrEmpty())
                return null;


            for (int i = 0; i < custCatArray.Length; i++)
            {
                custCatArray[i] = custCatArray[i].ToTitleSentance();
            }

            return custCatArray;

        }
    }
}
