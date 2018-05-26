using InterfacesLibrary.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// This allows us to have unlimited item identifiers for the product. One identifier to be used for one product i.e. Two different products
    /// cannot have the same identifier.
    /// </summary>
    public class ProductIdentifier : CommonWithId, IProductIdentifier
    {
        /// <summary>
        /// This is the product that owns the product Identifier
        /// </summary>
        [Display(Name = "Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
