using AliKuli.Extentions;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDocLibrary.InvoiceNS;
using System;
using System.Collections.Generic;

namespace IndexNS
{
    public partial class IndexEngine 
    {




        /// <summary>
        /// The table of the MigraDoc document that contains the invoice items.
        /// </summary>
        Table _itemTable;
        //Table _addressTable;
        //Table _infoTable;
        //Table _commentAndTotalsTable;
        Table _headingTable;
        Table _footerTable;

        private Table create_Table(Section section)
        {
            // Create the item table.
            Table t = section.AddTable();
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
