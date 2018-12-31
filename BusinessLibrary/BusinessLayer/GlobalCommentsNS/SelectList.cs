using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;

namespace UowLibrary.GlobalCommentsNS
{
    public partial class GlobalCommentBiz : BusinessLayer<GlobalComment>
    {

        public override string SelectListCacheKey
        {
            get { return "GlobalCommentSelectListData"; }
        }
    }
}
