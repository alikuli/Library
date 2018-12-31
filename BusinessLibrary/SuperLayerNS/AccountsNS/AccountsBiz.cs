using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using UowLibrary.CashTtxNS;

namespace UowLibrary.SuperLayerNS.AccountsNS
{
    public class AccountsBiz
    {
        CashTrxBiz _cashTrxBiz;
        public AccountsBiz(CashTrxBiz cashTrxBiz)
        {
            _cashTrxBiz = cashTrxBiz;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }

        CashTrxBiz CashTrxBiz
        {
            get
            {
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in. User Id not found.");
                UserName.IsNullOrWhiteSpaceThrowException("You are not logged in. User Name not found");

                _cashTrxBiz.UserId = UserId;
                _cashTrxBiz.UserName = UserName;
                return _cashTrxBiz;
            }
        }


        public UserMoneyAccount UserMoneyAccount
        {

            get
            {
                if (UserId.IsNullOrWhiteSpace()) return null;
                UserMoneyAccount userMoneyAccount = CashTrxBiz.UserMoneyAccountForUser(UserId);
                return userMoneyAccount;

            }
        }
    }
}
