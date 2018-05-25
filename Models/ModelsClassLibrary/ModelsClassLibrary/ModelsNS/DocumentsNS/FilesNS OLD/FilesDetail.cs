using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS
{
    /// <summary>
    /// This is used to print the FileDocs.
    /// </summary>
    public class FilesDetail
    {
        #region Constructor
        public FilesDetail()
        {

        }

        public FilesDetail(string fileNumber, string fileName)
        {
            FileName = fileName;
            FileNumber = fileNumber;
        } 
        #endregion

        #region Properties
        public string FileName { get; set; }

        /// <summary>
        /// This is only a part of
        /// </summary>
        public string FileNumber { get; set; }
        
        #endregion

        public override string ToString()
        {
            return string.Format("{0,10} - {1}", FileNumber, FileName);
        }

    }
}