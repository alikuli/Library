using AliKuli.Extentions;
using MigraDoc.DocumentObjectModel;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {



        private void summaryLetter_Body(Section mainSection, AddressVerificationModel_Header hdr)
        {
            _bodyTable = create_Table(mainSection);

            // Before you can add a row, you must define the columns.
            bodyTable_DefineColumns();

            var row = _bodyTable.AddRow();
            row.Borders.Visible = false;
            row.Format.Alignment = ParagraphAlignment.Left;

            row.Cells[0].AddParagraph();
            row.Cells[0].AddParagraph();

            Paragraph headingBodyPara = row.Cells[0].AddParagraph(hdr.Summary.Body);
            headingBodyPara.Format.Font.Size = "22pt";
            headingBodyPara.Format.Font.Bold = true;
            headingBodyPara.Format.Font.Underline = Underline.Single;
            headingBodyPara.Format.Alignment = ParagraphAlignment.Center;


            row.Cells[0].AddParagraph();

            row.Cells[0].AddParagraph("1) Type of Service Requested: " + hdr.MailServiceEnum.ToString().ToTitleSentance());
            row.Cells[0].AddParagraph("2) Service Domain: " + hdr.MailLocalOrForiegnEnum.ToString().ToTitleSentance());
            row.Cells[0].AddParagraph();
            row.Cells[0].AddParagraph("3) Batch Number: " + hdr.BatchNumber);
            row.Cells[0].AddParagraph("4) Mailer: " + hdr.MailerName);
            row.Cells[0].AddParagraph("5) User Name: " + hdr.UserName);

            row.Cells[0].AddParagraph();

            Paragraph numberOfLettersPara = row.Cells[0].AddParagraph("6) Number of letters: " + hdr.NumberOfLetters_Str);

            row.Cells[0].AddParagraph("7) Letter Ref Numbers: " + hdr.LetterRefNumbers);
            row.Cells[0].AddParagraph();

            Paragraph instructionHeading = row.Cells[0].AddParagraph("Instructions");
            instructionHeading.Format.Alignment = ParagraphAlignment.Center;
            instructionHeading.Format.Font.Bold = true;
            instructionHeading.Format.Font.Size = "14pt";


            Paragraph instructionPara = row.Cells[0].AddParagraph(hdr.Summary.Instructions);
            instructionPara.Format.Font.Italic = true;
            instructionPara.Format.Alignment = ParagraphAlignment.Center;
            instructionPara.Format.Borders.Visible = true;
            //instructionPara.Format.LeftIndent = "1cm";
            //instructionPara.Format.RightIndent = "1cm";
            //instructionPara.Format.SpaceAfter = "1cm";
            //instructionPara.Format.SpaceBefore = "1cm";


        }






    }
}
