using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonCategoryNS;

namespace UowLibrary.PlayersNS.PersonNS
{
    public partial class PersonBiz : BusinessLayer<Person>
    {
        readonly UserBiz _userBiz;
        readonly PersonCategoryBiz _personCategoryBiz;
        //readonly AddressBiz _addressBiz;
        //readonly AddressBiz _addressBiz;
        public PersonBiz(IRepositry<Person> entityDal, BizParameters bizParameters, PersonCategoryBiz personCategoryBiz, UserBiz userBiz)
            : base(entityDal, bizParameters)
        {

            _userBiz = userBiz;
            //_addressBiz = addressBiz;
            _personCategoryBiz = personCategoryBiz;
        }

        public UserBiz UserBiz { get { return _userBiz; } }


        public PersonCategoryBiz PersonCategoryBiz { get { return _personCategoryBiz; } }
        //public AddressBiz AddressBiz { get { return _addressBiz; } }
        //public AddressVerificationHdrBiz AddressVerificationHdrBiz
        //{
        //    get
        //    {
        //        return AddressBiz.AddressVerificationHdrBiz;
        //    }
        //}

        //public AddressVerificationTrxBiz AddressVerificationTrxBiz
        //{
        //    get
        //    {
        //        return AddressBiz.AddressVerificationHdrBiz.AddressVerificationTrxBiz;
        //    }
        //}


        //public CountryBiz CountryBiz
        //{
        //    get
        //    {
        //        return AddressBiz.CountryBiz;
        //    }
        //}

        //public string PakistanId
        //{
        //    get
        //    {
        //        return CountryBiz.PakistanId;
        //    }
        //}

    }
}
