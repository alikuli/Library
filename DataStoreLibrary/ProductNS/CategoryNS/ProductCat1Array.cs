using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using System;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class ProductCat1Array
    {
        public static string[] DataArray()
        {
            var productCat1Array = Enum.GetNames(typeof(ProductCategory1ENUM));

            if (productCat1Array.IsNullOrEmpty())
                return null;


            for (int i = 0; i < productCat1Array.Length; i++)
            {
                productCat1Array[i] = productCat1Array[i].ToTitleSentance();
            }

            return productCat1Array;

        }
    }

}
