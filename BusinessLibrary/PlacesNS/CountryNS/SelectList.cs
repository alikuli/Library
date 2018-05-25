using ModelsClassLibrary.ModelsNS.PlacesNS;

namespace UowLibrary
{
    public partial class CountryBiz : BusinessLayer<Country>
    {

        public override string SelectListCacheKey
        {
            get { return "CountrySelectListData"; }
        }
    }
}
