using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrDistributionNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.BuySellDocNS;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.BankNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UserModels;
namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class CashTrxDistributionsController : EntityAbstractController<CashTrxDistribution>
    {

        CashTrxBiz _cashTrxsBiz;
        BankBiz _bankBiz;
        BuySellDocBiz _buySellDocBiz;
        public CashTrxDistributionsController(BankBiz bankBiz, AbstractControllerParameters param, BuySellDocBiz buySellDocBiz)
            : base(bankBiz.CashTrxBiz.CashTrxDistributionBiz, param)
        {
            _cashTrxsBiz = bankBiz.CashTrxBiz;
            _bankBiz = bankBiz;
            _buySellDocBiz = buySellDocBiz;

        }

        BankBiz BankBiz
        {
            get
            {
                _bankBiz.IsNullThrowException();
                _bankBiz.UserId = UserId;
                _bankBiz.UserName = UserName;

                return _bankBiz;
            }
        }

        PersonBiz PersonBiz
        {
            get
            {
                return _cashTrxsBiz.PersonBiz;
            }
        }

        UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }
        }

        CashTrxBiz CashTrxBiz
        {
            get
            {

                return _cashTrxsBiz;
            }
        }
        BuySellDocBiz BuySellDocBiz
        {
            get
            {
                _buySellDocBiz.IsNullThrowException();
                _buySellDocBiz.UserId = UserId;
                _buySellDocBiz.UserName = UserName;

                return _buySellDocBiz;
            }
        }

        CustomerBiz CustomerBiz
        {
            get
            {
                return BuySellDocBiz.CustomerBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                return BuySellDocBiz.OwnerBiz;
            }
        }

        DeliverymanBiz DeliverymanBiz
        {
            get
            {
                return BuySellDocBiz.DeliverymanBiz;
            }
        }

        CashTrxDistributionBiz CashTrxDistributionBiz
        {
            get
            {
                return CashTrxBiz.CashTrxDistributionBiz;
            }
        }

    }
}