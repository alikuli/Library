using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UserModels;


namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {


        //public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        //{
        //    base.Event_ModifyIndexList(indexListVM, parameters);

        //    //indexListVM.Heading_Column = "All Files";
        //    indexListVM.Heading.Column = "All Files";

        //    indexListVM.Show.EditDeleteAndCreate = true;
        //    indexListVM.Show.Create = true;

        //    indexListVM.NameInput2 = "File Number";
        //    indexListVM.Show.ImageInList = false;


        //}

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



        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {
            // errIfNotLoggedIn();

            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            var lst = (await base.GetListForIndexAsync(parms)).Cast<FileDoc>().ToList();

            if (lst.IsNullOrEmpty())
                return null;

            ApplicationUser user = UserBiz.Find(UserId);
            user.IsNullThrowException("User not found.");
            user.PersonId.IsNullOrWhiteSpaceThrowException("No person is attached to this user.");
            string personId = user.PersonId;

            //this has been changed to getting files from Person as opposed to User.
            var lstIcommonwithId = (lst.Where(x => x.PersonId== personId)).Cast<ICommonWithId>().ToList();

            //var lstIcommonwithId = (lst.Where(x => x.UserId == UserId)).Cast<ICommonWithId>().ToList();

            return lstIcommonwithId;
        }


    }
}
