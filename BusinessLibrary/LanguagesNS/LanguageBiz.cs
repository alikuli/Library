using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InvoiceNS;
using MigraDocLibrary;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.LanguageNS
{
    public partial class LanguageBiz : BusinessLayer<Language>
    {
        public LanguageBiz(IRepositry<Language> entityDal, MyWorkClasses myWorkClasses, UploadedFileBiz uploadedFileBiz, BreadCrumbManager breadCrumbManager)
            : base(myWorkClasses, entityDal, uploadedFileBiz, breadCrumbManager)
        {
            
        }

        //public byte[] PrintInvoice()
        //{
        //    PdfInvoiceData data = new PdfInvoiceData();

        //    InvoicPdfParameter parm = data.Load(System.Web.HttpContext.Current.Server.MapPath(@"..\Content\MyImages\raddicco.jpg"));
        //    //parm.Logo.Address = System.Web.HttpContext.Current.Server.MapPath(@"..\Content\MyImages\raddicco.jpg");

        //    var factory = new InvoiceFactory();
        //    byte[] pdf = factory.Build(parm);

        //    return pdf;


        //}


    }
}
