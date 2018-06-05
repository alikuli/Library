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
            User.IsNullThrowException("The Owner is null. This cannot be null and should be set at creation! Programming error");
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw, User.UserName);
        }

        public string MiscFilesLocation_Initialization()
        {

            User.IsNullThrowException("The Owner is null. This cannot be null and should be set at creation! Programming error");
            return AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
        }
        #endregion

    }





}
