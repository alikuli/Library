using ModelsClassLibrary.Interfaces;
using ModelsClassLibrary.Models.AddressNS;
using ModelsClassLibrary.Models.Documents.Abstract.Header;
using ModelsClassLibrary.Models.PeopleNS.Abstracts;
using System;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using Microsoft.Owin.BuilderProperties;
using ModelsClassLibrary.ModelsNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;
using UserModelsLibrary.ModelsNS;
namespace ModelsClassLibrary.Models.Documents.Abstract
{
    public interface IAbstractSaleDocumentHeader: IAbstractDocHeader
    {
        void Check_ConsignTo();
        void Check_Customer_AllowedToShip();
        void Check_Customer_BlackListed();
        void Check_InformTo();
        void Check_Owner_AllowedToShip();
        void Check_Owner_BlackListed();
        void Check_Salesman_AllowedToShip();
        void Check_Salesman_BlackListed();
        void Check_ShipTo();
        Customer ConsignTo { get; set; }
        AddressComplex ConsignToAddress { get; set; }
        bool IsConsignToAllowedToShip { get; }
        Guid? ConsignToID { get; set; }
        DeliveryMethod DeliveryMethod { get; set; }
        Guid? DeliveryMethodID { get; set; }
        //long DocNo { get; set; }
        //DateTime ExpectedDate { get; set; }

        Address InformTo { get; set; }
        AddressComplex InformToAddress { get; set; }
        Guid? InformToID { get; set; }
        //void Initialize();
        bool IsComplexAddressEmpty(IAddressComplex a);
        void LoadAddressIntoConsignTo(IAddressComplex addressIn);
        void LoadAddressIntoConsignTo(IPlayerAbstract addressIn);
        void LoadAddressIntoInformTo(IPlayerAbstract addressIn);
        void LoadAddressIntoOwner(IAddressComplex addressIn);
        void LoadAddressIntoOwnersAddress(IAddressComplex addressIn);
        void LoadAddressIntoOwnersAddress(IPlayerAbstract addressIn);
        void LoadAddressIntoShipTo(IAddress a);
        void LoadAddressIntoShipTo(IAddressComplex addressIn);
        void LoadAddressIntoShipTo(User user);

        decimal MiscPaymentAmount { get; set; }
        Owner Owner { get; set; }
        bool IsOwnerAllowedToShip { get; }
        Guid OwnerID { get; set; }
        AddressComplex OwnersAddress { get; set; }
        PaymentMethod PaymentMethod { get; set; }
        Guid? PaymentMethodID { get; set; }
        PaymentTerm PaymentTerm { get; set; }
        Guid? PaymentTermID { get; set; }
        SaleTypeEnum SaleTypeENUM { get; set; }

        Salesman Salesman { get; set; }
        bool SalesmanAllowedToShip { get; }
        bool SalesmanExists { get; }
        Guid? SalesmanID { get; set; }
        //void SelfErrorCheck();
        decimal ShippingAmount { get; set; }
        Address ShipTo { get; set; }
        AddressComplex ShipToAddress { get; set; }
        Guid? ShipToID { get; set; }
        decimal TaxAmount { get; set; }
        string TheirPurchaseOrderNumber { get; set; }
        CounterClass TotalItems_Ordered_MoneyAmount { get; set; }
        CounterClass TotalItems_Ship_MoneyAmount { get; set; }

        CounterClass TotalDoc_Ordered_MoneyAmount { get; set; }
        CounterClass TotalDoc_Shipped_MoneyAmount { get; set; }

        CounterClass TotalMiscCharges { get; set; }
        //decimal GetTotalMiscCharges();


    }
}
