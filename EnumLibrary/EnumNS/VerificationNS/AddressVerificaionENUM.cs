
namespace EnumLibrary.EnumNS
{
    public enum VerificaionStatusENUM
    {
        Unknown,        //No action taken
        NotVerified,    //Normal item. Not verified.
        Requested,      //User has requested verification
        SelectedForProcessing,     //The mailer has been allocated the verification
        Printed,        //The mailer has printed the verification
        Mailed,         //The mailer has mailed the verification
        Verified,       //The user has entered the verification address in due time
        Failed, //the user has been unable to verifiy the verification in due time.
    }
}
