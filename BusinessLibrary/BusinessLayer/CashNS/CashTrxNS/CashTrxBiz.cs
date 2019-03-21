using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;

namespace UowLibrary.CashTtxNS
{
    public partial class CashTrxBiz
            : BusinessLayer<CashTrx>
    {
        PersonBiz _personBiz;
        public CashTrxBiz(IRepositry<CashTrx> entityDal, BizParameters bizParameters, PersonBiz personBiz)
            : base(entityDal, bizParameters)
        {
            _personBiz = personBiz;
            _personBiz.UserId = UserId;
            _personBiz.UserName = UserName;
        }

        public PersonBiz PersonBiz
        {
            get
            {
                _personBiz.IsNullThrowException("PersonBiz");
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






    }
}
