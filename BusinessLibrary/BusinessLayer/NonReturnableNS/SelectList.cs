using ModelsClassLibrary.ModelsNS.PeopleNS;

namespace UowLibrary.NonReturnableNS
{
    public partial class NonReturnableTrxBiz
    {

        public override string SelectListCacheKey
        {
            get { return "NonReturnableTrxsSelectListData"; }
        }
    }
}
