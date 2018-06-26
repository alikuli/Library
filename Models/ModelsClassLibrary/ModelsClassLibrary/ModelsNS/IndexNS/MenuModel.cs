using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;

namespace ModelsClassLibrary.ViewModels
{
    /// <summary>
    /// This contains all the items that are required extra to run menus' I have tried to use as much of the old machinery
    /// in IndexListVM and IndexItemVM.
    /// </summary>
    public class MenuModel
    {
        public MenuModel()
        {
            MenuPathMain = new MenuPathMain();
        }


        //public List<Product> ProductList { get; set; }

        public MenuLevelENUM MenuLevelEnum { get; set; }
        public bool IsMenu
        {
            get
            {
                return !MenuPathMain.IsNull();
            }
        }

        public string MenuPath1_Id
        {
            get
            {
                //MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");
                if (MenuPathMain.IsNull())
                    return "";
                if (MenuPathMain.MenuPath1Id.IsNullOrWhiteSpace())
                    return "";

                return MenuPathMain.MenuPath1Id;
            }

        }

        public MenuPath1 MenuPath1
        {
            get
            {
                MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");
                MenuPathMain.MenuPath1.IsNullThrowException("Menu Path 1 is null. Programming Error");

                return MenuPathMain.MenuPath1;
            }
        }

        public string MenuPath2_Id
        {
            get
            {
                if (MenuPathMain.IsNull())
                    return "";
                if (MenuPathMain.MenuPath2Id.IsNullOrWhiteSpace())
                    return "";

                return MenuPathMain.MenuPath2Id;
            }

        }


        public string MenuPath3_Id
        {
            get
            {
                if (MenuPathMain.IsNull())
                    return "";
                if (MenuPathMain.MenuPath3Id.IsNullOrWhiteSpace())
                    return "";

                return MenuPathMain.MenuPath3Id;
            }

        }


        public MenuPathMain MenuPathMain { get; set; }
        public Product Product { get; set; }
        public ProductChild ProductChild { get; set; }


        public string MenuPath1Name
        {
            get
            {
                MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");
                if (MenuPathMain.MenuPath1.IsNull())
                    return "";

                return MenuPathMain.MenuPath1.Name;
            }
        }

        public string MenuPath2Name
        {
            get
            {
                MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");
                if (MenuPathMain.MenuPath2.IsNull())
                    return "";

                return MenuPathMain.MenuPath2.Name;
            }
        }

        public string MenuPath3Name
        {
            get
            {
                MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");
                if (MenuPathMain.MenuPath3.IsNull())
                    return "";
                return MenuPathMain.MenuPath3.Name;
            }
        }

        /// <summary>
        /// This is the product level
        /// </summary>
        public string MenuPath4Name
        {
            get
            {
                Product.IsNullThrowException("Product is NULL. Programming Error");
                return Product.Name;
            }
        }

        /// <summary>
        /// This is the product child level.
        /// </summary>
        public string MenuPath5Name
        {
            get
            {
                ProductChild.IsNullThrowException("Product Child is NULL. Programming Error");
                return ProductChild.Name;
            }
        }

        //This gets the previous menu level
        public MenuLevelENUM GetPreviousMenuLevel()
        {
            //for the sort we need to go back one menu level
            MenuLevelENUM previousMenuLevel = MenuLevelENUM.unknown;
            switch (MenuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    break;
                case MenuLevelENUM.Level_1:
                    break;
                case MenuLevelENUM.Level_2:
                    previousMenuLevel = MenuLevelENUM.Level_1;

                    break;
                case MenuLevelENUM.Level_3:
                    previousMenuLevel = MenuLevelENUM.Level_2;
                    break;
                case MenuLevelENUM.Level_4:
                    previousMenuLevel = MenuLevelENUM.Level_3;
                    break;
                default:
                    break;
            }

            return previousMenuLevel;

        }

        //this is the return URL will be employed when in Menus
        public string ReturnUrl { get; set; }

        /// <summary>
        /// This is what the controller will be called for all ProductVMs, basicaly the class name with a "s" attached, showing a plural.
        /// However, strict pluralization is not followed. Always a "s" is added.
        /// </summary>
        public string ControlerNameForProductVms
        {
            get
            {

                switch (MenuPath1.MenuPath1Enum)
                {
                    case MenuPath1ENUM.Unknown:
                        break;
                    case MenuPath1ENUM.Automobiles:
                        return typeof(ProductAutomobileVM).Name + "s";

                    case MenuPath1ENUM.MensClothing:
                        break;
                    case MenuPath1ENUM.WomensClothing:
                        break;
                    case MenuPath1ENUM.Electronics:
                        break;
                    case MenuPath1ENUM.Foods:
                        break;
                    case MenuPath1ENUM.HomeServants:
                        break;
                    case MenuPath1ENUM.FactoryWorkers:
                        break;
                    case MenuPath1ENUM.OfficeWorkers:
                        break;
                    case MenuPath1ENUM.Machines:
                        break;
                    case MenuPath1ENUM.Stationary:
                        break;
                    case MenuPath1ENUM.FruitProccessors:
                        break;
                    case MenuPath1ENUM.Steel:
                        break;
                    case MenuPath1ENUM.Cement:
                        break;
                    case MenuPath1ENUM.Electricity:
                        break;
                    default:
                        break;
                }

                return MenuPathMain.MenuPath1Id;

            }

        }
    }
}
