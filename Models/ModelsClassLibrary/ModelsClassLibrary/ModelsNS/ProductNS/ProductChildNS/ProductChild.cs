using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.ProductChildNS
{
    /// <summary>
    /// These are the products that will be entered by users. I need to decide:
    ///     Will the users be allowed to enter that same product twice? Why would they want to do that? 
    ///     If they need to sell a used car, or the 2 used cars of the same yera, and model but different quality.
    ///     In such a case, the User would have to enter the product twice. Each product will have a different
    ///     price and serial number. If product has no serial number, eg engine number etc, then it will be a general
    ///     product, but by adding a serial numbr, it would then become different, within the product. Each serial numbered product
    ///     would then have its own price.
    /// </summary>
    public partial class ProductChild : CommonWithId, IHasUploads, IHasUser, IHaveMenuManager
    {
        public ProductChild()
        {
            Sell = new SalePriceComplex();
            Buy = new CostsComplex();

        }


        /// <summary>
        /// This will be unique for product/customer. If it does not exist, then it will be considered a general product.
        /// If serial number exists, then it is a specific product. A single seller would be able to add mulitple products like this of the
        /// same type as long as they give them different serial numbers.
        /// </summary>
        public string SerialNumber { get; set; }
        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.ProductChild; ;
        }
        public override string ClassNamePlural
        {
            get
            {
                return "ProductChildren";
            }
        }


    }





}
