
using System.Web.Mvc;
namespace UowLibrary.PlayersNS.MessageNS
{
    public partial class MessageBiz
    {

        public override string SelectListCacheKey
        {
            get { return "MessageSelectList"; }
        }


        //public SelectList GetProductsAccordingToLevel()
        //{

        //}
    }
}
