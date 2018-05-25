using System;
using InterfacesLibrary.AddressNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;

namespace InterfacesLibrary.DocumentsNS
{
    public interface IAbstractSaleDocumentHeader : IAbstractDocHeader
    {
        //void Check_ConsignTo();
        //void Check_Customer_AllowedToShip();
        //void Check_Customer_BlackListed();
        //void Check_InformTo();
        //void Check_Owner_AllowedToShip();
        //void Check_Owner_BlackListed();
        //void Check_Salesman_AllowedToShip();
        //void Check_Salesman_BlackListed();
        //void Check_ShipTo();

        #region Owner

        Guid OwnerId { get; set; }
        IOwner Owner { get; set; }
        AddressComplex OwnersAddress { get; set; }

        #endregion
        #region Consign To

        Guid? ConsignToId { get; set; }
        ICustomer ConsignTo { get; set; }
        AddressComplex ConsignToAddress { get; set; }

        #endregion
        #region Inform To

        Guid? InformToId { get; set; }
        IAddressWithId InformTo { get; set; }
        AddressComplex InformToAddress { get; set; }

        #endregion
        #region Ship to
        Guid? ShipToId { get; set; }
        IAddressWithId ShipTo { get; set; }
        AddressComplex ShipToAddress { get; set; }

        #endregion
        #region Payment Method
        IPaymentMethod PaymentMethod { get; set; }
        Guid? PaymentMethodId { get; set; }

        #endregion
        #region Payment Term
        IPaymentTerm PaymentTerm { get; set; }
        Guid? PaymentTermId { get; set; }

        #endregion
        #region Salesman
        ISalesman Salesman { get; set; }
        Guid? SalesmanId { get; set; }

        #endregion
        #region Delivery Method
        Guid? DeliveryMethodId { get; set; }
        IDeliveryMethod DeliveryMethod { get; set; }

        #endregion
        #region Ship and Expected Date
        DateTime DateExpected { get; set; }
        DateTime DateShipped { get; set; }

        #endregion
        MiscChargesAndPayments MiscCharges { get; set; }


        string TheirPurchaseOrderNumber { get; set; }
        //ICounterClass TotalItems_Ordered_MoneyAmount { get; set; }
        //ICounterClass TotalItems_Ship_MoneyAmount { get; set; }

        //ICounterClass TotalDoc_Ordered_MoneyAmount { get; set; }
        //ICounterClass TotalDoc_Shipped_MoneyAmount { get; set; }

        //ICounterClass TotalMiscCharges { get; set; }
        //decimal GetTotalMiscCharges();


    }
}
