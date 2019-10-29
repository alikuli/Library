using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    [ComplexType]
    public class AddressComplex : AddressStringWithNames
    {

        public AddressComplex()
        {

        }


        public AddressComplex(AddressComplex addressComplex)
            : this(addressComplex.HouseNo, addressComplex.Road, addressComplex.Address2, addressComplex.TownName, addressComplex.CityName, addressComplex.StateName, addressComplex.CountryName, addressComplex.WebAddress, addressComplex.Zip, addressComplex.Phone, addressComplex.Attention, addressComplex.Email)
        {

        }


        public AddressComplex(
            string houseNo,
            string road,
            string address2,
            string townName,
            string cityName,
            string stateOrProvince,
            string country,
            string webAddress,
            string zip,
            string phone,
            string attention,
            string email)
            : base(houseNo, road, address2, townName, cityName, stateOrProvince, country, webAddress, zip, phone, attention, email)
        {

        }

        public AddressComplex(AddressStringWithNames add):base(add.HouseNo,add.Road,add.Address2,add.TownName,add.CityName,add.StateName,add.CountryName,add.WebAddress,add.Zip,add.Phone,add.Attention,add.Email)
        {

        }


        public static AddressComplex SystemAddress_Complex()
        {
            AddressComplex addy = new AddressComplex(AddressStringWithNames.SystemAddress());
            return  addy;
        }

        //public static AddressStringWithNames ToAddressStringWithNames(AddressComplex addressComplex)
        //{

        //}
    }
}