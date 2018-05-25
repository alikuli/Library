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
        public Document CreateNewDocument(InvoicPdfParameter param)
        {

            _document = new Document();
            addDocumentInfo(param);
            DefineStyles();
            create_InvoiceInfraStructure(param);
            FillContent(param);

            return _document;

        }

        private void addDocumentInfo(InvoicPdfParameter param)
        {
            if (!param.DocHeaderInfo.Title.IsNullOrWhiteSpace())
                _document.Info.Title = param.DocHeaderInfo.Title;

            if (!param.DocHeaderInfo.Subject.IsNullOrWhiteSpace())
                _document.Info.Subject = param.DocHeaderInfo.Subject;

            if (!param.DocHeaderInfo.Author.IsNullOrWhiteSpace())
                _document.Info.Author = param.DocHeaderInfo.Author;

        }

        public void create_InvoiceInfraStructure(InvoicPdfParameter param)
        {
            // Each MigraDoc document needs at least one section.
            //var mainSection = Create_HeaderSection(param);
            Section mainSection = _document.AddSection();
            mainSection.PageSetup = _document.DefaultPageSetup.Clone();

            headingTable_setup(mainSection, param);

            // Create the footer.
            setup_footerTable(mainSection, param);


            // We use an empty paragraph to move the first text line below the address field.
            var paragraph = mainSection.AddParagraph();

            paragraph.Format.LineSpacing = param.Logo.TopMarginAfterLogo();
            paragraph.Format.LineSpacingRule = LineSpacingRule.Exactly;

            setup_AddressTable(mainSection, param);

            infoTable_setup(mainSection, param);

            itemTable_setup(mainSection, param);
            setup_commentAndTotalsTable(mainSection, param);
        }

        private void setupHeaderTable(Section section, InvoicPdfParameter param)
        {

            _headingTable = section.Headers.Primary.AddTable();


            //var image = section.Headers.Primary.AddImage(param.Logo.Address ?? "");

            //image.Height = param.Logo.Height;

            //image.LockAspectRatio = true;
            //image.RelativeVertical = RelativeVertical.Line;
            //image.RelativeHorizontal = RelativeHorizontal.Margin;
            //image.Top = ShapePosition.Top;
            //image.Left = ShapePosition.Right;
            //image.WrapFormat.Style = WrapStyle.Through;
        }

    }
}
