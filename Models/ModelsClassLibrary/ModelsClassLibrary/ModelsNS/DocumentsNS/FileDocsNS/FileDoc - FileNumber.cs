using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.ComponentModel.DataAnnotations;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS
{
    //We will only use 
    public partial class FileDoc : CommonWithId, IHasUploads
    {

        
        //This is the file number
        [Display(Name = "File #")]

        public long FileNumber { get; set; }

        [Display(Name = "Old File #")]
        public string OldFileNumber { get; set; }




    }
}
