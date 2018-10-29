
namespace EnumLibrary.EnumNS.VerificationNS
{
    /// <summary>
    /// Every section that needs trust uses this in its own way.
    /// The Mailers use it in such a way that it decides how many mails a mailer is allowed
    /// </summary>
    public enum TrustLevelENUM
    {
        Unknown,
        Level1, //this is the very first trust level
        Level2,
        Level3,
        Level4,
        Level5,
        BlackListed,
    }
}
