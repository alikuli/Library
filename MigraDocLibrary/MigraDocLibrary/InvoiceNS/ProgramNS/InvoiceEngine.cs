using AliKuli.Extentions;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDocLibrary.InvoiceNS;
using System;
using System.Collections.Generic;

namespace InvoiceNS
{
    public partial class InvoiceEngine
    {


        public InvoiceEngine()
        {
            Addresses = new Addresses();
        }

        private const double FOOTER_HEIGHT = 2;


        #region Colors

        // ... in RGB.
        readonly static Color Color_SeaBlue = new Color(81, 125, 192);
        readonly static Color Color_Table_LtBlue = new Color(235, 240, 249);
        readonly static Color Color_Table_Gray = new Color(242, 242, 242);
        readonly static Color Color_Red = new Color(255, 0, 0);
        readonly static Color Color_NavyBlue = new Color(0, 0, 255);
        readonly static Color Color_Green = new Color(0, 255, 0);


        #endregion        
        
        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        Document _document;
        public Addresses Addresses { get; set; }
        public List<LineItem> _lineItems { get; set; }

        //#region Address Frames
        ///// <summary>
        ///// The text frame of the MigraDoc document that contains the address.
        ///// </summary>
        ////TextFrame _customerAddressFrame;
        ////TextFrame _sellerAddressFrame;
        ////TextFrame _infoToAddressFrame;
        ////TextFrame _shipToAddressFrame;

        //#endregion







        //#region Styles
        //void DefineStyles()
        //{
        //    var style = createNormalStyle();

        //    style = modify_HeaderStyle(style);
        //    style = modify_FooterStyle(style);
        //    style = addStyle_Table(style);
        //    style = addStyle_Title(style);
        //    style = addStyle_Reference(style);
        //}

        //private Style createNormalStyle()
        //{
        //    // Get the predefined style Normal.
        //    var style = _document.Styles["Normal"];


        //    // Because all styles are derived from Normal, the next line changes the 
        //    // font of the whole document. Or, more exactly, it changes the font of
        //    // all styles and paragraphs that do not redefine the font.
        //    style.Font.Name = "Segoe UI";
        //    return style;
        //}

        //private Style modify_FooterStyle(Style style)
        //{
        //    style = _document.Styles[StyleNames.Footer];
        //    style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
        //    return style;
        //}

        //private Style modify_HeaderStyle(Style style)
        //{
        //    style = _document.Styles[StyleNames.Header];
        //    style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);
        //    return style;
        //}

        //private Style addStyle_Table(Style style)
        //{
        //    style = _document.Styles.AddStyle("Table", "Normal");
        //    style.Font.Name = "Segoe UI Semilight";
        //    style.Font.Size = 9;
        //    return style;
        //}

        //private Style addStyle_Title(Style style)
        //{
        //    style = _document.Styles.AddStyle("Title", "Normal");
        //    style.Font.Name = "Segoe UI Semibold";
        //    style.Font.Size = 9;
        //    return style;
        //}

        //private Style addStyle_Reference(Style basedOnStyle)
        //{
        //    basedOnStyle = _document.Styles.AddStyle("Reference", "Normal");
        //    basedOnStyle.ParagraphFormat.SpaceBefore = "5mm";
        //    basedOnStyle.ParagraphFormat.SpaceAfter = "5mm";
        //    basedOnStyle.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
        //    return basedOnStyle;
        //}
        //#endregion


        //#region Create

        ////private void create_CustomerAddressFrame(Section mainSection)
        ////{
        ////    _customerAddressFrame = mainSection.AddTextFrame();
        ////    _customerAddressFrame.Height = "3.0cm";
        ////    _customerAddressFrame.Width = "7.0cm";
        ////    _customerAddressFrame.Left = ShapePosition.Left;
        ////    _customerAddressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
        ////    _customerAddressFrame.Top = "5.0cm";
        ////    _customerAddressFrame.RelativeVertical = RelativeVertical.Page;
        ////}

        ////private void create_SellerAddressFrame(Section mainSection)
        ////{
        ////    _sellerAddressFrame = mainSection.AddTextFrame();
        ////    _sellerAddressFrame.Height = "3.0cm";
        ////    _sellerAddressFrame.Width = "7.0cm";

        ////    _sellerAddressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
        ////    _sellerAddressFrame.Left = "7.5cm";

        ////    _sellerAddressFrame.Top = "5.0cm";
        ////    _sellerAddressFrame.RelativeVertical = RelativeVertical.Page;


        ////}

        ////private void create_InfoToAddressFrame(Section mainSection)
        ////{
        ////    _infoToAddressFrame = mainSection.AddTextFrame();
        ////    _infoToAddressFrame.Height = "3.0cm";
        ////    _infoToAddressFrame.Width = "7.0cm";
        ////    _infoToAddressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
        ////    _infoToAddressFrame.Left = "7.5cm";


        ////    _infoToAddressFrame.Top = "8.5cm";
        ////    _infoToAddressFrame.RelativeVertical = RelativeVertical.Page;
        ////}

        ////private void create_ShipToAddressFrame(Section mainSection)
        ////{
        ////    _shipToAddressFrame = mainSection.AddTextFrame();
        ////    _shipToAddressFrame.Height = "3.0cm";
        ////    _shipToAddressFrame.Width = "7.0cm";
        ////    _shipToAddressFrame.Left = ShapePosition.Left;
        ////    _shipToAddressFrame.RelativeHorizontal = RelativeHorizontal.Character;

