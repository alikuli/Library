using AliKuli.UtilitiesNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Threading.Tasks;
namespace UowLibrary.Shared
{
    public interface IUpload
    {
        void DeleteUploadedFile(IHasUploads entity);
        //void Event_AddUploadedFileInfoIntoDb(IHasUploads entity, UploadObject uploadObj);
        string Event_SaveLocationForUploadedFiles();
    }
}
