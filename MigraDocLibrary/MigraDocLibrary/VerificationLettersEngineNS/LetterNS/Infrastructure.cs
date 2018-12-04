using MigraDoc.DocumentObjectModel;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {

        //private void addressTable_defineItemTableColumns_VerificationLetter()
        //{
        //    var column = _addressTable.AddColumn("6.4cm");
        //    column.Format.Alignment = ParagraphAlignment.Left;


        //}


        private void letter_Infrastructure(AddressVerificationModel letter, AddressVerificationModel_Header hdr)
        {
            // Each MigraDoc document needs at least one section.
            //var mainSection = Create_HeaderSection(param);

            Section mainSection = _document.AddSection();
            mainSection.PageSetup = _document.DefaultPageSetup.Clone();
            letter_Header(mainSection, letter, hdr);
            letter_SendToAddress(mainSection, letter);
            letter_Body(mainSection, letter, hdr);



        }


    }
}
