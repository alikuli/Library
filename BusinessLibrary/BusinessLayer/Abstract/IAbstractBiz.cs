using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary.ExceptionsNS;
using System;
using System.Web;
using UowLibrary.PageViewNS;
using UowLibrary.UploadFileNS;
using WebLibrary.Programs;
namespace UowLibrary.BusinessLayer.Abstract
{
    public interface IAbstractBiz
    {
        void AddErrorsFrom(global::Microsoft.AspNet.Identity.IdentityResult result);
        ApplicationMemory ApplicationMemory { get; }
        BreadCrumbManager BreadCrumbManager { get; }
        CacheMemory CacheMemory { get; }
        ConfigManagerHelper ConfigManagerHelper { get; }
        void EncryptDecrypt();
        ErrorSet ErrorsGlobal { get; }
        HttpContextBase HttpContextBaseBiz { get; }
        bool IsInitializedAlready { get; set; }
        MemoryMain MemoryMain { get; }
        PageViewBiz PageViewBiz { get; }
        SessionMemory SessionMemory { get; }
        UploadedFileBiz UploadedFileBiz { get; }
        string UserId { get; set; }
        string UserName { get; set; }
    }
}
