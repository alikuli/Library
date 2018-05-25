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

        //private Paragraph setup_Footer(Section section, IndexListVM param)
        //{
        //    var paragraph = section.Footers.Primary.AddParagraph();
        //    section.PageSetup.BottomMargin = Unit.FromCentimeter(3);

        //    footerTable_DefineColums();

        //    paragraph.AddText(param.Addresses.WebCompany.ToStringTwoLine());
        //    paragraph.Format.Font.Size = 9;
        //    paragraph.Format.Alignment = ParagraphAlignment.Center;
        //    paragraph.Format.Borders.Top.Width = "1pt";
        //    return paragraph;
        //}

        private void setup_footerTable(Section section, IndexPdfParameter param)
        {
            //Create a table to hold the itemns
            section.PageSetup.BottomMargin = Unit.FromCentimeter(FOOTER_HEIGHT);

            _footerTable = section.Footers.Primary.AddTable();
            _footerTable.Borders.Visible = false;
            _footerTable.Borders.Top.Visible = true;
            _footerTable.Borders.Top.Width = Unit.FromPoint(1);
            _footerTable.RightPadding = 0;
            _footerTable.Format.Font.Size = Unit.FromPoint(8);

            // Before you can add a row, you must define the columns.
            footerTable_DefineColums();

            // Create the header of the table.
            footerTable_Row1(param);
        }
        private void footerTable_DefineColums()
        {
            //col 0
            var logoAddress = _footerTable.AddColumn("3cm");
            logoAddress.Format.Alignment = ParagraphAlignment.Left;

            //col 1
            var addressCol2 = _footerTable.AddColumn("11.02cm");
            addressCol2.Format.Alignment = ParagraphAlignment.Left;

            //col 2
            var blankCol = _footerTable.AddColumn("3cm");
            blankCol.Borders.Visible = false;
            blankCol.Shading.Visible = false;
        }

        private void footerTable_Row1(IndexPdfParameter param)
        {
            var row = _footerTable.AddRow();
            //row.Borders.Visible = true;
            row.Shading.Visible = false;
            row.VerticalAlignment = VerticalAlignment.Top;
            row.Borders.Top.Visible = true;

            var left = row.Cells[0];
            var middle = row.Cells[1];
            var right = row.Cells[2];

            left.Format.Alignment = ParagraphAlignment.Left;
            middle.Format.Alignment = ParagraphAlignment.Center;
            right.Format.Alignment = ParagraphAlignment.Right;
            //leftMostCell.AddParagraph();


        }

    }
}
