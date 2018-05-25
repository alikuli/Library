using AliKuli.Extentions;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDocLibrary.InvoiceNS;
using System;

namespace InvoiceNS
{
    public partial class InvoiceEngine
    {
        void FillContent(InvoicPdfParameter param)
        {
            _lineItems = param.DocumentInfo.LineItems;
            fillHeader(param);
            fillFooter(param);
            fillAddresses(param);
            fillDocInfo(param);

            if (_lineItems.IsNullOrEmpty())
            {
                var blankRow = this._itemTable.AddRow();
                blankRow.Borders.Visible = false;
            }
            else
            {
                int counter = 0;
                foreach (var item in _lineItems)
                {
                    counter += 1;
                    fillBothItemRows(counter, item, param);
                }
            }
            fillTotalsUpToHere(param);
            fillCommentEtc(param);
        }

        private void fillHeader(InvoicPdfParameter param)
        {
            var headerRows = _headingTable.Rows;

            var header_Logo_Cell = headerRows[0].Cells[0];
            var header_CoAddy_Cell = headerRows[2].Cells[0];

            var header_DocNo_Cell = headerRows[0].Cells[3];
            var header_DocDate_Cell = headerRows[1].Cells[3];
            var header_Title_Cell = headerRows[2].Cells[3];

            header_Logo_Cell.AddImage(param.Logo.Address);

            Paragraph docType = header_Title_Cell.AddParagraph(param.DocumentInfo.DocType.ToString().ToTitleSentance().ToUpper());
            docType.Format.Font.Size = "18pt";

            switch (param.DocumentInfo.DocType)
            {
                case EnumLibrary.EnumNS.DocumentTypeENUM.Unknown:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.PurchaseOrder:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Receiving:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
                    docType.Format.Font.Color = Color_Green;
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Invoice:
                    docType.Format.Font.Color = Color_NavyBlue;
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Credit:
                    docType.Format.Font.Color = Color_Red;
                    docType.AddText(" - DO NOT PAY");
                    
                    break;
                default:
                    break;
            }
            header_DocNo_Cell.AddParagraph(param.DocumentInfo.DocumentNoWithTitle);
            header_DocDate_Cell.AddParagraph(param.DocumentInfo.Date);

            string webCoAddy = param.Addresses.WebCompany.ToStringTwoLine();
            //header_CoAddy_Cell.AddParagraph(param.WebCompanyName);
            header_CoAddy_Cell.AddParagraph(webCoAddy);
        }

        private void fillFooter(InvoicPdfParameter param)
        {
            var footerRows = _footerTable.Rows;

            var left = footerRows[0].Cells[0];
            var middle = footerRows[0].Cells[1];

            var right = footerRows[0].Cells[2];

            //left.AddParagraph();
            //right

            Paragraph leftparagraph = left.AddParagraph();
            leftparagraph.AddText("Printed: " + DateTime.Now.ToLocalTime().ToString());

            middle.AddParagraph(param.Addresses.WebCompany.ToStringTwoLine());

            Paragraph paragraph = right.AddParagraph();
            paragraph.AddText("Page ");
            paragraph.AddPageField();
            paragraph.AddText(" of ");
            paragraph.AddNumPagesField();


        }

        private void fillBothItemRows(int counter, LineItem item, InvoicPdfParameter param)
        {
            var row1 = this._itemTable.AddRow();

            row1.TopPadding = 1.5;

            var serialNo = row1.Cells[0];
            var description = row1.Cells[1];
            var ordered = row1.Cells[2];
            var shipped = row1.Cells[3];

            row1.VerticalAlignment = VerticalAlignment.Center;

            //if there is no comment then do not show it.
            if (!item.Comment.IsNullOrWhiteSpace())
            {
                var row2 = this._itemTable.AddRow();
                row2.BottomPadding = 1.5;
                row2.Format.Alignment = ParagraphAlignment.Left;

                var comment = row2.Cells[1];
                comment.MergeRight = 3;

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
                        comment.MergeRight = 2;
                        break;

                    case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
                        break;
                    case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
                        break;
                    default:
                        break;
                }

                comment.AddParagraph(item.Comment);
                comment.Format.Font.Italic = true;
                comment.Format.Font.Size = "8pt";

                serialNo.MergeDown = 1;
                //row2.Borders.Bottom.Width = "0.5pt";
                row2.VerticalAlignment = VerticalAlignment.Center;

                if (counter % 2 == 0)
                {
                    row2.Shading.Color = Color_Table_LtBlue;
                }
            }

            if (counter % 2 == 0)
            {
                row1.Shading.Color = Color_Table_LtBlue;
            }

            serialNo.VerticalAlignment = VerticalAlignment.Center;
            description.Format.Alignment = ParagraphAlignment.Left;
            ordered.Format.Alignment = ParagraphAlignment.Right;
            shipped.Format.Alignment = ParagraphAlignment.Right;


            serialNo.AddParagraph(counter.ToString() + ".");
            description.AddParagraph(item.Description);
            ordered.AddParagraph(item.Ordered.ToString("N2"));
            shipped.AddParagraph(item.Shipped.ToString("N2"));


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
                    return;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
                    break;
                default:
                    break;
            }

            var price = row1.Cells[4];
            var extended = row1.Cells[5];

            //if there is a comment....
            if (!item.Comment.IsNullOrWhiteSpace())
            {
                extended.MergeDown = 1;
            }

            price.Format.Alignment = ParagraphAlignment.Right;
            extended.Format.Alignment = ParagraphAlignment.Right;

