using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.FeatureNS.MenuFeatureNS
{
    public partial class MenuFeatureBiz
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            MenuFeature mf = parm.Entity as MenuFeature;
            mf.IsNullThrowException("Unable to box");

            if (mf.FeatureTypeEnum == FeatureTypeENUM.Unknown)
                mf.FeatureTypeEnum = FeatureTypeENUM.Value;
        }


    }
}
