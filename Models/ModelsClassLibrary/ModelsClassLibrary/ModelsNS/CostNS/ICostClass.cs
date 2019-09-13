namespace ModelsClassLibrary.ModelsNS.CostNS
{
    public interface ICostClass
    {
        decimal Amount { get;  }
        string Heading { get; set; }
        string DetailOfInfo { get; set; }
        string GoAheadUrl { get; set; }
        string ReturnUrl { get; set; }
    }
}
