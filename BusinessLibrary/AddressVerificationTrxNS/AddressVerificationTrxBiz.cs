using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressVerificationTrxBiz : BusinessLayer<AddressVerificationTrx>
    {

        //UserBiz _userBiz;
        CountryBiz _countryBiz;
        public AddressVerificationTrxBiz(IRepositry<AddressVerificationTrx> entityDal, BizParameters bizParameters, CountryBiz countryBiz)
            : base(entityDal, bizParameters)
        {
            //_userBiz = userBiz;
            _countryBiz = countryBiz;
        }

        //UserBiz UserBiz
        //{
        //    get
        //    {
        //        return _userBiz;
        //    }
        //}

        CountryBiz CountryBiz
        {
            get
            {
                return _countryBiz;
            }
        }


        //public SelectList CountrySelectList
        //{
        //    get
        //    {
        //        return CountryBiz.SelectList();
        //    }
        //}

        public override string SelectListCacheKey
        {
            get { return "AddressVerificationTrxSelectList"; }
        }



    }
}
