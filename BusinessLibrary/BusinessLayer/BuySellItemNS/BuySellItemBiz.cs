using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.MenuNS.MenuStateNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.BuySellDocNS
{
    /// <summary>
    /// The index of the buySellItem is used for Pickup
    /// </summary>
    /// 
    [TestClass]
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





        public override IQueryable<BuySellItem> GetDataToCheckDuplicateName(BuySellItem entity)
        {
            return base.GetDataToCheckDuplicateName(entity).Where(x => x.BuySellDocId == entity.BuySellDocId);
        }





        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            BuySellItem buySellItem = parm.Entity as BuySellItem;


            if (buySellItem.BuySellDoc.IsNull())
            {

            }
            //during edit,the program tries to save the parent as well
            if (parm.Entity.IsEditing)
            {
                BuySellDoc buySellDoc = buySellItem.BuySellDoc;
                buySellDoc.IsNullThrowException();
            }


        }

        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            //Item shipped amount cannot begreater than the order amount.
            //if it is, make them equal.
            BuySellItem buySellItem = parm.Entity as BuySellItem;
            buySellItem.IsNullThrowException();

            switch (buySellItem.BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Sale:              //--------  SALE
                    switch (buySellItem.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.InProccess:
                            break;

                        case BuySellDocStateENUM.BackOrdered:
                            break;

                        case BuySellDocStateENUM.All:
                            break;

                        case BuySellDocStateENUM.RequestUnconfirmed:
                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                            double order = getOrderAmount(buySellItem);

                            if (order <= buySellItem.Quantity.Order_Original)
                            {
                                buySellItem.Quantity.Order = order;
                            }
                            else
                            {
                                string errMsg = string.Format("Unable to update the amount. Your amount {0} is greater than the original amount. This is not allowed.",
                                   order.ToString("N2"),
                                    buySellItem.Quantity.Order_Original.ToString("N2"));

                                ErrorsGlobal.AddMessage(errMsg);
                            }
                            buySellItem.SalePrice = getSalePriceFor(buySellItem);
                            break;

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            break;

                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            break;

                        case BuySellDocStateENUM.CourierComingToPickUp:
                            break;

                        case BuySellDocStateENUM.PickedUp:
                            break;

                        case BuySellDocStateENUM.Delivered:
                            break;

                        case BuySellDocStateENUM.Rejected:
                            break;

                        case BuySellDocStateENUM.Problem:
                            break;

                        case BuySellDocStateENUM.Unknown:
                        default:
                            break;
                    }
                    break;

                case BuySellDocumentTypeENUM.Purchase:              //-------- PURCHASE
                    switch (buySellItem.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.InProccess:
                            break;

                        case BuySellDocStateENUM.BackOrdered:
                            break;

                        case BuySellDocStateENUM.All:
                            break;

                        case BuySellDocStateENUM.RequestUnconfirmed:
                            buySellItem.Quantity.Order = getOrderAmount(buySellItem);
                            buySellItem.SalePrice = getSalePriceFor(buySellItem);
                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                            break;

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            break;
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            break;


                        case BuySellDocStateENUM.CourierComingToPickUp:
                            break;

                        case BuySellDocStateENUM.PickedUp:
                            break;

                        case BuySellDocStateENUM.Delivered:
                            break;

                        case BuySellDocStateENUM.Rejected:
                            break;

                        case BuySellDocStateENUM.Problem:
                            break;

                        case BuySellDocStateENUM.Unknown:
                        default:
                            break;
                    }
                    break;

                case BuySellDocumentTypeENUM.Delivery:          //-------- DELIVERY
                    switch (buySellItem.BuySellDocStateEnum)
                    {
                        case BuySellDocStateENUM.InProccess:
                            break;

                        case BuySellDocStateENUM.BackOrdered:
                            break;

                        case BuySellDocStateENUM.All:
                            break;

                        case BuySellDocStateENUM.RequestUnconfirmed:
                            break;

                        case BuySellDocStateENUM.RequestConfirmed:
                            break;

                        case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                            break;

                        case BuySellDocStateENUM.ReadyForPickup:
                            break;
                        case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                            break;


                        case BuySellDocStateENUM.CourierComingToPickUp:
                            break;

                        case BuySellDocStateENUM.PickedUp:
                            break;

                        case BuySellDocStateENUM.Enroute:
                        case BuySellDocStateENUM.Delivered:
                            break;

                        case BuySellDocStateENUM.Rejected:
                            break;

                        case BuySellDocStateENUM.Problem:
                            break;

                        case BuySellDocStateENUM.Unknown:
                        default:
                            break;
                    } break;

                case BuySellDocumentTypeENUM.Unknown:
                default:
                    break;
            }

        }


        private decimal getSalePriceFor(BuySellItem buySellItem)
        {
            string salePriceStr = buySellItem.SalePriceStr;
            salePriceStr = salePriceStr.ToNumericString();
            decimal salePriceOut;
            bool success = decimal.TryParse(buySellItem.SalePriceStr, out salePriceOut);

            if (success)
                buySellItem.SalePrice = buySellItem.SalePriceStr.ToDecimal();
            else
                throw new Exception("Unable to understand number: " + buySellItem.SalePriceStr);

            if (buySellItem.SalePrice == 0)
                ErrorsGlobal.AddMessage("The sale price is zero");

            return salePriceOut;
        }

        public decimal GetSalePriceFor(BuySellItem buySellItem)
        {
            return getSalePriceFor(buySellItem);
        }





        private double getOrderAmount(BuySellItem buySellItem)
        {
            double orderOut;
            bool success = double.TryParse(buySellItem.Quantity.OrderStr, out orderOut);
            if (success)
            {

            }
            else
            {
                throw new Exception(string.Format("Order price '{0}' is not a number", buySellItem.Quantity.OrderStr));
            }
            return orderOut;
        }


        public override void Event_ModifyIndexList(ModelsClassLibrary.ViewModels.IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.IsImageTiled = true;
            IMenuManager mm = makeMenuManager(parameters);
            indexListVM.MenuManager = mm;


        }

        /// <summary>
        /// This is being used for Pickups.
        /// </summary>
        /// <param name="indexListVM"></param>
        /// <param name="indexItemVM"></param>
        /// <param name="icommonWithId"></param>
        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItemVM, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItemVM, icommonWithId);

            //conversions
            BuySellItem buySellItem = icommonWithId as BuySellItem;
            buySellItem.IsNullThrowException();
            buySellItem.ProductChild.IsNullThrowException();

            ProductChild productChild = buySellItem.ProductChild;
            productChild.IsNullThrowException();
            indexItemVM.Description = productChild.DetailInfoToDisplayOnWebsite;


            BuySellDoc buySellDoc = buySellItem.BuySellDoc;
            buySellDoc.IsNullThrowException();

            UploadedFile uf = productChild.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

            //product child has no image?
            if (uf.IsNull())
                uf = productChild.Product.MiscFiles.FirstOrDefault(x => !x.MetaData.IsDeleted);

            indexItemVM.MenuManager = new MenuManager(null, null, productChild, MenuENUM.IndexMenuProductChild, BreadCrumbManager, new LikeUnlikeParameters(0, 0, ""), UserId, indexListVM.MenuManager.ReturnUrl, UserName);

            //get the pictures list from the productChild
            List<string> pictureAddresses = GetCurrItemsPictureList(productChild);

            //if none are available get them from the product
            if (pictureAddresses.IsNullOrEmpty())
            {
                productChild.Product.IsNullThrowException();
                pictureAddresses = GetCurrItemsPictureList(productChild.Product);

            }

            indexItemVM.MenuManager.PictureAddresses = pictureAddresses;
            //indexItem.PictureViews = productChild.NoOfVisits.Amount;
            //indexItem.CompleteMenuPathViews = productChild.NoOfVisits.Amount;

            //indexItem.Price = productChild.Sell.SellPrice;

            AddEntryToIndex = buySellItem.BuySellDocStateEnum == BuySellDocStateENUM.ReadyForPickup;

            string countyName = buySellDoc.AddressShipToComplex.CountryName;
            string cityName = buySellDoc.AddressShipToComplex.CityName;
            string townName = buySellDoc.AddressShipToComplex.TownName;
            //add the price into the 2nd heading

            if (!countyName.IsNullOrWhiteSpace() && !cityName.IsNullOrWhiteSpace() && !townName.IsNullOrWhiteSpace())
            {
                indexItemVM.Amount2ndLine = string.Format("{0} {1}, {2}", townName, cityName, countyName);
            }
            else
            {
                if (!countyName.IsNullOrWhiteSpace() && !cityName.IsNullOrWhiteSpace() && townName.IsNullOrWhiteSpace())
                {
                    indexItemVM.Amount2ndLine = string.Format("{0}, {1}", cityName, countyName);
                }
                else
                {
                    throw new Exception("Address incomplete");
                }
            }

            indexItemVM.IsPickup = true;
            indexItemVM.ParentId = buySellDoc.Id;
            //buySellDoc.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Delivery;


        }

        //This supplies a dummy MenuPathMain for the Back to List in the Create.
        protected IMenuManager makeMenuManager(ControllerIndexParams parm)
        {
            MenuManager mm = new MenuManager(null, null, null, parm.MenuEnum, parm.BreadCrumbManager, parm.LikeUnlikeCounter, UserId, parm.ReturnUrl, UserName);
            mm.BreadCrumbManager = parm.BreadCrumbManager;
            return mm as IMenuManager;

        }



    }


}


