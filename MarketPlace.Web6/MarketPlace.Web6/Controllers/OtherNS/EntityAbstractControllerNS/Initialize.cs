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


        #region Initialize
        public virtual ActionResult InitializeDb()
        {
            try
            {
                //Biz.SaveAfterEveryAddition(IsSavingAfterEveryAddition);
                Biz.InitializationData();
                //ErrorsGlobal.Add(string.Format("*** {0} Initialized", typeof(TEntity).Name), "");

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Unable to initialize", MethodBase.GetCurrentMethod(), e);
            }

            ErrorsGlobal.MemorySave();
            return RedirectToAction("Index", "Home", null);

        }

        /// <summary>
        /// If true then items will be saved after every addition during initialization. This kind of initilization
        /// is required by some items such as fileDocs which generate the next file number.
        /// </summary>
        //public bool IsSavingAfterEveryAddition { get; set; }
        #endregion
    }
}