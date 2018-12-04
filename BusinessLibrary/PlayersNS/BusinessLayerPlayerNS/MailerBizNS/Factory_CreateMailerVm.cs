using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        public CreateMailerVM Factory_CreateMailerVm()
        {
            CreateMailerVM vm = new CreateMailerVM();
            vm.TrustLevelEnum = EnumLibrary.EnumNS.VerificationNS.TrustLevelENUM.Level1;
            return vm;
        }





    }
}
