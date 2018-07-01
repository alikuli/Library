
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.ComponentModel.DataAnnotations.Schema;
namespace ModelsClassLibrary.MenuNS
{

    [NotMapped]
    public class MenuManagerState_Lv2 : MenuManagerStateAbstract
    {
        public MenuManagerState_Lv2(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu, string controllerNameInCurrentView, string selectId, string searchString, SortOrderENUM sortOrderEnum, ActionNameENUM actionNameEnum)
            : base(id, menuPathMain, product, productChild, menuLevelEnum, returnUrl, isMenu, controllerNameInCurrentView, selectId, searchString, sortOrderEnum, actionNameEnum)
        {
        }


        public override string MenuPath1Id
        {
            get
            {
                _menuPathMain.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
                return _menuPathMain.MenuPath1Id;
            }
        }
        public override string ControllerNameInViewIndexCreateLink
        {
            get
            {
                if (_isMenu)
                    return GetProductVmName();

                return base.ControllerNameInViewIndexCreateLink;
            }
        }




        public override ICreateMenuState CreateMenuState
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}