using MigraDoc.DocumentObjectModel;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {



        private void summaryLetter_Infrastructure(AddressVerificationModel_Header header)
        {
            Section mainSection = _document.AddSection();
            mainSection.PageSetup = _document.DefaultPageSetup.Clone();

            summaryLetter_Logo(mainSection, header);
            summaryLetter_Body(mainSection, header);



        }





    }
}
