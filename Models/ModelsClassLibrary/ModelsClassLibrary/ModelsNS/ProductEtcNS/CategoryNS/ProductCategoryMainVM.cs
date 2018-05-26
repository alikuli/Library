using AliKuli.UtilitiesNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// This was created because of the Name that cannot be used in Linq.
    /// </summary>
    [NotMapped]
    public class ProductCategoryMainVM:CommonWithId
    {

        public Guid ProductCategory1ID { get; set; }
        public virtual ProductCategory1 ProductCategory1 { get; set; }

        
        public Guid?  ProductCategory2ID { get; set; }
        public virtual ProductCategory2 ProductCategory2 { get; set; }
        
        
        public Guid?  ProductCategory3ID { get; set; }
        public virtual ProductCategory3 ProductCategory3 { get; set; }

    }
}