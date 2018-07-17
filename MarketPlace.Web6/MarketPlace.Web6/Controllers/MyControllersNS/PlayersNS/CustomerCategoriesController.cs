using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary.PlayersNS.CustomerCategoryNS;
using UowLibrary;
using BreadCrumbsLibraryNS.Programs;

namespace MarketPlace.Web6.Controllers
{
    public class CustomerCategoriesController : EntityAbstractController<CustomerCategory>
    {

        CustomerCategoryBiz _customerCategoryBiz;
        #region Constructo and initializers

        public CustomerCategoriesController(CustomerCategoryBiz CustomerCategoryBiz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(CustomerCategoryBiz, errorSet, userbiz, breadCrumbManager) 
        {
            _customerCategoryBiz = CustomerCategoryBiz;
        }

        #endregion


    }
}