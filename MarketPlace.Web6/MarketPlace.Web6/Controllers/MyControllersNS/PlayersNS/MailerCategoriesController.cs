using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary.PlayersNS.MailerCategoryNS;
using UowLibrary;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ErrorHandlerLibrary;
using UowLibrary.PageViewNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS.MailerNS;

namespace MarketPlace.Web6.Controllers
{
    public class MailerCategoriesController : EntityAbstractController<MailerCategory>
    {

        MailerCategoryBiz _mailerCategoryBiz;
        #region Constructo and initializers

        public MailerCategoriesController(MailerCategoryBiz biz,  AbstractControllerParameters param)
            : base(biz, param) 
        {
            _mailerCategoryBiz = biz;
        }

        #endregion


    }
}