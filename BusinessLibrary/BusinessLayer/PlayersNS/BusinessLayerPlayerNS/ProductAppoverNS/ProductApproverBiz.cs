using AliKuli.Extentions;
using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using UowLibrary.PlayersNS.ProductApproverCategoryNS;

namespace UowLibrary.PlayersNS.ProductApproverNS
{
    public partial class ProductApproverBiz : BusinessLayerPlayer<ProductApprover>
    {

        readonly ProductApproverCategoryBiz _productApproverBiz;

        public ProductApproverBiz(IRepositry<ProductApprover> entityDal, BizParameters bizParameters, ProductApproverCategoryBiz ownerCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
        {

            _productApproverBiz = ownerCategoryBiz;
        }



        //public PersonBiz PersonBiz
        //{
        //    get
        //    {
                
        //        return CashTrxBiz.PersonBiz; ;

        //    }
        //}

        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        return PersonBiz.UserBiz;
        //    }

        //}
        public ProductApproverCategoryBiz ProductApproverCategoryBiz
        {
            get
            {
                _productApproverBiz.IsNullThrowException("_ownerCategoryBiz");
                _productApproverBiz.UserId = UserId;
                _productApproverBiz.UserName = UserName;
                return _productApproverBiz;
            }
        }
        public ProductApprover GetProductApproverForUser(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowException("userId");
            Person person = UserBiz.GetPersonFor(userId);
            person.IsNullThrowException("Person not found");
            string personId = person.Id;
            //now get the ProductApprover for this person
            ProductApprover productApprover = GetProductApproverForPerson(personId);
            return productApprover;

        }
        public bool IsApprover(string userId)
        {
            if (UserBiz.IsAdmin(userId))
                return true;

            return !GetProductApproverForUser(userId).IsNull();
        }
        public ProductApprover GetProductApproverForPerson(string personId)
        {
            personId.IsNullOrWhiteSpaceThrowArgumentException("personId");
            //now get the ProductApprover for this person
            ProductApprover productApprover = FindAll().FirstOrDefault(X => X.PersonId == personId);
            return productApprover;

        }

        public override System.Collections.Generic.IList<InterfacesLibrary.SharedNS.ICommonWithId> GetListForIndex()
        {

            IList<ICommonWithId> lstEntities = FindAll().ToList() as IList<ICommonWithId>;

            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;

        }
    }
}
