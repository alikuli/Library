using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using System;

namespace ModelsClassLibrary.ModelsNS.ProductChildNS
{
    public partial class ProductChild
    {

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            ProductChild pc = icommonWithId as ProductChild;

            if (pc.IsNull())
            {
                throw new Exception("Product Child is Null. Programming error.");
            }

            //UserId = pc.UserId;
            ProductId = pc.ProductId;
            Sell = pc.Sell;
            Buy = pc.Buy;
            ExpiryDate = pc.ExpiryDate;
            OwnerId = pc.OwnerId;
            SerialNumber = pc.SerialNumber;
            IdentificationNumber = pc.IdentificationNumber;

        }

    }
}