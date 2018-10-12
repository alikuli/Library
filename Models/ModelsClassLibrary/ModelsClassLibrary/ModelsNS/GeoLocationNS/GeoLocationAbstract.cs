
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.ModelsNS.GeoLocationNS
{
    public class GeoLocationAbstract
    {
        public GeoLocationAbstract()
        {
            Created = new DateAndByComplex();
            Modified = new DateAndByComplex();
        }

        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
        public string Latitude { get; set; }

        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
        public string Longitude { get; set; }

        public DateAndByComplex Created { get; set; }
        public DateAndByComplex Modified { get; set; }


    }
}