        ////    _shipToAddressFrame.Top = "8.5cm";
        ////    _shipToAddressFrame.RelativeVertical = RelativeVertical.Page;
        ////}

        //public Document CreateNewDocument(InvoicPdfParameter param)
        //{

        //    _document = new Document();
        //    addDocumentInfo(param);
        //    DefineStyles();
        //    create_InvoiceInfraStructure(param);
        //    FillContent(param);

        //    return _document;

        //}

        //private void addDocumentInfo(InvoicPdfParameter param)
        //{
        //    if (!param.DocHeaderInfo.Title.IsNullOrWhiteSpace())
        //        _document.Info.Title = param.DocHeaderInfo.Title;

        //    if (!param.DocHeaderInfo.Subject.IsNullOrWhiteSpace())
        //        _document.Info.Subject = param.DocHeaderInfo.Subject;

        //    if (!param.DocHeaderInfo.Author.IsNullOrWhiteSpace())
        //        _document.Info.Author = param.DocHeaderInfo.Author;

        //}


        //public void create_InvoiceInfraStructure(InvoicPdfParameter param)
        //{
        //    // Each MigraDoc document needs at least one section.
        //    //var mainSection = Create_HeaderSection(param);
        //    Section mainSection = _document.AddSection();
        //    mainSection.PageSetup = _document.DefaultPageSetup.Clone();

        //    setup_headingTable(mainSection, param);

        //    // Create the footer.
        //    setup_footerTable(mainSection, param);


        //    // We use an empty paragraph to move the first text line below the address field.
        //    var paragraph = mainSection.AddParagraph();

        //    paragraph.Format.LineSpacing = param.Logo.TopMarginAfterLogo();
        //    paragraph.Format.LineSpacingRule = LineSpacingRule.Exactly;

        //    setup_AddressTable(mainSection, param);

        //    setup_InfoTable(mainSection, param);

        //    setup_ItemTable(mainSection, param);
        //    setup_commentAndTotalsTable(mainSection, param);
        //}

        //private void setupHeaderTable(Section section, InvoicPdfParameter param)
        //{

        //    _headingTable = section.Headers.Primary.AddTable();


        //    //var image = section.Headers.Primary.AddImage(param.Logo.Address ?? "");

        //    //image.Height = param.Logo.Height;

        //    //image.LockAspectRatio = true;
        //    //image.RelativeVertical = RelativeVertical.Line;
        //    //image.RelativeHorizontal = RelativeHorizontal.Margin;
        //    //image.Top = ShapePosition.Top;
        //    //image.Left = ShapePosition.Right;
        //    //image.WrapFormat.Style = WrapStyle.Through;
        //}

        //#endregion

        //private Paragraph show_SenderAddressFrameOnTop(Paragraph paragraph, PdfParameter param)
        //{
        //    paragraph = _customerAddressFrame.AddParagraph(param.Addresses.WebCompany.ToString());
        //    paragraph.Format.Font.Size = 7;
        //    paragraph.Format.Font.Bold = true;
        //    paragraph.Format.SpaceAfter = 3;
        //    return paragraph;
        //}

        //private static Paragraph para_InvoiceAndDate(Section section, Paragraph paragraph)
        //{
        //    // And now the paragraph with text.
        //    paragraph = section.AddParagraph();
        //    paragraph.Format.SpaceBefore = 0;
        //    paragraph.Style = "Reference";
        //    paragraph.AddFormattedText("INVOICE", TextFormat.Bold);
        //    paragraph.AddTab();
        //    paragraph.AddText("Cologne, ");
        //    paragraph.AddDateField("dd.MM.yyyy");
        //    return paragraph;
        //}

        //#region Paging

        //private Paragraph PagingParagraph()
        //{
        //    Paragraph paragraph = new Paragraph();

        //    return paragraph;
        //}
        //#endregion

        //#region itemTable
        //private void setup_ItemTable(Section mainSection, InvoicPdfParameter param)
        //{
        //    //Create a table to hold the itemns
        //    _itemTable = create_Table(mainSection);

        //    // Before you can add a row, you must define the columns.
        //    itemTable_defineColumns(param);

        //    // Create the header of the table.
        //    itemTable_Title_Row1(param);
        //    itemTable_title_Row2(param);

        //    //setTableEdge(_itemTable);
        //}

        //private void itemTable_defineColumns(InvoicPdfParameter param)
        //{
        //    var serialCol = _itemTable.AddColumn("0.97cm");
        //    serialCol.Format.Alignment = ParagraphAlignment.Center;


        //    var description = _itemTable.AddColumn("4.62cm");
        //    description.Format.Alignment = ParagraphAlignment.Right;

        //    var ordered = _itemTable.AddColumn("2.54cm");
        //    ordered.Format.Alignment = ParagraphAlignment.Right;


        //    var shipped = _itemTable.AddColumn("2.54cm");
        //    shipped.Format.Alignment = ParagraphAlignment.Right;

        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            //the next 2 columns are not added.
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var price = _itemTable.AddColumn("2.54cm");
        //    price.Format.Alignment = ParagraphAlignment.Center;

        //    var extended = _itemTable.AddColumn("3.81cm");
        //    extended.Format.Alignment = ParagraphAlignment.Right;
        //}

