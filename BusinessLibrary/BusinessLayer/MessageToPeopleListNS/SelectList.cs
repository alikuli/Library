
using System.Web.Mvc;
namespace UowLibrary.PlayersNS.MessageToPeopleListNS
{
    public partial class MessageToPeopleListBiz
    {
        public override string SelectListCacheKey
        {
            get { return "MessageToPeopleListSelectList"; }
        }
    }
}
