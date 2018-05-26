using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class ProductCatVM : ProductCategoryAbstract
    {
        public ICollection<ProductCategoryMain> ProductCategoriesList { get; set; }
    }
}