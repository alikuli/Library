using AliKuli.Extentions;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDocLibrary.IndexNS;
using ModelsClassLibrary.ViewModels;
using System;

namespace IndexNS
{
    public partial class IndexEngine
    {

        void FillContent(IndexPdfParameter param)
        {
            fillHeader(param);
            fillFooter(param);
            if (!param.DataSortedAndFiltered.IsNullOrEmpty())
            {
                int counter = 0;
                foreach (var item in param.DataSortedAndFiltered)
                {
                    counter += 1;
                    fillItemRow(counter, item, param);
                }
            }
            //fillTotalsUpToHere(param);
            //fillCommentEtc(param);
        }

        private void fillHeader(IndexPdfParameter param)
        {
            var headerRows = _headingTable.Rows;

            var header_Logo_Cell = headerRows[0].Cells[0];
            var header_CoAddy_Cell = headerRows[2].Cells[0];

            var header_Filter_Cell = headerRows[0].Cells[3];
            var header_Sorting_Cell = headerRows[1].Cells[3];
            var header_Title_Cell = headerRows[2].Cells[3];

            header_Logo_Cell.AddImage(param.Logo.Address);

            if (param.SearchString.IsNullOrWhiteSpace())
            {
                header_Filter_Cell.AddParagraph(string.Format("No Filter"));

            }
            else
            {
                header_Filter_Cell.AddParagraph(string.Format("Filtered By: '{0}'", param.SearchString));

            }

            header_CoAddy_Cell.AddParagraph(param.WebCompanyName);
            //header_CoAddy_Cell.AddParagraph(webCoAddy);
            Paragraph title = header_Title_Cell.AddParagraph(param.Headings.RecordName + " List.");
            title.Format.Font.Size = Unit.FromPoint(18);

            if (param.Headings.SortOrderDescription.IsNullOrWhiteSpace())
            {
                header_Filter_Cell.AddParagraph(string.Format("No sort Received"));

            }
            else
            {
                header_Filter_Cell.AddParagraph(string.Format("Sorted By: {0}", param.Headings.SortOrderDescription));

            }

            Paragraph sorting = header_Sorting_Cell.AddParagraph();
        }

        private void fillFooter(IndexPdfParameter param)
        {
            var footerRows = _footerTable.Rows;

            var left = footerRows[0].Cells[0];
            var middle = footerRows[0].Cells[1];
            var right = footerRows[0].Cells[2];



            Paragraph leftparagraph = left.AddParagraph();
            leftparagraph.AddText("Printed: " + DateTime.Now.ToLocalTime().ToString());

            //this prints the file name
            middle.AddParagraph(param.DownloadFileName + ".pdf");

            Paragraph paragraph = right.AddParagraph();
            paragraph.AddText("Page ");
            paragraph.AddPageField();
            paragraph.AddText(" of ");
            paragraph.AddNumPagesField();


        }

        private void fillItemRow(int counter, IndexItemVM item, IndexPdfParameter param)
        {
            var row1 = this._itemTable.AddRow();

            row1.TopPadding = 1.5;

            var serialNo = row1.Cells[0];
            var description = row1.Cells[1];

            row1.VerticalAlignment = VerticalAlignment.Center;


            if (counter % 2 == 0)
            {
                row1.Shading.Color = Color_Table_LtBlue;
            }

            serialNo.VerticalAlignment = VerticalAlignment.Center;
            description.Format.Alignment = ParagraphAlignment.Left;

            serialNo.AddParagraph(counter.ToString() + ".");
            description.AddParagraph(item.FullName);
        }

        //private void fillCommentEtc(IndexListVM param)
        //{
        //    var rows = _commentAndTotalsTable.Rows;

        //    var row1 = rows[0];
        //    var row2 = rows[1];
        //    var row3 = rows[2];
        //    var row4 = rows[3];
        //    var row5 = rows[4];


        //    var comment = row2.Cells[0];
        //    comment.AddParagraph(param.DocumentInfo.Comment);
        //    comment.Format.Font.Italic = true;
        //    comment.Format.Font.Color = Color_Red;
        //    comment.Format.Alignment = ParagraphAlignment.Left;
        //    comment.Format.Font.Bold = false;


        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var tax = row1.Cells[3];
        //    var shippingAndHandling = row2.Cells[3];
        //    var misc = row3.Cells[3];
        //    var advancePayment = row4.Cells[3];
        //    var grandTotal = row5.Cells[3];

        //    tax.AddParagraph(param.DocumentInfo.Tax.ToString("N2"));
        //    shippingAndHandling.AddParagraph(param.DocumentInfo.ShippingHandling.ToString("N2"));
        //    misc.AddParagraph(param.DocumentInfo.Misc.ToString("N2"));
        //    advancePayment.AddParagraph(param.DocumentInfo.AdvancePayment.ToString("N2"));
        //    grandTotal.AddParagraph(param.DocumentInfo.GrandTotal);

