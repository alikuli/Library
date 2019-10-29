using AliKuli.Extentions;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS
{
    //We will only use 
    public partial class FileDoc
    {
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

        //[NotMapped]
        //public string UserName { get; set; }
        /// <summary>
        /// Do not allow change of userName because users FileDoc images will be loaded in the user name.
        /// </summary>
        string IHasUploads.MiscFilesLocation(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "FileDoc", userName);
        }




        public string MiscFilesLocation_Initialization()
        {
            return AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
        }
    }
}
