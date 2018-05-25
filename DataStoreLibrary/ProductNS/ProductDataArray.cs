using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using System;
using System.Collections.Generic;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class ProductDataArray
    {
        public string DataArray()
        {
            List<ProductInitializerHelper> pList = new List<ProductInitializerHelper>();

            //Product items to initalize
            string name;
            decimal salePrice;
            decimal mrsp;
            decimal mlsp;
            decimal cost;
            string cat1;
            string cat2;
            string cat3;
            DateTime lastOrderedDate;
            double height;
            double width;
            double length;
            string uomShipWeight;
            double shipWeight;



            name = "Toyato Salon 2015";
            salePrice = 30000;
            mrsp = 45000;
            mlsp = 29000;
            cost = 15000;
            cat1 = "Automobile";
            cat2 = "Car";
            cat3 = "Toyota";
            lastOrderedDate = DateTime.Now.AddYears(-1);
            height = 5;
            width = 8;
            length = 18;
            uomShipWeight = "KG";
            shipWeight = 15093;

            pList.Add(CreateProductInitializer(name,   salePrice,   mrsp,   mlsp,   cost,   cat1, cat2, cat3,   lastOrderedDate,   height,   width,   length,   uomShipWeight,   shipWeight));





            name = "Car";
            salePrice = 30000;
            mrsp = 45000;
            mlsp = 29000;
            cost = 15000;
            cat1 = "Automobile";
            cat2 = "Car";
            cat3 = "Toyota";
            lastOrderedDate = DateTime.Now.AddYears(-1);
            height = 5;
            width = 8;
            length = 18;
            uomShipWeight = "KG";
            shipWeight = 15093;

            pList.Add(CreateProductInitializer(name, salePrice, mrsp, mlsp, cost, cat1, cat2, cat3, lastOrderedDate, height, width, length, uomShipWeight, shipWeight));

            string s ="";
            return s;
        }

        private ProductInitializerHelper CreateProductInitializer(string name, decimal salePrice, decimal mrsp, decimal mlsp, decimal cost, string cat1, string cat2, string cat3, DateTime lastOrderedDate, double height, double width, double length, string uomShipWeight, double shipWeight)
        {

            ProductInitializerHelper ph = new ProductInitializerHelper(name, salePrice, mrsp, mlsp, cost, cat1, cat2, cat3, lastOrderedDate, height, width, length, uomShipWeight, shipWeight);

            return ph;

        }
    }

}
