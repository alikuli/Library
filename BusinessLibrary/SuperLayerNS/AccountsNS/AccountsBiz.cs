using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.CashTtxNS;
using UowLibrary.PlayersNS.BankNS;

namespace UowLibrary.SuperLayerNS.AccountsNS
{
    public class AccountsBizSuper
    {
        CashTrxBiz _cashTrxBiz;
        BankBiz _bankBiz;
        BuySellDocBiz _buySellDocBiz;
        public AccountsBizSuper(CashTrxBiz cashTrxBiz, BankBiz bankBiz, BuySellDocBiz buySellDocBiz)
        {
            _cashTrxBiz = cashTrxBiz;
            _bankBiz = bankBiz;
            _buySellDocBiz = buySellDocBiz;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }


        public BankBiz BankBiz
        {
            get
            {
                //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in. User Id not found.");
                //UserName.IsNullOrWhiteSpaceThrowException("You are not logged in. User Name not found");

                _bankBiz.UserId = UserId;
                _bankBiz.UserName = UserName;
                return _bankBiz;

            }
        }
        public CashTrxBiz CashTrxBiz
        {
            get
            {
                //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in. User Id not found.");
                //UserName.IsNullOrWhiteSpaceThrowException("You are not logged in. User Name not found");

                _cashTrxBiz.UserId = UserId;
                _cashTrxBiz.UserName = UserName;
                return _cashTrxBiz;
            }
        }


        public UserMoneyAccount UserMoneyAccount(string userId)
        {

            if (userId.IsNullOrWhiteSpace())
                return new UserMoneyAccount();

            bool isAdmin = UserBiz.IsAdmin(userId);
            
            //if there is no
            UserMoneyAccount userMoneyAccount = CashTrxBiz.MoneyAccountForUser(userId, isAdmin);

            if (userId.IsNullOrWhiteSpace())
                return new UserMoneyAccount();

            return userMoneyAccount;

        }

        public UserBiz UserBiz
        {
            get
            {
                UserBiz _userBiz = CashTrxBiz.UserBiz;
                return _userBiz;
            }
        }

        public BuySellDocBiz BuySellDocBiz
        {
            get
            {
                _buySellDocBiz.UserId = UserId;
                _buySellDocBiz.UserName = UserName;
                return _buySellDocBiz;
            }
        }
    }
}
