using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.AddressNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressBiz : BusinessLayer<AddressWithId>
    {

        UserBiz _userBiz;
        CountryBiz _countryBiz;
        AddressVerificationTrxBiz _addressVerificationTrxBiz;
        public AddressBiz(IRepositry<AddressWithId> entityDal, BizParameters bizParameters, UserBiz userBiz, CountryBiz countryBiz, AddressVerificationTrxBiz addressVerificationTrxBiz)
            : base(entityDal, bizParameters)
        {
            _userBiz = userBiz;
            _countryBiz = countryBiz;
            _addressVerificationTrxBiz = addressVerificationTrxBiz;
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

        AddressVerificationTrxBiz AddressVerificationTrxBiz
        {
            get
            {
                return _addressVerificationTrxBiz;
            }
        }
    }
}
