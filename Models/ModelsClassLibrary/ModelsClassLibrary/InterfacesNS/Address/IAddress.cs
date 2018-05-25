using ModelsClassLibrary.Interfaces;
using System;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using UserModelsLibrary.ModelsNS;
using UserModels;
namespace ModelsClassLibrary.Models.AddressNS
{
    public interface IAddress : IAddressWithTownClass, ICommonWithId
    {
        string UserId { get; set; }
        ApplicationUser User { get; set; }

        bool IsConsignToAddress { get; set; }
        bool IsShipToAddress { get; set; }
        bool IsInformToAddress { get; set; }

        bool IsAllAddressTypesFalse {get;}


        AddressComplex ToAddressComplex { get; }

        //AddressWithTownClass ToAddressWithTownClass();

        //AddressCommon ToAddressCommon { get; }



        
    }
}
