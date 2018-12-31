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


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);

            Right right = parm.Entity as Right;

            fixId(right);
            fixName(right);
        }

        private void fixName(Right entity)
        {
            entity.Name = entity.MakeKey();
        }

        private void fixId(Right entity)
        {
            entity.Id = entity.MakeKey();
        }



    }
}
