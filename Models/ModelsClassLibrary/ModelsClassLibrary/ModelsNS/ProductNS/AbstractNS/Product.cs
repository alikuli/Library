using InterfacesLibrary.SharedNS;
//using ModelsClassLibrary.Models.DiscountNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class Product : ProductAbstract
    {
        public ICollection<ProductCategoryMain> Catogories { get; set; }


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
        }

        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.Product;
        }
    }
}