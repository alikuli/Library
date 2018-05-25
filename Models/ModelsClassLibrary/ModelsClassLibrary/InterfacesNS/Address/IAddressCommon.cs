using ModelsClassLibrary.Models.Shared;
using System;
namespace ModelsClassLibrary.Models.AddressNS
{
    public interface IAddressCommon : ICommonNoIdBasics
    {
        string Address2 { get; set; }
        string Email { get; set; }
        string HouseNo { get; set; }
        string Road { get; set; }
        string WebAddress { get; set; }
        string Zip { get; set; }

        string ContactPhone { get; set; }
        string Attention { get; set; }
        
        // EncryptionDecryption below this.
        string Address2EncryptDecrypt { get; set; }
        string EmailEncryptDecrypt { get; set; }
        string HouseNoEncryptDecrypt { get; set; }
        string RoadEncryptDecrypt { get; set; }
        string WebAddressEncryptDecrypt { get; set; }
        string ZipEncryptDecrypt { get; set; }

        string ContactPhoneEncryptDecrypt { get; set; }
        string AttentionEncryptDecrypt { get; set; }





    }
}
