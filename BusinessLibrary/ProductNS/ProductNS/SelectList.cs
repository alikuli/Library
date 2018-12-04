using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        public SelectList SelectList_ForParent(ICommonWithId entity)
        {

            var allItemsExceptThisOne = FindAll();

            if (!entity.IsNull())
                if (!entity.Name.IsNullOrWhiteSpace())
                    allItemsExceptThisOne = allItemsExceptThisOne.Where(x => x.Name.ToLower() != entity.Name.ToLower());

            var s = Dal.SelectList_Engine(allItemsExceptThisOne);
            return s;
        }

        public override string SelectListCacheKey
        {
            get { return "ProductSelectListCache"; }
        }


        public SelectList SelectList_UomPurchaseQty()
        {
            return UomQuantityBiz.SelectList();
        }


        public SelectList SelectList_UomVolume()
        {
            return UomVolumeBiz.SelectList();
        }


        public SelectList SelectList_UomShipWeight()
        {
            return UomWeightBiz.SelectList();
        }


        public SelectList SelectList_UomWeight()
        {
            return UomWeightBiz.SelectList();
        }


        public SelectList SelectList_UomLength()
        {
            return UomLengthBiz.SelectList();
        }

        //public SelectList SelectList_AutomobileGearTypeEnum()
        //{
        //    AutomobileGearTypeENUM automobileGearTypeEnum = AutomobileGearTypeENUM.Unknown;
        //    return EnumExtention.ToSelectListSorted<AutomobileGearTypeENUM>(automobileGearTypeEnum);
        //}


        //public SelectList SelectList_FuelTypeEnum()
        //{
        //    FuelTypeENUM fuelEnum = FuelTypeENUM.Unknown;
        //    return EnumExtention.ToSelectListSorted<FuelTypeENUM>(fuelEnum);
        //}

        private SelectList SelectList_Children(Product p)
        {
            p.IsNullThrowException();
            var allItems = p.ProductChildren.AsQueryable();
            return ProductChildBiz.SelectList_Engine(allItems);
        }

        public SelectList GetProductChildSelectListFor(string productId)
        {
            productId.IsNullOrWhiteSpaceThrowArgumentException();
            Product p = Find(productId);
            var s = SelectList_Children(p);
            return s;

        }




    }
}
