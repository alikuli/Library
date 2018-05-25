using ModelsClassLibrary.ModelsNS.SharedNS.CounterNS;

namespace UowLibrary.CounterNS
{
    public partial class CounterBiz : BusinessLayer<Counter>
    {

        public override string SelectListCacheKey
        {
            get { return "CounterSelectListData"; }
        }
    }
}
