using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS
{
    //We will only use 
    public partial class FileDoc : CommonWithId, IHasUploads
    {

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.FileDoc;
        }

        #region User
        //File owner.
        //public virtual ApplicationUser User { get; set; }

        //public string UserId { get; set; }



        public virtual Person Person { get; set; }
        public string PersonId { get; set; }

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
