using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.ModelsNS.ProductNS
{

    public  partial class Product
    {


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            Product p = icommonWithId as Product;
            IsSaleable = p.IsSaleable;
            ParentId = p.ParentId;

            UomWeightListedId = p.UomWeightListedId;
            WeightListed = p.WeightListed;

            UomWeightActualId = p.UomWeightActualId;
            WeightActual = p.WeightActual;
            
            UomVolumeId = p.UomVolumeId;
            Volume = p.Volume;

            UomDimensionsId = p.UomDimensionsId;
            Dimensions = p.Dimensions;


        }

    }
}