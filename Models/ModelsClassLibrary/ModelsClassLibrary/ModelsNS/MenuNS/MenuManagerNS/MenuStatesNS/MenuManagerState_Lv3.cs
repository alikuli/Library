
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsClassLibrary.MenuNS
{

    [NotMapped]
    public class MenuManagerState_Lv3 : MenuManagerStateAbstract
    {
        public MenuManagerState_Lv3(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu, string controllerNameInCurrentView, string selectId, string searchString, SortOrderENUM sortOrderEnum, ActionNameENUM actionNameEnum)
            : base(id, menuPathMain, product, productChild, menuLevelEnum, returnUrl, isMenu, controllerNameInCurrentView, selectId, searchString, sortOrderEnum, actionNameEnum)
        {
        }

        public override string MenuPath1Id
        {
            get
            {
                _menuPathMain.IsNullThrowException();
                _menuPathMain.MenuPath1Id.IsNullOrWhiteSpaceThrowException();

                return _menuPathMain.MenuPath1Id;
            }
        }

        public override string MenuPath2Id
        {
            get
            {
                _menuPathMain.IsNullThrowException();
                _menuPathMain.MenuPath2Id.IsNullOrWhiteSpaceThrowException();

                return _menuPathMain.MenuPath2Id;
            }
        }




        public override ICreateMenuState CreateMenuState
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}