        ///// <summary>
        ///// This is actually a part of the table.
        ///// </summary>
        ///// <param name="section"></param>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //private Row itemTable_Title_Row1(InvoicPdfParameter param)
        //{
        //    var row = _itemTable.AddRow();

        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;
        //    row.Shading.Color = Color_Table_LtBlue;

        //    var serialNo = row.Cells[0];
        //    var description = row.Cells[1];
        //    var ordered = row.Cells[2];
        //    var shipped = row.Cells[3];

        //    serialNo.AddParagraph("Sr#");
        //    serialNo.Format.Alignment = ParagraphAlignment.Center;
        //    serialNo.VerticalAlignment = VerticalAlignment.Center;
        //    serialNo.MergeDown = 1;

        //    description.AddParagraph("Description");
        //    description.Format.Font.Bold = true;
        //    description.Format.Alignment = ParagraphAlignment.Left;

        //    ordered.AddParagraph("Ordered");
        //    ordered.Format.Font.Bold = true;

        //    ordered.Format.Alignment = ParagraphAlignment.Right;

        //    shipped.AddParagraph("Shipped");
        //    shipped.Format.Alignment = ParagraphAlignment.Right;
        //    shipped.Format.Font.Bold = true;


        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            //the next 2 columns are not added.
        //            return row;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var price = row.Cells[4];
        //    var extended = row.Cells[5];

        //    price.AddParagraph("Price");
        //    price.Format.Font.Bold = true;
        //    price.Format.Alignment = ParagraphAlignment.Right;

        //    extended.AddParagraph("Extended Price");
        //    extended.Format.Alignment = ParagraphAlignment.Right;
        //    extended.VerticalAlignment = VerticalAlignment.Center;
        //    extended.MergeDown = 1;
        //    extended.Format.Font.Bold = true;

        //    serialNo.Borders.Bottom.Width = "1pt";
        //    extended.Borders.Bottom.Width = "1pt";

        //    return row;
        //}

        //private Row itemTable_title_Row2(InvoicPdfParameter param)
        //{
        //    var row = _itemTable.AddRow();

        //    //row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;
        //    row.Shading.Color = Color_Table_LtBlue;

        //    var itemComment = row.Cells[1];
        //    itemComment.AddParagraph("---- Comment ----");
        //    itemComment.Format.Alignment = ParagraphAlignment.Center;
        //    row.Borders.Bottom.Width = "1pt";
        //    itemComment.MergeRight = 3;

        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            itemComment.MergeRight = 2;
        //            return row;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }


        //    return row;
        //}


        //#endregion


        //#region Address Table

        //private void setup_AddressTable(Section mainSection, InvoicPdfParameter param)
        //{
        //    //Create a table to hold the itemns
        //    _addressTable = create_Table(mainSection);

        //    // Before you can add a row, you must define the columns.
        //    addressTable_defineItemTableColumns();

        //    // Create the header of the table.
        //    var row = addressTable_Row1(param);

        //    //blank row
        //    row = _addressTable.AddRow();
        //    row.Borders.Visible = false;

        //    row = addressTable_Row2();
        //    //setTableEdge(_addressTable);

        //    row = _addressTable.AddRow();
        //    row.Borders.Visible = false;
        //}

        //private void addressTable_defineItemTableColumns()
        //{
        //    var column = _addressTable.AddColumn("8.4cm");
        //    column.Format.Alignment = ParagraphAlignment.Center;

        //    column = _addressTable.AddColumn("0.22cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;

        //    column = _addressTable.AddColumn("8.4cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;

        //}
        //private Row addressTable_Row1(InvoicPdfParameter param)
        //{
        //    var row = _addressTable.AddRow();
        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;
        //    row.Shading.Color = Color_Table_LtBlue;

        //    row.Cells[0].AddParagraph("Buyer:");
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[0].Borders.Bottom.Visible = true;
        //    //row.Borders.r

        //    row.Cells[2].AddParagraph("Seller:");
        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[2].Borders.Bottom.Visible = true;

        //    row.Cells[1].Borders.Top.Clear();
        //    row.Cells[1].Borders.Bottom.Clear();
        //    row.Cells[1].Shading.Visible = false;

        //    //this is the row where the address will appear
        //    row = _addressTable.AddRow();
        //    row.Shading = null;

        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[0].Borders.Bottom.Visible = true;

        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[2].Borders.Bottom.Visible = true;


        //    row.Cells[1].Borders.Top.Clear();
        //    row.Cells[1].Borders.Bottom.Clear();
        //    row.Cells[1].Shading.Visible = false;

        //    return row;
        //}

        //private Row addressTable_Row1Address(InvoicPdfParameter param)
        //{
        //    var row = _addressTable.AddRow();
        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;
        //    row.Shading.Color = Color_Table_LtBlue;

        //    row.Cells[0].AddParagraph("Buyer:");
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[0].Borders.Bottom.Visible = true;

        //    row.Cells[2].AddParagraph("Seller:");
        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[2].Borders.Bottom.Visible = true;

        //    row.Cells[1].Borders.Top.Clear();
        //    row.Cells[1].Borders.Bottom.Clear();
        //    row.Cells[1].Shading.Visible = false;



        //    //this is the row where the address will appear
        //    row = _addressTable.AddRow();
        //    row.Shading = null;

        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[0].Borders.Bottom.Visible = true;

        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[2].Borders.Bottom.Visible = true;


