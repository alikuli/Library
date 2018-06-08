using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;

namespace InterfacesLibrary.SharedNS.FeaturesNS

{
    public interface IHasUploads : ICommonWithId
    {
        ICollection<UploadedFile> MiscFiles { get; set; }
        string MiscFilesLocation();
        string MiscFilesLocation_Initialization();

    }
}
