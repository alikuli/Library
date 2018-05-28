using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using System.Linq;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        public System.Web.Mvc.SelectList SelectList_ForParent(ICommonWithId entity)
        {

            var allItemsExceptThisOne = FindAll();

            if(!entity.IsNull())
                if(!entity.Name.IsNullOrWhiteSpace())
                    allItemsExceptThisOne = allItemsExceptThisOne.Where(x => x.Name.ToLower() != entity.Name.ToLower());

            return Dal.SelectList_Engine(allItemsExceptThisOne);
        }

        public override string SelectListCacheKey
        {
            get { return "ProductSelectListCache"; }
        }

    }
}
