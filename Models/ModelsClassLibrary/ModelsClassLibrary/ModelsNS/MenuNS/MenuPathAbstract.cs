using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public abstract class MenuPathAbstract : CommonWithId
    {
        public virtual ICollection<UploadedFile> MiscFiles { get; set; }


    }
}