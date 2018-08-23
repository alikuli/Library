using System.Web.Mvc;

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
        }

    }
}