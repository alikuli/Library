using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UserModels;
using WebLibrary.Programs;
using System;
using ModelsClassLibrary.RightsNS;
using System.Web.Mvc;
using UowLibrary.UploadFileNS;
namespace UowLibrary.PlayersNS
{
    public partial class RightBiz : BusinessLayer<Right>
    {
        private UserBiz _userBiz;
        public RightBiz(IRepositry<ApplicationUser> userDal, UserBiz userBiz, IRepositry<Right> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            _userBiz = userBiz;
        }

        public override string SelectListCacheKey
        {
            get { return "RightsSelectListData"; ; }
        }

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "User Rights";
            indexListVM.Show.EditDeleteAndCreate = true;

        }

        public SelectList UserSelectList ()
        { 
            return _userBiz.SelectList();  
        }



        
    }
}
