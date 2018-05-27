using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using System;

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
    }

}
