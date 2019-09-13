using AliKuli.Extentions;
using AliKuli.ToolsNS;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Hosting;
namespace ModelsClassLibrary.ModelsNS.UploadedFileNS
{
    /// <summary>
    /// Note the new name of the file will be in Name in the website, without the extention. This stores the old and the new name along with the extention.
    /// This automatically saves the files to the disk, i.e. in the constructor.
    /// 
    /// Name: Has the actual stored name with extention
    /// OriginalNameWithoutExtention: Just that.
    /// Extention: just that.
    /// RelativeWebsitePath: This is calculated for every particular image.
    /// </summary>
    public partial class UploadedFile : CommonWithId
    {
        public UploadedFile()
        {

        }
        public UploadedFile(HttpPostedFileBase file, string relativePath, int noOfTrys = 0)
            : this(
            Path.GetFileNameWithoutExtension(file.FileName),
            FileTools.CreateNewNameForFile(Path.GetExtension(file.FileName)),
            Path.GetExtension(file.FileName),
            relativePath)
        {

            try
            {
                file.SaveAs(AbsolutePathWithFileName());
            }
            catch (DirectoryNotFoundException)
            {
                if (noOfTrys > 3)
                    throw new Exception("Unable to create directory " + AbsolutePathWithFileName());

                //create the directory
                FileTools.CreateDirectory(GetAbsolutePath(relativePath));
                noOfTrys += 1;
                new UploadedFile(file, relativePath, noOfTrys);
            }

            //if (file.IsNull())
            //    return;
            //if (!file.FileName.IsNullOrWhiteSpace())
            //{
            //    OriginalNameWithoutExtention = Path.GetFileNameWithoutExtension(file.FileName);
            //    Extention = Path.GetExtension(file.FileName);
            //    Name = FileTools.CreateNewNameForFile(Extention);
            //    RelativeWebsitePath = relativePath;
            //    //Now we will standardize the size of the picture by max bit size and height and width
            //    HttpPostedFileBase adjustedsizeFile = ConvertHttpImageToSmall(file);
            //    adjustedsizeFile.SaveAs(AbsolutePathWithFileName());
            //}
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="originalNameWithoutExtention">This is there because during initialization the name of the file is the name of the item. That is why there is no extention. Also at this time we have no idea what the extention is.</param>
        ///// <param name="newNameWithMappedPathPlusExtention"></param>
        ///// <param name="saveToRelativePath"></param>
        //public UploadedFile(string originalNameWithoutExtention, string newNameWithMappedPathPlusExtention, string saveToRelativePath)
        //{
        //    OriginalNameWithoutExtention = originalNameWithoutExtention;
        //    Name = newNameWithMappedPathPlusExtention;
        //    Extention = Path.GetExtension(newNameWithMappedPathPlusExtention);
        //    RelativeWebsitePath = saveToRelativePath;
        //}

        public UploadedFile(string originalNameNoExtention, string newNameNoExtention, string extention, string saveToRelativePath)
        {
            OriginalNameWithoutExtention = originalNameNoExtention;
            Name = Path.ChangeExtension(newNameNoExtention, extention);
            Extention = extention;
            RelativeWebsitePath = saveToRelativePath;
        }


        //public UploadedFile(string getFromRelativePath, string saveToRelativePath, string nameWithExtention)
        //{

        //}
        //public UploadedFile(string fileNameWithPathAndExtention, string saveToRelativePath)
        //    : this(
        //    Path.GetFileName(fileNameWithPathAndExtention),
        //    AliKuli.ToolsNS.FileTools.CreateNewNameForFile(Path.GetExtension(fileNameWithPathAndExtention)),
        //    saveToRelativePath)
        //{
        //    //string orignalNameWithExtention = Path.GetFileName(fileNameWithPathAndExtention);
        //    //string orignalNameWithoutExtention = Path.GetFileNameWithoutExtension(orignalNameWithExtention);
        //    //string relativePath = Path.GetFullPath(fileNameWithPathAndExtention);
        //    //string newNameWithExtention = AliKuli.ToolsNS.FileTools.CreateNewNameForFile(Path.GetExtension(fileNameWithPathAndExtention));

        //}


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.UploadFile;
        }
        /// <summary>
        /// This stores the Original name without the extention
        /// </summary>
        /// 
        [Display(Name = "Original Name")]
        public string OriginalNameWithoutExtention { get; set; }

        /// <summary>
        /// This is the extention of the file.
        /// </summary>
        public string Extention { get; set; }

        #region Path

        /// <summary>
        /// This is the location of the file in the website. i.e the virtual address. This is provided from the rules of the 
        /// particular object being saved. Eg: Product Category 1 will be saved in ProductCategory1 which address comes from constants.
        /// </summary>
        /// 
        public string RelativeWebsitePath { get; set; }

        /// <summary>
        /// This is the location of the path on the server. You get this using Server.MapPath
        /// </summary>
        /// <returns></returns>
        public string AbsolutePathWithFileName()
        {
            return Path.Combine(AbsolutePath(), Name);
        }
        

        public string GetRelativePathWithFileName()
        {
            return GetRelativePathWithFileName(Name, RelativeWebsitePath);
        }

        public string GetRelativePathWithFileName(string name,string relativeWebsitePath)
        {
            if (relativeWebsitePath.IsNullOrEmpty())
                return AliKuli.UtilitiesNS.ConfigManagerHelper.DefaultBlankPicture;

            if (name.IsNullOrEmpty())
                return AliKuli.UtilitiesNS.ConfigManagerHelper.DefaultBlankPicture;
            return Path.Combine(relativeWebsitePath, name);
        }

        public static string GetAbsolutePath(string relativePath)
        {
            try
            {
                string absolutePath = HostingEnvironment.MapPath(relativePath);
                //Directory.CreateDirectory(absolutePath);

                if (absolutePath.IsNullOrWhiteSpace())
                {
                    //ErrorSet.Add("File path is empty.", MethodBase.GetCurrentMethod());
                    throw new Exception("File path is empty.");
                }
                return absolutePath;

            }
            catch (Exception e)
            {

                //ErrorSet.Add("Error while saving location.", MethodBase.GetCurrentMethod(), e);
                throw new Exception("Error while saving location. " + e.Message);

            }
        }

        public string AbsolutePath()
        {
            return GetAbsolutePath(RelativeWebsitePath);
        }

        #endregion


        public override bool DisableNameInView()
        {
            return false;
        }

        public string OrignalCompleteName
        {
            get
            {
                return OriginalNameWithoutExtention + Extention;
            }
        }




    }
}
