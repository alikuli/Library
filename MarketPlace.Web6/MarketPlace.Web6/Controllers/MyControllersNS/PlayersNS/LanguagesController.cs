using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using System;
using System.Web.Mvc;
using UowLibrary.LanguageNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    public class LanguagesController : EntityAbstractController<Language>
    {


        public LanguagesController(LanguageBiz biz, AbstractControllerParameters param)
            : base(biz, param) { }


        public ActionResult PrintInvoice()
        {

            LanguageBiz bz = (LanguageBiz)Biz;

            string downloadFileName = "invoice_" + DateTime.Now.Ticks.ToString() + ".pdf";
            return File(bz.PrintInvoice(), "application/pdf", downloadFileName);
        }


    }
}