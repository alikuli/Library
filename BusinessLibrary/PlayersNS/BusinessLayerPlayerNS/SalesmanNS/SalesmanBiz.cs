using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using UowLibrary.PlayersNS.SalesmanCategoryNS;
using AliKuli.Extentions;

namespace UowLibrary.PlayersNS.SalesmanNS
{
    public partial class SalesmanBiz : BusinessLayerPlayer<Salesman>
    {
        //readonly UserBiz _userBiz;
        readonly SalesmanCategoryBiz _salesmanCategoryBiz;
        //PersonBiz _personBiz;

        public SalesmanBiz(IRepositry<Salesman> entityDal, BizParameters bizParameters, SalesmanCategoryBiz salesmanCategoryBiz, AddressBiz addressBiz)
            : base(entityDal, bizParameters, addressBiz)
        {
            //_personBiz = personBiz;
            _salesmanCategoryBiz = salesmanCategoryBiz;
        }


        public SalesmanCategoryBiz SalesmanCategoryBiz 
        { 
            get 
            {
                _salesmanCategoryBiz.IsNullThrowException();
                _salesmanCategoryBiz.UserId = UserId;
                _salesmanCategoryBiz.UserName = UserName;

                return _salesmanCategoryBiz; 
            } 
        }

    }
}
