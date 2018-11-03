using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.AddressNS.AddessWithIdNS
{
    //use this when an item can be verified, but has no Verification ENUM. It will get it's verification Enum from
    //the address transactions. Moreover, it does not save the dates etc of verification. They are saved in the AddressVerificationTrx
    //for the record. If there are multiple address verifications then the latest one is used according to the Create date.
    public interface IamVerified
    {
        ICollection<AddressVerificationTrx> AddressVerificationTrxs { get; set; }
        Verification Verification { get; }

    }
}
