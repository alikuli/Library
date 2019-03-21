using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using ModelsClassLibrary.ModelsNS.MenuNS;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    /// <summary>
    /// The name will hold th feature name
    /// </summary>
    public class FeatureAbstract : CommonWithId
    {

        [Display(Name = "Feature Type")]
        public FeatureTypeENUM FeatureTypeEnum { get; set; }

        [NotMapped]
        public SelectList SelectListFeatureTypeEnum { get { return EnumExtention.ToSelectListSorted<FeatureTypeENUM>(FeatureTypeENUM.Unknown); } }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            FeatureAbstract f = icommonWithId as FeatureAbstract;
            FeatureTypeEnum = f.FeatureTypeEnum;

        }

       public ProductChildFeature ToProductChildFeature()
       {
           ProductChildFeature pcf = new ProductChildFeature();
           pcf.Name = Name;
           pcf.Comment = Comment;
           return pcf;
       }

    }

    /// <summary>
    /// The name will hold th feature name
    /// </summary>
    //public class CopyOfFeatureAbstract : CommonWithId
    //{

    //    [Display(Name = "Feature Type")]
    //    public FeatureTypeENUM FeatureTypeEnum { get; set; }

    //    [NotMapped]
    //    public SelectList SelectListFeatureTypeEnum { get { return EnumExtention.ToSelectListSorted<FeatureTypeENUM>(FeatureTypeENUM.Unknown); } }

    //    public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
    //    {
    //        base.UpdatePropertiesDuringModify(icommonWithId);
    //        CopyOfFeatureAbstract f = icommonWithId as CopyOfFeatureAbstract;
    //        FeatureTypeEnum = f.FeatureTypeEnum;

    //    }

    //}

}
