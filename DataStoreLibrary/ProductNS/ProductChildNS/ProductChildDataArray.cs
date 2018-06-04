using System;
using System.Collections.Generic;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class ProductChildtDataArray
    {
        public List<ProductChildInitializer> DataArray()
        {
            List<ProductChildInitializer> pList = new List<ProductChildInitializer>();

            string parentName = "Car";
            ProductChildInitializer p = new ProductChildInitializer("Car -slightly used", "admin", 20000, 0, DateTime.Now.AddDays(3), "", parentName);
            pList.Add(p);

            ProductChildInitializer p1 = new ProductChildInitializer("Car -Not so Used", "admin", 13000, 0, DateTime.Now.AddDays(3), "1", parentName);
            pList.Add(p1);

            ProductChildInitializer p2 = new ProductChildInitializer("Car very used", "admin", 10000, 0, DateTime.Now.AddDays(3), "2", parentName);
            pList.Add(p2);

            ProductChildInitializer p3 = new ProductChildInitializer("Car yes but barely", "admin", 19000, 0, DateTime.Now.AddDays(3), "3", parentName);
            pList.Add(p3);

            ProductChildInitializer p4 = new ProductChildInitializer("Car is red", "admin", 18900, 0, DateTime.Now.AddDays(3), "4", parentName);
            pList.Add(p4);

            return pList;
        }


    }

}
