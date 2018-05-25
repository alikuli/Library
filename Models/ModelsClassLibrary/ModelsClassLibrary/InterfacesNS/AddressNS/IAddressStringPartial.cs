namespace InterfacesLibrary.AddressNS
{
    public interface IAddressStringPartial
    {
        string Address2 { get; set; }
        string Attention { get; set; }
        string HouseNo { get; set; }
        string Phone { get; set; }
        string Road { get; set; }
        string WebAddress { get; set; }
        string Zip { get; set; }


        void LoadFrom(IAddressStringPartial iaddressStringPartial);
    }
}
