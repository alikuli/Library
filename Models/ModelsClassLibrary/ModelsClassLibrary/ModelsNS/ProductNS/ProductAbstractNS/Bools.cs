using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.ModelsNS.ProductNS
{

    public abstract partial class ProductAbstract
    {



        /// <summary>
        /// If true, product will be displayed on Website.
        /// </summary>
        [Display(Name = "Display on Website?")]
        public bool IsDisplayedOnWebsite { get; set; }



    }
}



