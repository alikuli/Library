using MigraDoc.DocumentObjectModel;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {

        private void defineStyles()
        {
            var style = normalStyle();

        }

        private Style normalStyle()
        {
            // Get the predefined style Normal.
            var style = _document.Styles["Normal"];


            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Segoe UI";
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


    }
}
