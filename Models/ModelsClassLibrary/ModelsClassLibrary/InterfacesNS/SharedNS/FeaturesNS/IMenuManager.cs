using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS
{
    public interface IMenuManager
    {
        MenuPathMain MenuPathMain { get; set; }
        string MenuPathMainId { get;  }

        MenuLevelENUM MenuLevelEnum { get; set; }
        string ReturnUrl { get; set; }
        
        Product Product { get; set; }
        string ProductId { get; }

        
        
        ProductChild ProductChild { get; set; }
        string ProductChildId { get; }
        //------------------------------------------

        //string ProductId { get; set; }

        string MenuPath1Id { get; }
        string MenuPath2Id { get; }
        string MenuPath3Id { get; }

        string ControlerNameForProductVms { get; }
        bool IsMenu { get; set; }
        MenuPath1 MenuPath1 { get; }
        string MenuPath1Name { get; }
        string MenuPath2Name { get; }
        string MenuPath3Name { get; }
        string MenuPath4Name { get; }
        string MenuPath5Name { get; }

        MenuLevelENUM GetPreviousMenuLevel();


    }
}
