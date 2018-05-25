//using AliKuli.Extentions;
//using MigraDoc.DocumentObjectModel;
//using MigraDoc.DocumentObjectModel.Tables;
//using MigraDocLibrary.InvoiceNS;
//using System;
//using System.Collections.Generic;

//namespace IndexNS
//{
//    public partial class IndexEngine 
//    {
//        private void setup_commentAndTotalsTable(Section mainSection, InvoicPdfParameter param)
//        {
//            //Create a table to hold the itemns
//            _commentAndTotalsTable = create_Table(mainSection);

//            // Before you can add a row, you must define the columns.
//            commentAndTotalsTable_defineColumns(param);

//            // Create the header of the table.
//            commentAndTotalsTable_Row1(param);
//            commentAndTotalsTable_Row2(param);
//            commentAndTotalsTable_Row3(param);
//            commentAndTotalsTable_Row4(param);
//            commentAndTotalsTable_Row5(param);

//            //setTableEdge(_commentAndTotalsTable);
//        }

//        private Row commentAndTotalsTable_Row1(InvoicPdfParameter param)
//        {
//            var row = _commentAndTotalsTable.AddRow();
//            row.Shading.Color = Color_Table_LtBlue;
//            row.Shading.Visible = true;
//            row.Format.Font.Bold = true;
//            row.Borders.Visible = true;

//            var comment = row.Cells[0];
//            comment.AddParagraph("Comment:");
//            comment.Format.Alignment = ParagraphAlignment.Center;
//            comment.VerticalAlignment = VerticalAlignment.Top;

//            row.Shading.Color = Color_Table_LtBlue;
//            row.Shading.Visible = true;
//            row.Cells[1].Shading.Visible = false;

//            //hide columns depending on document type
//            switch (param.DocumentInfo.DocType)
//            {
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
//                    comment.MergeRight = 1;
//                    //stops here.
//                    return row;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
//                    break;
//                default:
//                    break;
//            }

//            var misc = row.Cells[2];
//            misc.AddParagraph("Tax");
//            misc.Format.Alignment = ParagraphAlignment.Right;

//            var blankCol = row.Cells[1];
//            blankCol.Borders.Visible = false;

//            showAndHideColorInCommentsAndTotalsTable(row, param);

//            return row;
//        }

//        private void commentAndTotalsTable_Row2(InvoicPdfParameter param)
//        {
//            var row = _commentAndTotalsTable.AddRow();
//            row.Format.Font.Bold = true;
//            row.Borders.Visible = true;

//            var comment = row.Cells[0];
//            comment.MergeDown = 3;
//            comment.Shading.Visible = false;


//            //hide columns depending on document type
//            switch (param.DocumentInfo.DocType)
//            {
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
//                    comment.MergeRight = 1;
//                    //stops here.
//                    return;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
//                    break;
//                default:
//                    break;
//            }

//            var shippingAndHandling = row.Cells[2];
//            shippingAndHandling.AddParagraph("Shipping etc");
//            shippingAndHandling.Format.Alignment = ParagraphAlignment.Right;

//            var blankCol = row.Cells[1];
//            blankCol.Borders.Visible = false;

//            showAndHideColorInCommentsAndTotalsTable(row, param);

//        }

//        private void commentAndTotalsTable_Row3(InvoicPdfParameter param)
//        {
//            var row = _commentAndTotalsTable.AddRow();
//            row.Format.Font.Bold = true;

//            //hide columns depending on document type
//            switch (param.DocumentInfo.DocType)
//            {
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
//                    //stops here.
//                    return;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
//                    break;
//                default:
//                    break;
//            }

//            var misc = row.Cells[2];
//            misc.AddParagraph("Misc");
//            misc.Format.Alignment = ParagraphAlignment.Right;

//            showAndHideColorInCommentsAndTotalsTable(row, param);

//        }

//        private void commentAndTotalsTable_Row4(InvoicPdfParameter param)
//        {
//            var row = _commentAndTotalsTable.AddRow();
//            row.Format.Font.Bold = true;


//            //hide columns depending on document type
//            switch (param.DocumentInfo.DocType)
//            {
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
//                    //stops here.
//                    return;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
//                    break;
//                default:
//                    break;
//            }

//            var misc = row.Cells[2];
//            misc.AddParagraph("LESS: Paid");
//            misc.Format.Alignment = ParagraphAlignment.Right;
//            showAndHideColorInCommentsAndTotalsTable(row, param);

//        }

//        private void commentAndTotalsTable_Row5(InvoicPdfParameter param)
//        {
//            var row = _commentAndTotalsTable.AddRow();
//            row.Format.Font.Bold = true;

//            switch (param.DocumentInfo.DocType)
//            {
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
//                    //stops here
//                    return;

//                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
//                    break;
//                default:
//                    break;
//            }

//            var grandTotal = row.Cells[2];

//            switch (param.DocumentInfo.DocType)
//            {
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
//                    grandTotal.AddParagraph("Please Pay");
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
//                    grandTotal.AddParagraph("Please Pay");
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
//                    grandTotal.AddParagraph("Your Credit");
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
//                    grandTotal.AddParagraph("Due");
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
//                    throw new Exception("Code should not reach here. Programming error.");

//                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
//                    grandTotal.AddParagraph("Total Offer");
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
//                    grandTotal.AddParagraph("Total Request");
//                    break;
//                default:
//                    break;
//            }

//            grandTotal.Format.Alignment = ParagraphAlignment.Right;
//            showAndHideColorInCommentsAndTotalsTable(row, param);

//        }

//        private static void showAndHideColorInCommentsAndTotalsTable(Row row, InvoicPdfParameter param)
//        {
//            row.Shading.Color = Color_Table_LtBlue;
//            row.Shading.Visible = true;
//            row.Cells[1].Shading.Visible = false;

//            switch (param.DocumentInfo.DocType)
//            {
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
//                    //stops here
//                    return;

//                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
//                    break;
//                default:
//                    break;
//            }

//            row.Cells[3].Shading.Visible = false;


//        }

//        private void commentAndTotalsTable_defineColumns(InvoicPdfParameter param)
//        {
//            //col 0
//            var column = _commentAndTotalsTable.AddColumn("10.57cm");
//            column.Format.Alignment = ParagraphAlignment.Center;

//            //col 1
//            column = _commentAndTotalsTable.AddColumn("0.1cm");
//            column.Format.Alignment = ParagraphAlignment.Right;
//            column.Borders.Visible = false;
//            column.Shading.Visible = false;


//            switch (param.DocumentInfo.DocType)
//            {
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
//                    break;

//                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
//                    //the next 2 columns are not added.
//                    return;

//                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
//                    break;
//                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
//                    break;
//                default:
//                    break;
//            }
//            //col 2
//            column = _commentAndTotalsTable.AddColumn("2.54cm");
//            column.Format.Alignment = ParagraphAlignment.Center;

//            //col 3
//            column = _commentAndTotalsTable.AddColumn("3.81cm");
//            column.Format.Alignment = ParagraphAlignment.Right;

//        }

//    }
//}
