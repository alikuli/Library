using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.MessageNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace UowLibrary.PlayersNS.MessageNS
{
    public partial class MessageBiz : BusinessLayer<Message>
    {

        readonly PersonBiz _personBiz;
        readonly ProductBiz _productBiz;
        readonly OwnerBiz _ownerBiz;
        readonly PeopleMessageBiz _peopleMessageBiz;
        public MessageBiz(IRepositry<Message> entityDal, BizParameters bizParameters, PersonBiz personBiz, ProductBiz productBiz, OwnerBiz ownerBiz, PeopleMessageBiz peopleMessageBiz)
            : base(entityDal, bizParameters)
        {
            _personBiz = personBiz;
            _productBiz = productBiz;
            _ownerBiz = ownerBiz;
            _peopleMessageBiz = peopleMessageBiz;
        }

        #region Bizs


        PeopleMessageBiz peopleMessageBiz
        {
            get
            {
                _peopleMessageBiz.UserId = UserId;
                _peopleMessageBiz.UserName = UserName;
                return _peopleMessageBiz;
            }

        }
        OwnerBiz OwnerBiz
        {
            get
            {
                _ownerBiz.UserId = UserId;
                _ownerBiz.UserName = UserName;
                return _ownerBiz;
            }
        }
        protected ProductBiz ProductBiz
        {
            get
            {
                _productBiz.UserId = UserId;
                _productBiz.UserName = UserName;
                return _productBiz;
            }
        }

        protected ProductChildBiz ProductChildBiz
        {
            get
            {
                return _productBiz.ProductChildBiz;
            }
        }

        protected MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                return _productBiz.MenuPathMainBiz;
            }
        }

        protected MenuPath1Biz MenuPath1Biz
        {
            get
            {
                return _productBiz.MenuPath1Biz;
            }
        }

        protected MenuPath2Biz MenuPath2Biz
        {
            get
            {
                return _productBiz.MenuPath2Biz;
            }
        }

        protected MenuPath3Biz MenuPath3Biz
        {
            get
            {
                return _productBiz.MenuPath3Biz;
            }
        }


        public PersonBiz PersonBiz
        {
            get
            {
                _personBiz.UserId = UserId;
                _personBiz.UserName = UserName;
                return _personBiz;
            }
        }
        public UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }
        }
        #endregion


    }
}
