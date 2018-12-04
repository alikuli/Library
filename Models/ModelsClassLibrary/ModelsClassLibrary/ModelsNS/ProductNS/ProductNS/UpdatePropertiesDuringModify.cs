using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Linq;
namespace ModelsClassLibrary.ModelsNS.ProductNS
{

    public partial class Product
    {


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            Product p = icommonWithId as Product;


            UomWeightListedId = p.UomWeightListedId;
            UomWeightActualId = p.UomWeightActualId;
            UomVolumeId = p.UomVolumeId;
            UomDimensionsId = p.UomDimensionsId;
            UomPurchaseId = p.UomPurchaseId;
            CheckedBoxesList = p.CheckedBoxesList;
            Dimensions = p.Dimensions;

            //NameFieldsData = p.NameFieldsData;
            //IsSaleable = p.IsSaleable;
            WeightListed = p.WeightListed;
            Volume = p.Volume;
            WeightActual = p.WeightActual;

            if (!p.ProductFeatures.IsNullOrEmpty())
            {
                //we want to get the description from the incomming product
                foreach (ProductFeature pf in p.ProductFeatures)
                {
                    ProductFeature pfFound = ProductFeatures.FirstOrDefault(x => x.Name.Trim().ToLower() == pf.Name.Trim().ToLower());
                    pfFound.IsNullThrowException("Programming error");
                    pfFound.FeatureDescription = pf.FeatureDescription;
                }
            }
            //ProductFeatures = null;
            //ProductFeatures = p.ProductFeatures;

            //ParentId = p.ParentId;


        }

    }
}