        //    row.Cells[1].Borders.Top.Clear();
        //    row.Cells[1].Borders.Bottom.Clear();
        //    row.Cells[1].Shading.Visible = false;

        //    return row;
        //}

        //private Row addressTable_Row2()
        //{
        //    var row = _addressTable.AddRow();
        //    row.HeadingFormat = true;
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.Format.Font.Bold = true;
        //    row.Shading.Color = Color_Table_LtBlue;

        //    row.Cells[0].AddParagraph("Ship To:");
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;

        //    row.Cells[2].AddParagraph("Inform To:");
        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Top;

        //    row.Cells[1].Borders.Top.Clear();
        //    row.Cells[1].Borders.Bottom.Clear();
        //    row.Cells[1].Shading.Visible = false;

        //    //this is the row where the address will appear
        //    row = _addressTable.AddRow();
        //    row.Shading = null;

        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[0].Borders.Bottom.Visible = true;

        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
        //    row.Cells[2].Borders.Bottom.Visible = true;


        //    row.Cells[1].Borders.Top.Clear();
        //    row.Cells[1].Borders.Bottom.Clear();
        //    row.Cells[1].Shading.Visible = false;

        //    return row;
        //}

        //#endregion


        //#region Info Table
        //private void setup_InfoTable(Section mainSection, InvoicPdfParameter param)
        //{
        //    //Create a table to hold the itemns
        //    _infoTable = create_Table(mainSection);

        //    // Before you can add a row, you must define the columns.
        //    infoTable_defineItemTableColumns();

        //    // Create the header of the table.
        //    var row = infoTable_Row1(param);

        //    //blank row
        //    //row = _infoTable.AddRow();
        //    //row.Borders.Visible = false;

        //    row = infoTable_Row2(row);
        //    //setTableEdge(_infoTable);

        //    row = _infoTable.AddRow();
        //    row.Borders.Visible = false;
        //}

        //private Row infoTable_Row2(Row row)
        //{
        //    row = _infoTable.AddRow();
        //    row.Format.Alignment = ParagraphAlignment.Center;
        //    row.VerticalAlignment = VerticalAlignment.Bottom;


        //    return row;

        //}

        //private Row infoTable_Row1(InvoicPdfParameter param)
        //{
        //    var row = _infoTable.AddRow();
        //    row.Format.Font.Bold = true;
        //    row.Shading.Color = Color_Table_LtBlue;
        //    row.Shading.Visible = true;

        //    row.Cells[0].AddParagraph("PO#:");
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
        //    row.Cells[0].VerticalAlignment = VerticalAlignment.Top;

        //    row.Cells[1].AddParagraph("PO Date");
        //    row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
        //    row.Cells[1].VerticalAlignment = VerticalAlignment.Top;

        //    row.Cells[2].AddParagraph("Ship Date");
        //    row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
        //    row.Cells[2].VerticalAlignment = VerticalAlignment.Top;

        //    row.Cells[3].AddParagraph("Ship Wt.");
        //    row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
        //    row.Cells[3].VerticalAlignment = VerticalAlignment.Top;

        //    row.Cells[4].AddParagraph("Carrier");
        //    row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
        //    row.Cells[4].VerticalAlignment = VerticalAlignment.Top;

        //    row.Cells[5].AddParagraph();
        //    row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
        //    row.Cells[5].VerticalAlignment = VerticalAlignment.Top;

        //    return row;
        //}

        ////private Row infoTable_Row2(Row row)
        ////{
        ////    //row = _infoTable.AddRow();
        ////    //row.Cells[0].AddParagraph("PO#:");
        ////    //row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
        ////    //row.Cells[0].VerticalAlignment = VerticalAlignment.Top;

        ////    //row.Cells[2].AddParagraph("PO Date");
        ////    //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
        ////    //row.Cells[2].VerticalAlignment = VerticalAlignment.Top;

        ////    //row.Cells[2].AddParagraph("PO Date");
        ////    //row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
        ////    //row.Cells[2].VerticalAlignment = VerticalAlignment.Top;

        ////    return row;
        ////}

        //private void infoTable_defineItemTableColumns()
        //{
        //    var column = _infoTable.AddColumn("2.836667cm");
        //    column.Format.Alignment = ParagraphAlignment.Center;

        //    column = _infoTable.AddColumn("2.836667cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;

        //    column = _infoTable.AddColumn("2.836667cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;

        //    column = _infoTable.AddColumn("2.836667cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;

        //    column = _infoTable.AddColumn("2.836667cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;

        //    column = _infoTable.AddColumn("2.836667cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;



        //}

        //#endregion

        //#region Comment And Totals Table
        //private void setup_commentAndTotalsTable(Section mainSection, InvoicPdfParameter param)
        //{
        //    //Create a table to hold the itemns
        //    _commentAndTotalsTable = create_Table(mainSection);

        //    // Before you can add a row, you must define the columns.
        //    commentAndTotalsTable_defineColumns(param);

        //    // Create the header of the table.
        //    commentAndTotalsTable_Row1(param);
        //    commentAndTotalsTable_Row2(param);
        //    commentAndTotalsTable_Row3(param);
        //    commentAndTotalsTable_Row4(param);
        //    commentAndTotalsTable_Row5(param);

