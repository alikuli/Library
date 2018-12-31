using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Web.Mvc;

namespace UowLibrary.EmailAddressNS
{
    public partial class EmailAddressBiz 
    {

        public override string SelectListCacheKey
        {
            get { return "EmailAddresssSelectListData"; }
        }


        public SelectList SelectListAddressVerificationEnum
        {
            get
            {
                return EnumExtention.ToSelectListSorted<VerificaionStatusENUM>(VerificaionStatusENUM.Unknown);
            }
        }
    }
}
