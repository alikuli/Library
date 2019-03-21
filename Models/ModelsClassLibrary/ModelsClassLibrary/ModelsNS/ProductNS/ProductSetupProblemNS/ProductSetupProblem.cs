using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public class ProductSetupProblem : CommonWithId
    {
        [Display(Name = "Product")]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public bool ActivateProduct { get; set; }

        public string PersonId { get; set; }
        public virtual Person Person { get; set; }


    }
}
