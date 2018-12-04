using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {





        private void footerLetter_Logo(Section section, AddressVerificationModel_Header header)
        {
            //Create a table to hold the itemns
            _footerTable = section.Headers.Primary.AddTable();
            section.PageSetup.TopMargin = Unit.FromCentimeter(5);

            // Before you can add a row, you must define 2 columns.
            footerPage_DefineColumns();


            // Create the header of the table.
            _footerTable.RightPadding = 0;

            footerTable_Row1();
            footerTable_Row2();
            //headingTable_Row3();
            //_headingTable.AddRow();


            string title = header.Summary.Title;
            string documentNumberString = header.BatchNumber;
            string webCoAddy = header.AddressMailFrom;
            string logoAddress = header.Logo_AddressAbsolute;
            string printingDate = header.PrintDate;

            var footerRows = _footerTable.Rows;
            var footer_Title_Cell = footerRows[0].Cells[1];
            var footer_Logo_Cell = footerRows[0].Cells[0];
            var footer_CoAddy_Cell = footerRows[1].Cells[1];
            var footer_DocNo_Cell = footerRows[0].Cells[1];
            var footer_PrintDate_Cell = footerRows[1].Cells[1];

            Paragraph titlePara = footer_Title_Cell.AddParagraph(title);
            titlePara.Format.Font.Size = "22pt";
            titlePara.Format.Font.Color = Color_Green;


            Image image = footer_Logo_Cell.AddImage(logoAddress);
            image.Width = Unit.FromCentimeter(2.5);
            image.Height = Unit.FromCentimeter(2.5);

            Paragraph webAddyPara = footer_Logo_Cell.AddParagraph(webCoAddy);
            webAddyPara.Format.Font.Size = "8pt";
            webAddyPara.Format.Font.Bold = false;

            Paragraph websiteAddy = footer_Logo_Cell.AddParagraph(header.WebsiteAddress);
            websiteAddy.Format.Font.Size = "8pt";
            websiteAddy.Format.Font.Bold = false;

            footer_DocNo_Cell.AddParagraph(documentNumberString);
            footer_DocNo_Cell.Format.Font.Size = "12pt";

            footer_DocNo_Cell.AddParagraph(printingDate);
        }



        //private void createInfrastructureFor_VerificationLetter(AddressVerificationModel letter, AddressVerificationModel_Header hdr)
        //{
        //    // Each MigraDoc document needs at least one section.
        //    //var mainSection = Create_HeaderSection(param);

        //    Section mainSection = _document.AddSection();
        //    mainSection.PageSetup = _document.DefaultPageSetup.Clone();
        //    setup_LogoAndHeaderAreas_VerificationLetter(mainSection, letter, hdr);
        //    setup_CustomerAddress_VerificationLetter(mainSection, letter);
        //    setup_BodyTable_For_VerificationLetter(mainSection, letter,hdr);



        //}

        //private void setup_LogoAndHeaderAreas_VerificationLetter(Section section, AddressVerificationModel letter, AddressVerificationModel_Header hdr)
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


        //    string title = hdr.VerificationLetter.Title;
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

        //    Paragraph websiteAddy = header_Logo_Cell.AddParagraph(letter.Website);
        //    websiteAddy.Format.Font.Size = "8pt";
        //    websiteAddy.Format.Font.Bold = false;

        //    header_DocNo_Cell.AddParagraph(documentNumberString);
        //    header_DocNo_Cell.Format.Font.Size = "12pt";

        //    header_DocNo_Cell.AddParagraph(printingDate);

        //}
        //private void setup_CustomerAddress_VerificationLetter(Section mainSection, AddressVerificationModel param)
        //{
        //    //Create a table to hold the itemns
        //    _addressTable = create_Table(mainSection);

        //    // Before you can add a row, you must define the columns.
        //    addressTable_defineItemTableColumns_VerificationLetter();

        //    // Create the header of the table.
        //    //var row = addressTable_Row1(param);
        //    var row = _addressTable.AddRow();
        //    row.BottomPadding = Unit.FromCentimeter(0.5);
        //    row.TopPadding = Unit.FromCentimeter(0.5);
        //    row.Borders.Visible = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    //row.HeadingFormat = true;
        //    //row.Format.Font.Bold = true;
        //    //row.Shading.Color = Color_Table_LtBlue;

        //    var addyHeadingPara = row.Cells[0].AddParagraph("Deliver To:");
        //    row.Format.Font.Bold = true;
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;


        //    //var addyRow = _addressTable.AddRow();
        //    //addyRow.Borders.Visible = false;

        //    Paragraph addyPara = row.Cells[0].AddParagraph(param.AddressMailTo);
        //    //Paragraph p = addyBox.AddParagraph(address);
        //    addyPara.Format.Font.Bold = false;
        //    addyPara.Format.LeftIndent = "0.5cm";
        //    addyPara.Style = "Normal";


        //    //row.Cells[0].Borders.Bottom.Visible = true;

        //    //blank row
        //    row = _addressTable.AddRow();
        //    row.Borders.Visible = false;

        //    //row.Borders.Visible = false;

        //    //row = addressTable_Row2();
        //    ////setTableEdge(_addressTable);

        //    //row = _addressTable.AddRow();
        //    //row.Borders.Visible = false;
        //}

        //private void setup_BodyTable_For_VerificationLetter(Section mainSection, AddressVerificationModel letter, AddressVerificationModel_Header hdr)
        //{
        //    //Create a table to hold the itemns
        //    _bodyTable = create_Table(mainSection);

        //    // Before you can add a row, you must define the columns.
        //    bodyTable_DefineTableColumns();

        //    var row = _bodyTable.AddRow();
        //    row.Borders.Visible = false;
        //    //row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Left;
        //    //row.Format.Font.Bold = true;
        //    //row.Shading.Color = Color_Table_LtBlue;

        //    row.Cells[0].AddParagraph();
        //    row.Cells[0].AddParagraph();

        //    row.Cells[0].AddParagraph(hdr.VerificationLetter.WelcomePara);
        //    row.Cells[0].AddParagraph();

        //    row.Cells[0].AddParagraph(hdr.VerificationLetter.Body);
        //    row.Cells[0].AddParagraph();

        //    row.Cells[0].AddParagraph(hdr.VerificationLetter.Instructions);
        //    row.Cells[0].AddParagraph();

        //    //row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    //row.Cells[0].VerticalAlignment = VerticalAlignment.Top;


        //    //row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    //row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        //    //row.Cells[0].Borders.Bottom.Visible = true;

        //    var verificationRow = _bodyTable.AddRow();
        //    verificationRow.Cells[0].AddParagraph();
        //    var p = verificationRow.Cells[0].AddParagraph("Verification Code: " + letter.VerificationNumber);
        //    verificationRow.Cells[0].Format.Alignment = ParagraphAlignment.Center;
        //    p.Format.Font.Bold = true;
        //    verificationRow.Borders.Visible = true;
        //    verificationRow.Cells[0].AddParagraph();

        //    //row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    //row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        //    //row.Cells[0].Borders.Bottom.Visible = true;

        //}

        //private void bodyTable_DefineTableColumns()
        //{
        //    var column = _bodyTable.AddColumn("17.02cm");
        //    column.Format.Alignment = ParagraphAlignment.Left;

        //}





        ////private Row addressTable_Row1(AddressVerificationModel param)
        ////{
        ////    //var row = _addressTable.AddRow();
        ////    //row.HeadingFormat = true;
        ////    //row.Format.Alignment = ParagraphAlignment.Center;
        ////    //row.Format.Font.Bold = true;
        ////    //row.Shading.Color = Color_Table_LtBlue;

        ////    //row.Cells[0].AddParagraph("Mailing Address:");
        ////    //row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        ////    //row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        ////    //row.Cells[0].Borders.Bottom.Visible = true;
        ////    //row.Borders.r

        ////    //row.Cells[2].AddParagraph("Seller:");
        ////    //row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        ////    //row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
        ////    //row.Cells[2].Borders.Bottom.Visible = true;

        ////    //row.Cells[1].Borders.Top.Clear();
        ////    //row.Cells[1].Borders.Bottom.Clear();
        ////    //row.Cells[1].Shading.Visible = false;

        ////    //this is the row where the address will appear
        ////    //row = _addressTable.AddRow();
        ////    //row.Shading = null;

        ////    //row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        ////    //row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        ////    //row.Cells[0].Borders.Bottom.Visible = true;

        ////    //row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        ////    //row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
        ////    //row.Cells[2].Borders.Bottom.Visible = true;


        ////    //row.Cells[1].Borders.Top.Clear();
        ////    //row.Cells[1].Borders.Bottom.Clear();
        ////    //row.Cells[1].Shading.Visible = false;

        ////    //return row;
        ////}

        //private void addressTable_defineItemTableColumns_VerificationLetter()
        //{
        //    var column = _addressTable.AddColumn("6.4cm");
        //    column.Format.Alignment = ParagraphAlignment.Left;

        //    //column = _addressTable.AddColumn("0.22cm");
        //    //column.Format.Alignment = ParagraphAlignment.Right;

        //    //column = _addressTable.AddColumn("8.4cm");
        //    //column.Format.Alignment = ParagraphAlignment.Right;

        //}

        //private Table create_Table(Section mainSection)
        //{
        //    Table t = mainSection.AddTable();
        //    t.Style = "Table";
        //    t.Borders.Color = Color_SeaBlue;
        //    t.Borders.Width = 0.25;
        //    t.Borders.Left.Width = 0.5;
        //    t.Borders.Right.Width = 0.5;
        //    t.Rows.LeftIndent = 0;
        //    return t;

        //}









        //private void setup_footerTable(Section section, AddressVerificationModel param)
        //{
        //    //Create a table to hold the itemns
        //    section.PageSetup.BottomMargin = Unit.FromCentimeter(FOOTER_HEIGHT);

        //    _footerTable = section.Footers.Primary.AddTable();
        //    _footerTable.Borders.Visible = false;
        //    _footerTable.Borders.Top.Visible = true;
        //    _footerTable.Borders.Top.Width = Unit.FromPoint(1);
        //    _footerTable.RightPadding = 0;
        //    _footerTable.Format.Font.Size = Unit.FromPoint(8);

        //    // Before you can add a row, you must define the columns.
        //    footerTable_DefineColums();

        //    // Create the header of the table.
        //    footerTable_Row1(param);
        //}

        //private void footerTable_DefineColums()
        //{
        //    //col 0
        //    var logoAddress = _footerTable.AddColumn("3cm");
        //    logoAddress.Format.Alignment = ParagraphAlignment.Left;

        //    //col 1
        //    var addressCol2 = _footerTable.AddColumn("11.02cm");
        //    addressCol2.Format.Alignment = ParagraphAlignment.Left;

        //    //col 2
        //    var blankCol = _footerTable.AddColumn("3cm");
        //    blankCol.Borders.Visible = false;
        //    blankCol.Shading.Visible = false;
        //}

        //private void footerTable_Row1(AddressVerificationModel param)
        //{
        //    var row = _footerTable.AddRow();
        //    //row.Borders.Visible = true;
        //    row.Shading.Visible = false;
        //    row.VerticalAlignment = VerticalAlignment.Top;
        //    row.Borders.Top.Visible = true;

        //    var left = row.Cells[0];
        //    var middle = row.Cells[1];
        //    var right = row.Cells[2];

        //    left.Format.Alignment = ParagraphAlignment.Left;
        //    middle.Format.Alignment = ParagraphAlignment.Center;
        //    right.Format.Alignment = ParagraphAlignment.Right;
        //    //leftMostCell.AddParagraph();


        //}


        //private void letterHeader_DefineColumns()
        //{
        //    //col 0
        //    var logoAddress = _headingTable.AddColumn("9cm");
        //    logoAddress.Format.Alignment = ParagraphAlignment.Left;

        //    var docNoAndHeading = _headingTable.AddColumn("8.02cm");
        //    docNoAndHeading.Format.Alignment = ParagraphAlignment.Right;

        //}


        //private void headingTable_Row1()
        //{
        //    var row = _headingTable.AddRow();
        //    row.Cells[0].MergeDown = 1;
        //    row.Cells[1].MergeDown = 1;
        //    row.Cells[1].Borders.Top.Visible = false;
        //    row.Cells[1].Borders.Right.Visible = false;
        //    row.Shading.Visible = false;
        //    row.Format.Font.Bold = true;
        //    row.VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[1].Format.Font.Size = "20pt";
        //    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;


        //}

        //private void headingTable_Row2()
        //{
        //    var row = _headingTable.AddRow();
        //    row.Cells[0].MergeDown = 1;
        //    row.Shading.Visible = false;
        //    row.Format.Font.Bold = true;
        //    row.VerticalAlignment = VerticalAlignment.Top;

        //}

        ////private void headingTable_Row3()
        //{
        //    var row = _headingTable.AddRow();
        //    var webAddress = row.Cells[0];
        //    webAddress.MergeRight = 1;
        //    webAddress.Format.Font.Size = "6pt";
        //    webAddress.Format.Font.Italic = true;

        //    var documentType = row.Cells[3];
        //    documentType.Format.Font.Underline = Underline.Single;

        //    row.Shading.Visible = false;
        //    row.Format.Font.Bold = false;

        //    row.Cells[3].Format.Font.Bold = true;
        //    row.VerticalAlignment = VerticalAlignment.Bottom;



        //}




    }
}
