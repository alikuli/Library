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


        private void headingTable_setup(Section section, IndexPdfParameter param)
        {
            //Create a table to hold the itemns
            _headingTable = section.Headers.Primary.AddTable();
            section.PageSetup.TopMargin = Unit.FromCentimeter(param.Logo.TopMarginAfterLogo());

            // Before you can add a row, you must define the columns.
            headingTable_defineColumns();
            //_headingTable.Borders.Visible = true;
            // Create the header of the table.
            _headingTable.RightPadding = 0;
            headingTable_Row1(param);
            headingTable_Row2();
            headingTable_Row3();
            //_headingTable.AddRow();
        }

        private void headingTable_Row1(IndexPdfParameter param)
        {
            var row = _headingTable.AddRow();
            row.Cells[0].MergeDown = 1;
            row.Cells[1].MergeDown = 1;
            row.Cells[1].Borders.Top.Visible = false;
            row.Cells[1].Borders.Right.Visible = false;
            row.Shading.Visible = false;
            row.Format.Font.Bold = true;
            row.VerticalAlignment = VerticalAlignment.Top;
            row.Cells[1].Format.Font.Size = "20pt";
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        }

        private void headingTable_Row2()
        {
            var row = _headingTable.AddRow();
            row.Cells[0].MergeDown = 1;
            row.Shading.Visible = false;
            row.Format.Font.Bold = true;
            row.VerticalAlignment = VerticalAlignment.Top;

        }
        
        private void headingTable_Row3()
        {
            var row = _headingTable.AddRow();
            var webAddress = row.Cells[0];
            webAddress.MergeRight = 1;
            webAddress.Format.Font.Size = "6pt";
            webAddress.Format.Font.Italic = true;

            var documentType = row.Cells[3];
            documentType.Format.Font.Underline = Underline.Single;

            row.Shading.Visible = false;
            row.Format.Font.Bold = false;

            row.Cells[3].Format.Font.Bold = true;
            row.VerticalAlignment = VerticalAlignment.Bottom;



        }

        private void headingTable_defineColumns()
        {
            //col 0
            var logoAddress = _headingTable.AddColumn("5cm");
            logoAddress.Format.Alignment = ParagraphAlignment.Left;

            //col 1
            var addressCol2 = _headingTable.AddColumn("5cm");
            addressCol2.Format.Alignment = ParagraphAlignment.Left;

            //col 2
            var blankCol = _headingTable.AddColumn("0.12cm");
            blankCol.Borders.Visible = false;
            blankCol.Shading.Visible = false;

            //col 3
            var docNoAndHeading = _headingTable.AddColumn("6.9cm");
            docNoAndHeading.Format.Alignment = ParagraphAlignment.Right;

        }



    }
}
