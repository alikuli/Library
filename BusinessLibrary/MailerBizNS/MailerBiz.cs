using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz : BusinessLayer<Mailer>
    {
        
        readonly UserBiz _userBiz;
        
        public MailerBiz(IRepositry<Mailer> entityDal, BizParameters bizParameters,/*CountryBiz countryBiz, */ UserBiz userBiz)
            : base(entityDal, bizParameters)
        {

            _userBiz = userBiz;
        }

        public AddressBiz AddressBiz { get { return _userBiz.AddressBiz; } }
        public AddressVerificationHdrBiz AddressVerificationHdrBiz
        {
            get
            {
                return _userBiz.AddressBiz.AddressVerificationHdrBiz;
            }
        }

        public AddressVerificationTrxBiz AddressVerificationTrxBiz
        {
            get
            {
                return _userBiz.AddressBiz.AddressVerificationHdrBiz.AddressVerificationTrxBiz;
            }
        }


        public UserBiz UserBiz
        {
            get
            {
                return _userBiz;
            }
        }
        public CountryBiz CountryBiz
        {
            get
            {
                return UserBiz.CountryBiz;
            }
        }

        public string PakistanId
        {
            get
            {
                return CountryBiz.PakistanId;
            }
        }
    }
}