        //    //setTableEdge(_commentAndTotalsTable);
        //}
        //private Row commentAndTotalsTable_Row1(InvoicPdfParameter param)
        //{
        //    var row = _commentAndTotalsTable.AddRow();
        //    row.Shading.Color = Color_Table_LtBlue;
        //    row.Shading.Visible = true;
        //    row.Format.Font.Bold = true;
        //    row.Borders.Visible = true;

        //    var comment = row.Cells[0];
        //    comment.AddParagraph("Comment:");
        //    comment.Format.Alignment = ParagraphAlignment.Center;
        //    comment.VerticalAlignment = VerticalAlignment.Top;

        //    row.Shading.Color = Color_Table_LtBlue;
        //    row.Shading.Visible = true;
        //    row.Cells[1].Shading.Visible = false;

        //    //hide columns depending on document type
        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            comment.MergeRight = 1;
        //            //stops here.
        //            return row;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var misc = row.Cells[2];
        //    misc.AddParagraph("Tax");
        //    misc.Format.Alignment = ParagraphAlignment.Right;

        //    var blankCol = row.Cells[1];
        //    blankCol.Borders.Visible = false;

        //    showAndHideColorInCommentsAndTotalsTable(row, param);

        //    return row;
        //}

        //private void commentAndTotalsTable_Row2(InvoicPdfParameter param)
        //{
        //    var row = _commentAndTotalsTable.AddRow();
        //    row.Format.Font.Bold = true;
        //    row.Borders.Visible = true;

        //    var comment = row.Cells[0];
        //    comment.MergeDown = 3;
        //    comment.Shading.Visible = false;


        //    //hide columns depending on document type
        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            comment.MergeRight = 1;
        //            //stops here.
        //            return;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var shippingAndHandling = row.Cells[2];
        //    shippingAndHandling.AddParagraph("Shipping etc");
        //    shippingAndHandling.Format.Alignment = ParagraphAlignment.Right;

        //    var blankCol = row.Cells[1];
        //    blankCol.Borders.Visible = false;

        //    showAndHideColorInCommentsAndTotalsTable(row, param);

        //}
        //private void commentAndTotalsTable_Row3(InvoicPdfParameter param)
        //{
        //    var row = _commentAndTotalsTable.AddRow();
        //    row.Format.Font.Bold = true;

        //    //hide columns depending on document type
        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            //stops here.
        //            return;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var misc = row.Cells[2];
        //    misc.AddParagraph("Misc");
        //    misc.Format.Alignment = ParagraphAlignment.Right;

        //    showAndHideColorInCommentsAndTotalsTable(row, param);

        //}

        //private void commentAndTotalsTable_Row4(InvoicPdfParameter param)
        //{
        //    var row = _commentAndTotalsTable.AddRow();
        //    row.Format.Font.Bold = true;


        //    //hide columns depending on document type
        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            //stops here.
        //            return;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var misc = row.Cells[2];
        //    misc.AddParagraph("LESS: Paid");
        //    misc.Format.Alignment = ParagraphAlignment.Right;
        //    showAndHideColorInCommentsAndTotalsTable(row, param);

        //}

        //private void commentAndTotalsTable_Row5(InvoicPdfParameter param)
        //{
        //    var row = _commentAndTotalsTable.AddRow();
        //    row.Format.Font.Bold = true;

        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            //stops here
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var grandTotal = row.Cells[2];

        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            grandTotal.AddParagraph("Please Pay");
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            grandTotal.AddParagraph("Please Pay");
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            grandTotal.AddParagraph("Your Credit");
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            grandTotal.AddParagraph("Due");
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            throw new Exception("Code should not reach here. Programming error.");

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            grandTotal.AddParagraph("Total Offer");
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            grandTotal.AddParagraph("Total Request");
        //            break;
        //        default:
        //            break;
        //    }

        //    grandTotal.Format.Alignment = ParagraphAlignment.Right;
        //    showAndHideColorInCommentsAndTotalsTable(row, param);

        //}



        //private static void showAndHideColorInCommentsAndTotalsTable(Row row, InvoicPdfParameter param)
        //{
        //    row.Shading.Color = Color_Table_LtBlue;
        //    row.Shading.Visible = true;
        //    row.Cells[1].Shading.Visible = false;

        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            //stops here
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    row.Cells[3].Shading.Visible = false;


        //}
        //private void commentAndTotalsTable_defineColumns(InvoicPdfParameter param)
        //{
        //    //col 0
        //    var column = _commentAndTotalsTable.AddColumn("10.57cm");
        //    column.Format.Alignment = ParagraphAlignment.Center;

        //    //col 1
        //    column = _commentAndTotalsTable.AddColumn("0.1cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;
        //    column.Borders.Visible = false;
        //    column.Shading.Visible = false;


        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            //the next 2 columns are not added.
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }
        //    //col 2
        //    column = _commentAndTotalsTable.AddColumn("2.54cm");
        //    column.Format.Alignment = ParagraphAlignment.Center;

        //    //col 3
        //    column = _commentAndTotalsTable.AddColumn("3.81cm");
        //    column.Format.Alignment = ParagraphAlignment.Right;

        //}

        ////private void setup_CommentAndTotalsTable(Section mainSection)
        ////{
        ////    //Create a table to hold the itemns
        ////    _headingTable = create_Table(mainSection);

        ////    // Before you can add a row, you must define the columns.
        ////    itemTable_defineColumns();

