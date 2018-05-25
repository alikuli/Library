using InvoiceNS;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDocLibrary.InvoiceNS;

namespace AliKuli.Tools.PdfNS
{
    /// <summary>
    /// Creates the invoice form.
    /// </summary>
    public class CreateInvoice
    {
        /// <summary>
        /// The MigraDoc document that represents the invoice.
        /// </summary>
        Document _document;


        string logoAddress { get { return "../../../../assets/images/MigraDoc.png"; } }
        string logoHeight { get { return "3.5cm"; } }

        string companyAddress { get { return "PowerBooks Inc ● Sample Street 75 ● 56789 Cologne ● Germany"; } }

        ///// <summary>
        ///// The root navigator for the XML document.
        ///// </summary>
        //readonly XPathNavigator _navigator;

        /// <summary>
        /// The text frame of the MigraDoc document that contains the address.
        /// </summary>
        TextFrame _addressFrame;

        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        Table _table;

        ///// <summary>
        ///// Initializes a new instance of the class InvoiceForm and opens the specified XML document.
        ///// </summary>
        //public InvoiceForm()
        //{
        //    //var invoice = new XmlDocument();
        //    //// An XML invoice based on a sample created with Microsoft InfoPath.
        //    //invoice.Load(filename);
        //    //_navigator = invoice.CreateNavigator();
        //}

        /// <summary>
        /// Creates the invoice document.
        /// </summary>
        public Document CreateDocument(InvoicPdfParameter param)
        {
            // Create a new MigraDoc document.
            createNewDocument(param);
            DefineStyles();

            CreatePage();

            //FillContent();

            return _document;
        }

        private void createNewDocument(InvoicPdfParameter param)
        {
            _document = new Document();
            _document.Info.Title = param.DocHeaderInfo.Title;
            _document.Info.Subject = param.DocHeaderInfo.Subject;
            _document.Info.Author = param.DocHeaderInfo.Author;
        }

        /// <summary>
        /// Defines the styles used to format the MigraDoc document.
        /// </summary>
        void DefineStyles()
        {
            var style = getAndAdjustNormalStyle();

            // Create a new style called Table based on style Normal.
            style = createNewStyleCalledTable(style);

            // Create a new style called Title based on style Normal.
            style = createNewStyleCalledTitle(style);

            // Create a new style called Reference based on style Normal.
            style = createNewStyleCalledReference(style);
        }

        private Style getAndAdjustNormalStyle()
        {
            // Get the predefined style Normal.
            var style = _document.Styles["Normal"];

            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Segoe UI";

            style = _document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = _document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
            return style;
        }

        private Style createNewStyleCalledTable(Style style)
        {
            style = _document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Segoe UI Semilight";
            style.Font.Size = 9;
            return style;
        }

        private Style createNewStyleCalledTitle(Style style)
        {
            style = _document.Styles.AddStyle("Title", "Normal");
            style.Font.Name = "Segoe UI Semibold";
            style.Font.Size = 9;
            return style;
        }

        private Style createNewStyleCalledReference(Style basedOnStyle)
        {
            basedOnStyle = _document.Styles.AddStyle("Reference", "Normal");
            basedOnStyle.ParagraphFormat.SpaceBefore = "5mm";
            basedOnStyle.ParagraphFormat.SpaceAfter = "5mm";
            basedOnStyle.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
            return basedOnStyle;
        }

        /// <summary>
        /// Creates the static parts of the invoice.
        /// </summary>
        void CreatePage()
        {
            // Each MigraDoc document needs at least one section.
            var section = createSection();

            // Put the logo in the header.
            putLogoIntoHeader(section);

            // Create the footer.
            var paragraph = createFooter(section);

            // Create the text frame for the address.
            createAddressFrameForAddress(section);

            // Show the sender in the address frame on top
            paragraph = showSenderInAddressFrameOnTop(paragraph);

            // Add the print date field.
            paragraph = section.AddParagraph();
            addEmptyParagraph(paragraph);
            paragraph = addParaWithInvoiceAndDate(section, paragraph);
            addItemTable(section);

            // Before you can add a row, you must define the columns.
            defineColumnsOfTable();

            // Create the header of the table.
            var row = createHeaderOfTable();

            row = addRowNextRowToTable(row);
        }

        private Row addRowNextRowToTable(Row row)
        {
            row = _table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[1].AddParagraph("Quantity");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].AddParagraph("Unit Price");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].AddParagraph("Discount (%)");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[4].AddParagraph("Taxable");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;

