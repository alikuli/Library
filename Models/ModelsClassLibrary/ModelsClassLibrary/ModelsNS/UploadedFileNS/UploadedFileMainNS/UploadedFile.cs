using AliKuli.Extentions;
using AliKuli.ToolsNS;
using EnumLibrary.EnumNS;
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
        public UploadedFile(HttpPostedFileBase file, string relativePath)
            : this(
            Path.GetFileNameWithoutExtension(file.FileName),
            FileTools.CreateNewNameForFile(Path.GetExtension(file.FileName)),
            Path.GetExtension(file.FileName),
            relativePath)
        {

            File = file;
            

        }
        //int noOfTrys = 0;

        public UploadedFile(string originalNameNoExtention, string newNameNoExtention, string extention, string saveToRelativePath)
        {
            OriginalNameWithoutExtention = originalNameNoExtention;
            Name = Path.ChangeExtension(newNameNoExtention, extention);
            Extention = extention;
            RelativeWebsitePath = saveToRelativePath;
        }


        //public void SaveFile()
        //{
        //    try
        //    {
        //        File.SaveAs(AbsolutePathWithFileName());
        //    }
        //    catch (DirectoryNotFoundException)
        //    {
        //        if (noOfTrys > 3)
        //            throw new Exception("Unable to create directory " + AbsolutePathWithFileName());

        //        //create the directory
        //        FileTools.CreateDirectory(GetAbsolutePath(RelativeWebsitePath));
        //        noOfTrys += 1;
        //        SaveFile();
        //    }

        //}

        HttpPostedFileBase File { get; set; }



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
            return UploadedFile.AbsolutePathWithFileName (AbsolutePath(), Name);
        }

        public static string AbsolutePathWithFileName(string absolutePath, string name)
        {
            return Path.Combine(absolutePath, name);
        }


        public string GetRelativePathWithFileName()
        {
            return GetRelativePathWithFileName(Name, RelativeWebsitePath);
        }

        public string GetRelativePathWithFileName(string name, string relativeWebsitePath)
        {
            if (relativeWebsitePath.IsNullOrEmpty())
                return DefaultBlankPictureLocation();

            if (name.IsNullOrEmpty())
                return DefaultBlankPictureLocation();

            return Path.Combine(relativeWebsitePath, name);
        }

        public static string DefaultBlankPictureLocation()
        {
            string str = AliKuli.UtilitiesNS.ConfigManagerHelper.DefaultBlankPicture;
            str.IsNullOrWhiteSpaceThrowException("DefaultBlankPictureLocation");
            return str;
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


        public bool IsImage()
        {
            string extention_Fixed = Extention.ToLower();
            switch (extention_Fixed)
            {
                case ".bmp":
                case ".jpeg":
                case ".jpg":
                case ".png":
                    return true;
                default:
                    return false;
            }

        }


    }
}