        ////    // Create the header of the table.
        ////    var row = itemTable_Title_Row1();
        ////    row = itemTable_title_Row2();

        ////    setTableEdge(_itemTable);
        ////}


        //#endregion


        //#region Heading Table

        ////private Section Create_HeaderSection(InvoicPdfParameter param)
        ////{
        ////    var section = _document.AddSection();
        ////    section.PageSetup = _document.DefaultPageSetup.Clone();

        ////    // Define the page setup. We use an image in the header, therefore the
        ////    // default top margin is too small for our invoice.
        ////    section.PageSetup.TopMargin = Unit.FromCentimeter(param.Logo.TopMarginAfterLogo());

        ////    // Put the logo in the header.
        ////    setupHeaderTable(section, param);

        ////    return section;
        ////}


        //private void setup_headingTable(Section section, InvoicPdfParameter param)
        //{
        //    //Create a table to hold the itemns
        //    _headingTable = section.Headers.Primary.AddTable();
        //    section.PageSetup.TopMargin = Unit.FromCentimeter(param.Logo.TopMarginAfterLogo());

        //    // Before you can add a row, you must define the columns.
        //    headingTable_defineItemTableColumns();
        //    //_headingTable.Borders.Visible = true;
        //    // Create the header of the table.
        //    _headingTable.RightPadding = 0;
        //    headingTable_Row1(param);
        //    headingTable_Row2();
        //    headingTable_Row3();
        //    _headingTable.AddRow();
        //}
        //private void headingTable_Row1(InvoicPdfParameter param)
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
        //private void headingTable_Row3()
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

        //private void headingTable_defineItemTableColumns()
        //{
        //    //col 0
        //    var logoAddress = _headingTable.AddColumn("5cm");
        //    logoAddress.Format.Alignment = ParagraphAlignment.Left;

        //    //col 1
        //    var addressCol2 = _headingTable.AddColumn("5cm");
        //    addressCol2.Format.Alignment = ParagraphAlignment.Left;

        //    //col 2
        //    var blankCol = _headingTable.AddColumn("0.12cm");
        //    blankCol.Borders.Visible = false;
        //    blankCol.Shading.Visible = false;

        //    //col 3
        //    var docNoAndHeading = _headingTable.AddColumn("6.9cm");
        //    docNoAndHeading.Format.Alignment = ParagraphAlignment.Right;

        //}

        ////private void setup_headingTable(Section mainSection)
        ////{
        ////    //Create a table to hold the itemns
        ////    _itemTable = create_Table(mainSection);

        ////    // Before you can add a row, you must define the columns.
        ////    itemTable_defineColumns();

        ////    // Create the header of the table.
        ////    var row = itemTable_Title_Row1();
        ////    row = itemTable_title_Row2();

        ////    setTableEdge(_itemTable);
        ////}


        //#endregion

        //#region footer

        ////private Paragraph setup_Footer(Section section, InvoicPdfParameter param)
        ////{
        ////    var paragraph = section.Footers.Primary.AddParagraph();
        ////    section.PageSetup.BottomMargin = Unit.FromCentimeter(3);

        ////    footerTable_DefineColums();

        ////    paragraph.AddText(param.Addresses.WebCompany.ToStringTwoLine());
        ////    paragraph.Format.Font.Size = 9;
        ////    paragraph.Format.Alignment = ParagraphAlignment.Center;
        ////    paragraph.Format.Borders.Top.Width = "1pt";
        ////    return paragraph;
        ////}

        //private void setup_footerTable(Section section, InvoicPdfParameter param)
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

        //private void footerTable_Row1(InvoicPdfParameter param)
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

        //#endregion


        //#region fill content.

        ///// <summary>
        ///// Creates the static parts of the invoice.
        ///// </summary>

        ////private Paragraph addAddressFor(TextFrame addressFrame, Address address, Paragraph paragraph)
        ////{
        ////    // Fill the address in the address text frame.
        ////    paragraph.AddText(address.ToString());
        ////    return paragraph;
        ////    //paragraph.AddLineBreak();

        ////}
        //void FillContent(InvoicPdfParameter param)
        //{
        //    _lineItems = param.DocumentInfo.LineItems;


        //    //fill the header
        //    fillHeader(param);
        //    fillFooter(param);
        //    //fill the addresses
        //    fillAddresses(param);

        //    fillDocInfo(param);

        //    if (_lineItems.IsNullOrEmpty())
        //    {
        //        var blankRow = this._itemTable.AddRow();
        //        blankRow.Borders.Visible = false;
        //    }
        //    else
        //    {

        //        int counter = 0;
        //        foreach (var item in _lineItems)
        //        {
        //            counter += 1;

        //            // Each item fills two rows.
        //            fillItems(counter, item, param);

        //        }
        //    }

        //    fillTotalsUpToHere(param);
        //    fillCommentEtc(param);
        //}

        //private void fillFooter(InvoicPdfParameter param)
        //{
        //    var footerRows = _footerTable.Rows;

        //    var left = footerRows[0].Cells[0];
        //    var middle = footerRows[0].Cells[1];

        //    var right = footerRows[0].Cells[2];

        //    //left.AddParagraph();
        //    //right

        //    Paragraph leftparagraph = left.AddParagraph();
        //    leftparagraph.AddText("Printed: " + DateTime.Now.ToLocalTime().ToString());

