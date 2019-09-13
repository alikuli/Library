using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS
{
    public interface IPenaltyClass
    {
        decimal PenaltyAmount();
        decimal Percent { get; }
        string Text { get; }
        WhoPaysWhoENUM WhoPaysWhoEnum { get; }
    }
}
