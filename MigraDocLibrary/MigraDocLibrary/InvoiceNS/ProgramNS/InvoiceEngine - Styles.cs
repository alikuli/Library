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


        void DefineStyles()
        {
            var style = createNormalStyle();

            style = modify_HeaderStyle(style);
            style = modify_FooterStyle(style);
            style = addStyle_Table(style);
            style = addStyle_Title(style);
            style = addStyle_Reference(style);
        }

        private Style createNormalStyle()
        {
            // Get the predefined style Normal.
            var style = _document.Styles["Normal"];


            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Segoe UI";
            return style;
        }

        private Style modify_FooterStyle(Style style)
        {
            style = _document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);
            return style;
        }

        private Style modify_HeaderStyle(Style style)
        {
            style = _document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);
            return style;
        }

        private Style addStyle_Table(Style style)
        {
            style = _document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Segoe UI Semilight";
            style.Font.Size = 9;
            return style;
        }

        private Style addStyle_Title(Style style)
        {
            style = _document.Styles.AddStyle("Title", "Normal");
            style.Font.Name = "Segoe UI Semibold";
            style.Font.Size = 9;
            return style;
        }

        private Style addStyle_Reference(Style basedOnStyle)
        {
            basedOnStyle = _document.Styles.AddStyle("Reference", "Normal");
            basedOnStyle.ParagraphFormat.SpaceBefore = "5mm";
            basedOnStyle.ParagraphFormat.SpaceAfter = "5mm";
            basedOnStyle.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
            return basedOnStyle;
        }



    }
}
