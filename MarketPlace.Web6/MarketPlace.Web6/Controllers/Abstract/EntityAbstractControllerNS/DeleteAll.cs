using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserModels;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
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