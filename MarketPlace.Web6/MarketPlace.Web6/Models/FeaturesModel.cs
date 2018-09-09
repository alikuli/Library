using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AliKuli.Extentions;
namespace MarketPlace.Web6.Models
{
    public class FeaturesModel
    {
        public FeaturesModel()
        {
            OriginalDirtyFlag = this.GetHashCode();
            NewDirtyFlag = this.GetHashCode();
        }

        public bool IsUpadated;
        public string Id { get; set; }
        public string FeatureName { get; set; }
        public string FeatureValue { get; set; }

        public int OrignalHashCode { get; set; }
        public int OriginalDirtyFlag { get; set; }
        public int NewDirtyFlag { get; set; }

        public bool IsChanged
        {
            get
            {
                return OriginalDirtyFlag != NewDirtyFlag;
            }
        }

        public bool IsDeleted
        {
            get;
            set;
        }

        public override int GetHashCode()
        {
            if (FeatureName.IsNullOrWhiteSpace() && FeatureValue.IsNullOrWhiteSpace())
                return 0 + IsDeleted.GetHashCode(); ;

            if (FeatureName.IsNullOrWhiteSpace() && !FeatureValue.IsNullOrWhiteSpace())
                return FeatureValue.GetHashCode() + IsDeleted.GetHashCode(); 

            if (!FeatureName.IsNullOrWhiteSpace() && FeatureValue.IsNullOrWhiteSpace())
                return FeatureName.GetHashCode() + IsDeleted.GetHashCode();

            int overallHashCode = FeatureName.GetHashCode() + FeatureValue.GetHashCode() + IsDeleted.GetHashCode(); 
            
            return overallHashCode;
        }
    }
}
