using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using System.Web.Mvc;
using AliKuli.Extentions;

namespace MarketPlace.Web6.Controllers.Abstract
{




    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity>
    {



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            OnActionExecutedSetUpUserNameAndId();

        }


        private void OnActionExecutedSetUpUserNameAndId()
        {
            Biz.UserId = UserId;
            Biz.UserName = UserName;
            //AccountsBiz.UserId = UserId;
            //AccountsBiz.UserName = UserName;

            //UserMoneyAccount userMoneyAccount = AccountsBiz.UserMoneyAccount;
            //if (!userMoneyAccount.IsNull())
            //{
            //    ViewBag.Refundable = userMoneyAccount.AmountRefundableStr;
            //    ViewBag.NonRefundable = userMoneyAccount.AmountNonRefundableStr;

            //}
        }

    }
}