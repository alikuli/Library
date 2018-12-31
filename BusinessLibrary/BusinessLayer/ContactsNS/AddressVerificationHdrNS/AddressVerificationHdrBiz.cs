using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using System.Web.Mvc;
using UowLibrary.ParametersNS;

namespace UowLibrary.AddressNS
{
    public partial class AddressVerificationHdrBiz : BusinessLayer<AddressVerificationHdr>
    {

        //UserBiz _userBiz;
        //CountryBiz _countryBiz;
        AddressVerificationTrxBiz _addressVerificationTrxBiz;
        public AddressVerificationHdrBiz(IRepositry<AddressVerificationHdr> entityDal, BizParameters bizParameters, AddressVerificationTrxBiz addressVerificationTrxBiz)
            : base(entityDal, bizParameters)
        {
            //_userBiz = userBiz;
            //_countryBiz = countryBiz;
            _addressVerificationTrxBiz = addressVerificationTrxBiz;
        }

        //UserBiz UserBiz
        //{
        //    get
        //    {
        //        return _userBiz;
        //    }
        //}

        //CountryBiz CountryBiz
        //{
        //    get
        //    {
        //        return _countryBiz;
        //    }
        //}


        //public SelectList CountrySelectList
        //{
        //    get
        //    {
        //        return CountryBiz.SelectList();
        //    }
        //}

        public override string SelectListCacheKey
        {
            get { return "AddressVerificationHdrSelectList"; }
        }

        public AddressVerificationTrxBiz AddressVerificationTrxBiz
        {
            get
            {
                return _addressVerificationTrxBiz;
            }
        }
    }
}
