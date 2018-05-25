using InterfacesLibrary.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.UploadedFileNS
{
    public interface IHasUploads : ICommonWithId
    {
        ICollection<UploadedFile> MiscFiles { get; set; }
        string MiscFilesLocation();
        string MiscFilesLocation_Initialization();

    }
}
