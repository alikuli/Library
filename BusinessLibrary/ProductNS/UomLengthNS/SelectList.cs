using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary
{
    public partial class UomLengthBiz : BusinessLayer<UomLength>
    {


        public override string SelectListCacheKey
        {
            get { return "UomLengthSelectListData"; }
        }




    }
}
