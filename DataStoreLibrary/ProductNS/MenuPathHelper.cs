
using ModelsClassLibrary.MenuNS;
using System;
using System.Collections.Generic;
namespace DatastoreNS
{
    public class MenuPathHelper
    {

        public MenuPathHelper(string menuPath1Name,string menuPath2Name)
        {
            MenuPath1Name = menuPath1Name;
            MenuPath2Name = menuPath2Name;
        }
        public string MenuPath1Name { get; set; }
        public string MenuPath2Name { get; set; }

    }
}
