using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Linq;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.BankCategoryNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
namespace UowLibrary.PlayersNS.BankNS
{
    public partial class BankBiz : BusinessLayerPlayer<Bank>
    {
        readonly BankCategoryBiz _bankCategoryBiz;
        AddressBiz _addressBiz;
        CashTrxBiz _cashTrxBiz;
        //PersonBiz _personBiz;


        public BankBiz(IRepositry<Bank> entityDal, BizParameters bizParameters, BankCategoryBiz bankCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
        {
            //_personBiz = personBiz;
            _bankCategoryBiz = bankCategoryBiz;
            _addressBiz = addressBiz;
            _cashTrxBiz = cashTrxBiz;
        }

        //CashTrxBiz CashTrxBiz
        //{
        //    get
        //    {

        //        _cashTrxBiz.IsNullThrowException("_cashTrxBiz");
        //        _cashTrxBiz.UserId = UserId;
        //        _cashTrxBiz.UserName = UserName;
        //        return _cashTrxBiz;
        //    }
        //}

        //AddressBiz AddressBiz
        //{
        //    get
        //    {
        //        _addressBiz.IsNullThrowException("_addressBiz");
        //        _addressBiz.UserId = UserId;
        //        _addressBiz.UserName = UserName;
        //        return _addressBiz;
        //    }
        //}

        //PersonBiz PersonBiz
        //{
        //    get
        //    {
        //        return AddressBiz.PersonBiz;
        //    }

        //}
        public BankCategoryBiz BankCategoryBiz
        {
            get
            {
                _bankCategoryBiz.IsNullThrowException("_bankCategoryBiz");
                _bankCategoryBiz.UserId = UserId;
                _bankCategoryBiz.UserName = UserName;
                return _bankCategoryBiz;
            }
        }

        //public bool IsBankerFor(string userId)
        //{

        //    return !GetBankFor(userId).IsNull();
        //}

        //public Bank GetBankFor(string userId)
        //{
        //    Person person = PersonBiz.GetPersonForUserId(userId);
        //    person.IsNullThrowException("Person");
        //    string personId = person.Id;
        //    personId.IsNullOrWhiteSpaceThrowException("personId");
        //    Bank bank = FindAll().FirstOrDefault(x => x.PersonId == personId);
        //    return bank;
        //}

        public bool IsBanker_User(string userId)
        {
            Bank bank = GetPlayerFor(userId);
            return !bank.IsNull();
        }
        public bool IsBanker_Person(string personId)
        {
            personId.IsNullOrWhiteSpaceThrowException();

            Bank bank = GetPlayerForPersonId(personId);
            return !bank.IsNull();
        }

        //public bool Add_RefundablePayment(CashPaymentModel cashPaymentModel)
        //{
        //    UserId.IsNullOrWhiteSpaceThrowArgumentException("UserId");
        //    bool isBank = IsBankerFor(UserId);
        //    string personFromId = PersonBiz.GetPersonIdForUserId(UserId);

        //    return CashTrxBiz.Add_RefundablePayment(personFromId, cashPaymentModel.PersonToId, cashPaymentModel.Amount, cashPaymentModel.Comment, isBank);
        //}

        //public bool Add_NON_RefundablePayment(CashPaymentModel cashPaymentModel)
        //{
        //    UserId.IsNullOrWhiteSpaceThrowArgumentException("UserId");
        //    bool isBank = IsBankerFor(UserId);
        //    string personFromId = PersonBiz.GetPersonIdForUserId(UserId);

        //    return CashTrxBiz.Add_NON_RefundablePayment(personFromId, cashPaymentModel.PersonToId, cashPaymentModel.Amount, cashPaymentModel.Comment, isBank);
        //}

    }
}
