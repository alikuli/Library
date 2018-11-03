using System;
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    //use this if it has its own Verification ENUM and info
    public interface IHasVerification

    {
        Verification Verification { get; set; }
    }
}
