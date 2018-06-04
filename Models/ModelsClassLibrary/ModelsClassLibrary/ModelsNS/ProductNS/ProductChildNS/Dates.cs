using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.ProductChildNS
{
    public partial class ProductChild
    {

        [Column(TypeName = "DateTime2")]
        [Display(Name = "Expirey(UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }
    }





}
