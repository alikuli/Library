using ModelsClassLibrary.ModelsNS.SharedNS.Common;
namespace InterfacesLibrary.AddressNS
{
    public interface IAddressStringWithNames
    {
        string Address2 { get; set; }
        string Attention { get; set; }
        string HouseNo { get; set; }
        //string Phone { get; set; }
        string Road { get; set; }
        string WebAddress { get; set; }
        string Zip { get; set; }
        string Phone { get; set; }
        string TownName { get; set; }
        string StateName { get; set; }
        string CityName { get; set; }
        //string DistrictName { get; set; }
        string CountryName { get; set; }
        string Email { get; set; }


    }
}
