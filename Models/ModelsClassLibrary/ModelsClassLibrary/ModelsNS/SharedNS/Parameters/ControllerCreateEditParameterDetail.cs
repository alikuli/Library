using AliKuli.Extentions;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.SharedNS

{
    public class ControllerCreateEditParameterDetail
    {
        public HttpPostedFileBase[] HttpBase { get; set; }
        //public ICollection<UploadedFile> PicSelfUpload { get; set; }
        public string FileLocationConst { get; set; }
        public bool IsHttpBaseNull
        {
            get
            {
                if (HttpBase.IsNullOrEmpty())
                    return true;

                return HttpBase[0].IsNull();
            }
        }


    }
}
