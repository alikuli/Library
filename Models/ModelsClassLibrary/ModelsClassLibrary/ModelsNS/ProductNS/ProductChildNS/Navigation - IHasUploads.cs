using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.ProductChildNS
{
    public partial class ProductChild
    {
        #region IHasUploads
        public virtual ICollection<UploadedFile> MiscFiles { get; set; }
        public List<UploadedFile> MiscFiles_Fixed
        {
            get
            {
                if (MiscFiles.IsNullOrEmpty())
                    return new List<UploadedFile>();

                List<UploadedFile> miscFile = MiscFiles.Where(x => x.MetaData.IsDeleted == false).ToList();
                return miscFile;
            }
        }

        public string MiscFilesLocation(string aName)
        {

            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw, aName);
        }

        public string MiscFilesLocation_Initialization()
        {

            return AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
        }
        #endregion

    }





}
