//using AliKuli.Extentions;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
//using System.Linq;

//namespace UowLibrary.CountryNS
//{
//    public partial class FileDocOldDataBiz : BusinessLayer<OldFileData>
//    {

//        public override void BusinessRulesFor(OldFileData entity)
//        {
//            base.BusinessRulesFor(entity);
//            GetNextFileNumber(entity);
//        }

//        private void GetNextFileNumber(OldFileData entity)
//        {
//            string userId = GetUserIdOrThrowErrorIfNull();
//            var allDataForuser = FindAllNoTracking().Where(x => x.UserId == userId).ToList();

//            if (allDataForuser.IsNullOrEmpty())
//            {
//                entity.FileNumber = 1;
//            }
//            else
//            {
//                entity.FileNumber = allDataForuser.Max(x => x.FileNumber) + 1;
//            }
//        }

//    }
//}
