using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.PlayersNS.ProductApproverCategoryNS
{
    public partial class ProductApproverCategoryBiz : BusinessLayer<ProductApproverCategory>
    {
        public ProductApproverCategoryBiz(IRepositry<ProductApproverCategory> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }



    }
}
