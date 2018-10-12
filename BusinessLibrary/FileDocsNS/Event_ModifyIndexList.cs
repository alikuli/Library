using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;


namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            //indexListVM.Heading_Column = "All Files";
            indexListVM.Heading.Column = "All Files";

            indexListVM.Show.EditDeleteAndCreate = true;
            indexListVM.Show.Create = true;

            indexListVM.NameInput2 = "File Number";
            indexListVM.Show.ImageInList = false;


        }

        //public override IList<ICommonWithId> GetListForIndex()
        //{

        //    try
        //    {


        //        //errIfNotLoggedIn();


        //        var lstAsFileDoc = base.GetListForIndex().ToList() as IList<FileDoc>;

        //        if (lstAsFileDoc.IsNullOrEmpty())
        //            return null;

        //        var lst = lstAsFileDoc.Where(x => x.UserId == UserId).ToList();

        //        if (lst.IsNullOrEmpty())
        //            return null;

        //        var lstIcommonwithId = lst as IList<ICommonWithId>;
        //        return lstIcommonwithId;
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Unable to continue", MethodBase.GetCurrentMethod(), e);
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }
        //}



        //public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        //{
        //   // errIfNotLoggedIn();

        //    var lst = (await base.GetListForIndexAsync(parms)).Cast<FileDoc>().ToList();

        //    if (lst.IsNullOrEmpty())
        //        return null;


        //    var lstIcommonwithId = (lst.Where(x => x.UserId == UserId)).Cast<ICommonWithId>().ToList();

        //    return lstIcommonwithId;
        //}

        ////private void errIfNotLoggedIn()
        ////{
        ////    if (UserId.IsNullOrWhiteSpace())
        ////    {
        ////        ErrorsGlobal.Add("You must log in to continue", "");
        ////    }
        ////}

        //public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithid)
        //{
        //    base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithid);
        //    FileDoc filedoc = icommonWithid as FileDoc;

        //    if (filedoc.IsNull())
        //    {
        //        ErrorsGlobal.Add("Unable to convert to File Doc", MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }
        //    indexItem.Name = filedoc.FullNameWithFileNumber();

        //    indexItem.PrintLineNumber = filedoc.FileNumber.ToString();
        //    if (!filedoc.OldFileNumber.IsNullOrWhiteSpace())
        //        indexItem.PrintLineNumber = filedoc.OldFileNumber.ToString();

        //}
    }
}
