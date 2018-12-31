using AliKuli.Extentions;
using Data.MenuNS;
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.Product_Related;
using System;
using System.Collections.Generic;
namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class MenuPathMainInitilizingDataList
    {
        public List<MenuPathMainHelper> DataList()
        {
            List<MenuPathMainHelper> lst = new List<MenuPathMainHelper>();

            CreateAutoMobiles(lst);
            CreateFoods(lst);
            CreateElectronics(lst);
            CreateClothes(lst);
            CreateMotorCycles(lst);

            return lst;


        }

        #region Creating data
        private void CreateAutoMobiles(List<MenuPathMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;

            #region Bus

            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Bus);
            c3 = Cat3Name(MenuPath3ENUM.Toyota);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Bus);
            c3 = Cat3Name(MenuPath3ENUM.MercedesBenz);


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Bus);
            c3 = Cat3Name(MenuPath3ENUM.BMW);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Bus);
            c3 = Cat3Name(MenuPath3ENUM.Datsun);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Bus);
            c3 = Cat3Name(MenuPath3ENUM.Daewoo);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Bus);
            c3 = Cat3Name(MenuPath3ENUM.Volkswagon);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------------------------------------
            #endregion

            #region Car

            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Car);
            c3 = Cat3Name(MenuPath3ENUM.Toyota);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Car);
            c3 = Cat3Name(MenuPath3ENUM.MercedesBenz);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Car);
            c3 = Cat3Name(MenuPath3ENUM.BMW);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Car);
            c3 = Cat3Name(MenuPath3ENUM.Datsun);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Car);
            c3 = Cat3Name(MenuPath3ENUM.Daewoo);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Car);
            c3 = Cat3Name(MenuPath3ENUM.Volkswagon);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            //-------------------------------------------------------
            #endregion

            #region Coaster

            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Coaster);
            c3 = Cat3Name(MenuPath3ENUM.Toyota);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Coaster);
            c3 = Cat3Name(MenuPath3ENUM.MercedesBenz);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Coaster);
            c3 = Cat3Name(MenuPath3ENUM.BMW);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Coaster);
            c3 = Cat3Name(MenuPath3ENUM.Datsun);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Coaster);
            c3 = Cat3Name(MenuPath3ENUM.Daewoo);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Coaster);
            c3 = Cat3Name(MenuPath3ENUM.Volkswagon);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            //-------------------------------------------------------
            #endregion

            #region Recreationalvehical
            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.RecreationalVehical);
            c3 = Cat3Name(MenuPath3ENUM.Toyota);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.RecreationalVehical);
            c3 = Cat3Name(MenuPath3ENUM.MercedesBenz);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.RecreationalVehical);
            c3 = Cat3Name(MenuPath3ENUM.BMW);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.RecreationalVehical);
            c3 = Cat3Name(MenuPath3ENUM.Datsun);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.RecreationalVehical);
            c3 = Cat3Name(MenuPath3ENUM.Daewoo);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.RecreationalVehical);
            c3 = Cat3Name(MenuPath3ENUM.Volkswagon);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            //-------------------------------------------------------

            #endregion

            #region Truck
            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Truck);
            c3 = Cat3Name(MenuPath3ENUM.Toyota);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Truck);
            c3 = Cat3Name(MenuPath3ENUM.MercedesBenz);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Truck);
            c3 = Cat3Name(MenuPath3ENUM.BMW);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Truck);
            c3 = Cat3Name(MenuPath3ENUM.Datsun);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Truck);
            c3 = Cat3Name(MenuPath3ENUM.Daewoo);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Truck);
            c3 = Cat3Name(MenuPath3ENUM.Volkswagon);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            //-------------------------------------------------------

            #endregion

            #region SUV

            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.SUV);
            c3 = Cat3Name(MenuPath3ENUM.Toyota);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.SUV);
            c3 = Cat3Name(MenuPath3ENUM.MercedesBenz);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.SUV);
            c3 = Cat3Name(MenuPath3ENUM.BMW);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.SUV);
            c3 = Cat3Name(MenuPath3ENUM.Datsun);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.SUV);
            c3 = Cat3Name(MenuPath3ENUM.Daewoo);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.SUV);
            c3 = Cat3Name(MenuPath3ENUM.Volkswagon);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            //-------------------------------------------------------

            #endregion

            #region Van


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Van);
            c3 = Cat3Name(MenuPath3ENUM.Toyota);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Van);
            c3 = Cat3Name(MenuPath3ENUM.MercedesBenz);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Van);
            c3 = Cat3Name(MenuPath3ENUM.BMW);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Van);
            c3 = Cat3Name(MenuPath3ENUM.Datsun);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Van);
            c3 = Cat3Name(MenuPath3ENUM.Daewoo);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Van);
            c3 = Cat3Name(MenuPath3ENUM.Volkswagon);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------------------------------------
            #endregion

        }
        private void CreateFoods(List<MenuPathMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;


            #region Resteraunts


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Resteraunts);
            c3 = Cat3Name(MenuPath3ENUM.SaltNPepper);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Resteraunts);
            c3 = Cat3Name(MenuPath3ENUM.Macdonalds);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Resteraunts);
            c3 = Cat3Name(MenuPath3ENUM.Hardees);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Resteraunts);
            c3 = Cat3Name(MenuPath3ENUM.MinistryOfBurgers);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Resteraunts);
            c3 = Cat3Name(MenuPath3ENUM.BurgerKing);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Resteraunts);
            c3 = Cat3Name(MenuPath3ENUM.SaltNPepper);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------------------------------------

            #endregion
            #region FoodDelivery

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FoodDelivery);
            c3 = Cat3Name(MenuPath3ENUM.BurgerKing);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FoodDelivery);
            c3 = Cat3Name(MenuPath3ENUM.Macdonalds);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FoodDelivery);
            c3 = Cat3Name(MenuPath3ENUM.PizzaHut);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FoodDelivery);
            c3 = Cat3Name(MenuPath3ENUM.MinistryOfBurgers);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FoodDelivery);
            c3 = Cat3Name(MenuPath3ENUM.PizzaHut);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            #endregion
            //-------------------------------------------------------

            #region FastFoods

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FastFoods);
            c3 = Cat3Name(MenuPath3ENUM.Macdonalds);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FastFoods);
            c3 = Cat3Name(MenuPath3ENUM.Hardees);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FastFoods);
            c3 = Cat3Name(MenuPath3ENUM.BurgerKing);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FastFoods);
            c3 = Cat3Name(MenuPath3ENUM.PizzaHut);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.FastFoods);
            c3 = Cat3Name(MenuPath3ENUM.PizzaHut);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));



            //-------------------------------------------------------

            #endregion
            #region SuperMarkets

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.SuperMarkets);
            c3 = Cat3Name(MenuPath3ENUM.RahatBakery);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.SuperMarkets);
            c3 = Cat3Name(MenuPath3ENUM.Metro);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.SuperMarkets);
            c3 = Cat3Name(MenuPath3ENUM.RahatBakery);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.SuperMarkets);
            c3 = Cat3Name(MenuPath3ENUM.JalalSons);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.SuperMarkets);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));



            //-------------------------------------------------------
            #endregion

            #region Fruits


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Fruits);
            c3 = Cat3Name(MenuPath3ENUM.Metro);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Fruits);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------------------------------------

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Vegitables);
            c3 = Cat3Name(MenuPath3ENUM.Metro);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Vegitables);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------------------------------------
            #endregion

            #region IceCreams

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.IceCreams);
            c3 = Cat3Name(MenuPath3ENUM.Chaman);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.IceCreams);
            c3 = Cat3Name(MenuPath3ENUM.Macdonalds);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.IceCreams);
            c3 = Cat3Name(MenuPath3ENUM.Hardees);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.IceCreams);
            c3 = Cat3Name(MenuPath3ENUM.BurgerKing);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            //-------------------------------------------------------
            #endregion

            #region Pakistani Foods
            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.PakistaniFoods);
            c3 = Cat3Name(MenuPath3ENUM.Chaman);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.PakistaniFoods);
            c3 = Cat3Name(MenuPath3ENUM.SaltNPepper);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.PakistaniFoods);
            c3 = Cat3Name(MenuPath3ENUM.TheVillage);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            #endregion

            #region Chinese Food
            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.ChineseFoods);
            c3 = Cat3Name(MenuPath3ENUM.MeiFie);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.ChineseFoods);
            c3 = Cat3Name(MenuPath3ENUM.ChinaTown);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));
            #endregion

            #region Malaysian Foods

            #endregion


            #region JapaneseFoods

            #endregion


            #region Pizzas
            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.PakistaniFoods);
            c3 = Cat3Name(MenuPath3ENUM.PizzaHut);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.PakistaniFoods);
            c3 = Cat3Name(MenuPath3ENUM.RahatBakery);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.PakistaniFoods);
            c3 = Cat3Name(MenuPath3ENUM.JalalSons);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.PakistaniFoods);
            c3 = Cat3Name(MenuPath3ENUM.Metro);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.PakistaniFoods);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            #endregion


            #region Hamburgers
            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.RahatBakery);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.JalalSons);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.Metro);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.Macdonalds);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.Hardees);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.MinistryOfBurgers);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.BurgerKing);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.SaltNPepper);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Foods);
            c2 = Cat2Name(MenuPath2ENUM.Hamburgers);
            c3 = Cat3Name(MenuPath3ENUM.TheVillage);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            #endregion


            #region Currys

            #endregion


            #region Sandwichs

            #endregion




        }

        private void CreateElectronics(List<MenuPathMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;


            #region Electronics

            c1 = Cat1Name(MenuPath1ENUM.Electronics);
            c2 = Cat2Name(MenuPath2ENUM.ElectronicParts);
            c3 = Cat3Name(MenuPath3ENUM.ComputerBoards);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));



            c1 = Cat1Name(MenuPath1ENUM.Electronics);
            c2 = Cat2Name(MenuPath2ENUM.ElectronicParts);
            c3 = Cat3Name(MenuPath3ENUM.HardDrives);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.Electronics);
            c2 = Cat2Name(MenuPath2ENUM.ElectronicParts);
            c3 = Cat3Name(MenuPath3ENUM.BasicElectronicParts);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));
            #endregion





        }

        private void CreateClothes(List<MenuPathMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;


            #region clothes
            //-------------------------------------------------------

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensJeans);
            c3 = Cat3Name(MenuPath3ENUM.Levis);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensJeans);
            c3 = Cat3Name(MenuPath3ENUM.Gucci);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensJeans);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensJeans);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensTwoPieceSuits);
            c3 = Cat3Name(MenuPath3ENUM.JunaidJamshed);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensTwoPieceSuits);
            c3 = Cat3Name(MenuPath3ENUM.JohnTailor);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensTwoPieceSuits);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensThreePieceSuits);
            c3 = Cat3Name(MenuPath3ENUM.JunaidJamshed);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensThreePieceSuits);
            c3 = Cat3Name(MenuPath3ENUM.JohnTailor);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensThreePieceSuits);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensSwimSuits);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensShalwaarKameezs);
            c3 = Cat3Name(MenuPath3ENUM.JunaidJamshed);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensShalwaarKameezs);
            c3 = Cat3Name(MenuPath3ENUM.JohnTailor);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensShalwaarKameezs);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));





            //-------------------------------------------------------


            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensJeans);
            c3 = Cat3Name(MenuPath3ENUM.Levis);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensJeans);
            c3 = Cat3Name(MenuPath3ENUM.Gucci);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensJeans);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.MensClothing);
            c2 = Cat2Name(MenuPath2ENUM.MensJeans);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------

            c1 = Cat1Name(MenuPath1ENUM.WomensClothing);
            c2 = Cat2Name(MenuPath2ENUM.WomensTwoPcSuits);
            c3 = Cat3Name(MenuPath3ENUM.JunaidJamshed);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.WomensClothing);
            c2 = Cat2Name(MenuPath2ENUM.WomensTwoPcSuits);
            c3 = Cat3Name(MenuPath3ENUM.JohnTailor);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.WomensClothing);
            c2 = Cat2Name(MenuPath2ENUM.WomensTwoPcSuits);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------

            c1 = Cat1Name(MenuPath1ENUM.WomensClothing);
            c2 = Cat2Name(MenuPath2ENUM.WomensThreePcSuits);
            c3 = Cat3Name(MenuPath3ENUM.JunaidJamshed);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.WomensClothing);
            c2 = Cat2Name(MenuPath2ENUM.WomensThreePcSuits);
            c3 = Cat3Name(MenuPath3ENUM.Warda);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            c1 = Cat1Name(MenuPath1ENUM.WomensClothing);
            c2 = Cat2Name(MenuPath2ENUM.WomensThreePcSuits);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------

            c1 = Cat1Name(MenuPath1ENUM.WomensClothing);
            c2 = Cat2Name(MenuPath2ENUM.WomensSwimsuits);
            c3 = Cat3Name(MenuPath3ENUM.MallOfLahore);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));

            //-------------------------








            //-------------------------------------------------------
            #endregion




        }

        private void CreateMotorCycles(List<MenuPathMainHelper> lst)
        {
            string c1;
            string c2;
            string c3;


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Motorcycle);
            c3 = Cat3Name(MenuPath3ENUM.Yamaha);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Motorcycle);
            c3 = Cat3Name(MenuPath3ENUM.Kawasaki);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Motorcycle);
            c3 = Cat3Name(MenuPath3ENUM.BMW);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Motorcycle);
            c3 = Cat3Name(MenuPath3ENUM.Qingqi);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));


            c1 = Cat1Name(MenuPath1ENUM.Automobiles);
            c2 = Cat2Name(MenuPath2ENUM.Motorcycle);
            c3 = Cat3Name(MenuPath3ENUM.HondaMotorcycle);
            lst.Add(new MenuPathMainHelper(c1, c2, c3));





        }
        #endregion

        #region helpers
        private string Cat1Name(MenuPath1ENUM c)
        {
            return Enum.GetName(c.GetType(), c).ToTitleSentance();
        }
        private string Cat2Name(MenuPath2ENUM c)
        {
            return Enum.GetName(c.GetType(), c).ToTitleSentance();
        }
        private string Cat3Name(MenuPath3ENUM c)
        {
            return Enum.GetName(c.GetType(), c).ToTitleSentance();
        }

        #endregion
    }

}
