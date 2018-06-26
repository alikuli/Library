using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ProductAbstractNS;
using ModelsClassLibrary.SharedNS;
using System.Collections.Generic;
namespace ModelsClassLibrary.ModelsNS.ProductNS.ProductNS
{
    public interface IProduct : IProductAbstract, IHaveMenuManager
    {
        List<CheckBoxItem> CheckedBoxesList { get; set; }
        ICollection<MenuPathMain> MenuPathMains { get; set; }
        ICollection<ProductChild> ProductChildren { get; set; }
        ICollection<ProductIdentifier> ProductIdentifiers { get; set; }
        string NameFieldsData { get; set; }
        string NameFieldsSeperator { get; }

        bool IsAutomobile { get; }

        Product SetupAndMakeProduct(string menutPath1Id, IProduct iproduct);

        //ClassesWithRightsENUM ClassNameForRights();
        //string MenuPath1Id { get; set; }
        //string MenuPath2Id { get; set; }
        //string MenuPath3Id { get; set; }
        //string ProductId { get; set; }
        //void UpdatePropertiesDuringModify(ICommonWithId icommonWithId);
    }
}
