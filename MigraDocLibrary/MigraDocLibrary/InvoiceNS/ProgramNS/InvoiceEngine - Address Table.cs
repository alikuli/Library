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

        private void setup_AddressTable(Section mainSection, InvoicPdfParameter param)
        {
            //Create a table to hold the itemns
            _addressTable = create_Table(mainSection);

            // Before you can add a row, you must define the columns.
            addressTable_defineItemTableColumns();

            // Create the header of the table.
            var row = addressTable_Row1(param);

            //blank row
            row = _addressTable.AddRow();
            row.Borders.Visible = false;

            row = addressTable_Row2();
            //setTableEdge(_addressTable);

            row = _addressTable.AddRow();
            row.Borders.Visible = false;
        }

        private void addressTable_defineItemTableColumns()
        {
            var column = _addressTable.AddColumn("8.4cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _addressTable.AddColumn("0.22cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = _addressTable.AddColumn("8.4cm");
            column.Format.Alignment = ParagraphAlignment.Right;

        }
        private Row addressTable_Row1(InvoicPdfParameter param)
        {
            var row = _addressTable.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Color_Table_LtBlue;

            row.Cells[0].AddParagraph("Buyer:");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[0].Borders.Bottom.Visible = true;
            //row.Borders.r

            row.Cells[2].AddParagraph("Seller:");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[2].Borders.Bottom.Visible = true;

            row.Cells[1].Borders.Top.Clear();
            row.Cells[1].Borders.Bottom.Clear();
            row.Cells[1].Shading.Visible = false;

            //this is the row where the address will appear
            row = _addressTable.AddRow();
            row.Shading = null;

            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[0].Borders.Bottom.Visible = true;

            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[2].Borders.Bottom.Visible = true;


            row.Cells[1].Borders.Top.Clear();
            row.Cells[1].Borders.Bottom.Clear();
            row.Cells[1].Shading.Visible = false;

            return row;
        }

        private Row addressTable_Row1Address(InvoicPdfParameter param)
        {
            var row = _addressTable.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Color_Table_LtBlue;

            row.Cells[0].AddParagraph("Buyer:");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[0].Borders.Bottom.Visible = true;

            row.Cells[2].AddParagraph("Seller:");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[2].Borders.Bottom.Visible = true;

            row.Cells[1].Borders.Top.Clear();
            row.Cells[1].Borders.Bottom.Clear();
            row.Cells[1].Shading.Visible = false;



            //this is the row where the address will appear
            row = _addressTable.AddRow();
            row.Shading = null;

            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[0].Borders.Bottom.Visible = true;

            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[2].Borders.Bottom.Visible = true;


            row.Cells[1].Borders.Top.Clear();
            row.Cells[1].Borders.Bottom.Clear();
            row.Cells[1].Shading.Visible = false;

            return row;
        }

        private Row addressTable_Row2()
        {
            var row = _addressTable.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Color_Table_LtBlue;

            row.Cells[0].AddParagraph("Ship To:");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Top;

            row.Cells[2].AddParagraph("Inform To:");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Top;

            row.Cells[1].Borders.Top.Clear();
            row.Cells[1].Borders.Bottom.Clear();
            row.Cells[1].Shading.Visible = false;

            //this is the row where the address will appear
            row = _addressTable.AddRow();
            row.Shading = null;

            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[0].Borders.Bottom.Visible = true;

            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Top;
            row.Cells[2].Borders.Bottom.Visible = true;


            row.Cells[1].Borders.Top.Clear();
            row.Cells[1].Borders.Bottom.Clear();
            row.Cells[1].Shading.Visible = false;

            return row;
        }


    }
}
