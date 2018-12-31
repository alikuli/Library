using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using System.Linq;
namespace UowLibrary
{
    public partial class CountryBiz : BusinessLayer<Country>
    {

        public Country Pakistan
        {
            get
            {
                Country country = FindAll().Where(x => x.Name.ToLower().Contains("west pakistan")).FirstOrDefault(); ;
                return country;
            }

        }

        public string PakistanId
        {
            get
            {
                Pakistan.IsNullThrowException("Pakistan not found! Serious error. Programming error");
                return Pakistan.Id;
            }
        }
        public bool IsAddressInPakistan(string countryId)
        {
            return countryId == PakistanId;
        }


    }
}
