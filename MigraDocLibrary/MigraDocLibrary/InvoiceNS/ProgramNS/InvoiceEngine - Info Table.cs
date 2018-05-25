using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDocLibrary.InvoiceNS;

namespace InvoiceNS
{
    public partial class InvoiceEngine
    {




        private void infoTable_setup(Section mainSection, InvoicPdfParameter param)
        {
            //Create a table to hold the itemns
            _infoTable = create_Table(mainSection);

            // Before you can add a row, you must define the columns.
            infoTable_defineColumns();

            // Create the header of the table.
            var row = infoTable_Row1(param);

            //blank row
            //row = _infoTable.AddRow();
            //row.Borders.Visible = false;

            row = infoTable_Row2(row);
            //setTableEdge(_infoTable);

            row = _infoTable.AddRow();
            row.Borders.Visible = false;
        }

        private Row infoTable_Row2(Row row)
        {
            row = _infoTable.AddRow();
            row.Format.Alignment = ParagraphAlignment.Center;
            row.VerticalAlignment = VerticalAlignment.Bottom;


            return row;

        }

        private Row infoTable_Row1(InvoicPdfParameter param)
        {
            var row = _infoTable.AddRow();
            row.Format.Font.Bold = true;
            row.Shading.Color = Color_Table_LtBlue;
            row.Shading.Visible = true;

            row.Cells[0].AddParagraph("PO#:");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Top;

            row.Cells[1].AddParagraph("PO Date");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Top;

            row.Cells[2].AddParagraph("Ship Date");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Top;

            row.Cells[3].AddParagraph("Ship Wt.");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Top;

            row.Cells[4].AddParagraph("Carrier");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Top;

            row.Cells[5].AddParagraph();
            row.Cells[5].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Top;

            return row;
        }

        private void infoTable_defineColumns()
        {
            var column = _infoTable.AddColumn("2.836667cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = _infoTable.AddColumn("2.836667cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = _infoTable.AddColumn("2.836667cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = _infoTable.AddColumn("2.836667cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = _infoTable.AddColumn("2.836667cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = _infoTable.AddColumn("2.836667cm");
            column.Format.Alignment = ParagraphAlignment.Right;



        }



    }
}
