using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


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

        public override IList<ICommonWithId> GetListForIndex()
        {
            var lst = FindAllForUser(UserId);

            if (lst.IsNullOrEmpty())
                return null;
            var lstIcommonWithId = lst.Cast<ICommonWithId>().ToList();
            return lstIcommonWithId;        
        }



        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {


            List<FileDoc> lst = await FindAllForUserAsync(UserId);
           
            if (lst.IsNullOrEmpty())
                return null;

            var lstIcommonWithId = lst.Cast<ICommonWithId>().ToList();

            return lstIcommonWithId;
        }

    }
}
