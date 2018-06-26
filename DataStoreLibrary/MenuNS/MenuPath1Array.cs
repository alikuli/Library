using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using System;
using System.Collections.Generic;

namespace DatastoreNS
{
    /// <summary>
    /// This is a starting database of ProductCat1s that the worker can speak or understand.
    /// </summary>
    public class MenuPath1Array
    {
        public static string[] DataArray()
        {
            var menupath1Array = Enum.GetNames(typeof(MenuPath1ENUM));

            if (menupath1Array.IsNullOrEmpty())
                return null;


            for (int i = 0; i < menupath1Array.Length; i++)
            {
                menupath1Array[i] = menupath1Array[i].ToTitleSentance();
            }

            return menupath1Array;

        }

        public static MenuPath1ENUM [] DataArray2()
        {
            List<MenuPath1ENUM> lstMenuPath1ENUM = new List<MenuPath1ENUM>();

            foreach (int e in Enum.GetValues(typeof(MenuPath1ENUM)))
            {

                MenuPath1ENUM eMade = (MenuPath1ENUM) Enum.Parse(typeof(MenuPath1ENUM), e.ToString());
                lstMenuPath1ENUM.Add(eMade);
            }

            return lstMenuPath1ENUM.ToArray();
        }
    }

}
