using System;
using System.Collections.Generic;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class ProductDataArray
    {

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
        double shipWeight;
        double shipVol;
        string uomShipWeight;
        //string uomHeightName;
        //string uomWidthName;
        string uomLengthName;
        string uomVolumeName;
        string uomStock;
        ProductInitializerHelper pi;

        public List<ProductInitializerHelper> DataArray()
        {
            List<ProductInitializerHelper> pList = new List<ProductInitializerHelper>();
            addToyatoSalon2015(pList);
            addCar(pList);

            return pList;
        }

        private void addToyatoSalon2015(List<ProductInitializerHelper> lst)
        {

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
            shipVol = 0;

            uomVolumeName = "L";
            uomLengthName = "Ft";
            uomStock = "ea";
            //uomHeightName = "Ft";
            //uomWidthName = "Ft";


            pi = CreateProductInitializer(name, salePrice, mrsp, mlsp, cost, cat1, cat2, cat3, lastOrderedDate, height, width, length, uomShipWeight, shipWeight, shipVol, uomLengthName, uomVolumeName, uomStock);

            lst.Add(pi);

        }


        private void addCar(List<ProductInitializerHelper> lst)
        {



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
            shipVol = 0;

            uomVolumeName = "L";
            uomLengthName = "Ft";
            uomStock = "ea";

            pi = CreateProductInitializer(name, salePrice, mrsp, mlsp, cost, cat1, cat2, cat3, lastOrderedDate, height, width, length, uomShipWeight, shipWeight, shipVol, uomLengthName, uomVolumeName, uomStock);

            lst.Add(pi);


        }

        private ProductInitializerHelper CreateProductInitializer(string name, decimal salePrice, decimal mrsp, decimal mlsp, decimal cost, string cat1, string cat2, string cat3, DateTime lastOrderedDate, double height, double width, double length, string uomShipWeight, double shipWeight, double shipVol, string uomLengthName, string uomVolumeName, string uomStock)
        {

            ProductInitializerHelper ph = new ProductInitializerHelper(
                name,
                salePrice,
                mrsp,
                mlsp,
                cost,
                cat1,
                cat2,
                cat3,
                lastOrderedDate,
                height,
                width,
                length,
                uomShipWeight,
                shipWeight,
                shipVol,
                uomLengthName,
                uomVolumeName,uomStock);

            return ph;

        }
    }

}
