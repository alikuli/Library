using AliKuli.Extentions;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDocLibrary.IndexNS;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections.Generic;

namespace IndexNS
{
    public partial class IndexEngine 
    {

        public Document CreateNewDocument(IndexPdfParameter param)
        {

            _document = new Document();
            addDocumentInfo(param);
            DefineStyles();
            create_InvoiceInfraStructure(param);
            FillContent(param);

            return _document;

        }

        private void addDocumentInfo(IndexPdfParameter param)
        {
            if (!param.PdfHeaderInfo.Title.IsNullOrWhiteSpace())
                _document.Info.Title = param.PdfHeaderInfo.Title;

            if (!param.PdfHeaderInfo.Subject.IsNullOrWhiteSpace())
                _document.Info.Subject = param.PdfHeaderInfo.Subject;

            if (!param.PdfHeaderInfo.Author.IsNullOrWhiteSpace())
                _document.Info.Author = param.PdfHeaderInfo.Author;

        }

        public void create_InvoiceInfraStructure(IndexPdfParameter param)
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

            //setup_AddressTable(mainSection, param);

            //infoTable_setup(mainSection, param);

            itemTable_setup(mainSection, param);
            //setup_commentAndTotalsTable(mainSection, param);
        }

        private void setupHeaderTable(Section section, IndexListVM param)
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
