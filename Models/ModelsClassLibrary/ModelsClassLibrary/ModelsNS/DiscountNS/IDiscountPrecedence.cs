namespace ModelsClassLibrary.ModelsNS.DiscountNS
{
    public interface IDiscountPrecedence
    {
        int CompareTo(object obj);
        //void DecreaseRank();
        EnumLibrary.EnumNS.DiscountRuleENUM DiscountRuleEnum { get; set; }
        EnumLibrary.EnumNS.DiscountTypeENUM DiscountTypeEnum { get; set; }
        string FixNameFor(DiscountPrecedence discountPrecedence);
        //void IncreaseRank();
        string Input2SortString { get; }
        string Input3SortString { get; }
        int Rank { get; set; }
        void SelfErrorCheck();
        UserModels.ApplicationUser User { get; set; }
        string UserId { get; set; }
    }
}
