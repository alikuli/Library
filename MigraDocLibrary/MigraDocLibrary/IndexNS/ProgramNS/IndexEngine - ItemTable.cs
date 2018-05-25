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

        private void itemTable_setup(Section mainSection, IndexPdfParameter param)
        {
            //Create a table to hold the itemns
            _itemTable = create_Table(mainSection);

            // Before you can add a row, you must define the columns.
            itemTable_defineColumns(param);

            // Create the header of the table.
            itemTable_Title_Row1(param);
        }

        /// <summary>
        /// This is actually a part of the table.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private Row itemTable_Title_Row1(IndexPdfParameter param)
        {
            var row = _itemTable.AddRow();

            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Color_Table_LtBlue;
            row.Borders.Bottom.Width = Unit.FromPoint(1);

            var serialNo = row.Cells[0];
            var description = row.Cells[1];
            //var ordered = row.Cells[2];
            //var shipped = row.Cells[3];

            serialNo.AddParagraph("Sr#");
            serialNo.Format.Alignment = ParagraphAlignment.Center;
            serialNo.VerticalAlignment = VerticalAlignment.Center;
            serialNo.Format.Font.Bold = true;

            description.AddParagraph("Description");
            description.Format.Font.Bold = true;
            description.Format.Alignment = ParagraphAlignment.Left;



            return row;
        }



        private void itemTable_defineColumns(IndexPdfParameter param)
        {
            var serialCol = _itemTable.AddColumn("0.97cm");
            serialCol.Format.Alignment = ParagraphAlignment.Center;


            var description = _itemTable.AddColumn("16.05cm");
            description.Format.Alignment = ParagraphAlignment.Right;


        }

    }
}
