
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.CashNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ShopNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UowLibrary.SuperLayerNS
{
    public partial class SuperBiz
    {

        public ShopVM Setup_Shop_For_Edit_Get(string returnUrl, string productId)
        {
            productId.IsNullOrWhiteSpaceThrowException("productId");
            returnUrl.IsNullOrWhiteSpaceThrowException("returnUrl");

            //locate the shop
            Product product = ProductBiz.Find(productId);
            product.IsNullThrowException("product");

            //now we have the shop...
            MenuPathMain menuPathMain = product.MenuPathMains_Fixed.FirstOrDefault();
            menuPathMain.IsNullThrowException("menuPathMain");
            string menuPathMainId = menuPathMain.Id;
            menuPathMainId.IsNullOrWhiteSpaceThrowException("menuPathMainId");


            List<string> shopNames = getShopNamesForCurrUser();
            int noOfMonths = 1;
            decimal ratePerMonth = getShopRatePerMonth();
            decimal totalAmount = ratePerMonth * noOfMonths;

            CashDistributionEngine cashDistributionEnginge = get_CashDistributionEngineAndCheckBalance(totalAmount, true);

            string explaintion = string.Format("{0}. All your products will be collected and will be shown in your shop. Note, every shop must have a unique name and will be created in its own area. This shop will be created in: ", cashDistributionEnginge.Message);

            AddressStringWithNames customerAddress = getDefaultCustomerAddress();

            ShopVM shopCreate = new ShopVM(
                product.Id,
                product.Name,
                explaintion,
                noOfMonths,
                ratePerMonth,
                returnUrl,
                shopNames,
                menuPathMain,
                customerAddress,
                product.MiscFiles_Fixed);

            shopCreate.Id = product.Id;

            return shopCreate;


        }
        public ShopVM Setup_To_Create_Shop_Get(string returnUrl, string menuPathMainId)
        {
            menuPathMainId.IsNullOrWhiteSpaceThrowException("No Menu Path");
            UserId.IsNullOrWhiteSpaceThrowException("You must be logged in to continue.");
            decimal ratePerMonth = getShopRatePerMonth();
            int noOfMonths = 1;
            decimal totalAmount = ratePerMonth * noOfMonths;

            AddressStringWithNames customerAddress = getDefaultCustomerAddress();
            MenuPathMain mpm = MenuPathMainBiz.Find(menuPathMainId);
            mpm.IsNullThrowException("Menu Path not found");

            CashDistributionEngine cashDistributionEnginge = get_CashDistributionEngineAndCheckBalance(totalAmount, true);

            string explaintion = string.Format("{0}. All your products will be collected and will be shown in your shop. Note, every shop must have a unique name and will be created in its own area. This shop will be created in: ", cashDistributionEnginge.Message);



            List<string> shopNames = getShopNamesForCurrUser();
            string shopName = getAUniqueNameForShop();

            ShopVM shopCreate = new ShopVM("", shopName, explaintion, noOfMonths, ratePerMonth, returnUrl, shopNames, mpm, customerAddress);
            return shopCreate;
        }

        private static decimal getShopRatePerMonth()
        {
            decimal ratePerMonth = MenuPathMain.Payment_To_Buy_Shop();
            return ratePerMonth;
        }

        private AddressStringWithNames getDefaultCustomerAddress()
        {
            AddressStringWithNames customerAddress = null;
            Customer customerUser = getCustomerOfUser();
            if (!customerUser.DefaultBillAddress.IsNull())
            {
                customerAddress = customerUser.DefaultBillAddress.ToAddressComplex();
            }

            return customerAddress;
        }
        public async Task<ShopCreatedSuccessfullyVM> Setup_To_Edit_Shop_Post_Async(ShopVM shopVm, string button, HttpPostedFileBase[] httpMiscUploadedFiles)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            shopVm.IsNullThrowExceptionArgument("Shop");
            button.IsNullOrWhiteSpaceThrowArgumentException("button");
            Owner userOwner = OwnerBiz.GetPlayerFor(UserId);
            userOwner.IsNullThrowException("You are not authourized to create a shop. Please become a seller first.");
            if (button == "accept")
            {
                //locate the shop

                if (shopVm.NoOfMonths == 0)
                {
                    throw new Exception("No of months is zero");
                }
                decimal salePrice = MenuPathMain.Payment_To_Buy_Shop();
                int quantity = shopVm.NoOfMonths;

                //see if User has the money to do what they want.
                decimal paymentAmount = salePrice * quantity;

                bool isNonRefundablePaymentAllowed = true;
                CashDistributionEngine cde = get_CashDistributionEngineAndCheckBalance(paymentAmount, isNonRefundablePaymentAllowed);

                //get the shop Child Product
                ProductChild shopBuySellItem = ProductChildBiz.FindForName(ProductChild.GetShopName());
                shopBuySellItem.IsNullThrowException("Shop not found");

                Owner ownerOfProductChild = shopBuySellItem.Owner;
                ownerOfProductChild.IsNullThrowException("No Product Child Owner");

                Customer customerUser = getCustomerOfUser();

                Product product = ProductBiz.Find(shopVm.Id);
                product.IsNullThrowException("product");
                string shopId = product.Id;
                updateTheProductFromTheShopVm(product, shopVm);

                SelectList selectListOwner = null;
                SelectList selectListCustomer = null;
                SelectList selectListBillTo = null;
                SelectList selectListShipTo = null;
                string addressBillToId = "";
                string addressShipToId = "";
                DateTime expectedDeliveryDate = DateTime.Now;
                DateTime guaranteePeriodEnds = DateTime.Now;


                BuySellDoc sale = CreateSale(
                    shopBuySellItem,
                    ownerOfProductChild,
                    quantity,
                    salePrice,
                    customerUser,
                    selectListOwner,
                    selectListCustomer,
                    addressBillToId,
                    addressShipToId,
                    selectListBillTo,
                    selectListShipTo,
                    BuySellDocStateENUM.RequestUnconfirmed,
                    BuySellDocStateModifierENUM.Accept,
                    expectedDeliveryDate,
                    guaranteePeriodEnds,
                    shopId);

                product.BuySellDocs.Add(sale);

                ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
                    product as ICommonWithId,
                    httpMiscUploadedFiles,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    MenuENUM.EditDefault,
                    UserName,
                    UserId,
                    shopVm.ReturnUrl,
                    null, //globalObject not required for products
                    button);

                await ProductBiz.UpdateAndSaveAsync(parm);


                MenuPathMain mpm = product.MenuPathMains_Fixed.FirstOrDefault();

                decimal refundable_Spent = cde.Refundable_Final;
                decimal nonRefundable_spent = cde.NonRefundable_Final;
                string shopName = product.FullName();
                string mp1Name = mpm.MenuPath1.FullName();
                string mp2Name = mpm.MenuPath2.FullName();
                string mp3Name = mpm.MenuPath3.FullName();
                int numberOfMonths = quantity;
                DateTime expiryDate = product.ShopExpiryDate.Date_NotNull_Min;

                ShopCreatedSuccessfullyVM shopCreatedSuccessfully = new ShopCreatedSuccessfullyVM(
                    refundable_Spent,
                    nonRefundable_spent,
                    shopName,
                    mp1Name,
                    mp2Name,
                    mp3Name,
                    numberOfMonths,
                    expiryDate,
                    shopVm.ReturnUrl);

                return shopCreatedSuccessfully;
            }

            return null;
        }

        private void updateTheProductFromTheShopVm(Product product, ShopVM shopVm)
        {
            //update the name
            product.Name = shopVm.ShopName;
            UpdateExpiryDate(product, shopVm);



        }

        private void UpdateExpiryDate(Product product, ShopVM shopVm)
        {
            //if the date is null
            if (product.ShopExpiryDate.Date_NotNull_Min == DateTime.MinValue)
            {
                product.ShopExpiryDate.Date = DateTime.Now;
            }
            else
            {
                DateParameter dp = new DateParameter();
                //if the date expired before today
                if (dp.Date1AfterDate2(DateTime.Now, product.ShopExpiryDate.Date_NotNull_Min))
                {
                    product.ShopExpiryDate.Date = DateTime.Now;
                }
                //else the date is today or after today. In either case add the month to that date.
            }

            int noOfMonths = shopVm.NoOfMonths;
            DateTime newDate = product.ShopExpiryDate.Date_NotNull_Min.AddMonths(noOfMonths);
            product.ShopExpiryDate.SetDateTo(UserName, UserId, newDate);
        }

        private static decimal getTotalPayment(ShopVM shopCreate)
        {
            if (shopCreate.NoOfMonths == 0)
            {
                throw new Exception("No of months is zero");
            }

            //see if User has the money to do what they want.
            decimal paymentAmount = MenuPathMain.Payment_To_Buy_Shop() * shopCreate.NoOfMonths;
            return paymentAmount;
        }
        public async Task<ShopCreatedSuccessfullyVM> Setup_To_Create_Shop_Post_Async(ShopVM shopCreate, string button, HttpPostedFileBase[] httpMiscUploadedFiles)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            shopCreate.MenuPathMainId.IsNullOrWhiteSpaceThrowException("Main Menu Path not received");
            Owner userOwner = OwnerBiz.GetPlayerFor(UserId);
            userOwner.IsNullThrowException("You are not authourized to create a shop. Please become a seller first.");

            if (button == "accept")
            {
                //CashDistributionEngine cde;
                //int quantity;
                //Product product;
                //MenuPathMain mpm;
                //ControllerCreateEditParameter parm = setup_Shop_Into_ControllerCreateEditParameter(shopCreate, button, httpMiscUploadedFiles, userOwner, out cde, out quantity, out product, out mpm, out parm);

                decimal paymentAmount = MenuPathMain.Payment_To_Buy_Shop();
                decimal commissionPct = BuySellDoc.Get_Maximum_Commission_Product_Percent();
                bool isNonRefundablePaymentAllowed = true;
                CashDistributionEngine cde = get_CashDistributionEngineAndCheckBalance(paymentAmount, isNonRefundablePaymentAllowed);

                //check the name, if it is already used, throw error and return to create screen
                if (isShopNameExists(shopCreate.ShopName))
                {
                    string err = string.Format("The shop name '{0}' already exists... sorry. Try again.", shopCreate.ShopName);
                    throw new Exception(err);
                }
                //create the shop


                int quantity = shopCreate.NoOfMonths;
                if (quantity == 0)
                    throw new Exception("The quantity is zero. This is not allowed");

                Customer customerUser = getCustomerOfUser();
                SelectList selectListOwner = null;
                SelectList selectListCustomer = null;
                SelectList selectListBillTo = null;
                SelectList selectListShipTo = null;
                string addressBillToId = "";
                string addressShipToId = "";
                decimal salePrice = MenuPathMain.Payment_To_Buy_Shop();
                DateTime expectedDeliveryDate = DateTime.Now;
                DateTime guaranteePeriodEnds = DateTime.Now;

                //MenuPathMain mpm = MenuPathMainBiz.Find(shopCreate.MenuPathMainId);
                //mpm.IsNullThrowException("Menu Path Main is null");

                MenuPathMain mpm = getMainMenuPath(shopCreate);
                Product product = setup_Product_For_Shop(shopCreate, userOwner);

                //get the shop Child Product
                ProductChild shopBuySellItem = ProductChildBiz.FindForName(ProductChild.GetShopName());
                shopBuySellItem.IsNullThrowException("Shop not found");

                string shopId = product.Id;
                Owner ownerOfProductChild = shopBuySellItem.Owner;
                ownerOfProductChild.IsNullThrowException("No Product Child Owner");

                BuySellDoc sale = CreateSale(
                    shopBuySellItem,
                    ownerOfProductChild,
                    quantity,
                    salePrice,
                    customerUser,
                    selectListOwner,
                    selectListCustomer,
                    addressBillToId,
                    addressShipToId,
                    selectListBillTo,
                    selectListShipTo,
                    BuySellDocStateENUM.RequestUnconfirmed,
                    BuySellDocStateModifierENUM.Accept,
                    expectedDeliveryDate,
                    guaranteePeriodEnds,
                    shopId);

                product.BuySellDocs.Add(sale);

                ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
                        product as ICommonWithId,
                        httpMiscUploadedFiles,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        MenuENUM.EditDefault,
                        UserName,
                        UserId,
                        shopCreate.ReturnUrl,
                        GetGlobalObject(),
                        button);

                await ShopBiz.CreateAndSaveAsync(parm);


                decimal refundable_Spent = cde.Refundable_Final;
                decimal nonRefundable_spent = cde.NonRefundable_Final;
                string shopName = product.FullName();
                string mp1Name = mpm.MenuPath1.FullName();
                string mp2Name = mpm.MenuPath2.FullName();
                string mp3Name = mpm.MenuPath3.FullName();
                int numberOfMonths = quantity;
                DateTime expiryDate = product.ShopExpiryDate.Date_NotNull_Min;

                ShopCreatedSuccessfullyVM shopCreatedSuccessfully = new ShopCreatedSuccessfullyVM(
                    refundable_Spent,
                    nonRefundable_spent,
                    shopName,
                    mp1Name,
                    mp2Name,
                    mp3Name,
                    numberOfMonths,
                    expiryDate,
                    shopCreate.ReturnUrl);

                //BuySellDocBiz.SaveChanges();
                return shopCreatedSuccessfully;
            }
            else
            {
                string err = string.Format("You caneled the operation.");
                throw new Exception(err);
            }
        }

        private Customer getCustomerOfUser()
        {
            Customer customerUser = CustomerBiz.GetPlayerFor(UserId);
            customerUser.IsNullThrowException("Unable to get customer for user.");
            return customerUser;
        }

        private ControllerCreateEditParameter setup_Shop_Into_ControllerCreateEditParameter(
            ShopVM shopCreate,
            string button,
            HttpPostedFileBase[] httpMiscUploadedFiles,
            Owner userOwner,
            out CashDistributionEngine cde,
            out int quantity,
            out Product product,
            out MenuPathMain mpm,
            out ControllerCreateEditParameter parm)
        {
            decimal paymentAmount = MenuPathMain.Payment_To_Buy_Shop();
            decimal commissionPct = BuySellDoc.Get_Maximum_Commission_Product_Percent();
            bool isNonRefundablePaymentAllowed = true;
            cde = get_CashDistributionEngineAndCheckBalance(paymentAmount, isNonRefundablePaymentAllowed);

            //check the name, if it is already used, throw error and return to create screen
            if (isShopNameExists(shopCreate.ShopName))
            {
                string err = string.Format("The shop name '{0}' already exists... sorry. Try again.", shopCreate.ShopName);
                throw new Exception(err);
            }
            //create the shop
            //get the shop Child Product
            ProductChild shop = ProductChildBiz.FindForName(ProductChild.GetShopName());
            shop.IsNullThrowException("Shop not found");

            Owner ownerProductChild = shop.Owner;
            ownerProductChild.IsNullThrowException("No Product Child Owner");

            quantity = shopCreate.NoOfMonths;
            if (quantity == 0)
                throw new Exception("The quantity is zero. This is not allowed");

            Customer customerUser = getCustomerOfUser();

            SelectList selectListOwner = null;
            SelectList selectListCustomer = null;
            SelectList selectListBillTo = null;
            SelectList selectListShipTo = null;
            string addressBillToId = "";
            string addressShipToId = "";
            decimal salePrice = MenuPathMain.Payment_To_Buy_Shop();
            DateTime expectedDeliveryDate = DateTime.Now;
            DateTime guaranteePeriodEnds = DateTime.Now;

            //MenuPathMain mpm = MenuPathMainBiz.Find(shopCreate.MenuPathMainId);
            //mpm.IsNullThrowException("Menu Path Main is null");

            mpm = getMainMenuPath(shopCreate);
            product = setup_Product_For_Shop(shopCreate, userOwner);

            string shopId = product.Id;

            BuySellDoc sale = CreateSale(
                shop,
                ownerProductChild,
                quantity,
                salePrice,
                customerUser,
                selectListOwner,
                selectListCustomer,
                addressBillToId,
                addressShipToId,
                selectListBillTo,
                selectListShipTo,
                BuySellDocStateENUM.RequestUnconfirmed,
                BuySellDocStateModifierENUM.Accept,
                expectedDeliveryDate,
                guaranteePeriodEnds,
                shopId);

            product.BuySellDocs.Add(sale);

            parm = new ControllerCreateEditParameter(
                    product as ICommonWithId,
                    httpMiscUploadedFiles,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    MenuENUM.EditDefault,
                    UserName,
                    UserId,
                    shopCreate.ReturnUrl,
                    GetGlobalObject(),
                    button);

            return parm;
        }

        private MenuPathMain getMainMenuPath(ShopVM shopCreate)
        {
            MenuPathMain mpm = MenuPathMainBiz.Find(shopCreate.MenuPathMainId);
            mpm.IsNullThrowException("Menu Path Not Found.");
            return mpm;
        }

        private static Product setup_Product_For_Shop(ShopVM shopCreate, Owner userOwner)
        {
            //now create the shop
            Product product = new Product();
            product.Name = shopCreate.ShopName;
            product.OwnerId = userOwner.Id;
            product.ShopExpiryDate.Date = DateTime.Now.AddMonths(shopCreate.NoOfMonths);

            if (product.BuySellDocs.IsNull())
                product.BuySellDocs = new List<BuySellDoc>();
            product.MainMenuIdForShop = shopCreate.MenuPathMainId;
            return product;
        }

        private List<string> getShopNamesForCurrUser()
        {
            Owner currUserOwner = OwnerBiz.GetPlayerFor(UserId);
            currUserOwner.IsNullThrowException("You must be authourized to sell to continue");

            List<Product> shops = ProductBiz.FindAll().Where(x => x.OwnerId == currUserOwner.Id).ToList();
            List<string> shopNames = new List<string>();

            if (shops.IsNullOrEmpty())
            { }
            else
            {
                foreach (Product shop in shops)
                {
                    string name = string.Format("{0} NO Path. This will not show. Expires: {1}, Is Expired? {2}",
                        shop.FullName(),
                        shop.ShopExpiryDate.Date_NotNull_Min.ToLongDateString(),
                        shop.IsShopExpired ? "Yes" : "No");

                    MenuPathMain mpm = shop.MenuPathMains.FirstOrDefault();

                    if (mpm.IsNull())
                    {

                    }
                    else
                    {
                        name = string.Format("{0} - {1} - {2} - {3} expires: {4}, Is Expired? {5}",
                            mpm.MenuPath1.FullName(),
                            mpm.MenuPath2.FullName(),
                            mpm.MenuPath3.FullName(),
                            shop.FullName(),
                            shop.ShopExpiryDate.Date_NotNull_Min.ToLongDateString(),
                            shop.IsShopExpired ? "Yes" : "No");

                    }


                    shopNames.Add(name);
                }
            }
            return shopNames;
        }

        private string getAUniqueNameForShop()
        {
            string shopName = UserName + " Shop";
            int count = 1;
            bool nameExists = false;
            do
            {
                nameExists = isShopNameExists(shopName);
                if (nameExists)
                {
                    shopName = UserName + " Shop " + count;
                    count++;

                }

            } while (nameExists);
            return shopName;
        }

        private bool isShopNameExists(string shopName)
        {
            Product productNameCheck = ProductBiz.FindForName(shopName);
            bool nameExists = !productNameCheck.IsNull();
            return nameExists;
        }
        private CashDistributionEngine get_CashDistributionEngineAndCheckBalance(decimal paymentAmount, bool isNonRefundablePaymentAllowed)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You must be logged in");
            decimal amountToBuyShop = MenuPathMain.Payment_To_Buy_Shop();

            Person person = PersonBiz.GetPersonForUserId(UserId);
            person.IsNullThrowException();

            CashBalanceVM cashBalance = BalanceFor_Person(person.Id, CashStateENUM.Available);

            CashDistributionEngine cde = new CashDistributionEngine(
                cashBalance,
                paymentAmount,
                0,
                isNonRefundablePaymentAllowed);


            if (cde.CanBuy())
            {
                string msg = string.Format("You have a total balance of: Rs{0:N2}. Money = {1:N2}, Non Refundable Tokens {2:N2}. You can buy the shop for this month! The expected spending will be as follows: Money = Rs{3:N2}, Tokens = Rs{4:N2}",
                    cde.CashBalance.Total(),
                    cde.CashBalance.Refundable,
                    cde.CashBalance.NonRefundable,
                    cde.Refundable_Final,
                    cde.NonRefundable_Final);

                cde.Message = msg;
                return cde;
            }
            else
            {
                throw new Exception("You have a total balance of: Rs" + cde.CashBalance.Total().ToString("N2") + ". You do not have sufficent money to buy the shop.");

            }
        }



    }
}
