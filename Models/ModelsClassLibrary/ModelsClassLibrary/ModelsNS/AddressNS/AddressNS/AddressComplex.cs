using System.ComponentModel.DataAnnotations.Schema;
using ModelsClassLibrary.ModelsNS.SharedNS.Common;

namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    [ComplexType]
    public class AddressComplex : AddressStringWithNames
    {

        public AddressComplex()
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
            string email): base (houseNo, road, address2,townName,cityName, stateOrProvince, country, webAddress, zip, phone, attention,email)
        {
            
        }


    }
}