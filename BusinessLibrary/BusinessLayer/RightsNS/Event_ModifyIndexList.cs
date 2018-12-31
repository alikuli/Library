using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System.Web.Mvc;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
namespace UowLibrary.PlayersNS
{
    public partial class RightBiz 
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "User Rights";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
