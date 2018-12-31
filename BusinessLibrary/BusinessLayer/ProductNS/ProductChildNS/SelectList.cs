using InterfacesLibrary.SharedNS;
using System;

namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {

        public System.Web.Mvc.SelectList SelectList_ForParent(ICommonWithId entity)
        {
            throw new NotImplementedException();
            //var allItemsExceptThisOne = FindAll();

            //if(!entity.IsNull())
            //    if(!entity.Name.IsNullOrWhiteSpace())
            //        allItemsExceptThisOne = allItemsExceptThisOne.Where(x => x.Name.ToLower() != entity.Name.ToLower());

            //return Dal.SelectList_Engine(allItemsExceptThisOne);
        }

        public override string SelectListCacheKey
        {
            get { return "ProductChildSelectListCache"; }
        }

    }
}
