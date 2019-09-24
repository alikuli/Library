using AliKuli.Extentions;
using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Configuration;
using UowLibrary.AddressNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerCategoryNS;
using UowLibrary.PlayersNS.PlayerAbstractCategoryNS;
using UserModels;
using System.Linq;
using ModelsClassLibrary.CashTrxNS;
using System.Collections.Generic;
namespace UowLibrary.PlayersNS.CustomerNS
{
    public partial class CustomerBiz : BusinessLayerPlayer<Customer>
    {


        readonly CustomerCategoryBiz _customerCategoryBiz;
        CashTrxBiz _cashTrxBiz;
        public CustomerBiz(IRepositry<Customer> entityDal, BizParameters bizParameters, CustomerCategoryBiz customerCategoryBiz, AddressBiz addressBiz, CashTrxBiz cashTrxBiz)
            : base(entityDal, bizParameters, addressBiz, cashTrxBiz)
        {

            _customerCategoryBiz = customerCategoryBiz;
            _cashTrxBiz = cashTrxBiz;
        }

        public CustomerCategoryBiz CustomerCategoryBiz
        {
            get
            {
                _customerCategoryBiz.IsNullThrowException("_customerCategoryBiz");
                _customerCategoryBiz.UserId = UserId;
                _customerCategoryBiz.UserName = UserName;
                return _customerCategoryBiz;
            }
        }



        //public CashTrxBiz CashTrxBiz
        //{
        //    get
        //    {
        //        _cashTrxBiz.IsNullThrowException("_cashTrxBiz");
        //        _cashTrxBiz.UserId = UserId;
        //        _cashTrxBiz.UserName = UserName;
        //        return _cashTrxBiz;
        //    }
        //}
        //public PersonBiz PersonBiz
        //{
        //    get
        //    {
        //        return CashTrxBiz.PersonBiz;
        //    }
        //}

        //public override Customer GetPlayerFor(string userId)
        //{
        //    Customer customer = base.GetPlayerFor(userId);

        //    if(customer.IsNull())
        //    {
        //        ApplicationUser user = GetUser(userId);
        //        user.IsNullThrowException("user");

        //        //create a new customer for this
        //        customer = Factory() as Customer;
        //        customer.IsNullThrowException();
        //        customer.Name = user.UserName;
        //    }

        //    return customer;
        //}

        


    }
}
