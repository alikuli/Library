using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;

namespace UowLibrary.PlayersNS.PersonNS
{
    public partial class PersonBiz
    {


        public override void AddEntityRecordIntoUpload(UploadedFile uploadedFile, Person entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            uploadedFile.PersonId = entity.Id;
            uploadedFile.Person = entity;
        }


    }
}
