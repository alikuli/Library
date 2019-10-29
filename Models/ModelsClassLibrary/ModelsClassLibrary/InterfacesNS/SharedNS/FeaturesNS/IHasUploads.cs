using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;

namespace InterfacesLibrary.SharedNS.FeaturesNS
{
    public interface IHasUploads : ICommonWithId
    {
        ICollection<UploadedFile> MiscFiles { get; set; }
        List<UploadedFile> MiscFiles_Fixed { get; }
        string MiscFilesLocation(string userName);
        string MiscFilesLocation_Initialization();


    }
}
