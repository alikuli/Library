using MigraDoc.DocumentObjectModel;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {

        private void letter_SendToAddress(Section mainSection, AddressVerificationModel param)
        {
            //Create a table to hold the itemns
            _addressTable = create_Table(mainSection);

            // Before you can add a row, you must define the columns.
            ToAddress_DefineColumns();

            // Create the header of the table.
            var row = _addressTable.AddRow();
            row.BottomPadding = Unit.FromCentimeter(0.5);
            row.TopPadding = Unit.FromCentimeter(0.5);
            row.Borders.Visible = true;
            row.Format.Alignment = ParagraphAlignment.Center;


            var addyHeadingPara = row.Cells[0].AddParagraph("Deliver To:");
            addyHeadingPara.Format.Font.Bold = true;
            addyHeadingPara.Format.Alignment = ParagraphAlignment.Left;
            addyHeadingPara.Format.Font.Size = "14pt";


            Paragraph addyPara = row.Cells[0].AddParagraph(param.AddressMailTo);
            addyPara.Format.Font.Bold = false;
            addyPara.Format.LeftIndent = "0.5cm";
            addyPara.Style = "Normal";
            addyPara.Format.Font.Size = "12pt";


            row = _addressTable.AddRow();
            row.Borders.Visible = false;

        }







    }
}
