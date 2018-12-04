using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {


        private void letter_Header(Section section, AddressVerificationModel letter, AddressVerificationModel_Header hdr)
        {
            //Create a table to hold the itemns
            _headingTable = section.Headers.Primary.AddTable();
            section.PageSetup.TopMargin = Unit.FromCentimeter(5);

            // Before you can add a row, you must define the columns.
            header_DefineColumns();

            // Create the header of the table.
            _headingTable.RightPadding = 0;

            headingTable_Row1();
            headingTable_Row2();


            string title = hdr.Letter.Title;
            string documentNumberString = letter.CompleteNumber(hdr.BatchNumber);
            string webCoAddy = hdr.AddressMailFrom;
            string logoAddress = hdr.Logo_AddressAbsolute;
            string printingDate = hdr.PrintDate;
            string requestDate = letter.RequestDate;
            string inProccessDate = letter.InProcessDate;

            var headerRows = _headingTable.Rows;
            var header_Title_Cell = headerRows[0].Cells[1];
            var header_Logo_Cell = headerRows[0].Cells[0];
            var header_CoAddy_Cell = headerRows[1].Cells[1];
            var header_DocNo_Cell = headerRows[0].Cells[1];
            var header_PrintDate_Cell = headerRows[1].Cells[1];
            //var header_RequestDate_Cell = headerRows[2].Cells[3];
            //var header_InProcessDate_Cell = headerRows[3].Cells[3];

            Paragraph titlePara = header_Title_Cell.AddParagraph(title);
            titlePara.Format.Font.Size = "22pt";
            titlePara.Format.Font.Color = Color_Green;


            Image image = header_Logo_Cell.AddImage(logoAddress);
            image.Width = Unit.FromCentimeter(2.5);
            image.Height = Unit.FromCentimeter(2.5);

            Paragraph webAddyPara = header_Logo_Cell.AddParagraph(webCoAddy);
            webAddyPara.Format.Font.Size = "8pt";
            webAddyPara.Format.Font.Bold = false;

            Paragraph websiteAddy = header_Logo_Cell.AddParagraph(hdr.WebsiteAddress);
            websiteAddy.Format.Font.Size = "8pt";
            websiteAddy.Format.Font.Bold = false;

            header_DocNo_Cell.AddParagraph(documentNumberString);
            header_DocNo_Cell.Format.Font.Size = "12pt";

            header_DocNo_Cell.AddParagraph(printingDate);

        }



    }
}
