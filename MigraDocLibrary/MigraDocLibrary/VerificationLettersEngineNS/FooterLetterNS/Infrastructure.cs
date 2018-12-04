using MigraDoc.DocumentObjectModel;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {



        private void footerLetter_Infrastructure(AddressVerificationModel_Header header)
        {
            Section mainSection = _document.AddSection();
            mainSection.PageSetup = _document.DefaultPageSetup.Clone();

            footerLetter_Logo(mainSection, header);
            footerLetter_Body(mainSection, header);



        }





    }
}
