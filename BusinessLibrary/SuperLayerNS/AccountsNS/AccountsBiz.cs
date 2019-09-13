using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.CashTtxNS;
using UowLibrary.PlayersNS.BankNS;
using UowLibrary.SuperLayerNS;

namespace UowLibrary.SuperLayerNS.AccountsNS
{
    public class AccountsBizSuper
    {
        //CashTrxBiz _cashTrxBiz;
        //BankBiz _bankBiz;
        //BuySellDocBiz _buySellDocBiz;
        SuperBiz _superCashBiz;
        public AccountsBizSuper(SuperBiz superCashBiz)
        {
            _superCashBiz = superCashBiz;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }

        SuperBiz SuperCashBiz
        {
            get
            {

                _superCashBiz.UserId = UserId;
                _superCashBiz.UserName = UserName;
                return _superCashBiz;
            }
        }
        public BankBiz BankBiz
        {
            get
            {
                //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in. User Id not found.");
                //UserName.IsNullOrWhiteSpaceThrowException("You are not logged in. User Name not found");

                return _superCashBiz.BankBiz;

            }
        }
        public CashTrxBiz CashTrxBiz
        {
            get
            {
                //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in. User Id not found.");
                //UserName.IsNullOrWhiteSpaceThrowException("You are not logged in. User Name not found");

                return _superCashBiz.CashTrxBiz; ;
            }
        }


        //public UserMoneyAccount UserMoneyAccount(string userId)
        //{

        //    if (userId.IsNullOrWhiteSpace())
        //        return new UserMoneyAccount();

        //    bool isAdmin = UserBiz.IsAdmin(userId);
            
        //    //if there is no
        //    UserMoneyAccount userMoneyAccount = MoneyAccountForUser(userId, isAdmin);

        //    if (userId.IsNullOrWhiteSpace())
        //        return new UserMoneyAccount();

        //    return userMoneyAccount;

        //}

        public UserBiz UserBiz
        {
            get
            {
                UserBiz _userBiz = CashTrxBiz.UserBiz;
                return _superCashBiz.CashTrxBiz.UserBiz;
            }
        }

        public BuySellDocBiz BuySellDocBiz
        {
            get
            {
                return _superCashBiz.BuySellDocBiz;
            }
        }
    }
}
