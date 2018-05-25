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
        private void itemTable_setup (Section mainSection, InvoicPdfParameter param)
        {
            //Create a table to hold the itemns
            _itemTable = create_Table(mainSection);

            // Before you can add a row, you must define the columns.
            itemTable_defineColumns(param);

            // Create the header of the table.
            itemTable_Title_Row1(param);
            itemTable_title_Row2(param);

            //setTableEdge(_itemTable);
        }

        /// <summary>
        /// This is actually a part of the table.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private Row itemTable_Title_Row1(InvoicPdfParameter param)
        {
            var row = _itemTable.AddRow();

            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Color_Table_LtBlue;

            var serialNo = row.Cells[0];
            var description = row.Cells[1];
            var ordered = row.Cells[2];
            var shipped = row.Cells[3];

            serialNo.AddParagraph("Sr#");
            serialNo.Format.Alignment = ParagraphAlignment.Center;
            serialNo.VerticalAlignment = VerticalAlignment.Center;
            serialNo.MergeDown = 1;

            description.AddParagraph("Description");
            description.Format.Font.Bold = true;
            description.Format.Alignment = ParagraphAlignment.Left;

            ordered.AddParagraph("Ordered");
            ordered.Format.Font.Bold = true;

            ordered.Format.Alignment = ParagraphAlignment.Right;

            shipped.AddParagraph("Shipped");
            shipped.Format.Alignment = ParagraphAlignment.Right;
            shipped.Format.Font.Bold = true;


            switch (param.DocumentInfo.DocType)
            {
                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
                    break;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
                    //the next 2 columns are not added.
                    return row;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
                    break;
                default:
                    break;
            }

            var price = row.Cells[4];
            var extended = row.Cells[5];

            price.AddParagraph("Price");
            price.Format.Font.Bold = true;
            price.Format.Alignment = ParagraphAlignment.Right;

            extended.AddParagraph("Extended Price");
            extended.Format.Alignment = ParagraphAlignment.Right;
            extended.VerticalAlignment = VerticalAlignment.Center;
            extended.MergeDown = 1;
            extended.Format.Font.Bold = true;

            serialNo.Borders.Bottom.Width = "1pt";
            extended.Borders.Bottom.Width = "1pt";

            return row;
        }

        private Row itemTable_title_Row2(InvoicPdfParameter param)
        {
            var row = _itemTable.AddRow();

            //row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Color_Table_LtBlue;

            var itemComment = row.Cells[1];
            itemComment.AddParagraph("---- Comment ----");
            itemComment.Format.Alignment = ParagraphAlignment.Center;
            row.Borders.Bottom.Width = "1pt";
            itemComment.MergeRight = 3;

            switch (param.DocumentInfo.DocType)
            {
                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
                    break;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
                    itemComment.MergeRight = 2;
                    return row;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
                    break;
                default:
                    break;
            }


            return row;
        }

        private void itemTable_defineColumns(InvoicPdfParameter param)
        {
            var serialCol = _itemTable.AddColumn("0.97cm");
            serialCol.Format.Alignment = ParagraphAlignment.Center;


            var description = _itemTable.AddColumn("4.62cm");
            description.Format.Alignment = ParagraphAlignment.Right;

            var ordered = _itemTable.AddColumn("2.54cm");
            ordered.Format.Alignment = ParagraphAlignment.Right;


            var shipped = _itemTable.AddColumn("2.54cm");
            shipped.Format.Alignment = ParagraphAlignment.Right;

            switch (param.DocumentInfo.DocType)
            {
                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
                    break;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
                    //the next 2 columns are not added.
                    return;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
                    break;
                default:
                    break;
            }

            var price = _itemTable.AddColumn("2.54cm");
            price.Format.Alignment = ParagraphAlignment.Center;

            var extended = _itemTable.AddColumn("3.81cm");
            extended.Format.Alignment = ParagraphAlignment.Right;
        }

    }
}