        //    middle.AddParagraph(param.Addresses.WebCompany.ToStringTwoLine());

        //    Paragraph paragraph = right.AddParagraph();
        //    paragraph.AddText("Page ");
        //    paragraph.AddPageField();
        //    paragraph.AddText(" of ");
        //    paragraph.AddNumPagesField();


        //}

        //private void fillItems(int counter, LineItem item, InvoicPdfParameter param)
        //{
        //    var row1 = this._itemTable.AddRow();

        //    row1.TopPadding = 1.5;

        //    var serialNo = row1.Cells[0];
        //    var description = row1.Cells[1];
        //    var ordered = row1.Cells[2];
        //    var shipped = row1.Cells[3];

        //    row1.VerticalAlignment = VerticalAlignment.Center;

        //    //if there is no comment then do not show it.
        //    if (!item.Comment.IsNullOrWhiteSpace())
        //    {
        //        var row2 = this._itemTable.AddRow();
        //        row2.BottomPadding = 1.5;
        //        row2.Format.Alignment = ParagraphAlignment.Left;

        //        var comment = row2.Cells[1];
        //        comment.MergeRight = 3;

        //        switch (param.DocumentInfo.DocType)
        //        {
        //            case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //                break;
        //            case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //                break;
        //            case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //                break;
        //            case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //                break;

        //            case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //                comment.MergeRight = 2;
        //                break;

        //            case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //                break;
        //            case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //                break;
        //            default:
        //                break;
        //        }

        //        comment.AddParagraph(item.Comment);
        //        comment.Format.Font.Italic = true;
        //        comment.Format.Font.Size = "8pt";

        //        serialNo.MergeDown = 1;
        //        //row2.Borders.Bottom.Width = "0.5pt";
        //        row2.VerticalAlignment = VerticalAlignment.Center;

        //        if (counter % 2 == 0)
        //        {
        //            row2.Shading.Color = Color_Table_LtBlue;
        //        }
        //    }

        //    if (counter % 2 == 0)
        //    {
        //        row1.Shading.Color = Color_Table_LtBlue;
        //    }

        //    serialNo.VerticalAlignment = VerticalAlignment.Center;
        //    description.Format.Alignment = ParagraphAlignment.Left;
        //    ordered.Format.Alignment = ParagraphAlignment.Right;
        //    shipped.Format.Alignment = ParagraphAlignment.Right;


        //    serialNo.AddParagraph(counter.ToString() + ".");
        //    description.AddParagraph(item.Description);
        //    ordered.AddParagraph(item.Ordered.ToString("N2"));
        //    shipped.AddParagraph(item.Shipped.ToString("N2"));


        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var price = row1.Cells[4];
        //    var extended = row1.Cells[5];

        //    //if there is a comment....
        //    if (!item.Comment.IsNullOrWhiteSpace())
        //    {
        //        extended.MergeDown = 1;
        //    }

        //    price.Format.Alignment = ParagraphAlignment.Right;
        //    extended.Format.Alignment = ParagraphAlignment.Right;

        //    price.AddParagraph(item.Price.ToString("N2"));
        //    extended.AddParagraph(item.Extended().ToString("N2"));
        //}

        //private void fillCommentEtc(InvoicPdfParameter param)
        //{
        //    var rows = _commentAndTotalsTable.Rows;

        //    var row1 = rows[0];
        //    var row2 = rows[1];
        //    var row3 = rows[2];
        //    var row4 = rows[3];
        //    var row5 = rows[4];


        //    var comment = row2.Cells[0];
        //    comment.AddParagraph(param.DocumentInfo.Comment);
        //    comment.Format.Font.Italic = true;
        //    comment.Format.Font.Color = Color_Red;
        //    comment.Format.Alignment = ParagraphAlignment.Left;
        //    comment.Format.Font.Bold = false;


        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var tax = row1.Cells[3];
        //    var shippingAndHandling = row2.Cells[3];
        //    var misc = row3.Cells[3];
        //    var advancePayment = row4.Cells[3];
        //    var grandTotal = row5.Cells[3];

        //    tax.AddParagraph(param.DocumentInfo.Tax.ToString("N2"));
        //    shippingAndHandling.AddParagraph(param.DocumentInfo.ShippingHandling.ToString("N2"));
        //    misc.AddParagraph(param.DocumentInfo.Misc.ToString("N2"));
        //    advancePayment.AddParagraph(param.DocumentInfo.AdvancePayment.ToString("N2"));
        //    grandTotal.AddParagraph(param.DocumentInfo.GrandTotal);

        //    misc.Format.Alignment = ParagraphAlignment.Right;
        //    misc.Format.Font.Bold = false;

        //    shippingAndHandling.Format.Alignment = ParagraphAlignment.Right;
        //    shippingAndHandling.Format.Font.Bold = false;

        //    tax.Format.Alignment = ParagraphAlignment.Right;
        //    tax.Format.Font.Bold = false;

        //    grandTotal.Format.Alignment = ParagraphAlignment.Right;
        //}

        //private void fillTotalsUpToHere(InvoicPdfParameter param)
        //{
        //    var blankRow1 = _itemTable.AddRow();
        //    blankRow1.Borders.Visible = false;
        //    blankRow1.HeightRule = RowHeightRule.Exactly;
        //    blankRow1.Height = 2;


        //    var totalsUpToHereRows = _itemTable.AddRow();

