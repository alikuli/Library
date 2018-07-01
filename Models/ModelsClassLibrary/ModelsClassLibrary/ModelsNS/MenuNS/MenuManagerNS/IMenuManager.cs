using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuManagerNS.MenuStatesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS
{
    public interface IMenuManager : IMenuManagerState
    {
        MenuPathMain MenuPathMain { get; set; }

        MenuLevelENUM MenuLevelEnum { get; set; }
        string ReturnUrl { get; set; }

        Product Product { get; set; }



        ProductChild ProductChild { get; set; }
        bool IsMenu { get; set; }
        string SelectId { get; set; }
        string SearchString { get; set; }

        SortOrderENUM SortOrderEnum { get; set; }

        /// <summary>
        /// Thisstores the Action that the Menu was generated from. We use this as the first Entry in the Menu Crumbs.
        /// </summary>
        ActionNameENUM ActionName { get; set; }

        //------------------------------------------



    }
}
