using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    public class ProductFeature : FeatureAbstract
    {


        [Display(Name = "Product")]
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        /// <summary>
        /// If true, then this comes to the product from the menu and cannot be deleted. Because
        /// if you delete it, the menu will push it right back!
        /// </summary>
        public bool IsMenuFeature { get; set; }


        [Display(Name = "Menu Feature")]
        public string MenuFeatureId { get; set; }
        public virtual MenuFeature MenuFeature { get; set; }


        [NotMapped]
        public SelectList SelectListProducts { get; set; }



        [NotMapped]
        public SelectList SelectListFeature { get; set; }



        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.ProductFeature;
        }


        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            ProductFeature pf = icommonWithId as ProductFeature;
            pf.IsNullThrowException("Unable to unbox product features");

            ProductId = pf.ProductId;
            MenuFeatureId = pf.MenuFeatureId;
        }



        public override string MakeUniqueName()
        {
            string name = string.Format("{0}", MenuFeature.Name);
            return name;
        }



        public override string FullName()
        {
            if (Product.IsNull())
                return "";
            string name = string.Format("{2}-{0}: {1}", Name, Comment, Product.Name);
            return name;
        }


    }
}
