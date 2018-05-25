
using InvoiceNS;
using ModelClassLibrary.MigraDocNS;
using ModelsClassLibrary.SharedNS;
namespace MigraDocLibrary.InvoiceNS
{
    public class InvoicPdfParameter
    {
        public InvoicPdfParameter():
            this(new Logo(), new PdfHeaderInfo(), new Addresses(),new DocumentInfo(),"")
        {
        }


        public InvoicPdfParameter(Logo logo, PdfHeaderInfo docHdrInfo, Addresses addresses, DocumentInfo docInfo, string webCompanyName)
        {
            Logo = logo;
            DocHeaderInfo = docHdrInfo;
            Addresses = addresses;
            DocumentInfo = docInfo;
            WebCompanyName = webCompanyName;
        }
        public Logo Logo { get; set; }

        /// <summary>
        /// Add information about the document info.
        /// </summary>
        public PdfHeaderInfo DocHeaderInfo { get; set; }
        public Addresses Addresses { get; set; }
        public string WebCompanyName { get; set; }
        public DocumentInfo DocumentInfo { get; set; }






    }
}
