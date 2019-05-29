using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.MoneyAndCountClass;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using System;
using System.Web.Mvc;

namespace MarketPlace.Web4.Controllers
{
    /// <summary>
    /// Note. The Error_Controller and the Error_Uow are the same
    /// After exectuing an action, the AbstractController automaticlly adds the errors.
    /// You must wire up tye Uow in the Controllers so that the Uow goes into Uow and then
    /// you can create one from that which is for that particular controller if you need it.
    /// </summary>
    public abstract partial class AbstractController : Controller
    {






        private void getUserMoneyAccount()
        {
            UserMoneyAccount userMoneyAccount = AccountsBizSuper.UserMoneyAccount(UserId);
            userMoneyAccount = AccountsBizSuper.BuySellDocBiz.GetMoneyAccount(UserId, AccountsBizSuper.UserBiz.IsAdmin(UserId), userMoneyAccount);
            userMoneyAccount.IsAdmin = Is_Admin;
            userMoneyAccount.IsBank = IsBank;

            ViewBag.MoneyAccount = userMoneyAccount;

            //------ New code ------- GetMoneyItemParent MoneyItemParent 
            MoneyItemParent moneyItemParent = AccountsBizSuper.BuySellDocBiz.GetMoneyItemParent(UserId, AccountsBizSuper.UserBiz.IsAdmin(UserId), DateTime.MinValue, DateTime.MaxValue);

            moneyItemParent.Money_System = AccountsBizSuper.CashTrxBiz.GetMoneyTypeForPerson("", Is_Admin);
            moneyItemParent.Money_User = AccountsBizSuper.CashTrxBiz.GetMoneyTypeForUser(UserId);
           
            ViewBag.MoneyItemParent = moneyItemParent;

        }
    }
}