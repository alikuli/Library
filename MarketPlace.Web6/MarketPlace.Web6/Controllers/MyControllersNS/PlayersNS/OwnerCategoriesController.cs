using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary.PlayersNS.OwnerCategoryNS;
using UowLibrary;

namespace MarketPlace.Web6.Controllers
{
    public class OwnerCategoriesController : EntityAbstractController<OwnerCategory>
    {

        OwnerCategoryBiz _OwnerCategoryBiz;
        #region Constructo and initializers

        public OwnerCategoriesController(OwnerCategoryBiz OwnerCategoryBiz, IErrorSet errorSet, UserBiz userbiz)
            : base(OwnerCategoryBiz, errorSet,  userbiz) 
        {
            _OwnerCategoryBiz = OwnerCategoryBiz;
        }

        #endregion


    }
}