        //    var totalsUpToHereHeading = totalsUpToHereRows.Cells[0];
        //    var orderedTotals = totalsUpToHereRows.Cells[2];
        //    var shippedTotals = totalsUpToHereRows.Cells[3];

        //    var blankRow2 = _itemTable.AddRow();
        //    blankRow2.Borders.Visible = false;
        //    blankRow2.HeightRule = RowHeightRule.Exactly;
        //    blankRow2.Height = 2;

        //    totalsUpToHereRows.Format.Font.Bold = true;
        //    totalsUpToHereRows.Format.Alignment = ParagraphAlignment.Right;
        //    totalsUpToHereRows.Borders.Visible = true;
        //    totalsUpToHereRows.Shading.Color = Color_Table_LtBlue;
        //    totalsUpToHereRows.Borders.Width = "0.5pt";

        //    totalsUpToHereHeading.AddParagraph("Totals up to here");
        //    totalsUpToHereHeading.MergeRight = 1;

        //    orderedTotals.Shading.Visible = false;
        //    shippedTotals.Shading.Visible = false;


        //    orderedTotals.AddParagraph(param.DocumentInfo.TotalOrdered.ToString("N2"));
        //    shippedTotals.AddParagraph(param.DocumentInfo.TotalShipped.ToString("N2"));

        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var priceAvg = totalsUpToHereRows.Cells[4];
        //    var extendedTotal = totalsUpToHereRows.Cells[5];

        //    priceAvg.Shading.Visible = false;
        //    extendedTotal.Shading.Visible = false;

        //    priceAvg.AddParagraph(param.DocumentInfo.AvgPrice);
        //    extendedTotal.AddParagraph(param.DocumentInfo.TotalExtended.ToString("N2"));


        //}

        //private void fillDocInfo(InvoicPdfParameter param)
        //{
        //    var docInfoRow = _infoTable.Rows;
        //    var poNo = docInfoRow[1].Cells[0];
        //    var poDate = docInfoRow[1].Cells[1];
        //    var shipDate = docInfoRow[1].Cells[2];
        //    var shipWt = docInfoRow[1].Cells[3];
        //    var carrier = docInfoRow[1].Cells[4];

        //    poNo.AddParagraph(param.DocumentInfo.PurchaseOrderNumber);
        //    poDate.AddParagraph(param.DocumentInfo.PurchaseOrderDate);
        //    shipDate.AddParagraph(param.DocumentInfo.ShipDate);
        //    shipWt.AddParagraph(param.DocumentInfo.ShipWeight);
        //    carrier.AddParagraph(param.DocumentInfo.ShippingCarrier);



        //}

        //private void fillAddresses(InvoicPdfParameter param)
        //{
        //    var addyTableRows = _addressTable.Rows;

        //    var addyRow1 = addyTableRows[1];
        //    var addyRow2 = addyTableRows[4];

        //    var buyerBox = addyRow1.Cells[0];
        //    var sellerBox = addyRow1.Cells[2];
        //    var shipToBox = addyRow2.Cells[0];
        //    var informToBox = addyRow2.Cells[2];

        //    addToAddressBox(param, buyerBox, param.Addresses.Customer.ToString());
        //    addToAddressBox(param, sellerBox, param.Addresses.Seller.ToString());
        //    addToAddressBox(param, shipToBox, param.Addresses.ShipTo.ToString());
        //    addToAddressBox(param, informToBox, param.Addresses.InformTo.ToString());
        //}

        //private void fillHeader(InvoicPdfParameter param)
        //{
        //    var headerRows = _headingTable.Rows;

        //    var header_Logo_Cell = headerRows[0].Cells[0];
        //    var header_CoAddy_Cell = headerRows[2].Cells[0];

        //    var header_DocNo_Cell = headerRows[0].Cells[3];
        //    var header_DocDate_Cell = headerRows[1].Cells[3];
        //    var header_Title_Cell = headerRows[2].Cells[3];

        //    header_Logo_Cell.AddImage(param.Logo.Address);

        //    Paragraph docType = header_Title_Cell.AddParagraph(param.DocumentInfo.DocType.ToString().ToTitleSentance().ToUpper());
        //    docType.Format.Font.Size = "18pt";

        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            docType.Format.Font.Color = Color_Green;
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            docType.Format.Font.Color = Color_NavyBlue;
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            docType.Format.Font.Color = Color_Red;
        //            break;
        //        default:
        //            break;
        //    }
        //    header_DocNo_Cell.AddParagraph(param.DocumentInfo.DocumentNoWithTitle);
        //    header_DocDate_Cell.AddParagraph(param.DocumentInfo.Date);

        //    string webCoAddy = param.Addresses.WebCompany.ToStringTwoLine();
        //    //header_CoAddy_Cell.AddParagraph(param.WebCompanyName);
        //    header_CoAddy_Cell.AddParagraph(webCoAddy);
        //}

        //private static void addToAddressBox(InvoicPdfParameter param, Cell buyerBox, string address)
        //{
        //    Paragraph p = buyerBox.AddParagraph(address);
        //    p.Format.Font.Bold = false;
        //    p.Format.LeftIndent = "0.5cm";
        //    p.Style = "Normal";
        //}


        //#endregion

        //private void setTableEdge(Table table)
        //{
        //    //table.SetEdge(0, 0, 6, 2, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);

        //}

    }
}
