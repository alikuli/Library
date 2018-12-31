using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz : BusinessLayerPlayer<Mailer>
    {
        
        //readonly UserBiz _userBiz;
        //PersonBiz _personBiz;

        public MailerBiz(IRepositry<Mailer> entityDal, BizParameters bizParameters, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
        {
            //_personBiz = personBiz;
            //_userBiz = userBiz;
        }


        //public AddressBiz AddressBiz { get { return _userBiz.AddressBiz; } }
        //public AddressVerificationHdrBiz AddressVerificationHdrBiz
        //{
        //    get
        //    {
        //        return _userBiz.AddressBiz.AddressVerificationHdrBiz;
        //    }
        //}

        //public AddressVerificationTrxBiz AddressVerificationTrxBiz
        //{
        //    get
        //    {
        //        return _userBiz.AddressBiz.AddressVerificationHdrBiz.AddressVerificationTrxBiz;
        //    }
        //}


        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        return _userBiz;
        //    }
        //}
        //public CountryBiz CountryBiz
        //{
        //    get
        //    {
        //        return UserBiz.CountryBiz;
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
