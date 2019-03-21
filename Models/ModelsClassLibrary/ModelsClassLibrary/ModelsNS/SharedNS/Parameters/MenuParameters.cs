
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class MenuParameters
    {
        public MenuParameters()
        {
        }
        public MenuParameters(MenuENUM menuEnum, string id)
        {
            MenuEnum = menuEnum;
            Id = id;
        }

        /// <summary>
        /// The value of Id can be different. 
        /// First 3 levels of the Menu it is MenuPathMainId
        /// The 4th level it is ProductId
        /// The 5th level it is ProductChildId
        /// </summary>
        public string Id { get; set; }
        public MenuENUM MenuEnum { get; set; }

        //public string ReturnUrl //This is now depreciated. Use the BreadCrumbManager
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public string MenuPathMainId
        {
            get
            {
                switch (MenuEnum)
                {
                    case EnumLibrary.EnumNS.MenuENUM.IndexMenuPath2:
                    case EnumLibrary.EnumNS.MenuENUM.IndexMenuPath3:
                    case EnumLibrary.EnumNS.MenuENUM.IndexMenuProduct:
                        return Id;

                    case EnumLibrary.EnumNS.MenuENUM.IndexMenuPath1:
                    case EnumLibrary.EnumNS.MenuENUM.IndexMenuProductChild:
                    case EnumLibrary.EnumNS.MenuENUM.EditMenuPath1:
                    case EnumLibrary.EnumNS.MenuENUM.EditMenuPath2:
                    case EnumLibrary.EnumNS.MenuENUM.EditMenuPath3:
                    case EnumLibrary.EnumNS.MenuENUM.EditMenuPathMain:
                    case EnumLibrary.EnumNS.MenuENUM.EditMenuProduct:
                    case EnumLibrary.EnumNS.MenuENUM.EditMenuProductChild:
                    case EnumLibrary.EnumNS.MenuENUM.CreateMenuPath1:
                    case EnumLibrary.EnumNS.MenuENUM.CreateMenuPath2:
                    case EnumLibrary.EnumNS.MenuENUM.CreateMenuPath3:
                    case EnumLibrary.EnumNS.MenuENUM.CreateMenuPathMenuPathMain:
                    case EnumLibrary.EnumNS.MenuENUM.CreateMenuProduct:
                    case EnumLibrary.EnumNS.MenuENUM.CreateMenuProductChild:
                    default:
                        return "";
                }
            }
        }
        public string ProductId
        {
            get
            {
                switch (MenuEnum)
                {
                    case EnumLibrary.EnumNS.MenuENUM.IndexMenuProductChild:
                        return Id;
                    default:
                        return "";
                }
            }
        }


        public string ProductChildId
        {
            get
            {
                switch (MenuEnum)
                {
                    case EnumLibrary.EnumNS.MenuENUM.IndexMenuProductChildLandingPage:
                        return Id;
                    default:
                        return "";
                }
            }
        }
        //public string MenuPath1Id { get; set; }
        //public string MenuPath2Id { get; set; }
        //public string MenuPath3Id { get; set; }

    }
}