        //    misc.Format.Alignment = ParagraphAlignment.Right;
        //    misc.Format.Font.Bold = false;

        //    shippingAndHandling.Format.Alignment = ParagraphAlignment.Right;
        //    shippingAndHandling.Format.Font.Bold = false;

        //    tax.Format.Alignment = ParagraphAlignment.Right;
        //    tax.Format.Font.Bold = false;

        //    grandTotal.Format.Alignment = ParagraphAlignment.Right;
        //}

        //private void fillTotalsUpToHere(IndexListVM param)
        //{
        //    var blankRow1 = _itemTable.AddRow();
        //    blankRow1.Borders.Visible = false;
        //    blankRow1.HeightRule = RowHeightRule.Exactly;
        //    blankRow1.Height = 2;


        //    var totalsUpToHereRows = _itemTable.AddRow();

        //    var totalsUpToHereHeading = totalsUpToHereRows.Cells[0];
        //    var orderedTotals = totalsUpToHereRows.Cells[2];
        //    var shippedTotals = totalsUpToHereRows.Cells[3];

        //    var blankRow2 = _itemTable.AddRow();
        //    blankRow2.Borders.Visible = false;
        //    blankRow2.HeightRule = RowHeightRule.Exactly;
        //    blankRow2.Height = 2;

        //    totalsUpToHereRows.Format.Font.Bold = true;
        //    totalsUpToHereRows.Format.Alignment = ParagraphAlignment.Right;
        //    totalsUpToHereRows.Borders.Visible = true;
        //    totalsUpToHereRows.Shading.Color = Color_Table_LtBlue;
        //    totalsUpToHereRows.Borders.Width = "0.5pt";

        //    totalsUpToHereHeading.AddParagraph("Totals up to here");
        //    totalsUpToHereHeading.MergeRight = 1;

        //    orderedTotals.Shading.Visible = false;
        //    shippedTotals.Shading.Visible = false;


        //    orderedTotals.AddParagraph(param.DocumentInfo.TotalOrdered.ToString("N2"));
        //    shippedTotals.AddParagraph(param.DocumentInfo.TotalShipped.ToString("N2"));

        //    switch (param.DocumentInfo.DocType)
        //    {
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
        //            break;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
        //            return;

        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
        //            break;
        //        case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
        //            break;
        //        default:
        //            break;
        //    }

        //    var priceAvg = totalsUpToHereRows.Cells[4];
        //    var extendedTotal = totalsUpToHereRows.Cells[5];

        //    priceAvg.Shading.Visible = false;
        //    extendedTotal.Shading.Visible = false;

        //    priceAvg.AddParagraph(param.DocumentInfo.AvgPrice);
        //    extendedTotal.AddParagraph(param.DocumentInfo.TotalExtended.ToString("N2"));


        //}

        //private void fillDocInfo(IndexListVM param)
        //{
        //    var docInfoRow = _infoTable.Rows;
        //    var poNo = docInfoRow[1].Cells[0];
        //    var poDate = docInfoRow[1].Cells[1];
        //    var shipDate = docInfoRow[1].Cells[2];
        //    var shipWt = docInfoRow[1].Cells[3];
        //    var carrier = docInfoRow[1].Cells[4];

        //    poNo.AddParagraph(param.DocumentInfo.PurchaseOrderNumber);
        //    poDate.AddParagraph(param.DocumentInfo.PurchaseOrderDate);
        //    shipDate.AddParagraph(param.DocumentInfo.ShipDate);
        //    shipWt.AddParagraph(param.DocumentInfo.ShipWeight);
        //    carrier.AddParagraph(param.DocumentInfo.ShippingCarrier);



        //}

        //private void fillAddresses(IndexListVM param)
        //{
        //    var addyTableRows = _addressTable.Rows;

        //    var addyRow1 = addyTableRows[1];
        //    var addyRow2 = addyTableRows[4];

        //    var buyerBox = addyRow1.Cells[0];
        //    var sellerBox = addyRow1.Cells[2];
        //    var shipToBox = addyRow2.Cells[0];
        //    var informToBox = addyRow2.Cells[2];

        //    addToAddressBox(param, buyerBox, param.Addresses.Customer.ToString());
        //    addToAddressBox(param, sellerBox, param.Addresses.Seller.ToString());
        //    addToAddressBox(param, shipToBox, param.Addresses.ShipTo.ToString());
        //    addToAddressBox(param, informToBox, param.Addresses.InformTo.ToString());
        //}


        //private static void addToAddressBox(IndexListVM param, Cell buyerBox, string address)
        //{
        //    Paragraph p = buyerBox.AddParagraph(address);
        //    p.Format.Font.Bold = false;
        //    p.Format.LeftIndent = "0.5cm";
        //    p.Style = "Normal";
        //}

    }
}
