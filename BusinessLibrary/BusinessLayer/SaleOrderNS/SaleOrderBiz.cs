using AliKuli.Extentions;
using DalLibrary.DalNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.ProductNS;

namespace UowLibrary.BuySellDocNS
{
    public partial class SaleOrderBiz
            : BuySellDocBiz
    {
        OwnerBiz _ownerBiz;
        CustomerBiz _customerBiz;
        ProductBiz _productBiz;
        BuySellDocBiz _buySellDocBiz;
        public SaleOrderBiz(Repositry<BuySellDoc> entityDal, BizParameters bizParameters, OwnerBiz ownerBiz, CustomerBiz customerBiz, ProductBiz productBiz, BuySellDocBiz buySellDocBiz)
            : base(entityDal, bizParameters, ownerBiz, customerBiz, productBiz)
        {
            _ownerBiz = ownerBiz;
            _customerBiz = customerBiz;
            _productBiz = productBiz;
            buySellDocBiz = _buySellDocBiz;
        }


        public BuySellDocBiz BuySellDocBiz
        {
            get
            {
                _buySellDocBiz.UserId = UserId;
                _buySellDocBiz.UserName = UserName;
                return _buySellDocBiz;
            }
        }

        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parms)
        {
            Customer customer = CustomerBiz.GetEntityFor(UserId);
            customer.IsNullThrowException("Customer");

            IList<ICommonWithId> lst = await base.GetListForIndexAsync(parms);
            if (lst.IsNullOrEmpty())
                return null;

            IList<BuySellDoc> buySellDocLst = lst.Cast<BuySellDoc>().ToList();
            buySellDocLst.IsNullOrEmptyThrowException();
            IList<BuySellDoc> buySellDocLst_Filtered = buySellDocLst.Where(x => x.CustomerId == customer.Id).ToList();
            var lstIcommonwithId = buySellDocLst_Filtered.Cast<ICommonWithId>().ToList();

            return lstIcommonwithId;
            //now just get the sales orders
        }


        public List<BuySellDoc> SalesOrderFor(string userId)
        {
            Customer customer = CustomerBiz.GetEntityFor(UserId);
            customer.IsNullThrowException("Customer");

            List<BuySellDoc> salesOrderList = FindAll().Where(x => x.CustomerId == customer.Id).ToList();
            return salesOrderList;
        }

    }
}
