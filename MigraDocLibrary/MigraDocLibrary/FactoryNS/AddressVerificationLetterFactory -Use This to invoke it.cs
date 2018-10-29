using ErrorHandlerLibrary.ExceptionsNS;
using MigraDoc.Rendering;
using MigraDocLibrary.FactoryNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;
using System;
using System.IO;
using System.Reflection;

namespace MigraDocLibrary.InvoiceNS
{
    /// <summary>
    /// This creates an invoice. Load up InvoicePdfParameter, and you will get a single invoice.
    /// </summary>
    public class CustomerAddressVerificationFactory
    {
        public byte[] Build(AddressVerificationModel_Header header)
        {
            byte[] fileContents = null;
            try
            {
                //PdfParameter param = new Data().Load();
                var document = new LetterEngine().CreateNewDocument(header);
                document.UseCmykColor = true;

                //#if DEBUG
                //                // For debugging only...
                //                MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
                //                var document2 = MigraDoc.DocumentObjectModel.IO.DdlReader.DocumentFromFile("MigraDoc.mdddl");
                //                //document = document2;
                //                // With PDFsharp 1.50 beta 3 there is a known problem: the blank before "by" gets lost while persisting as MDDDL.
                //#endif

                // Create a renderer for PDF that uses Unicode font encoding.
                var pdfRenderer = new PdfDocumentRenderer(true);

                // Set the MigraDoc document.
                pdfRenderer.Document = document;

                // Create the PDF document.
                pdfRenderer.RenderDocument();

                // Save the PDF document...
                //var filename = "Invoice.pdf";

                #if DEBUG
                // I don't want to close the document constantly...
                var filename = "AddressVerification-" + Guid.NewGuid().ToString("N").ToUpper() + ".pdf";
                #endif

                //pdfRenderer.Save(filename);

                using (MemoryStream stream = new MemoryStream())
                {
                    pdfRenderer.Save(stream, true);
                    fileContents = stream.ToArray();
                }

                //...and start a viewer.
                //Process.Start(filename);
            }
            catch (Exception ex)
            {
                ErrorSet _error = new ErrorSet();
                _error.Add("Something went wrong.", MethodBase.GetCurrentMethod(), ex);
                throw new Exception(_error.ToString());
            }

            return fileContents;

        }
    }
}

