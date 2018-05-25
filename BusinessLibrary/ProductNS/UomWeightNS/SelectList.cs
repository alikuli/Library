using AliKuli.ConstantsNS;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UowLibrary
{
    public partial class UomWeightBiz : BusinessLayer<UomWeight>
    {

        public override string SelectListCacheKey
        {
            get { return "UomWeightSelectListData"; }
        }






    }
}
