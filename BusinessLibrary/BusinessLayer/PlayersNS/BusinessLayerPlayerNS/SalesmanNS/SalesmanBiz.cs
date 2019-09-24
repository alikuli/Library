using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Linq;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using UowLibrary.PlayersNS.SalesmanCategoryNS;


namespace UowLibrary.PlayersNS.SalesmanNS
{
    public partial class SalesmanBiz : BusinessLayerPlayer<Salesman>
    {
        //readonly UserBiz _userBiz;
        readonly SalesmanCategoryBiz _salesmanCategoryBiz;
        //PersonBiz _personBiz;

        public SalesmanBiz(IRepositry<Salesman> entityDal, BizParameters bizParameters, SalesmanCategoryBiz salesmanCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
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


        public SelectList SelectListSuperSalesmen()
        {
            string salesmanCategory = SalesmanCategoryENUM.SuperSalesman.ToString().ToTitleSentance().ToLower();
            IQueryable<Salesman> data = FindAll().Where(x => x.SalesmanCategory.Name.ToLower() == salesmanCategory);
            SelectList selectList = SelectList_Engine(data);
            return selectList;
        }



    }
}
