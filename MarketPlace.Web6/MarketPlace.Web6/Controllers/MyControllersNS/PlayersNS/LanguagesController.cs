using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.LanguageNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;

namespace MarketPlace.Web6.Controllers
{
    public class LanguagesController : EntityAbstractController<Language>
    {

        #region Constructo and initializers

        public LanguagesController(LanguageBiz biz, BreadCrumbManager bcm, IErrorSet err)
            : base(biz, bcm, err) { }

        #endregion

        public ActionResult PrintInvoice()
        {

            LanguageBiz bz = (LanguageBiz)Biz;

            string downloadFileName = "invoice_" + DateTime.Now.Ticks.ToString() + ".pdf";
            return File(bz.PrintInvoice(), "application/pdf", downloadFileName);
        }


    }
}