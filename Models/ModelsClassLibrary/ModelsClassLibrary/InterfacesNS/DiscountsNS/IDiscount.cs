using System;
using EnumLibrary.EnumNS;
using InterfacesLibrary.PlacesNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductNS;


namespace InterfacesLibrary.DiscountNS
{
    public interface IDiscount : ICommonWithId
    {
        decimal AmountDiscount { get; set; }
        City City { get; set; }
        Guid? CityId { get; set; }
        Country Country { get; set; }
        Guid? CountryId { get; set; }
        Customer Customer { get; set; }
        CustomerCategory CustomerCategory { get; set; }
        Guid? CustomerCategoryId { get; set; }
        Guid? CustomerId { get; set; }
        string DiscountKey { get;}
        DiscountRuleENUM DiscountRuleEnum { get; set; }
        Product Product { get; set; }
        ProductCategoryMain ProductCategoryMain { get; set; }
        Guid? ProductCategoryMainId { get; set; }
        Guid? ProductId { get; set; }
        State State { get; set; }
        Guid? StateId { get; set; }
        string ToString();
        Town Town { get; set; }
        Guid? TownId { get; set; }
        Vendor Vendor { get; set; }
        VendorCategory VendorCategory { get; set; }
        Guid? VendorId { get; set; }
    }
}
