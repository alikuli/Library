using AliKuli.Extentions;
using System;
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
        public virtual ActionResult DeleteAll()
        {
            var entity = Biz.Factory();
            ViewBag.Heading = entity.ClassNamePlural.ToTitleSentance();

            return View();

        }


        // POST: Countries/Delete/5
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DeleteAllConfirmed()
        {
            try
            {
                await Biz.DeleteActuallyAllAndSaveAsync();
                return RedirectToAction("Index");


            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Unable to delete all {0}", typeof(TEntity).Name),
                    MethodBase.GetCurrentMethod(),
                    e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("DeleteAll");

            }
        }


    }
}