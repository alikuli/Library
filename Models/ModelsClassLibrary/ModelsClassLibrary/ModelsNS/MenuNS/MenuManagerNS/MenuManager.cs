
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Routing;

namespace ModelsClassLibrary.MenuNS
{
    /// <summary>
    /// This class is a part of all models that serve in the Menu.
    /// Note. All the Menupath1, MenuPath2, MenuPath3 and their Ids are derived from MenuPathMain.
    /// The job of the Menu Path is to help in the Views.
    /// 
    /// Level 1.
    ///     Menu Path helps by tracking that it is a menu.
    ///     The MenusController is the Main Controller here
    ///     
    /// Level 2.
    ///     Menu Path helps by tracking that it is a menu.
    ///     It tracks a MenuPathMain in which the MenuPath1 is significant.
    ///     The MenusController is the Main Controller here
    /// Level 3.
    ///     Menu Path helps by tracking that it is a menu.
    ///     It tracks a MenuPathMain in which the MenuPath1 and MenuPath2 is significant.
    ///     The MenusController is the Main Controller here
    ///     
    /// Level 4.
    ///     Menu Path helps by tracking that it is a menu.
    ///     It tracks a MenuPathMain in which the MenuPath1 and MenuPath2 and MenPath3 is significant.
    ///     The MenusController is the Main Controller here
    ///     
    /// Level 5.
    ///     Menu Path helps by tracking that it is a menu.
    ///     It tracks a MenuPathMain in which the MenuPath1 and MenuPath2 and MenPath3 is significant.
    ///     The MenusController is the Main Controller here
    ///     The ProductController VM is for Edit.
    ///     The ProductController VM is for Create
    ///     It also tracks the product.
    ///     For Edit, it tracks the returnUrl
    ///     
    ///     
    /// </summary>
    [NotMapped]
    public class MenuManager : IMenuManager
    {

        public MenuManager(string id)
        {
            Id = id;
            MenuPathMain = new MenuPathMain();
        }

        public MenuManager(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu)
            : this(id)
        {
            Load(id, menuPathMain, product, productChild, menuLevelEnum, returnUrl, isMenu);
        }


        public void Load(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu)
        {
            MenuPathMain = menuPathMain;
            Product = product;
            ProductChild = productChild;
            MenuLevelEnum = menuLevelEnum;
            ReturnUrl = returnUrl;
            IsMenu = isMenu;
        }

        #region MenuPathMain Related
        /// <summary>
        /// This is the current MenuPathMain.
        /// </summary>
        public MenuPathMain MenuPathMain { get; set; }
        /// <summary>
        /// This Id has various values.
        /// When MenuLevel is 1 to 3 - This is MenuPath1Id
        /// When MenuLevel is 4 -This is a productId 
        /// When MenuLevel is 5 -This is a productChildId 
        /// 
        /// </summary>
        protected string Id { get; set; }

        public MenuPath1 MenuPath1
        {
            get
            {
                MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");
                MenuPathMain.MenuPath1.IsNullThrowException("Menu Path 1 is null. Programming Error");


                return MenuPathMain.MenuPath1;
            }
        }

        public string MenuPathMainId
        {
            get
            {
                switch (MenuLevelEnum)
                {
                    case MenuLevelENUM.unknown:
                        throw new Exception("Menu Level is unknown. MenuManager");

                    case MenuLevelENUM.Level_1:
                    case MenuLevelENUM.Level_2:
                    case MenuLevelENUM.Level_3:
                    case MenuLevelENUM.Level_4:
                        return Id;
                    case MenuLevelENUM.Level_5:
                    default:
                        return "";
                }
            }
        }

        public string ProductId
        {
            get
            {
                switch (MenuLevelEnum)
                {
                    case MenuLevelENUM.unknown:
                    case MenuLevelENUM.Level_1:
                    case MenuLevelENUM.Level_2:
                    case MenuLevelENUM.Level_3:
                    case MenuLevelENUM.Level_4:
                        return "";
                    case MenuLevelENUM.Level_5:
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
                switch (MenuLevelEnum)
                {
                    case MenuLevelENUM.unknown:
                    case MenuLevelENUM.Level_1:
                    case MenuLevelENUM.Level_2:
                    case MenuLevelENUM.Level_3:
                    case MenuLevelENUM.Level_4:
                        return "";

                    case MenuLevelENUM.Level_5:
                        return Id;
                    default:
                        return "";
                }
            }

        }

        public string MenuPath1Id
        {
            get
            {
                //MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");
                if (MenuPathMain.IsNull())
                    return "";
                if (MenuPathMain.MenuPath1Id.IsNullOrWhiteSpace())
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.unknown)
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.Level_1)
                    return "";

                return MenuPathMain.MenuPath1Id;
            }

        }

        public string MenuPath2Id
        {
            get
            {
                if (MenuPathMain.IsNull())
                    return "";
                if (MenuPathMain.MenuPath2Id.IsNullOrWhiteSpace())
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.unknown)
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.Level_1)
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.Level_2)
                    return "";

                return MenuPathMain.MenuPath2Id;
            }

        }


        public string MenuPath3Id
        {
            get
            {
                if (MenuPathMain.IsNull())
                    return "";
                if (MenuPathMain.MenuPath3Id.IsNullOrWhiteSpace())
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.unknown)
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.Level_1)
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.Level_2)
                    return "";

                if (MenuLevelEnum == MenuLevelENUM.Level_3)
                    return "";

                return MenuPathMain.MenuPath3Id;
            }

        }


        public bool IsMenu { get; set; }


        public string MenuPath1Name
        {
            get
            {
                //MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");

                if (MenuPathMain.IsNull())
                    return "";

                if (MenuPathMain.MenuPath1.IsNull())
                    return "";

                return MenuPathMain.MenuPath1.Name;
            }
        }

        public string MenuPath2Name
        {
            get
            {
                //MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");
                if (MenuPathMain.IsNull())
                    return "";

                if (MenuPathMain.MenuPath2.IsNull())
                    return "";

                return MenuPathMain.MenuPath2.Name;
            }
        }

        public string MenuPath3Name
        {
            get
            {
                //MenuPathMain.IsNullThrowException("Menu Path Main is NULL. Programming Error");

                if (MenuPathMain.IsNull())
                    return "";

                if (MenuPathMain.MenuPath3.IsNull())
                    return "";
                return MenuPathMain.MenuPath3.Name;
            }
        }


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


        #endregion


        #region Product Related

        //public string ProductId { get; set; }

        public Product Product { get; set; }



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

        #endregion


        #region  ProductChild Related
        public ProductChild ProductChild { get; set; }
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

        #endregion

        #region MenuLevelEnum Related
        public MenuLevelENUM MenuLevelEnum { get; set; }

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

        #endregion

        /// <summary>
        /// This is what the controller will be called for all ProductVMs, basicaly the class name with a "s" attached, showing a plural.
        /// However, strict pluralization is not followed. Always a "s" is added.
        /// </summary>


        public string ReturnUrl { get; set; }


    }
}