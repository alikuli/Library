using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InvoiceNS;
using MigraDocLibrary;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.LikeUnlikeNS
{
    public partial class LikeUnlikeBiz : BusinessLayer<LikeUnlike>
    {

        
        MenuPathMainBiz _menuPathMainBiz;
        ProductBiz _productBiz;
        UserBiz _userBiz;
        public LikeUnlikeBiz(MenuPathMainBiz menuPathMainBiz, UserBiz userBiz, ProductBiz productBiz, AbstractControllerParameters myWorkClasses, IRepositry<LikeUnlike> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
            _menuPathMainBiz = menuPathMainBiz;
            _productBiz = productBiz;
            _userBiz = userBiz;
        }




        public override string SelectListCacheKey
        {
            get 
            { 
                return "LikeUnlikesSelectListData"; 
            }
        }
    }
}
