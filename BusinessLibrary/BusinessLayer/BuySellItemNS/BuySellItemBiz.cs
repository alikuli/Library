using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;
using System.Linq;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellItemBiz
            : BusinessLayer<BuySellItem>
    {
        //OwnerBiz _ownerBiz;
        //CustomerBiz _customerBiz;
        //ProductBiz _productBiz;
        //BuySellDocBiz _buySellBiz

        public BuySellItemBiz(IRepositry<BuySellItem> entityDal, BizParameters bizParameters /* , OwnerBiz ownerBiz, CustomerBiz customerBiz, ProductBiz productBiz */)
            : base(entityDal, bizParameters)
        {
            //_ownerBiz = ownerBiz;
            //_customerBiz = customerBiz;
            //_productBiz = productBiz;

        }



        public override string SelectListCacheKey
        {
            get { return "SelectListCacheBuySellItem"; }
        }


        //public ProductBiz ProductBiz
        //{
        //    get
        //    {
        //        _productBiz.IsNullThrowException("_productBiz");
        //        _productBiz.UserId = UserId;
        //        _productBiz.UserName = UserName;
        //        return _productBiz;
        //    }
        //}


        //public ProductChildBiz ProductChildBiz
        //{
        //    get
        //    {
        //        return ProductBiz.ProductChildBiz;
        //    }
        //}
        //public AddressBiz AddressBiz
        //{
        //    get
        //    {
        //        return OwnerBiz.AddressBiz;
        //    }
        //}

        //public CustomerBiz CustomerBiz
        //{
        //    get
        //    {
        //        _customerBiz.IsNullThrowException("_customerBiz");
        //        _customerBiz.UserId = UserId;
        //        _customerBiz.UserName = UserName;
        //        return _customerBiz;
        //    }
        //}

        //public OwnerBiz OwnerBiz
        //{
        //    get
        //    {
        //        _ownerBiz.IsNullThrowException("_ownerBiz");
        //        _ownerBiz.UserId = UserId;
        //        _ownerBiz.UserName = UserName;
        //        return _ownerBiz;
        //    }
        //}
        //public PersonBiz PersonBiz
        //{
        //    get
        //    {

        //        return OwnerBiz.PersonBiz;

        //    }
        //}

        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        return PersonBiz.UserBiz;
        //    }

        //}



        public override IQueryable<BuySellItem> GetDataToCheckDuplicateName(BuySellItem entity)
        {
            return base.GetDataToCheckDuplicateName(entity).Where(x => x.BuySellDocId == entity.BuySellDocId );
        }


        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);

            //during edit,the program tries to save the parent as well
            if(parm.Entity.IsEditing)
            {
                BuySellItem buySellItem = parm.Entity as BuySellItem;
                BuySellDoc buySellDoc = buySellItem.BuySellDoc;
                buySellDoc.IsNullThrowException();
                
             

            }
        }



    }


}


