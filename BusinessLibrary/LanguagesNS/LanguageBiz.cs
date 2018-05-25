using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InvoiceNS;
using MigraDocLibrary;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.LanguageNS
{
    public partial class LanguageBiz : BusinessLayer<Language>
    {
        public LanguageBiz(IRepositry<ApplicationUser> userDal, IRepositry<Language> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
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
