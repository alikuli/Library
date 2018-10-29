using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {

        private void header_DefineColumns()
        {
            //col 0
            var logoAddress = _headingTable.AddColumn("9cm");
            logoAddress.Format.Alignment = ParagraphAlignment.Left;

            var docNoAndHeading = _headingTable.AddColumn("8.02cm");
            docNoAndHeading.Format.Alignment = ParagraphAlignment.Right;

        }

        private void headingTable_Row1()
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



        private void bodyTable_DefineColumns()
        {
            var column = _bodyTable.AddColumn("17.02cm");
            column.Format.Alignment = ParagraphAlignment.Left;

        }


        private void footerPage_DefineColumns()
        {
            //col 0
            var logoAddress = _footerTable.AddColumn("9cm");
            logoAddress.Format.Alignment = ParagraphAlignment.Left;

            var docNoAndHeading = _footerTable.AddColumn("8.02cm");
            docNoAndHeading.Format.Alignment = ParagraphAlignment.Right;

        }


        private void footerTable_Row1()
        {
            var row = _footerTable.AddRow();
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
        private void footerTable_Row2()
        {
            var row = _footerTable.AddRow();
            row.Cells[0].MergeDown = 1;
            row.Shading.Visible = false;
            row.Format.Font.Bold = true;
            row.VerticalAlignment = VerticalAlignment.Top;

        }


        private Table create_Table(Section mainSection)
        {
            Table t = mainSection.AddTable();
            t.Style = "Table";
            t.Borders.Color = Color_SeaBlue;
            t.Borders.Width = 0.25;
            t.Borders.Left.Width = 0.5;
            t.Borders.Right.Width = 0.5;
            t.Rows.LeftIndent = 0;
            return t;

        }











    }
}
