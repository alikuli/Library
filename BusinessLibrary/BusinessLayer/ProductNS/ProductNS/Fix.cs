using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            //this does not work here because we need to update the number of visits and that can be by anyone.
            //You must be logged in
            //UserId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in.");

            Product product = parm.Entity as Product;
            product.IsNullThrowException("Unable to unbox product");


            if (product.UomDimensionsId.IsNullOrWhiteSpace())
                product.UomDimensionsId = null;

            if (product.UomPurchaseId.IsNullOrWhiteSpace())
                product.UomPurchaseId = null;

            if (product.UomSaleId.IsNullOrWhiteSpace())
                product.UomSaleId = null;


            if (product.UomVolumeId.IsNullOrWhiteSpace())
                product.UomVolumeId = null;


            if (product.UomWeightActualId.IsNullOrWhiteSpace())
                product.UomWeightActualId = null;

            if (product.UomWeightListedId.IsNullOrWhiteSpace())
                product.UomWeightListedId = null;

            if (product.OwnerId.IsNullOrWhiteSpace())
            {
                product.OwnerId = null;
            }



        }


    }
}
