using MigraDoc.DocumentObjectModel;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {

        //private void addressTable_defineItemTableColumns_VerificationLetter()
        //{
        //    var column = _addressTable.AddColumn("6.4cm");
        //    column.Format.Alignment = ParagraphAlignment.Left;


        //}


        //private void letter_Infrastructure(AddressVerificationModel letter, AddressVerificationModel_Header hdr)
        //{
        //    // Each MigraDoc document needs at least one section.
        //    //var mainSection = Create_HeaderSection(param);

        //    Section mainSection = _document.AddSection();
        //    mainSection.PageSetup = _document.DefaultPageSetup.Clone();
        //    letter_Logo(mainSection, letter, hdr);
        //    letter_SendToAddress(mainSection, letter);
        //    letter_Body(mainSection, letter, hdr);



        //}

        //private void letter_Logo(Section section, AddressVerificationModel letter, AddressVerificationModel_Header hdr)
        //{
        //    //Create a table to hold the itemns
        //    _headingTable = section.Headers.Primary.AddTable();
        //    section.PageSetup.TopMargin = Unit.FromCentimeter(5);

        //    // Before you can add a row, you must define the columns.
        //    letterHeader_DefineColumns();

        //    //_headingTable.Borders.Visible = true;

        //    // Create the header of the table.
        //    _headingTable.RightPadding = 0;

        //    headingTable_Row1();
        //    headingTable_Row2();
        //    //headingTable_Row3();
        //    //_headingTable.AddRow();


        //    string title = hdr.Letter.Title;
        //    string documentNumberString = letter.CompleteNumber(hdr.BatchNumber);
        //    string webCoAddy = hdr.AddressMailFrom;
        //    string logoAddress = hdr.Logo_AddressAbsolute;
        //    string printingDate = hdr.PrintDate;
        //    string requestDate = letter.RequestDate;
        //    string inProccessDate = letter.InProcessDate;

        //    var headerRows = _headingTable.Rows;
        //    var header_Title_Cell = headerRows[0].Cells[1];
        //    var header_Logo_Cell = headerRows[0].Cells[0];
        //    var header_CoAddy_Cell = headerRows[1].Cells[1];
        //    var header_DocNo_Cell = headerRows[0].Cells[1];
        //    var header_PrintDate_Cell = headerRows[1].Cells[1];
        //    //var header_RequestDate_Cell = headerRows[2].Cells[3];
        //    //var header_InProcessDate_Cell = headerRows[3].Cells[3];

        //    Paragraph titlePara = header_Title_Cell.AddParagraph(title);
        //    titlePara.Format.Font.Size = "22pt";
        //    titlePara.Format.Font.Color = Color_Green;


        //    Image image = header_Logo_Cell.AddImage(logoAddress);
        //    image.Width = Unit.FromCentimeter(2.5);
        //    image.Height = Unit.FromCentimeter(2.5);

        //    Paragraph webAddyPara = header_Logo_Cell.AddParagraph(webCoAddy);
        //    webAddyPara.Format.Font.Size = "8pt";
        //    webAddyPara.Format.Font.Bold = false;

        //    Paragraph websiteAddy = header_Logo_Cell.AddParagraph(hdr.WebsiteAddress);
        //    websiteAddy.Format.Font.Size = "8pt";
        //    websiteAddy.Format.Font.Bold = false;

        //    header_DocNo_Cell.AddParagraph(documentNumberString);
        //    header_DocNo_Cell.Format.Font.Size = "12pt";

        //    header_DocNo_Cell.AddParagraph(printingDate);

        //}


        //private void letter_SendToAddress(Section mainSection, AddressVerificationModel param)
        //{
        //    //Create a table to hold the itemns
        //    _addressTable = create_Table(mainSection);

        //    // Before you can add a row, you must define the columns.
        //    letter_Address_defineColumns();

        //    // Create the header of the table.
        //    var row = _addressTable.AddRow();
        //    row.BottomPadding = Unit.FromCentimeter(0.5);
        //    row.TopPadding = Unit.FromCentimeter(0.5);
        //    row.Borders.Visible = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;


        //    var addyHeadingPara = row.Cells[0].AddParagraph("Deliver To:");
        //    row.Format.Font.Bold = true;
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;



        //    Paragraph addyPara = row.Cells[0].AddParagraph(param.AddressMailTo);
        //    //Paragraph p = addyBox.AddParagraph(address);
        //    addyPara.Format.Font.Bold = false;
        //    addyPara.Format.LeftIndent = "0.5cm";
        //    addyPara.Style = "Normal";


        //    row = _addressTable.AddRow();
        //    row.Borders.Visible = false;

        //}

        private void letter_Body(Section mainSection, AddressVerificationModel letter, AddressVerificationModel_Header hdr)
        {
            //Create a table to hold the itemns
            _bodyTable = create_Table(mainSection);

            // Before you can add a row, you must define the columns.
            bodyTable_DefineColumns();

            var row = _bodyTable.AddRow();
            row.Borders.Visible = false;
            row.Format.Alignment = ParagraphAlignment.Left;

            row.Cells[0].AddParagraph();
            row.Cells[0].AddParagraph();

            //welcome para
            row.Cells[0].AddParagraph(hdr.Letter.WelcomePara);
            row.Cells[0].AddParagraph();

            //middle para - body
            row.Cells[0].AddParagraph(hdr.Letter.Body);
            row.Cells[0].AddParagraph();

            //instructions
            row.Cells[0].AddParagraph(hdr.Letter.Instructions);
            row.Cells[0].AddParagraph();

            //verification code
            var verificationRow = _bodyTable.AddRow();
            verificationRow.Cells[0].AddParagraph();
            var p = verificationRow.Cells[0].AddParagraph("Verification Code: " + letter.VerificationNumber);
            verificationRow.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            p.Format.Font.Bold = true;
            verificationRow.Borders.Visible = true;
            verificationRow.Cells[0].AddParagraph();


        }






    }
}
