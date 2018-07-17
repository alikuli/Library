using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using System;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.LanguageNS;

namespace MarketPlace.Web6.Controllers
{
    public class LanguagesController : EntityAbstractController<Language>
    {

        #region Constructo and initializers

        public LanguagesController(LanguageBiz languagesBiz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(languagesBiz, errorSet, userbiz, breadCrumbManager) { }

        #endregion

        public ActionResult PrintInvoice()
        {

            LanguageBiz bz = (LanguageBiz)Biz;

            string downloadFileName = "invoice_" + DateTime.Now.Ticks.ToString() + ".pdf";
            return File(bz.PrintInvoice(), "application/pdf", downloadFileName);
        }


    }
}