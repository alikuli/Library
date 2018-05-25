using System;
namespace InterfacesLibrary.GeoLocationNS
{
    public interface IGeoLocation
    {
        string Latitude { get; set; }
        string Longitude { get; set; }
        double Radius { get; set; }
    }
}
