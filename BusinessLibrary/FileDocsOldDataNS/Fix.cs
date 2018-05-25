//using AliKuli.Extentions;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;

//namespace UowLibrary.CountryNS
//{
//    public partial class FileDocOldDataBiz : BusinessLayer<OldFileData>
//    {

//        public override void Fix(OldFileData entity)
//        {
//            base.Fix(entity);
//            entity.Name = entity.Name.ToTitleCase();
//            string userId = UserId;

//            AttachUser(entity);
//        }

//        private void AttachUser(OldFileData entity)
//        {

//            entity.UserId = GetUserIdOrThrowErrorIfNull();
//            //entity.User = UserDal.FindById(UserId);
//        }

//    }
//}
