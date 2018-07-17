
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

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
    public abstract class MenuManagerStateAbstract : IMenuManager
    {


        protected MenuPathMain _menuPathMain;
        protected Product _product;
        protected ProductChild _productChild;
        protected bool _isMenu;
        protected string _id;
        protected string _returnUrl;
        protected MenuLevelENUM _menuLevelEnum;
        protected ICreateEditMenuState _createMenuState;
        protected string _selectId;
        protected string _searchString;
        //protected string _controllerCurrentName;


        public MenuManagerStateAbstract(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu, string controllerCurrentName, string selectId, string searchString, SortOrderENUM sortOrderEnum, ActionNameENUM actionName)
        {
            Load(id, menuPathMain, product, productChild, menuLevelEnum, returnUrl, isMenu, controllerCurrentName, selectId, searchString, sortOrderEnum, actionName);
        }


        public void Load(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu, string controllerCurrentName, string selectId, string searchString, SortOrderENUM sortOrderEnum, ActionNameENUM actionName)
        {
            _id = id;
            _menuPathMain = menuPathMain;
            _product = product;
            _productChild = productChild;
            _isMenu = isMenu;
            _returnUrl = returnUrl;
            MenuLevelEnum = menuLevelEnum;
            ControllerCurrentName = controllerCurrentName;
            _selectId = selectId;
            _searchString = searchString;
            SortOrderEnum = sortOrderEnum;
            ActionName = actionName;

        }



        /// <summary>
        /// This loads the correct state for the Create Buttons and Controller Names
        /// </summary>
        private void loadCreateMenuState()
        {
            switch (MenuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    _createMenuState = new CreateEditMenuState_Empty(
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "");
                    break;
                case MenuLevelENUM.Level_1:
                    _createMenuState = new CreateEditMenuState_Lv1(
                        ControllerCurrentName,
                        _menuPathMain.MenuPath1Id,
                        _menuPathMain.MenuPath2Id,
                        _menuPathMain.MenuPath3Id,
                        _product.Id,
                        _productChild.Id,
                        _menuPathMain.Id,
                        GetProductVmName());
                    break;
                case MenuLevelENUM.Level_2:
                    _createMenuState = new CreateEditMenuState_Lv2(
                        ControllerCurrentName,
                        _menuPathMain.MenuPath1Id,
                        _menuPathMain.MenuPath2Id,
                        _menuPathMain.MenuPath3Id,
                        _product.Id,
                        _productChild.Id,
                        _menuPathMain.Id,
                        GetProductVmName());
                    break;
                case MenuLevelENUM.Level_3:
                    _createMenuState = new CreateEditMenuState_Lv3(
                        ControllerCurrentName,
                        _menuPathMain.MenuPath1Id,
                        _menuPathMain.MenuPath2Id,
                        _menuPathMain.MenuPath3Id,
                        _product.Id,
                        _productChild.Id,
                        _menuPathMain.Id,
                        GetProductVmName());
                    break;
                case MenuLevelENUM.Level_4:
                case MenuLevelENUM.Level_5:
                case MenuLevelENUM.Level_6:
                    throw new NotImplementedException("loadCreateMenuState not implemented");
                default:
                    _createMenuState = new CreateEditMenuState_Empty(
                        ControllerCurrentName,
                        _menuPathMain.MenuPath1Id,
                        _menuPathMain.MenuPath2Id,
                        _menuPathMain.MenuPath3Id,
                        _product.Id,
                        _productChild.Id,
                        _menuPathMain.Id,
                        GetProductVmName());
                    break;
            }
        }


        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
            }
        }
        public string SelectId
        {
            get
            {
                return _selectId;
            }
            set
            {
                _selectId = value;
            }
        }


        /// <summary>
        /// This stores the name of the action that started it. we use this in the bread crumbs. It is the first bread crumb
        /// </summary>
        public ActionNameENUM ActionName { get; set; }

        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public virtual string ReturnUrl
        {
            get
            {
                return _returnUrl;
            }
            set
            {
                _returnUrl = value;
            }
        }

        public virtual Product Product
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
            }
        }

        public virtual ProductChild ProductChild
        {
            get
            {
                return _productChild;
            }
            set
            {
                _productChild = value;
            }
        }

        public virtual bool IsMenu
        {
            get
            {
                return _isMenu;
            }
            set
            {
                _isMenu = value;
            }
        }

        public virtual MenuPathMain MenuPathMain
        {
            get
            {
                return _menuPathMain;
            }
            set
            {
                _menuPathMain = value;
            }
        }

        public virtual MenuLevelENUM MenuLevelEnum
        {
            get
            {
                return _menuLevelEnum;
            }
            set
            {
                _menuLevelEnum = value;
            }
        }


        //---------------------------------------------------------



        public virtual MenuPath1 MenuPath1
        {
            get
            {
                if (_menuPathMain.IsNull())
                    return null;

                if (_menuPathMain.MenuPath1.IsNull())
                    return null;


                return _menuPathMain.MenuPath1;

            }
        }

        public virtual string MenuPathMainId
        {
            get
            {
                if (_menuPathMain.IsNull())
                    return "";
                return _menuPathMain.Id;
            }
        }

        public virtual string ProductId
        {
            get
            {
                return "";
            }
        }

        public virtual string ProductChildId
        {
            get
            {
                if (_product.IsNull())
                    return "";
                return _product.Id;
            }
        }

        public virtual string MenuPath1Id
        {
            get
            {
                if (_menuPathMain.IsNull())
                    return "";
                if (_menuPathMain.MenuPath1Id.IsNullOrWhiteSpace())
                    return "";
                return _menuPathMain.MenuPath1Id;
            }
        }

        public virtual string MenuPath2Id
        {
            get
            {
                if (_menuPathMain.IsNull())
                    return "";
                if (_menuPathMain.MenuPath2Id.IsNullOrWhiteSpace())
                    return "";
                return _menuPathMain.MenuPath1Id;
            }
        }


        public virtual string MenuPath3Id
        {
            get
            {
                if (_menuPathMain.IsNull())
                    return "";
                if (_menuPathMain.MenuPath3Id.IsNullOrWhiteSpace())
                    return "";
                return _menuPathMain.MenuPath1Id;
            }
        }



        public virtual string MenuPath1Name
        {
            get
            {
                if (_menuPathMain.IsNull())
                    return "";

                if (_menuPathMain.MenuPath1.IsNull())
                    return "";
                return _menuPathMain.MenuPath1.Name;

            }
        }

        public virtual string MenuPath2Name
        {
            get
            {
                if (_menuPathMain.IsNull())
                    return "";

                if (_menuPathMain.MenuPath2.IsNull())
                    return "";
                return _menuPathMain.MenuPath2.Name;

            }
        }

        public virtual string MenuPath3Name
        {
            get
            {
                if (_menuPathMain.IsNull())
                    return "";

                if (_menuPathMain.MenuPath3.IsNull())
                    return "";

                return _menuPathMain.MenuPath3.Name;

            }
        }
        public virtual string MenuPath4Name
        {
            get
            {
                if (_product.IsNull())
                    return "";

                return _product.Name;
            }
        }

        /// <summary>
        /// This is the product level
        /// </summary>
        public virtual string MenuPath5Name
        {
            get
            {
                if (_productChild.IsNull())
                    return "";

                return _productChild.Name;
            }
        }






        /// <summary>
        /// This is what the controller will be called for all ProductVMs, basicaly the class name with a "s" attached, showing a plural.
        /// However, strict pluralization is not followed. Always a "s" is added.
        /// </summary>

        public string GetProductVmName()
        {
            if (MenuPath1.IsNull())
                return "";
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

            return _menuPathMain.MenuPath1Id;


        }


        public MenuLevelENUM GetPreviousMenuLevel()
        {
            //for the sort we need to go back one menu level
            MenuLevelENUM previousMenuLevel = MenuLevelENUM.unknown;
            switch (_menuLevelEnum)
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

        public virtual string LinkForEdit
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string LinkForCreate
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string ControllerNameInViewIndexForCreate
        {
            get { return "Menus"; }
        }

        public virtual string BreadCrumbName_Lv1
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbName_Lv2
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbName_Lv3
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbName_Lv4
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbName_Lv5
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbLink_Lv1
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbLink_Lv2
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbLink_Lv3
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbLink_Lv4
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BreadCrumbLink_Lv5
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual bool ShowCreateButton
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual bool ShowEditButton
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual string BackToListLink
        {
            get { throw new System.NotImplementedException(); }
        }

        public SortOrderENUM sortOrderEnum { get; set; }



        public SortOrderENUM SortOrderEnum { get; set; }


        public virtual ICreateEditMenuState CreateMenuState { get { return _createMenuState; } }




        public string ControllerNameInViewIndexForEdit
        {
            get { return CreateMenuState.EditLinkController_ControllerName; }
        }
        public virtual string ControllerNameInViewIndexCreateLink
        {
            get
            {
                ControllerCurrentName.IsNullOrWhiteSpace();
                return ControllerCurrentName;
            }
        }





        /// <summary>
        /// This is the controllers current name and is loaded from the View.
        /// </summary>
        public string ControllerCurrentName
        {
            private get;
            set;
        }
    }
}