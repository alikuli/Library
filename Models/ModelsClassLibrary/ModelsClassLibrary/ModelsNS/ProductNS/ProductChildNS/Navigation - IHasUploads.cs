using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
using System.IO;

namespace ModelsClassLibrary.ModelsNS.ProductChildNS
{
    public partial class ProductChild
    {
        #region IHasUploads
        public virtual ICollection<UploadedFile> MiscFiles { get; set; }

        public string MiscFilesLocation()
        {
            
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw, Owner.Name);
        }

        public string MiscFilesLocation_Initialization()
        {

            return AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
        }
        #endregion

    }





}
