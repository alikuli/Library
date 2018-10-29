using AliKuli.Extentions;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {



        private void footerLetter_Body(Section mainSection, AddressVerificationModel_Header hdr)
        {
            _bodyTable = create_Table(mainSection);

            // Before you can add a row, you must define the columns.
            bodyTable_DefineColumns();

            var row = _bodyTable.AddRow();
            row.Borders.Visible = false;
            row.Format.Alignment = ParagraphAlignment.Left;

            Cell endCell = row.Cells[0];

            endCell.AddParagraph();
            endCell.AddParagraph();

            Paragraph headingBodyPara = endCell.AddParagraph("END PAGE");
            headingBodyPara.Format.Font.Size = "22pt";
            headingBodyPara.Format.Font.Bold = true;
            headingBodyPara.Format.Font.Underline = Underline.Single;
            headingBodyPara.Format.Alignment = ParagraphAlignment.Center;



            endCell.AddParagraph();

            endCell.AddParagraph("1) Type of Service Requested: " + hdr.MailServiceEnum.ToString().ToTitleSentance());
            endCell.AddParagraph("2) Service Domain: " + hdr.MailLocalOrForiegnEnum.ToString().ToTitleSentance());
            endCell.AddParagraph();
            endCell.AddParagraph("3) Batch Number: " + hdr.BatchNumber);
            endCell.AddParagraph("4) Mailer: " + hdr.MailerName);
            endCell.AddParagraph("5) User Name: " + hdr.UserName);

            endCell.AddParagraph();

            Paragraph numberOfLettersPara = row.Cells[0].AddParagraph("6) Number of letters: " + hdr.NumberOfLetters_Str);

            endCell.AddParagraph("7) Letter Ref Numbers: " + hdr.LetterRefNumbers);
            endCell.AddParagraph();


            Paragraph instructionPara = row.Cells[0].AddParagraph(hdr.Summary.Instructions);
            instructionPara.Format.Font.Italic = true;
            instructionPara.Format.Alignment = ParagraphAlignment.Center;
            instructionPara.Format.Borders.Visible = true;

            endCell.AddParagraph();

            Paragraph endPara = endCell.AddParagraph("*** END - There are no more pages to print.***");
            endPara.Format.Font.Underline = Underline.Single;
            endPara.Format.Alignment = ParagraphAlignment.Center;
            endCell.AddParagraph();

        }






    }
}
