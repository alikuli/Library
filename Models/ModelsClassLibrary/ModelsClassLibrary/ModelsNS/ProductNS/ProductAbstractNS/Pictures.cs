using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract
    {
        /// <summary>
        /// This stores the address of the small picture.
        /// </summary>
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
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, ClassNameRaw);
        }

        public string MiscFilesLocation_Initialization()
        {
            return AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;

        }







    }
}