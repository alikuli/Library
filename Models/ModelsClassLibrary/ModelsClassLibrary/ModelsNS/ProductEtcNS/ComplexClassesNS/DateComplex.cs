using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS
{
    [ComplexType]
    public class ProductDateComplex
    {
        /// <summary>
        /// This is the last Ordered date -Not presisted, calculated.
        /// </summary>
        /// 
        //[Display(Name = "Last Ordered Date (UTC)")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        //[NotMapped]
        //public DateTime? LastOrderedDate { get; set; }


    }
}
