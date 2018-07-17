
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.MenuManagerNS.MenuStatesNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
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
    public class MenuManager : IMenuManager
    {

        //public MenuManager(string id)
        //{
        //    Id = id;
        //    MenuPathMain = new MenuPathMain();
        //}

        public MenuManager(
            string id,                      //1
            MenuPathMain menuPathMain,      //2
            Product product,                //3
            ProductChild productChild,      //4
            MenuLevelENUM menuLevelEnum,    //5
            string returnUrl,               //6
            bool isMenu,                    //7
            string controllerCurrent,       //8
            string selectId,                //9
            string searchString,            //10
            SortOrderENUM sortOrderEnum,   //11
            ActionNameENUM actionNameEnum)             //12
        {
            Load(id, menuPathMain, product, productChild, menuLevelEnum, returnUrl, isMenu, controllerCurrent, selectId, searchString, sortOrderEnum, actionNameEnum);
        }


        public void Load(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu, string controllerCurrent, string selectId, string searchString, SortOrderENUM sortOrderEnum, ActionNameENUM actionNameEnum)
        {
            Id = id;
            MenuPathMain = menuPathMain;
            Product = product;
            ProductChild = productChild;
            MenuLevelEnum = menuLevelEnum;
            ReturnUrl = returnUrl;
            IsMenu = isMenu;
            ControllerCurrentName = controllerCurrent;
            SelectId = selectId;
            SearchString = searchString;
            SortOrderEnum = sortOrderEnum;
            ActionNameEnum = actionNameEnum;
            _createMenuState = loadCreateMenuState();
        }
        /// <summary>
        /// This gives the state to the CreateMenuState.
        /// </summary>
        private ICreateEditMenuState loadCreateMenuState()
        {
            switch (MenuLevelEnum)
            {
                case MenuLevelENUM.Level_1:
                    return new CreateEditMenuState_Lv1(ControllerCurrentName, MenuPath1Id, MenuPath2Id, MenuPath3Id, ProductId, ProductChildId, MenuPathMainId, GetProductVmName());
                case MenuLevelENUM.Level_2:
                    return new CreateEditMenuState_Lv2(ControllerCurrentName, MenuPath1Id, MenuPath2Id, MenuPath3Id, ProductId, ProductChildId, MenuPathMainId, GetProductVmName());
                case MenuLevelENUM.Level_3:
                    return new CreateEditMenuState_Lv3(ControllerCurrentName, MenuPath1Id, MenuPath2Id, MenuPath3Id, ProductId, ProductChildId, MenuPathMainId, GetProductVmName());
                case MenuLevelENUM.Level_4:
                case MenuLevelENUM.Level_5:
                case MenuLevelENUM.Level_6:
                    throw new NotImplementedException("loadCreateMenuState");
                case MenuLevelENUM.unknown:
                default:
                    return new CreateEditMenuState_Empty(ControllerCurrentName, MenuPath1Id, MenuPath2Id, MenuPath3Id, ProductId, ProductChildId, MenuPathMainId, GetProductVmName());
            }

        }
        //public void Load(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu)
        //{
        //}

        IMenuManagerState _menuManagerState;
        private IMenuManagerState MenuManagerState
        {
            get
            {
                refreshMenuManagerStateProperties();
                return _menuManagerState;
            }
            set
            {
                _menuManagerState = value;
            }
        }

        /// <summary>
        /// The CreateMenuState controls the state of the Create Menu Butoons by disabling them.
        /// </summary>
        ICreateEditMenuState _createMenuState;
        public ICreateEditMenuState CreateMenuState
        {
            get
            {
                _createMenuState = loadCreateMenuState();
                return _createMenuState;
            }
        }




        /// <summary>
        /// This stores the name of the action that started it. we use this in the bread crumbs. It is the first bread crumb
        /// </summary>
        public ActionNameENUM ActionNameEnum { get; set; }

        private bool isRefreshed { get; set; }
        private void refreshMenuManagerStateProperties()
        {
            if (isRefreshed)
                return;

            _menuManagerState.Load(Id, MenuPathMain, Product, ProductChild, MenuLevelEnum, ReturnUrl, IsMenu, ControllerCurrentName, SelectId, SearchString, SortOrderEnum, ActionNameEnum);
            isRefreshed = true;

        }
        private void selectMenuManagerState()
        {
            switch (MenuLevelEnum)
            {
                case MenuLevelENUM.unknown:
                    MenuManagerState = new MenuManagerState_Empty(Id, MenuPathMain, Product, ProductChild, MenuLevelEnum, ReturnUrl, IsMenu, ControllerCurrentName, SelectId, SearchString, SortOrderEnum, ActionNameEnum);
                    break;
                case MenuLevelENUM.Level_1:
                    MenuManagerState = new MenuManagerState_Lv1(Id, MenuPathMain, Product, ProductChild, MenuLevelEnum, ReturnUrl, IsMenu, ControllerCurrentName, SelectId, SearchString, SortOrderEnum, ActionNameEnum);
                    break;
                case MenuLevelENUM.Level_2:
                    MenuManagerState = new MenuManagerState_Lv2(Id, MenuPathMain, Product, ProductChild, MenuLevelEnum, ReturnUrl, IsMenu, ControllerCurrentName, SelectId, SearchString, SortOrderEnum, ActionNameEnum);
                    break;
                case MenuLevelENUM.Level_3:
                    MenuManagerState = new MenuManagerState_Lv3(Id, MenuPathMain, Product, ProductChild, MenuLevelEnum, ReturnUrl, IsMenu, ControllerCurrentName, SelectId, SearchString, SortOrderEnum, ActionNameEnum);
                    break;
                case MenuLevelENUM.Level_4:
                    MenuManagerState = new MenuManagerState_Lv4(Id, MenuPathMain, Product, ProductChild, MenuLevelEnum, ReturnUrl, IsMenu, ControllerCurrentName, SelectId, SearchString, SortOrderEnum, ActionNameEnum);
                    break;
                case MenuLevelENUM.Level_5:
                    MenuManagerState = new MenuManagerState_Lv5(Id, MenuPathMain, Product, ProductChild, MenuLevelEnum, ReturnUrl, IsMenu, ControllerCurrentName, SelectId, SearchString, SortOrderEnum, ActionNameEnum);
                    break;
                default:
                    MenuManagerState = new MenuManagerState_Empty(Id, MenuPathMain, Product, ProductChild, MenuLevelEnum, ReturnUrl, IsMenu, ControllerCurrentName, SelectId, SearchString, SortOrderEnum, ActionNameEnum);
                    break;
            }
        }

        //public EnableButtons CreateButtons { get; set; }

        protected string Id { get; set; }
        public SortOrderENUM SortOrderEnum { get; set; }

        public MenuPathMain MenuPathMain { get; set; }
        public Product Product { get; set; }
        public ProductChild ProductChild { get; set; }

        public string ReturnUrl { get; set; }


        public bool IsMenu { get; set; }
        public string SelectId { get; set; }

        public string SearchString { get; set; }

        private MenuLevelENUM _menuLevelEnum;
        public MenuLevelENUM MenuLevelEnum
        {
            get
            {
                return _menuLevelEnum;
            }
            set
            {
                _menuLevelEnum = value;
                selectMenuManagerState();
            }
        }

        public string ControllerCurrentName { protected get; set; }


        public MenuLevelENUM GetPreviousMenuLevel() { return MenuManagerState.GetPreviousMenuLevel(); }
        //public MenuPath1 MenuPath1 { get { return MenuManagerState.MenuPath1; } }
        public string MenuPathMainId { get { return MenuManagerState.MenuPathMainId; } }
        public string MenuPath1Id { get { ;return MenuManagerState.MenuPath1Id; } }
        public string MenuPath2Id { get { return MenuManagerState.MenuPath2Id; } }
        public string MenuPath3Id { get { return MenuManagerState.MenuPath3Id; } }
        public string ProductId { get { return MenuManagerState.ProductId; } }
        public string ProductChildId { get { return MenuManagerState.ProductChildId; } }

        public string MenuPath1Name { get { return MenuManagerState.MenuPath1Name; } }
        public string MenuPath2Name { get { return MenuManagerState.MenuPath2Name; } }
        public string MenuPath3Name { get { return MenuManagerState.MenuPath3Name; } }
        public string MenuPath4Name { get { return MenuManagerState.MenuPath4Name; } }
        public string MenuPath5Name { get { return MenuManagerState.MenuPath5Name; } }

        public string LinkForEdit { get { return MenuManagerState.LinkForEdit; } }







        public string LinkForCreate { get { return MenuManagerState.LinkForCreate; } }

        public string BreadCrumbName_Lv1 { get { return MenuManagerState.BreadCrumbName_Lv1; } }

        public string BreadCrumbName_Lv2 { get { return MenuManagerState.BreadCrumbName_Lv2; } }

        public string BreadCrumbName_Lv3 { get { return MenuManagerState.BreadCrumbName_Lv3; } }

        public string BreadCrumbName_Lv4 { get { return MenuManagerState.BreadCrumbName_Lv4; } }

        public string BreadCrumbName_Lv5 { get { return MenuManagerState.BreadCrumbName_Lv5; } }

        public string BreadCrumbLink_Lv1 { get { return MenuManagerState.BreadCrumbLink_Lv1; } }

        public string BreadCrumbLink_Lv2 { get { return MenuManagerState.BreadCrumbLink_Lv2; } }

        public string BreadCrumbLink_Lv3 { get { return MenuManagerState.BreadCrumbLink_Lv3; } }

        public string BreadCrumbLink_Lv4 { get { return MenuManagerState.BreadCrumbLink_Lv4; } }

        public string BreadCrumbLink_Lv5 { get { return MenuManagerState.BreadCrumbLink_Lv5; } }

        public bool ShowCreateButton { get { return MenuManagerState.ShowCreateButton; } }

        public bool ShowEditButton { get { return MenuManagerState.ShowEditButton; } }

        public string BackToListLink { get { return MenuManagerState.BackToListLink; } }


        public string ControllerNameInViewIndexForCreate { get { return MenuManagerState.ControllerNameInViewIndexForCreate; } }

        public string GetProductVmName() { return MenuManagerState.GetProductVmName(); }











        public ActionNameENUM ActionName { get; set; }


        public string ControllerNameInViewIndexForEdit
        {
            get { return CreateMenuState.EditLinkController_ControllerName; }
        }
    }
}
