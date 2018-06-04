using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.InterfacesNS.Shared;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.ComponentModel.DataAnnotations;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS
{
    //We will only use 
    public partial class FileDoc : CommonWithId, IHasUploads, IUserPartOfEntity
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
                return EnumLibrary.EnumNS.ClassesWithRightsENUM.FileDoc;
        }
        
        #region User
        //File owner.
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        #endregion



        public override string NameInput2
        {
            get
            {
                return "File Number";
            }
        }



        public override string Input2SortString
        {
            get
            {

                return FileNumber.ToString("000000000");
            }
        }


        public string FullNameWithFileNumber()
        {
            string fileNo = FileNumber.ToString();

            if (!OldFileNumber.IsNullOrWhiteSpace())
                fileNo = OldFileNumber;

            return string.Format("{0} [{1}]", base.FullName(), fileNo);
        }




        
    }
}
