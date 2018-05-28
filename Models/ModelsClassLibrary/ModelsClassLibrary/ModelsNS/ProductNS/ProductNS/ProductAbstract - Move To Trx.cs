using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract : CommonWithId
    {

        //I have removed this because this is just the header product. The real products will be under this.

        //[Column(TypeName = "DateTime2")]
        //[Display(Name = "Expirey(UTC)")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        //public DateTime? ExpiryDate { get; set; }



        ///// <summary>
        ///// This is the UOM we stock in. Should move to the Trx
        ///// </summary>
        //[Display(Name = "Stock UOM")]
        //public string UomStockID { get; set; }


        //public virtual UomQty UomStock { get; set; }





    }
}