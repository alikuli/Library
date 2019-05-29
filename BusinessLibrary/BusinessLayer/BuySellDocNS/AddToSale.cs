using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {


        public string AddToSale(string userId, string productChildId, string poNumber, DateTime poDate)
        {
            double totalThisItem = 0;
            userId.IsNullOrWhiteSpaceThrowException("No user");

            //who is the owner:The product Child owner is the owner

            //get the productChild
            productChildId.IsNullOrWhiteSpaceThrowException("No Product");
            ProductChild productChild = ProductChildBiz.Find(productChildId);
            productChild.IsNullThrowException("productChild");

            //get the product child's owner
            Owner ownerProductChild = productChild.Owner;
            ownerProductChild.IsNullThrowException("productChildOwner");

            //get the owner
            //Get the select list for owner
            Person ownerPerson = ownerProductChild.Person;
            ownerPerson.IsNullThrowException("ownerPerson");
            System.Web.Mvc.SelectList ownerSelectList = OwnerBiz.SelectListOnlyWith(ownerProductChild);


            ////the user is the customer user;
            //get the customer
            Customer customerUser = getCustomerFor(userId);
            //customerUser.Person.IsNullThrowException("No person for customer");
            //Person customerUserPerson = customerUser.Person;
            //Get the select list for Customer

            //remove the owner from the list... owner cannot sell to self.
            System.Web.Mvc.SelectList customerSelectList = CustomerBiz.SelectListWithout(ownerPerson);
            System.Web.Mvc.SelectList selectListOwner = OwnerBiz.SelectListOnlyWith(ownerProductChild);
            System.Web.Mvc.SelectList selectListCustomer = CustomerBiz.SelectListWithout(ownerPerson);

            //this is the address in the customer
            AddressMain addressInformTo = customerUser.DefaultInformToAddress;
            AddressMain addressShipTo = customerUser.DefaultShipAddress;

            string addressInformToId = "";
            string addressShipToId = "";

            if (!addressInformTo.IsNull())
            {
                addressInformToId = addressInformTo.Id;
            }

            if (!addressShipTo.IsNull())
            {
                addressShipToId = addressShipTo.Id;
            }

            //Get the select list for AddressInform
            System.Web.Mvc.SelectList selectListInformTo = AddressBiz.SelectListInformAddressCurrentUser();
            //Get the select list for AddressShipTo
            System.Web.Mvc.SelectList selectListShipTo = AddressBiz.SelectListShipAddressCurrentuser();



            BuySellDoc sale = getOpenSaleWithSameCustomerAndSeller(customerUser.Id, ownerProductChild.Id);
            //create the itemList.
            List<BuySellItem> buySellItems = new List<BuySellItem>();
            //check to see if there is any open sale which belongs to the same buyer and seller
            if (sale.IsNull())
            {
                //otherwise add a new sale
                sale = Factory() as BuySellDoc;

                sale.Initialize(
                    ownerProductChild.Id,
                    customerUser.Id,
                    addressInformToId,
                    addressShipToId,
                    poNumber,
                    poDate,
                    selectListOwner,
                    selectListCustomer,
                    selectListInformTo,
                    selectListShipTo);

                BuySellItem buySellItem = new BuySellItem(sale.Id, productChild.Id, 1, 0, productChild.Sell.SellPrice, productChild.FullName());
                sale.Add(buySellItem);
                CreateEntity(sale);
            }

            else
            {
                //now check to see if it is the same item... or is it a new item
                //get list of items for the sale
                List<BuySellItem> itemList = sale.BuySellItems.ToList();
                if (itemList.IsNullOrEmpty())
                {
                    BuySellItem buySellItem = new BuySellItem(sale.Id, productChild.Id, 1, 0, productChild.Sell.SellPrice, productChild.FullName());
                    sale.Add(buySellItem);
                    totalThisItem++;

                }
                else
                {
                    //there are items in the list
                    BuySellItem itemFound = itemList.FirstOrDefault(x => x.ProductChildId == productChild.Id);
                    if (itemFound.IsNull())
                    {
                        BuySellItem buySellItem = new BuySellItem(sale.Id, productChild.Id, 1, 0, productChild.Sell.SellPrice, productChild.FullName());
                        sale.Add(buySellItem);

                    }
                    else
                    {
                        itemFound.Quantity.Ordered += 1;
                        totalThisItem = itemFound.Quantity.Ordered;
                    }
                }
                Update(sale);

            }

            SaveChanges();
            string message = string.Format("Success. You ordered {0:N2} for {1:N2} (X{2:N2})", productChild.FullName(), productChild.Sell.SellPrice, totalThisItem);
            return message;

        }

        private BuySellDoc getOpenSaleWithSameCustomerAndSeller(string customerId, string ownerProductChildId)
        {
            customerId.IsNullThrowExceptionArgument("customerId");
            ownerProductChildId.IsNullThrowExceptionArgument("ownerProductChildId");

            BuySellDoc buysSellDoc = FindAll().FirstOrDefault(x => x.CustomerId == customerId &&
                x.OwnerId == ownerProductChildId && x.BuySellDocStateEnum == BuySellDocStateENUM.RequestUnconfirmed);
            return buysSellDoc;

        }



    }
}
