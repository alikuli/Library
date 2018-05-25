
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsClassLibrary.ModelsNS.SharedNS;
namespace ModelsClassLibrary.ModelsNS.GeoLocationNS
{
    public class GeoLocationAbstract:CommonWithId
    {
        #region Properties
        #region Latitude
        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
        [Required]
        public string Latitude { get; set; }

        #endregion


        #region Longitude
        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
        [Required]
        public string Longitude { get; set; }

        #endregion
        public double Radius { get; set; }
        
        #endregion
        public void LoadFrom(GeoLocationAbstract g)
        {
            Longitude = g.Longitude;
            Latitude = g.Latitude;
            Radius = g.Radius;
        }

    }
}