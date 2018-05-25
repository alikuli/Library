using System;
using InterfacesLibrary.AddressNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
//IUserGuidneeds this

namespace InterfacesLibrary.PeopleNS.PlayersNS
{
    public interface IUserCustom
    {


        bool IsBlackList { get; set; }

        bool IsAdmin { get; set; }
        void LoadFrom(IUserCustom u);
        string IpAddressOfLastLogin { get; set; }
        DateTime? LastLockout { get; set; }
        DateTime? LastLogin { get; set; }
        DateTime? LastSignInFailure { get; set; }
        int NoOfLogins { get; set; }
        bool IsSuspeneded { get; set; }
        PersonComplex Person { get; set; }
        MetaDataComplex MetaData { get; set; }
        
        Guid? AddressId { get; set; }
        IAddressWithId Address { get; set; }
        AddressComplex AddressStr { get; set; }

    }
}
