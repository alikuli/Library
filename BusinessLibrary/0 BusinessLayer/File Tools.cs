using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web;
using UowLibrary.Abstract;
using UowLibrary.Interface;
using AliKuli.Extentions;
using System.Reflection;
using System;
using System.IO;

namespace UowLibrary
{
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {

        public string CopyFile(string sourcePath, string targetPath, string nameOfSourceFile)
        {
            string sourcePathMapped = HttpContext.Current.Server.MapPath(sourcePath);
            string targetPathMapped = HttpContext.Current.Server.MapPath(targetPath);

            return AliKuli.ToolsNS.FileTools.CopyFileAndGiveNewName(sourcePathMapped, targetPathMapped, nameOfSourceFile);

        }

        /// <summary>
        /// this gets updated in checkIfFileExists
        /// </summary>
        protected string ExtentionFound { get; set; }

        /// <summary>
        /// If extention is found, it is returned in the property ExtentionFound. Supported image formats are 
        /// .jpg .png .tiff .jpeg .bmp .pdf
        /// </summary>
        /// <param name="fileNameNoExtention"></param>
        /// <param name="filenameWithoutExtention"></param>
        /// <returns></returns>
        protected bool imageFileExists(string fileNameNoExtention)
        {

            if(fileNameNoExtention.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("fileNameNoExtention is null", MethodBase.GetCurrentMethod());
                throw new ArgumentException(ErrorsGlobal.ToString());
            }

            bool success = checkIfFileExists(fileNameNoExtention, ".jpg");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".png");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".TIFF");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".JPEG");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".BMP");
            if (success)
                return true;

            success = checkIfFileExists(fileNameNoExtention, ".PDF");
            if (success)
                return true;

            return false;

        }

        protected bool txtFileExists(string fileNameNoExtention)
        {
            if (fileNameNoExtention.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("fileNameNoExtention is null", MethodBase.GetCurrentMethod());
                throw new ArgumentException(ErrorsGlobal.ToString());
            }

            return checkIfFileExists(fileNameNoExtention, ".txt");
        }

        /// <summary>
        /// If this returns true then it also adds a value to the property pathFileName_with_Extention;
        /// </summary>
        /// <param name="nameAndLocation_With_Extention"></param>
        /// <returns></returns>
        private bool checkIfFileExists(string pathAndname, string extention)
        {
            string nameAndLocation_With_Extention = Path.ChangeExtension(pathAndname, extention);
            if (AliKuli.ToolsNS.FileTools.IsExistFile(nameAndLocation_With_Extention))
            {
                ExtentionFound = extention;
                return true;

            }
            return false;
        }

    }
}
