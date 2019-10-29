using EnumLibrary.EnumNS.Product_Related;
using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using EnumLibrary.EnumNS;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class ProductDataArray
    {
        //Product items to initalize
        string name;
        //decimal salePrice;
        decimal mrsp;
        decimal mlsp;
        //decimal cost;
        string menupath1;
        string menupath2;
        string menuPath3;
        DateTime lastOrderedDate;
        double height;
        double width;
        double length;
        double shipVol;

        double actualWeight;
        string uomActualWeightName;
        string uomLengthName;
        string uomVolumeName;
        string uomPurchaseName;
        string uomSaleName;
        string uomWeightListedName;
        double weightListed;

        ProductInitializerHelper pi;


        
        
        
        public List<ProductInitializerHelper> DataArray()
        {
            List<ProductInitializerHelper> pList = new List<ProductInitializerHelper>();
            addToyatoSalon2015(pList);
            addCar(pList);

            #region BMW
            addBmwX1(pList);
            addBmwX3(pList);
            addBmw7Series(pList);
            addBmwX6(pList);
            addBmwX5(pList);
            addBmw6Series(pList);
            addBmw3Series(pList);
            addBmwI8(pList);
            addBmwMSeries(pList);
            addBmwZ4(pList);
            addBmw5Series(pList);
            addBmw8Series(pList);
            addBmwX4(pList);
            addBmw4Series(pList);
            addBmwI3(pList);
            addBmw1Series(pList);
            addBmwZ3(pList);
            addSystemProduct(pList);
            

            #endregion

            #region Mercedes Benz
            MercedesBenzAClass(pList);
            MercedesBenzCClass(pList);
            MercedesBenzClaClass(pList);
            MercedesBenzClsClass(pList);
            MercedesBenzEClass(pList);
            MercedesBenzEClassWagon(pList);
            MercedesBenzGlaClass(pList);
            MercedesBenzGlcCoupe(pList);
            MercedesBenzGlcClass(pList);
            MercedesBenzGleClass(pList);
            MercedesBenzGlsClass(pList);
            MercedesBenzMetrisClass(pList);
            MercedesBenzSClass(pList);
            MercedesBenzSlClass(pList);
            MercedesBenzSlcClass(pList);
            MercedesBenzSprinter(pList);
            #endregion

            return pList;
        }

        #region Misc



        private void addSystemProduct(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "System Product";


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void addToyatoSalon2015(List<ProductInitializerHelper> lst)
        {
            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();


            name = "Toyato Salon 2015";
            //salePrice = 30000;
            mrsp = 45000;
            mlsp = 29000;
            //cost = 15000;

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

            menupath1 = "Automobiles";
            menupath2 = "Car";
            menuPath3 = "Toyota";
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);

            productIdentifiers.Clear();
            productIdentifiers.Add("XXX");
            productIdentifiers.Add("XX4534");

            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);

        }


        private void addCar(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Car";
            mrsp = 45000;
            mlsp = 29000;
            //cost = 15000;
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


            menupath1 = "Automobiles";
            menupath2 = "Car";
            menuPath3 = "Toyota";
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);

            productIdentifiers.Clear();
            productIdentifiers.Add("PT20");
            productIdentifiers.Add("PT2036");

            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        #endregion


        #region BMW
        private void addBmwX1(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "BMW X1";
            mrsp = 4000000;
            mlsp = 4000000;
            productIdentifiers.Add("BMWX1");


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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmwX3(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "BMW X3";
            productIdentifiers.Add("BMWX3");
            mrsp = 5000000;
            mlsp = 5000000;

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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmw7Series(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "BMW 7 Series";
            productIdentifiers.Add("BMW7SERIES");
            mrsp = 12000000;



            mlsp = mrsp;
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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmwX6(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "BMW X6";
            productIdentifiers.Add("BMWX6");
            mrsp = 9500000;


            mlsp = mrsp;
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



            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmwX5(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "BMW X5";
            productIdentifiers.Add("BMWX5");
            mrsp = 7800000;


            mlsp = mrsp;
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




            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmw6Series(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "BMW 6 Series";
            productIdentifiers.Add("BMW6SERIES");
            mrsp = 6300000;


            mlsp = mrsp;
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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);

            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmw3Series(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw 3 Series";
            productIdentifiers.Add("Bmw3Series");
            mrsp = 4200000;


            mlsp = mrsp;
            
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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void addBmwI8(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw i8";
            productIdentifiers.Add("Bmwi8");
            mrsp = 26500000;


            mlsp = mrsp;
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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmwMSeries(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw M Series";
            productIdentifiers.Add("BmwMSeries");
            mrsp = 135000000;


            mlsp = mrsp;
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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void addBmwZ4(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw Z 4";
            productIdentifiers.Add("BmwZ4");
            mrsp = 8500000;


            mlsp = mrsp;
            
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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmw5Series(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw 5 Series";
            productIdentifiers.Add("Bmw5Series");
            mrsp = 5600000;


            mlsp = mrsp;

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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void addBmw8Series(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw 8 Series";
            productIdentifiers.Add("Bmw8Series");
            mrsp = 8800000;


            mlsp = mrsp;
            
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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void addBmwX2(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw X2";
            productIdentifiers.Add("BmwX2");
            mrsp = 4800000;


            mlsp = mrsp;
            
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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void addBmwX4(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw X4";
            productIdentifiers.Add("BmwX4");
            mrsp = 5500000;


            mlsp = mrsp;

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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmw4Series(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw 4 Series";
            productIdentifiers.Add("Bmw4Series");
            mrsp = 5800000;


            mlsp = mrsp;

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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmwI3(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw i3";
            productIdentifiers.Add("BmwI3");
            mrsp = 10500000;


            mlsp = mrsp;

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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmw1Series(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw 1 Series";
            productIdentifiers.Add("Bmw1Series");
            mrsp = 3200000;


            mlsp = mrsp;

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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void addBmwZ3(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Bmw Z3";
            productIdentifiers.Add("BmwZ3");
            mrsp = 3200000;


            mlsp = mrsp;

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


            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.BMW.ToString().ToTitleSentance();
            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            menupaths.Add(mph);

            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        #endregion

        #region Mercedes Benz
        private void MercedesBenzAClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz A Class";
            mrsp = 4000000;
            mlsp = 4000000;
            
            productIdentifiers.Add("MercedesBenzAClass");

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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzCClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz C Class";
            mrsp = 51000000;
            
            productIdentifiers.Add("MercedesBenzCClass");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzClaClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz CLA Class";
            mrsp = 35000000;
            productIdentifiers.Add("MercedesBenzClaClass");

            
            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzClsClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz CLS Class";
            mrsp = 80000000;
            productIdentifiers.Add("MercedesBenzClsClass");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzEClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz E Class";
            mrsp = 65000000;
            productIdentifiers.Add("MercedesBenzEClass");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzEClassWagon(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz E Class Wagon";
            mrsp = 70000000;
            productIdentifiers.Add("MercedesBenzEClassWagon");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzGClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz G Class";
            mrsp = 150000000;
            productIdentifiers.Add("MercedesBenzGClass");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzGlaClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz Gla Class";
            mrsp = 42000000;
            productIdentifiers.Add("MercedesBenzGlaClass");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }


        private void MercedesBenzGlcCoupe(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz GLC Coupe";
            mrsp = 56000000;
            productIdentifiers.Add("MercedesBenzGlcCoupe");

            
            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzGlcClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz GLC Class";
            mrsp = 53000000;
            productIdentifiers.Add("MercedesBenzGlcClass");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzGleClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz GLE Class";
            mrsp = 59000000;
            productIdentifiers.Add("MercedesBenzGleClass");

            
            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzGlsClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz GLS Class";
            mrsp = 79000000;
            productIdentifiers.Add("MercedesBenzGlsClass");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzMetrisClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz Metris Class";
            mrsp = 34000000;
            productIdentifiers.Add("MercedesBenzMetrisClass");

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzSClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz S Class";
            productIdentifiers.Add("MercedesBenzSClass");
            mrsp = 94000000;

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzSlClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz SL Class";
            productIdentifiers.Add("MercedesBenzSlClass");
            mrsp = 96000000;

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzSlcClass(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz SLC Class";
            productIdentifiers.Add("MercedesBenzSlcClass");
            mrsp = 56000000;

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private void MercedesBenzSprinter(List<ProductInitializerHelper> lst)
        {


            List<MenuPathHelper> menupaths = new List<MenuPathHelper>();
            List<string> productIdentifiers = new List<string>();

            name = "Mercedes Benz Sprinter";
            productIdentifiers.Add("MercedesBenzSprinter");
            mrsp = 39000000;

            mlsp = mrsp;
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


            MenuPathHelper mph = mercedezeBenzMenuPath();
            menupaths.Add(mph);


            pi = CreateProductInitializer(name, mrsp, mlsp, lastOrderedDate, height, width, length, uomActualWeightName, actualWeight, shipVol, uomLengthName, uomVolumeName, uomPurchaseName, uomSaleName, uomWeightListedName, weightListed, menupaths, productIdentifiers);

            lst.Add(pi);


        }

        private MenuPathHelper mercedezeBenzMenuPath()
        {
            menupath1 = MenuPath1ENUM.Automobiles.ToString().ToTitleSentance();
            menupath2 = MenuPath2ENUM.Car.ToString().ToTitleSentance();
            menuPath3 = MenuPath3ENUM.MercedesBenz.ToString().ToTitleSentance();

            MenuPathHelper mph = new MenuPathHelper(menupath1, menupath2, menuPath3);
            return mph;
        }

        #endregion






        private ProductInitializerHelper CreateProductInitializer(
            string name,
            //decimal salePrice,
            decimal mrsp,
            decimal mlsp,
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
