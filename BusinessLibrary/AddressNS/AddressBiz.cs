using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InvoiceNS;
using MigraDocLibrary;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz : BusinessLayer<AddressWithId>
    {

        UserBiz _userBiz;
        CountryBiz _countryBiz;
        public AddressBiz(IRepositry<AddressWithId> entityDal, BizParameters bizParameters, UserBiz userBiz, CountryBiz countryBiz)
            : base(entityDal, bizParameters)

        {
            _userBiz = userBiz;
            _countryBiz = countryBiz;
        }

        UserBiz UserBiz
        {
            get
            {
                return _userBiz;
            }
        }

        CountryBiz CountryBiz
        {
            get
            {
                return _countryBiz;
            }
        }


        public SelectList CountrySelectList
        {
            get
            {
                return CountryBiz.SelectList();
            }
        }
    }
}
