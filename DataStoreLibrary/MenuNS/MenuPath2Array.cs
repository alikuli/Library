using AliKuli.Extentions;
using System;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class MenuPath2Array
    {

        public static string[] DataArray()
        {
            //string[] menupath2Array = 
            //{

            //    "Car",
            //    "Coaster",
            //    "Recreational Vehical",
            //    "Truck",
            //    "Motorcycle",
            //    "Bus",
            //    "SUV",
            //    "Truck",
            //    "Van",
            //    "Vegitables", // note this has no cat 3
            //    "Fruits", // note this has no cat 3
                //"FastFood",
                //"Ethnic Food",
                //"Ladies Clothes",
                //"Gent Clothes",
                //"Cloth",
                //"HouseHold Appliances", // note this has no cat 3
                //"Factory Appliances",// note this has no cat 3
                //"Farming Machines",// note this has no cat 3
                //"Road Building Machines",// note this has no cat 3
                //"Textile Machines",// note this has no cat 3
                //"House Machines",
                //"Mechanics Machines",
            //};

            var menupath2Array = Enum.GetNames(typeof(EnumLibrary.EnumNS.MenuPath2ENUM));
            
            if (menupath2Array.IsNullOrEmpty())
                return null;


            for (int i = 0; i < menupath2Array.Length; i++)
            {
                menupath2Array[i] = menupath2Array[i].ToTitleSentance();
            }

            return menupath2Array;

        }
    }

}