            _table.SetEdge(0, 0, 6, 2, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);
            return row;
        }

        private Row createHeaderOfTable()
        {
            var row = _table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = TableBlue;
            row.Cells[0].AddParagraph("Item");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[0].MergeDown = 1;
            row.Cells[1].AddParagraph("Title and Author");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].MergeRight = 3;
            row.Cells[5].AddParagraph("Extended Price");
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[5].MergeDown = 1;
            return row;
        }

        private void defineColumnsOfTable()
        {
            var column = _table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = _table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = _table.AddColumn("3.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = _table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Right;
        }

        private void addItemTable(Section section)
        {
            // Create the item table.
            _table = section.AddTable();
            _table.Style = "Table";
            _table.Borders.Color = TableBorder;
            _table.Borders.Width = 0.25;
            _table.Borders.Left.Width = 0.5;
            _table.Borders.Right.Width = 0.5;
            _table.Rows.LeftIndent = 0;
        }

        private static Paragraph addParaWithInvoiceAndDate(Section section, Paragraph paragraph)
        {
            // And now the paragraph with text.
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = 0;
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("INVOICE", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Cologne, ");
            paragraph.AddDateField("dd.MM.yyyy");
            return paragraph;
        }

        private static void addEmptyParagraph(Paragraph paragraph)
        {
            // We use an empty paragraph to move the first text line below the address field.
            paragraph.Format.LineSpacing = "5.25cm";
            paragraph.Format.LineSpacingRule = LineSpacingRule.Exactly;
        }

        private Paragraph showSenderInAddressFrameOnTop(Paragraph paragraph)
        {
            paragraph = _addressFrame.AddParagraph("PowerBooks Inc · Sample Street 54 · 56789 Cologne");
            paragraph.Format.Font.Size = 7;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.SpaceAfter = 3;
            return paragraph;
        }

        private void createAddressFrameForAddress(Section section)
        {
            _addressFrame = section.AddTextFrame();
            _addressFrame.Height = "3.0cm";
            _addressFrame.Width = "7.0cm";
            _addressFrame.Left = ShapePosition.Left;
            _addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            _addressFrame.Top = "5.0cm";
            _addressFrame.RelativeVertical = RelativeVertical.Page;
        }

        private void putLogoIntoHeader(Section section)
        {
            var image = section.Headers.Primary.AddImage(logoAddress);

            image.Height = logoHeight;
            //image.Height = "3.5cm";
            image.LockAspectRatio = true;
            image.RelativeVertical = RelativeVertical.Line;
            image.RelativeHorizontal = RelativeHorizontal.Margin;
            image.Top = ShapePosition.Top;
            image.Left = ShapePosition.Right;
            image.WrapFormat.Style = WrapStyle.Through;
        }

        private Section createSection()
        {
            var section = _document.AddSection();

            // Define the page setup. We use an image in the header, therefore the
            // default top margin is too small for our invoice.
            section.PageSetup = _document.DefaultPageSetup.Clone();
            // We increase the TopMargin to prevent the document body from overlapping the page header.
            // We have an image of 3.5 cm height in the header.
            // The default position for the header is 1.25 cm.
            // We add 0.5 cm spacing between header image and body and get 5.25 cm.
            // Default value is 2.5 cm.
            section.PageSetup.TopMargin = "5.25cm";
            return section;
        }

        private Paragraph createFooter(Section section)
        {
            var paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText(companyAddress);
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            return paragraph;
        }

        /// <summary>
        /// Creates the dynamic parts of the invoice.
        /// </summary>
        //void FillContent()
        //{
        //    // Fill the address in the address text frame.
        //    var item = SelectItem("/invoice/to");
        //    var paragraph = _addressFrame.AddParagraph();
        //    paragraph.AddText(GetValue(item, "name/singleName"));
        //    paragraph.AddLineBreak();
        //    paragraph.AddText(GetValue(item, "address/line1"));
        //    paragraph.AddLineBreak();
        //    paragraph.AddText(GetValue(item, "address/postalCode") + " " + GetValue(item, "address/city"));

        //    // Iterate the invoice items.
        //    double totalExtendedPrice = 0;
        //    var iter = _navigator.Select("/invoice/items/*");
        //    while (iter.MoveNext())
        //    {
        //        item = iter.Current;
        //        var quantity = GetValueAsDouble(item, "quantity");
        //        var price = GetValueAsDouble(item, "price");
        //        var discount = GetValueAsDouble(item, "discount");

        //        // Each item fills two rows.
        //        var row1 = this._table.AddRow();
        //        var row2 = this._table.AddRow();
        //        row1.TopPadding = 1.5;
        //        row1.Cells[0].Shading.Color = TableGray;
        //        row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //        row1.Cells[0].MergeDown = 1;
        //        row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        //        row1.Cells[1].MergeRight = 3;
        //        row1.Cells[5].Shading.Color = TableGray;
        //        row1.Cells[5].MergeDown = 1;

        //        row1.Cells[0].AddParagraph(GetValue(item, "itemNumber"));
        //        paragraph = row1.Cells[1].AddParagraph();
        //        var formattedText = new FormattedText() { Style = "Title" };
        //        formattedText.AddText(GetValue(item, "title"));
        //        paragraph.Add(formattedText);
        //        paragraph.AddFormattedText(" by ", TextFormat.Italic);
        //        paragraph.AddText(GetValue(item, "author"));
        //        row2.Cells[1].AddParagraph(GetValue(item, "quantity"));
        //        row2.Cells[2].AddParagraph(price.ToString("0.00") + " €");
        //        if (discount > 0)
        //            row2.Cells[3].AddParagraph(discount.ToString("0") + '%');
        //        row2.Cells[4].AddParagraph();
        //        row2.Cells[5].AddParagraph(price.ToString("0.00"));
        //        var extendedPrice = quantity * price;
        //        extendedPrice = extendedPrice * (100 - discount) / 100;
        //        row1.Cells[5].AddParagraph(extendedPrice.ToString("0.00") + " €");
        //        row1.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
        //        totalExtendedPrice += extendedPrice;

        //        _table.SetEdge(0, _table.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.75);
        //    }

        //    // Add an invisible row as a space line to the table.
        //    var row = _table.AddRow();
        //    row.Borders.Visible = false;

        //    // Add the total price row.
        //    row = _table.AddRow();
        //    row.Cells[0].Borders.Visible = false;
        //    row.Cells[0].AddParagraph("Total Price");
        //    row.Cells[0].Format.Font.Bold = true;
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[0].MergeRight = 4;
        //    row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");
        //    row.Cells[5].Format.Font.Name = "Segoe UI";

        //    // Add the VAT row.
        //    row = _table.AddRow();
        //    row.Cells[0].Borders.Visible = false;
        //    row.Cells[0].AddParagraph("VAT (7%)");
        //    row.Cells[0].Format.Font.Bold = true;
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[0].MergeRight = 4;
        //    row.Cells[5].AddParagraph((0.07 * totalExtendedPrice).ToString("0.00") + " €");

        //    // Add the additional fee row.
        //    row = _table.AddRow();
        //    row.Cells[0].Borders.Visible = false;
        //    row.Cells[0].AddParagraph("Shipping and Handling");
        //    row.Cells[5].AddParagraph(0.ToString("0.00") + " €");
        //    row.Cells[0].Format.Font.Bold = true;
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[0].MergeRight = 4;

        //    // Add the total due row.
        //    row = _table.AddRow();
        //    row.Cells[0].AddParagraph("Total Due");
        //    row.Cells[0].Borders.Visible = false;
        //    row.Cells[0].Format.Font.Bold = true;
        //    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        //    row.Cells[0].MergeRight = 4;
        //    totalExtendedPrice += 0.19 * totalExtendedPrice;
        //    row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");
        //    row.Cells[5].Format.Font.Name = "Segoe UI";
        //    row.Cells[5].Format.Font.Bold = true;

        //    // Set the borders of the specified cell range.
        //    _table.SetEdge(5, _table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);

        //    // Add the notes paragraph.
        //    paragraph = _document.LastSection.AddParagraph();
        //    paragraph.Format.Alignment = ParagraphAlignment.Center;
        //    paragraph.Format.SpaceBefore = "1cm";
        //    paragraph.Format.Borders.Width = 0.75;
        //    paragraph.Format.Borders.Distance = 3;
        //    paragraph.Format.Borders.Color = TableBorder;
        //    paragraph.Format.Shading.Color = TableGray;
        //    item = SelectItem("/invoice");
        //    paragraph.AddText(GetValue(item, "notes"));
        //}

        /// <summary>
        /// Selects a subtree in the XML data.
        /// </summary>
        //XPathNavigator SelectItem(string path)
        //{
        //    var iter = _navigator.Select(path);
        //    iter.MoveNext();
        //    return iter.Current;
        //}

        /// <summary>
        /// Gets an element value from the XML data.
        /// </summary>
        //static string GetValue(XPathNavigator nav, string name)
        //{
        //    //nav = nav.Clone();
        //    var iter = nav.Select(name);
        //    iter.MoveNext();
        //    return iter.Current.Value;
        //}

        /// <summary>
        /// Gets an element value as double from the XML data.
        /// </summary>
        //static double GetValueAsDouble(XPathNavigator nav, string name)
        //{
        //    try
        //    {
        //        var value = GetValue(nav, name);
        //        if (value.Length == 0)
        //            return 0;
        //        return Double.Parse(value, CultureInfo.InvariantCulture);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //    }
        //    return 0;
        //}

        // Some pre-defined colors...
#if true
        // ... in RGB.
        readonly static Color TableBorder = new Color(81, 125, 192);
        readonly static Color TableBlue = new Color(235, 240, 249);
        readonly static Color TableGray = new Color(242, 242, 242);
#else
        // ... in CMYK.
        readonly static Color TableBorder = Color.FromCmyk(100, 50, 0, 30);
        readonly static Color TableBlue = Color.FromCmyk(0, 80, 50, 30);
        readonly static Color TableGray = Color.FromCmyk(30, 0, 0, 0, 100);
#endif
    }
}
