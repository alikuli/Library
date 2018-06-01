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
        string menupath1;
        string menupath2;
        string cat3;
        DateTime lastOrderedDate;
        double height;
        double width;
        double length;
        double shipVol;

        double actualWeight;
        string uomActualWeightName;
        //string uomHeightName;
        //string uomWidthName;
        string uomLengthName;
        string uomVolumeName;
        string uomPurchaseName;
        string uomSaleName;
        string uomWeightListedName;
        double weightListed;
        //string uomWeightActual;
        //string imageRelativeAddress;

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
            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();


            name = "Toyato Salon 2015";
            salePrice = 30000;
            mrsp = 45000;
            mlsp = 29000;
            cost = 15000;
            menupath1 = "Automobiles";
            menupath2 = "Car";
            cat3 = "Toyota";
            lastOrderedDate = DateTime.Now.AddYears(-1);
            height = 5;
            width = 8;
            length = 18;
            uomActualWeightName = "KG";
            actualWeight = 15093;
            shipVol = 52334;

            uomVolumeName = "L";
            uomLengthName = "Ft";
            uomPurchaseName = "Ea";
            uomSaleName = "Count";
            uomWeightListedName = "KG";
            weightListed = 23029;

            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2);
            menupaths.Add(mph);

            productIdentifiers.Clear();
            productIdentifiers.Add("XXX");
            productIdentifiers.Add("XX4534");

            pi = CreateProductInitializer(name, salePrice, mrsp, mlsp, cost, menupath1, menupath2, cat3, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);

        }


        private void addCar(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Car";
            salePrice = 30000;
            mrsp = 45000;
            mlsp = 29000;
            cost = 15000;
            menupath1 = "Automobiles";
            menupath2 = "Car";
            cat3 = "Toyota";
            lastOrderedDate = DateTime.Now.AddYears(-1);
            height = 5;
            width = 8;
            length = 18;
            uomActualWeightName = "KG";
            actualWeight = 15093;
            shipVol = 39281;

            uomVolumeName = "L";
            uomLengthName = "Ft";
            uomPurchaseName = "Ea";
            uomSaleName = "Count";
            uomWeightListedName = "KG";
            weightListed = 23023;
            //uomWeightActual = "KG";
            //imageRelativeAddress = "";
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2);
            menupaths.Add(mph);

            productIdentifiers.Clear();
            productIdentifiers.Add("PT20");
            productIdentifiers.Add("PT2036");

            pi = CreateProductInitializer(name, salePrice, mrsp, mlsp, cost, menupath1, menupath2, cat3, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private ProductInitializerHelper CreateProductInitializer(
            string name,
            decimal salePrice,
            decimal mrsp,
            decimal mlsp,
            decimal cost,
            string cat1,
            string cat2,
            string cat3,
            DateTime lastOrderedDate,
            double height,
            double width,
            double length,
            string uomShipWeight,
            double shipWeight,
            double shipVol,
            string uomLengthName,
            string uomVolumeName,
            string uomPurchaseName,
            string uomSaleName,
            string uomWeightListedName,
            double weightListed,
            List<MenuPathHelper> menupaths,
            List<string> productIdentifiers)
        {

            pi = new ProductInitializerHelper(
                name,
                height,
                width,
                length,
                uomShipWeight,
                shipWeight,
                shipVol,
                uomLengthName,
                uomVolumeName,
                uomPurchaseName,
                uomSaleName,
                uomWeightListedName,
                weightListed,
                mlsp,
                mrsp,
                menupaths,
                productIdentifiers);


            return pi;

        }
    }

}
