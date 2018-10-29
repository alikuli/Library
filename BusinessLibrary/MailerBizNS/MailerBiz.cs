using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz : BusinessLayer<Mailer>
    {
        readonly AddressVerificationHdrBiz _addressVerifHdrBiz;
        readonly UserBiz _userBiz;
        //readonly CountryBiz _countryBiz;
        public MailerBiz(IRepositry<Mailer> entityDal, BizParameters bizParameters, AddressVerificationHdrBiz addressVerifHdrBiz, /*CountryBiz countryBiz, */ UserBiz userBiz )
            : base(entityDal, bizParameters)
        {
            _addressVerifHdrBiz = addressVerifHdrBiz;
            _userBiz = userBiz;
            //_countryBiz = countryBiz;
        }


        AddressVerificationHdrBiz AddressVerificationHdrBiz
        {
            get
            {
                return _addressVerifHdrBiz;
            }
        }

        AddressVerificationTrxBiz AddressVerificationTrxBiz
        {
            get
            {
                return _addressVerifHdrBiz.AddressVerificationTrxBiz;
            }
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
                return UserBiz.CountryBiz;
            }
        }

        string PakistanId
        {
            get
            {
                return CountryBiz.PakistanId;
            }
        }
    }
}
