using System;
using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.Models.AddressNS
{
    public interface IAddressComplex: IAddressCommon
    {
        string TownName { get; set; }
        
        string CityName{ get; set; }

        string StateName { get; set; }

        string CountryName { get; set; }


    }
}