            price.AddParagraph(item.Price.ToString("N2"));
            extended.AddParagraph(item.Extended().ToString("N2"));
        }

        private void fillCommentEtc(InvoicPdfParameter param)
        {
            var rows = _commentAndTotalsTable.Rows;

            var row1 = rows[0];
            var row2 = rows[1];
            var row3 = rows[2];
            var row4 = rows[3];
            var row5 = rows[4];


            var comment = row2.Cells[0];
            comment.AddParagraph(param.DocumentInfo.Comment);
            comment.Format.Font.Italic = true;
            comment.Format.Font.Color = Color_Red;
            comment.Format.Alignment = ParagraphAlignment.Left;
            comment.Format.Font.Bold = false;


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
                    return;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
                    break;
                default:
                    break;
            }

            var tax = row1.Cells[3];
            var shippingAndHandling = row2.Cells[3];
            var misc = row3.Cells[3];
            var advancePayment = row4.Cells[3];
            var grandTotal = row5.Cells[3];

            tax.AddParagraph(param.DocumentInfo.Tax.ToString("N2"));
            shippingAndHandling.AddParagraph(param.DocumentInfo.ShippingHandling.ToString("N2"));
            misc.AddParagraph(param.DocumentInfo.Misc.ToString("N2"));
            advancePayment.AddParagraph(param.DocumentInfo.AdvancePayment.ToString("N2"));
            grandTotal.AddParagraph(param.DocumentInfo.GrandTotal);

            misc.Format.Alignment = ParagraphAlignment.Right;
            misc.Format.Font.Bold = false;

            shippingAndHandling.Format.Alignment = ParagraphAlignment.Right;
            shippingAndHandling.Format.Font.Bold = false;

            tax.Format.Alignment = ParagraphAlignment.Right;
            tax.Format.Font.Bold = false;

            grandTotal.Format.Alignment = ParagraphAlignment.Right;
        }

        private void fillTotalsUpToHere(InvoicPdfParameter param)
        {
            var blankRow1 = _itemTable.AddRow();
            blankRow1.Borders.Visible = false;
            blankRow1.HeightRule = RowHeightRule.Exactly;
            blankRow1.Height = 2;


            var totalsUpToHereRows = _itemTable.AddRow();

            var totalsUpToHereHeading = totalsUpToHereRows.Cells[0];
            var orderedTotals = totalsUpToHereRows.Cells[2];
            var shippedTotals = totalsUpToHereRows.Cells[3];

            var blankRow2 = _itemTable.AddRow();
            blankRow2.Borders.Visible = false;
            blankRow2.HeightRule = RowHeightRule.Exactly;
            blankRow2.Height = 2;

            totalsUpToHereRows.Format.Font.Bold = true;
            totalsUpToHereRows.Format.Alignment = ParagraphAlignment.Right;
            totalsUpToHereRows.Borders.Visible = true;
            totalsUpToHereRows.Shading.Color = Color_Table_LtBlue;
            totalsUpToHereRows.Borders.Width = "0.5pt";

            totalsUpToHereHeading.AddParagraph("Totals up to here");
            totalsUpToHereHeading.MergeRight = 1;

            orderedTotals.Shading.Visible = false;
            shippedTotals.Shading.Visible = false;


            orderedTotals.AddParagraph(param.DocumentInfo.TotalOrdered.ToString("N2"));
            shippedTotals.AddParagraph(param.DocumentInfo.TotalShipped.ToString("N2"));

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
                    return;

                case EnumLibrary.EnumNS.DocumentTypeENUM.Offer:
                    break;
                case EnumLibrary.EnumNS.DocumentTypeENUM.Request:
                    break;
                default:
                    break;
            }

            var priceAvg = totalsUpToHereRows.Cells[4];
            var extendedTotal = totalsUpToHereRows.Cells[5];

            priceAvg.Shading.Visible = false;
            extendedTotal.Shading.Visible = false;

            priceAvg.AddParagraph(param.DocumentInfo.AvgPrice);
            extendedTotal.AddParagraph(param.DocumentInfo.TotalExtended.ToString("N2"));


        }

        private void fillDocInfo(InvoicPdfParameter param)
        {
            var docInfoRow = _infoTable.Rows;
            var poNo = docInfoRow[1].Cells[0];
            var poDate = docInfoRow[1].Cells[1];
            var shipDate = docInfoRow[1].Cells[2];
            var shipWt = docInfoRow[1].Cells[3];
            var carrier = docInfoRow[1].Cells[4];

            poNo.AddParagraph(param.DocumentInfo.PurchaseOrderNumber);
            poDate.AddParagraph(param.DocumentInfo.PurchaseOrderDate);
            shipDate.AddParagraph(param.DocumentInfo.ShipDate);
            shipWt.AddParagraph(param.DocumentInfo.ShipWeight);
            carrier.AddParagraph(param.DocumentInfo.ShippingCarrier);



        }

        private void fillAddresses(InvoicPdfParameter param)
        {
            var addyTableRows = _addressTable.Rows;

            var addyRow1 = addyTableRows[1];
            var addyRow2 = addyTableRows[4];

            var buyerBox = addyRow1.Cells[0];
            var sellerBox = addyRow1.Cells[2];
            var shipToBox = addyRow2.Cells[0];
            var informToBox = addyRow2.Cells[2];

            addToAddressBox(param, buyerBox, param.Addresses.Customer.ToString());
            addToAddressBox(param, sellerBox, param.Addresses.Seller.ToString());
            addToAddressBox(param, shipToBox, param.Addresses.ShipTo.ToString());
            addToAddressBox(param, informToBox, param.Addresses.InformTo.ToString());
        }


        private static void addToAddressBox(InvoicPdfParameter param, Cell buyerBox, string address)
        {
            Paragraph p = buyerBox.AddParagraph(address);
            p.Format.Font.Bold = false;
            p.Format.LeftIndent = "0.5cm";
            p.Style = "Normal";
        }

    }
}
