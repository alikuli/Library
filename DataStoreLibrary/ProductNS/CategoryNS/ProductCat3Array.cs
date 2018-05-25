using AliKuli.Extentions;
using EnumLibrary.EnumNS.Product_Related;
using System;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class ProductCat3Array
    {
        public static string[] DataArray()
        {
            //string[] productCat3Array = 
            //{
            //    "Toyota",
            //    "Mercedeze",
            //    "BMW",
            //    "Datsun",
            //    "Daewoo",
            //    "Mercedeze",
            //    "Datsun",
            //    "Volkswagon",

            //    "Yamaha",
            //    "Kawasaki",
            //    "Qingqi",
            //    "Honda Motorcycle",

            //    "Pakistani Food",
            //    "Chinese Food",
            //    "Malaysian Food",
            //    "Japanse Food",
            //    "Pizza",
            //    "Burger",
            //    "Curry",
            //    "Sandwich",
                
            //    "Rahat Bakery",
            //    "Jallal Sons",
            //    "Metro",
            //    "Mall of Lahore",

            //    "Saleem Sons",
            //    "Jaffar Jees",
            //    "Butt Sarees",

            //    "Women Jeans",
            //    "Shalwaar Kameez",
            //    "Women Swim suits",
            //    "Women 2 Pc Suits",
            //    "Women 3 Pc Suits",

            //    "Men Jeans",
            //    "Men 2 Pc Suits",
            //    "Men 3 Pc Suits",
            //    "Men Swim suits",
            //    "Men Swim suits",
            //    "Men Shalwaar Kameez",
                
            //    "Maids",
            //    "Butlers",
            //    "CarDrivers",
            //    "Gardeners",
            //    "Janitors",

            //    "Steel Workers",
            //    "Warehouse Workers",
            //    "Construction Workers",
            //    "BusDrivers",
            //    "CraneDirvers",
            //    "TruckDrivers",

            //    //Manufactuerrs
            //    "Fruit Proccessors",
            //    "Steel",
            //    "Cement",
            //    "Electricity",

            //    "Adam and Sons wholesalers",
                
            //    "Lahore Ice Cream",
            //    "Chaman",



                

            //};

            var productCat3Array = Enum.GetNames(typeof(ProductCategory3ENUM));
            if (productCat3Array.IsNullOrEmpty())
                return null;


            for (int i = 0; i < productCat3Array.Length; i++)
            {
                productCat3Array[i] = productCat3Array[i].ToTitleSentance();
            }

            return productCat3Array;

        }
    }

}
