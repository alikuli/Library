using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.PeopleMessageNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace UowLibrary.PlayersNS.MessageNS
{
    public partial class PeopleMessageBiz : BusinessLayer<PeopleMessage>
    {

        //readonly PersonBiz _personBiz;
        //readonly ProductBiz _productBiz;
        //readonly OwnerBiz _ownerBiz;

        public PeopleMessageBiz(IRepositry<PeopleMessage> entityDal, BizParameters bizParameters /*, PersonBiz personBiz, MessageBiz messageBix , ProductBiz productBiz, OwnerBiz ownerBiz */)
            : base(entityDal, bizParameters)
        {
            //_personBiz = personBiz;
            //_productBiz = productBiz;
            //_ownerBiz = ownerBiz;
        }

        #region Bizs

        //OwnerBiz OwnerBiz
        //{
        //    get
        //    {
        //        _ownerBiz.UserId = UserId;
        //        _ownerBiz.UserName = UserName;
        //        return _ownerBiz;
        //    }
        //}
        //protected ProductBiz ProductBiz
        //{
        //    get
        //    {
        //        _productBiz.UserId = UserId;
        //        _productBiz.UserName = UserName;
        //        return _productBiz;
        //    }
        //}

        //protected ProductChildBiz ProductChildBiz
        //{
        //    get
        //    {
        //        return _productBiz.ProductChildBiz;
        //    }
        //}

        //protected MenuPathMainBiz MenuPathMainBiz
        //{
        //    get
        //    {
        //        return _productBiz.MenuPathMainBiz;
        //    }
        //}

        //protected MenuPath1Biz MenuPath1Biz
        //{
        //    get
        //    {
        //        return _productBiz.MenuPath1Biz;
        //    }
        //}

        //protected MenuPath2Biz MenuPath2Biz
        //{
        //    get
        //    {
        //        return _productBiz.MenuPath2Biz;
        //    }
        //}

        //protected MenuPath3Biz MenuPath3Biz
        //{
        //    get
        //    {
        //        return _productBiz.MenuPath3Biz;
        //    }
        //}


        //public PersonBiz PersonBiz
        //{
        //    get
        //    {
        //        _personBiz.UserId = UserId;
        //        _personBiz.UserName = UserName;
        //        return _personBiz;
        //    }
        //}
        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        return PersonBiz.UserBiz;
        //    }
        //}
        #endregion


    }
}
