using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.UploadedFileNS
{
    public interface IUserHasUploads : IHasUploads
    {
        ICollection<UploadedFile> SelfieUploads { get; set; }
        string SelfieLocationConst(string userName);

        ICollection<UploadedFile> IdCardFrontUploads { get;  }
        string IdCardFrontLocationConst(string userName);

        ICollection<UploadedFile> IdCardBackUploads { get; set; }
        string IdCardBackLocationConst(string userName);

        ICollection<UploadedFile> PassportFrontUploads { get; set; }
        string PassportFrontLocationConst(string userName);


        ICollection<UploadedFile> PassportVisaUploads { get; set; }
        string PassportVisaLocationConst(string userName);

        ICollection<UploadedFile> LiscenseFrontUploads { get; set; }
        string LiscenseFrontLocationConst(string userName);

        ICollection<UploadedFile> LiscenseBackUploads { get; set; }
        string LiscenseBackLocationConst(string userName);

    }
}
