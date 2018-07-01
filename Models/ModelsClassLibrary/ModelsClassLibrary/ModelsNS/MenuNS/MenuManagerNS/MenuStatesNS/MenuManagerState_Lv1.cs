
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.ComponentModel.DataAnnotations.Schema;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS;

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
    public class MenuManagerState_Lv1 : MenuManagerStateAbstract
    {
        public MenuManagerState_Lv1(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu, string controllerNameInCurrentView, string selectId, string searchString, SortOrderENUM sortOrderEnum, ActionNameENUM actionNameEnum)
            : base(id, menuPathMain, product, productChild, menuLevelEnum, returnUrl, isMenu, controllerNameInCurrentView, selectId, searchString, sortOrderEnum, actionNameEnum)
        {
        }


        public override string MenuPath1Name
        {
            get
            {
                if(base.MenuPath1Name.IsNullOrWhiteSpace())
                    return "List";

                return base.MenuPath1Name;
            }
        }


        public override ICreateMenuState CreateMenuState
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}