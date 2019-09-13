using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity>
    {

        // GET: Countries/Delete/5
        public async Task<ActionResult> Delete(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, string productId, string menuPathMainId, string productChildId, MenuENUM menuEnum = MenuENUM.IndexDefault, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = false)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                TEntity entity = await Biz.FindAsync(id);

                if (entity.IsNull())
                {
                    return HttpNotFound();
                }

                string buttonDud = "";
                //this is where the return URL is added.
                //this is used when you want to return somewhere else other than the Index
                //It will be passed on to a hidden field is Delete and then passed on
                //to DeleteConfirmed via the hidden field
                //entity.ReturnUrl = returnUrl;
                //string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);
                //ApplicationUser user = UserBiz.FindByUserName_UserManager(User.Identity.Name);
                //bool isUserAdmin = UserBiz.IsUserAdmin(user);

                ControllerIndexParams parms = MakeControlParameters(
                    id,
                    menuPathMainId,
                    searchFor,
                    isandForSearch,
                    selectedId,
                    entity,
                    entity,
                    BreadCrumbManager,
                    UserId,
                    UserName,
                    productId,
                    returnUrl,
                    isMenu,
                    buttonDud,
                    menuEnum,
                    sortBy,
                    print,
                    ActionNameENUM.Delete);

                Biz.InitializeMenuManagerForEntity(parms);
                parms.Entity.IsDeleting = true;
                return Event_Create_ViewAndSetupSelectList_GET(parms);
                //return View(parms.Entity);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Not found!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

                if (!returnUrl.IsNullOrWhiteSpace())
                    return Redirect(returnUrl);

                return RedirectToAction("Index", new { selectedId = id.ToString() });

            }
        }


        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string returnUrl)
        {
            try
            {
                bool success = await Biz.DeleteAsync(id);
                if (success)
                {
                    Event_AfterDeleting(id);
                }

                if (!returnUrl.IsNullOrWhiteSpace())
                    return Redirect(returnUrl);

                //                return RedirectToAction("Index");
                return Redirect(BreadCrumbManager.Url_CurrMinusOne);


            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Not found!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Delete", new { id = id });

            }
        }


        /// <summary>
        /// This event fires right after a SUCCESSFUL delete. If the delete is not successful, this event will not fire.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Event_AfterDeleting(string id)
        {
            //throw new NotImplementedException();
        }


    }
}