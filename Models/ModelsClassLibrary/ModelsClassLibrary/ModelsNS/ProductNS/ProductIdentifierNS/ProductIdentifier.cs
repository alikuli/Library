//using InterfacesLibrary.ProductNS;
//using ModelsClassLibrary.ModelsNS.SharedNS;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace ModelsClassLibrary.ModelsNS.ProductNS
//{
//    /// <summary>
//    /// This allows us to have unlimited item identifiers for the product. One identifier to be used for one product i.e. Two different products
//    /// cannot have the same identifier.
//    /// </summary>
//    public class ProductIdentifier : CommonWithId, IProductIdentifier
//    {

//        [Display(Name = "Product")]
//        public string ProductId { get; set; }
//        public virtual ICollection<ProductCategoryMain> Products { get; set; }
//    }
//}
