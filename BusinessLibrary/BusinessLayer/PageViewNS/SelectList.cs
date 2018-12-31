using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary.PageViewNS
{
    public partial class PageViewBiz
    {

        public override string SelectListCacheKey
        {
            get { return "PageViewSelectList"; }
        }

    }
}
