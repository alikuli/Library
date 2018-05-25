using AliKuli.Extentions;
using CountryDATA.ProductNS.CategoryNS;
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.Product_Related;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Collections.Generic;
namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class ProductCatMainInitilizingDataList
    {
        public List<ProductCatMainHelper> DataList()
        {
            List<ProductCatMainHelper> lst = new List<ProductCatMainHelper>();

            CreateAutoMobiles(lst);
            CreateFoods(lst);
            CreateElectronics(lst);
            CreateClothes(lst);
            CreateMotorCycles(lst);

            return lst;


        }

        #region Creating data
        private void CreateAutoMobiles(List<ProductCatMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;

            #region Bus

            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Bus);
            c3 = Cat3Name(ProductCategory3ENUM.Toyota);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Bus);
            c3 = Cat3Name(ProductCategory3ENUM.Mercedeze);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Bus);
            c3 = Cat3Name(ProductCategory3ENUM.BMW);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Bus);
            c3 = Cat3Name(ProductCategory3ENUM.Datsun);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Bus);
            c3 = Cat3Name(ProductCategory3ENUM.Daewoo);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Bus);
            c3 = Cat3Name(ProductCategory3ENUM.Volkswagon);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));

            //-------------------------------------------------------
            #endregion

            #region Car

            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Car);
            c3 = Cat3Name(ProductCategory3ENUM.Toyota);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Car);
            c3 = Cat3Name(ProductCategory3ENUM.Mercedeze);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Car);
            c3 = Cat3Name(ProductCategory3ENUM.BMW);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Car);
            c3 = Cat3Name(ProductCategory3ENUM.Datsun);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Car);
            c3 = Cat3Name(ProductCategory3ENUM.Daewoo);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Car);
            c3 = Cat3Name(ProductCategory3ENUM.Volkswagon);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //-------------------------------------------------------
            #endregion

            #region Coaster

            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Coaster);
            c3 = Cat3Name(ProductCategory3ENUM.Toyota);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Coaster);
            c3 = Cat3Name(ProductCategory3ENUM.Mercedeze);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Coaster);
            c3 = Cat3Name(ProductCategory3ENUM.BMW);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Coaster);
            c3 = Cat3Name(ProductCategory3ENUM.Datsun);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Coaster);
            c3 = Cat3Name(ProductCategory3ENUM.Daewoo);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Coaster);
            c3 = Cat3Name(ProductCategory3ENUM.Volkswagon);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //-------------------------------------------------------
            #endregion

            #region Recreationalvehical
            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.RecreationalVehical);
            c3 = Cat3Name(ProductCategory3ENUM.Toyota);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.RecreationalVehical);
            c3 = Cat3Name(ProductCategory3ENUM.Mercedeze);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.RecreationalVehical);
            c3 = Cat3Name(ProductCategory3ENUM.BMW);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.RecreationalVehical);
            c3 = Cat3Name(ProductCategory3ENUM.Datsun);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.RecreationalVehical);
            c3 = Cat3Name(ProductCategory3ENUM.Daewoo);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.RecreationalVehical);
            c3 = Cat3Name(ProductCategory3ENUM.Volkswagon);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //-------------------------------------------------------

            #endregion

            #region Truck
            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Truck);
            c3 = Cat3Name(ProductCategory3ENUM.Toyota);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Truck);
            c3 = Cat3Name(ProductCategory3ENUM.Mercedeze);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Truck);
            c3 = Cat3Name(ProductCategory3ENUM.BMW);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Truck);
            c3 = Cat3Name(ProductCategory3ENUM.Datsun);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Truck);
            c3 = Cat3Name(ProductCategory3ENUM.Daewoo);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Truck);
            c3 = Cat3Name(ProductCategory3ENUM.Volkswagon);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //-------------------------------------------------------

            #endregion

            #region SUV

            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.SUV);
            c3 = Cat3Name(ProductCategory3ENUM.Toyota);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.SUV);
            c3 = Cat3Name(ProductCategory3ENUM.Mercedeze);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.SUV);
            c3 = Cat3Name(ProductCategory3ENUM.BMW);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.SUV);
            c3 = Cat3Name(ProductCategory3ENUM.Datsun);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.SUV);
            c3 = Cat3Name(ProductCategory3ENUM.Daewoo);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.SUV);
            c3 = Cat3Name(ProductCategory3ENUM.Volkswagon);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //-------------------------------------------------------

            #endregion

            #region Van


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Van);
            c3 = Cat3Name(ProductCategory3ENUM.Toyota);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Van);
            c3 = Cat3Name(ProductCategory3ENUM.Mercedeze);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Van);
            c3 = Cat3Name(ProductCategory3ENUM.BMW);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Van);
            c3 = Cat3Name(ProductCategory3ENUM.Datsun);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Van);
            c3 = Cat3Name(ProductCategory3ENUM.Daewoo);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Van);
            c3 = Cat3Name(ProductCategory3ENUM.Volkswagon);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));

            //-------------------------------------------------------
            #endregion

        }
        private void CreateFoods(List<ProductCatMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;


            #region Resteraunts


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Resteraunts);
            c3 = Cat3Name(ProductCategory3ENUM.PakistaniFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Resteraunts);
            c3 = Cat3Name(ProductCategory3ENUM.ChineseFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Resteraunts);
            c3 = Cat3Name(ProductCategory3ENUM.MalaysianFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Resteraunts);
            c3 = Cat3Name(ProductCategory3ENUM.JapaneseFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Resteraunts);
            c3 = Cat3Name(ProductCategory3ENUM.Pizzas);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Resteraunts);
            c3 = Cat3Name(ProductCategory3ENUM.Hamburgers);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Resteraunts);
            c3 = Cat3Name(ProductCategory3ENUM.Currys);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Resteraunts);
            c3 = Cat3Name(ProductCategory3ENUM.Sandwichs);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));

            //-------------------------------------------------------

            #endregion
            #region FoodDelivery

            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FoodDelivery);
            c3 = Cat3Name(ProductCategory3ENUM.PakistaniFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FoodDelivery);
            c3 = Cat3Name(ProductCategory3ENUM.ChineseFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FoodDelivery);
            c3 = Cat3Name(ProductCategory3ENUM.MalaysianFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FoodDelivery);
            c3 = Cat3Name(ProductCategory3ENUM.JapaneseFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FoodDelivery);
            c3 = Cat3Name(ProductCategory3ENUM.Pizzas);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FoodDelivery);
            c3 = Cat3Name(ProductCategory3ENUM.Hamburgers);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FoodDelivery);
            c3 = Cat3Name(ProductCategory3ENUM.Currys);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FoodDelivery);
            c3 = Cat3Name(ProductCategory3ENUM.Sandwichs);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));
            #endregion
            //-------------------------------------------------------

            #region FastFoods

            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FastFoods);
            c3 = Cat3Name(ProductCategory3ENUM.PakistaniFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FastFoods);
            c3 = Cat3Name(ProductCategory3ENUM.ChineseFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FastFoods);
            c3 = Cat3Name(ProductCategory3ENUM.MalaysianFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FastFoods);
            c3 = Cat3Name(ProductCategory3ENUM.JapaneseFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FastFoods);
            c3 = Cat3Name(ProductCategory3ENUM.Pizzas);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FastFoods);
            c3 = Cat3Name(ProductCategory3ENUM.Hamburgers);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FastFoods);
            c3 = Cat3Name(ProductCategory3ENUM.Currys);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.FastFoods);
            c3 = Cat3Name(ProductCategory3ENUM.Sandwichs);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));

            //-------------------------------------------------------

            #endregion
            #region SuperMarkets

            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.SuperMarkets);
            c3 = Cat3Name(ProductCategory3ENUM.PakistaniFoods);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.SuperMarkets);
            c3 = Cat3Name(ProductCategory3ENUM.Metro);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.SuperMarkets);
            c3 = Cat3Name(ProductCategory3ENUM.RahatBakery);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.SuperMarkets);
            c3 = Cat3Name(ProductCategory3ENUM.JalalSons);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.SuperMarkets);
            c3 = Cat3Name(ProductCategory3ENUM.MallOfLahore);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));



            //-------------------------------------------------------
            #endregion

            #region Fruits


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Fruits);
            c3 = Cat3Name(ProductCategory3ENUM.Metro);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Fruits);
            c3 = Cat3Name(ProductCategory3ENUM.MallOfLahore);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));

            //-------------------------------------------------------

            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Vegitables);
            c3 = Cat3Name(ProductCategory3ENUM.Metro);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.Vegitables);
            c3 = Cat3Name(ProductCategory3ENUM.MallOfLahore);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));

            //-------------------------------------------------------
            #endregion

            #region IceCreams

            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.IceCreams);
            c3 = Cat3Name(ProductCategory3ENUM.Chaman);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.IceCreams);
            c3 = Cat3Name(ProductCategory3ENUM.Macdonalds);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.IceCreams);
            c3 = Cat3Name(ProductCategory3ENUM.Hardees);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Foods);
            c2 = Cat2Name(ProductCategory2ENUM.IceCreams);
            c3 = Cat3Name(ProductCategory3ENUM.BurgerKing);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //-------------------------------------------------------
            #endregion




        }

        private void CreateElectronics(List<ProductCatMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;


            #region Electronics

            c1 = Cat1Name(ProductCategory1ENUM.Electronics);
            c2 = Cat2Name(ProductCategory2ENUM.ElectronicParts);
            c3 = Cat3Name(ProductCategory3ENUM.ComputerParts);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));



            c1 = Cat1Name(ProductCategory1ENUM.Electronics);
            c2 = Cat2Name(ProductCategory2ENUM.ElectronicParts);
            c3 = Cat3Name(ProductCategory3ENUM.MiscParts);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));
            #endregion





        }

        private void CreateClothes(List<ProductCatMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;


            #region clothes
            //-------------------------------------------------------

            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.MensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.MensJeans);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.MensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.MensTwoPieceSuits);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.MensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.MensThreePieceSuits);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.MensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.MensSwimSuits);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.MensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.MensShalwaarKameezs);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //-------------------------------------------------------


            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.WomensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.WomensJeans);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.WomensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.WomensTwoPcSuits);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.WomensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.WomensThreePcSuits);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            c2 = Cat2Name(ProductCategory2ENUM.WomensClothing);
            c3 = Cat3Name(ProductCategory3ENUM.WomensSwimsuits);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //c1 = Cat1Name(ProductCategory1ENUM.Clothes);
            //c2 = Cat2Name(ProductCategory2ENUM.WomensClothing);
            //c3 = Cat3Name(ProductCategory3ENUM.WomensShalwaarKameezs);
            //lst.Add(new ProductCatMainHelper(c1, c2, c3));


            //-------------------------------------------------------
            #endregion




        }

        private void CreateMotorCycles(List<ProductCatMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Motorcycle);
            c3 = Cat3Name(ProductCategory3ENUM.Yamaha);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Motorcycle);
            c3 = Cat3Name(ProductCategory3ENUM.Kawasaki);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Motorcycle);
            c3 = Cat3Name(ProductCategory3ENUM.BMW);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Motorcycle);
            c3 = Cat3Name(ProductCategory3ENUM.Qingqi);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));


            c1 = Cat1Name(ProductCategory1ENUM.Automobiles);
            c2 = Cat2Name(ProductCategory2ENUM.Motorcycle);
            c3 = Cat3Name(ProductCategory3ENUM.HondaMotorcycle);
            lst.Add(new ProductCatMainHelper(c1, c2, c3));





        }
        #endregion

        #region helpers
        private string Cat1Name(ProductCategory1ENUM c)
        {
            return Enum.GetName(c.GetType(), c).ToTitleSentance();
        }
        private string Cat2Name(ProductCategory2ENUM c)
        {
            return Enum.GetName(c.GetType(), c).ToTitleSentance();
        }
        private string Cat3Name(ProductCategory3ENUM c)
        {
            return Enum.GetName(c.GetType(), c).ToTitleSentance();
        }

        #endregion
    }

}
