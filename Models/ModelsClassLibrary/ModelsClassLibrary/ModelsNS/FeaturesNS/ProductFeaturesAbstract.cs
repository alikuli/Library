using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.FeaturesNS
{
    /// <summary>
    /// The name will hold th feature name
    /// </summary>
    public class ProductFeatureAbstract : FeatureAbstract
    {

        public string Text { get; set; }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            ProductFeatureAbstract f = icommonWithId as ProductFeatureAbstract;

            Text = f.Text;

        }
    }
}
