using AliKuli.ConstantsNS;
using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Helpers;

namespace AliKuli.UtilitiesNS
{
    /// <summary>
    /// http://www.prideparrot.com/blog/archive/2012/8/uploading_and_returning_files
    /// This automatically gets the files and creats an UploadFile list, which we can then itterate through and save on to the disk..
    /// </summary>
    public class UploadObject
    {
        const string MAIN_UPLOAD_DIRECTORY = "Content";
        //readonly ErrorSet _globalErrorSet;
        //private IRepositry<UploadObject> _uploadObjDAL;

        public UploadObject()
        {
            //SaveToLocation = @"C:\Users\ALI\Documents\Visual Studio 2013\Projects\Libraries\MarketPlace.Web6\MarketPlace.Web6\App_Data\UploadedFile\ProductCategory1\";
            FileList = new List<UploadedFile>();
            ErrorSet = new ErrorSet();
            ErrorSet.ClassName = "UploadObject";
            ErrorSet.LibraryName = "UploadingLibrary";
        }

        ErrorSet ErrorSet { get; set; }
        public UploadObject(HttpPostedFileBase[] files, string saveToLocationConst)
            : this()
        {
            HttpPostedFile = files;
            RelativePathInWebSite = saveToLocationConst;
            SaveFilesInHttpPostedFileBase();
        }


        //public UploadObject()
        //    : this()
        //{
        //    RelativePathInWebSite = saveToLocationConst;

        //}

        HttpPostedFileBase[] _httpPostedFileBase;
        public HttpPostedFileBase[] HttpPostedFile
        {
            get
            {

                if (_httpPostedFileBase.IsNullOrEmpty())
                {
                    throw new Exception("No Upload files in memory!");
                }
                return _httpPostedFileBase;
            }
            set
            {
                _httpPostedFileBase = value;

                if (_httpPostedFileBase.IsNullOrEmpty())
                {
                    throw new Exception("No files receieved!");
                }

            }
        }


        #region HttpPostedFileBase File Save
        /// <summary>
        /// This contains a list of all the files that have been saved
        /// </summary>
        public List<UploadedFile> FileList { get; set; }



        public int NumberOfFilesInHttpPostedFileBase
        {
            get
            {
                if (HttpPostedFile.IsNullOrEmpty())
                    return 0;

                return HttpPostedFile.Length;
            }
        }

        public int NumberOfFilesInFileList
        {
            get
            {
                if (FileList.IsNullOrEmpty())
                    return 0;

                return FileList.Count;
            }
        }



        /// <summary>
        /// Saves the uploaded files and and s
        /// </summary>
        public void SaveFilesInHttpPostedFileBase()
        {
            int noOfFiles = NumberOfFilesInHttpPostedFileBase;
            //WebImage
            if (noOfFiles > 0)
            {
                for (int i = 0; i < noOfFiles; i++)
                {
                    UploadedFile uf = new UploadedFile(HttpPostedFile[i], RelativePathInWebSite);

                    long maxSizeallowed = 0;
                    bool success = long.TryParse(MyConstants.MAX_UPLOAD_SIZE_ALLOWED_IN_BYTES, out maxSizeallowed);

                    if (!success)
                    {
                        ErrorSet.Add(string.Format("Unable to parse MAX_UPLOAD_SIZE_ALLOWED_IN_BYTES. Current value is '{0:N2}'", MyConstants.MAX_UPLOAD_SIZE_ALLOWED_IN_BYTES), MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorSet.ToString());
                    }

                    if (HttpPostedFile[i].ContentLength > maxSizeallowed)
                    {
                        ErrorSet.AddMessage(string.Format("File '{0}' is bigger than the max size allowed ({1}), this file size is {2:N2} bytes. This file was skipped.",
                            HttpPostedFile[i].FileName,
                            maxSizeallowed,
                            HttpPostedFile[i].ContentLength));
                        continue;
                    }
                    FileList.Add(uf);
                }
            }
        }

        #endregion

        string _relativePath;

        /// <summary>
        /// This is the relative location.
        /// </summary>
        public string RelativePathInWebSite
        {
            get
            {
                return _relativePath;
            }
            set
            {
                string _locationConst = "";
                try
                {

                    _locationConst = value;

                    //Make sure it is not empty.
                    if (_locationConst.IsNullOrWhiteSpace())
                    {
                        ErrorSet.Add("The path received is empty!", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorSet.ToString());
                    }

                    //This will throw an error if the path is not valid...
                    //Check the path.
                    string temp = Path.GetFullPath(_locationConst);

                    //If you are here, the path is valid.
                    //first make sure App_Data is not part of the path
                    // C:\Users\ALI\Documents\Visual Studio 2013\Projects\Libraries\MarketPlace.Web6\MarketPlace.Web6\App_Start\

                    string toBeSearched = MAIN_UPLOAD_DIRECTORY;
                    int indexOf = _locationConst.IndexOf(toBeSearched);

                    if (indexOf != -1) //it has been found....
                    {
                        //now truncate the string from the end of App_data
                        string newString = _locationConst.Substring(indexOf + toBeSearched.Length);
                        _locationConst = newString;

                    }

                    //get rid of any leading "\"
                    while (_locationConst.Substring(0, 1) == @"\")
                    {
                        _locationConst = _locationConst.Substring(1);
                    }

                    //get rid of any leading "/"
                    while (_locationConst.Substring(0, 1) == @"/")
                    {
                        _locationConst = _locationConst.Substring(1);
                    }

                    string savePath = Path.Combine(MyConstants.SAVE_ROOT_DIRECTORY,_locationConst);
                    _relativePath = savePath;

                }
                catch (Exception e)
                {
                    ErrorSet.Add(string.Format("Path '{0}' is invalid. {1}", _relativePath, e.Message), MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorSet.ToString());
                }

            }
        }
        public List<string> GetMessages()
        {
            if (ErrorSet.HasMessages)
            {
                return ErrorSet.ToList_Messages() as List<string>;
            }

            return null;
        }


    }
}
