//using ModelsClassLibrary.Models.DiscountNS;

using System.ComponentModel.DataAnnotations.Schema;
namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public partial class Product
    {

        [NotMapped]
        public string MenuPath1Id { get; set; }

        [NotMapped]
        public string MenuPath2Id { get; set; }

        [NotMapped]
        public string MenuPath3Id { get; set; }

        [NotMapped]
        public string ProductId { get; set; }

    }
}