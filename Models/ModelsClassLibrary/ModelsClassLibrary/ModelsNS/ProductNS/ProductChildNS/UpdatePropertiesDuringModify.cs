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

            ProductChild pc = ProductChild.Unbox(icommonWithId);

            //UserId = pc.UserId;
            ExpiryDate = pc.ExpiryDate;
            IdentificationNumber = pc.IdentificationNumber;
            OwnerId = pc.OwnerId;
            ProductId = pc.ProductId;
            Sell = pc.Sell;
            Buy = pc.Buy;
            SerialNumber = pc.SerialNumber;

            ShipFromAddressComplex = pc.ShipFromAddressComplex;
            ShipFromAddressId = pc.ShipFromAddressId;
            Hide = pc.Hide;
            IsNonRefundablePaymentAccepted = pc.IsNonRefundablePaymentAccepted;
        }

    }
}