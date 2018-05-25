//using AliKuli.Extentions;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
//using ModelsClassLibrary.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;


//namespace UowLibrary.CountryNS
//{
//    public partial class FileDocOldDataBiz : BusinessLayer<OldFileData>
//    {


//        public override void Event_ModifyIndexList(IndexListVM indexListVM)
//        {

//            base.Event_ModifyIndexList(indexListVM);

//            indexListVM.Heading_Column = "All Files";
//            indexListVM.ShowEditDeleteAndCreate = true;
//            indexListVM.NameInput2 = "File Number";
//            indexListVM.ShowCreate = true;



//        }

//        public override System.Collections.Generic.IList<OldFileData> GetListForIndex()
//        {

//            errIfNotLoggedIn();

//            var lst = base.GetListForIndex().Where(x => x.UserId == UserId).ToList();

//            return lst;
//        }



//        public override async Task<IList<OldFileData>> GetListForIndexAsync()
//        {
//            errIfNotLoggedIn();

//            var lst = (await base.GetListForIndexAsync())
//                .Where(x => x.UserId == UserId);
//            return lst.ToList();
//        }

//        private void errIfNotLoggedIn()
//        {
//            if (UserId.IsNullOrWhiteSpace())
//            {
//                ErrorsGlobal.Add("You must log in to continue", "");
//                throw new Exception(ErrorsGlobal.ToString());
//            }
//        }
//    }
//}
