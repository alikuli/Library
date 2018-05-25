using AliKuli.Utilities;
using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.Inventory;
using ModelsClassLibrary.Models.ProductNS;
using ModelsClassLibrary.Models.ProductNS.UOM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeProducts
    {
        private static ApplicationDbContext _db;
        private static ProductCategoryMainDAL catDAL;
        private static ProductDAL _dal;

        
        private string _user;


        public InitializeProducts(ApplicationDbContext db, string user)
        {
            _db = db;
            _user=user;
            catDAL = new ProductCategoryMainDAL(db, user);
            _dal = new ProductDAL(db, user);

        }
                    //Add(number, serialNo, expiryDate, name, shortDescription, longDescription, msrp, mlpPrice, buyPrice, productCategoryId, IsChild, parentID, uomPurchaseID, uomStockID, uomShipWeightENUM, shipWeight, uomLengthENUM, height, width, length);

        private void Add(
            string number,
            string productId,
            DateTime expiryDate,
            string name,
            string shortDescription,
            string longDescription,
            decimal msrp,
            decimal mlpPrice,
            decimal buyPrice,
            Guid productCategoryId,
            bool IsChild,
            Guid? parentID,
            Guid uomPurchaseID,
            Guid uomStockID,
            Guid uomShipWeightId,
            double shipWeight ,
            Guid uomLengthId,
            double height,
            double width,
            double length


            )
        {
            Product p = _dal.Factory();
            try
            {
                //p.SerialNo=serialNo;
                p.Name= name;
                p.SerialNo=productId;
                p.ExpiryDate= expiryDate;
                p.ShortDescription= shortDescription;
                p.LongDescription= longDescription;
                p.MSRP= msrp;
                p.SellPrice = msrp;
                p.MlpPrice= mlpPrice;
                p.BuyPrice= buyPrice;
                p.ProdCategoryID= productCategoryId;
                p.IsChild= IsChild;
                p.ParentId= parentID;
                p.UomPurchaseID= uomPurchaseID;
                p.UomStockID= uomStockID;
                p.UomShipWeightId= uomShipWeightId;
                p.ShipWeight= shipWeight ;
                p.UomLengthForPackingVolId= uomLengthId;
                p.Height= height;
                p.Width= width;
                p.Length = length;

                _dal.Create(p);
                _dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            { }
            catch (AliKuli.ExceptionsNS.NoDataException)
            { }
            catch (AliKuli.ExceptionsNS.DuplicateScratchCardNumberException)
            { }
            catch (Exception)
            {
                throw;
            }
        }
        

        public void Initialize()
        {
            string number;
            string productId;
            DateTime expiryDate;
            string name;
            string shortDescription;
            string longDescription;
            decimal msrp;
            decimal mlpPrice;
            decimal buyPrice;
            Guid productCategoryId;
            bool IsChild;
            Guid? parentID;
            Guid uomPurchaseID;
            Guid uomStockID;
            Guid uomShipWeightId;
            double shipWeight;
            Guid uomLengthId;
            double height;
            double width;
            double length;


            var scratchCardList = new ScratchCardDAL(_db, _user).FindAll();

            var top10ScratchCards = scratchCardList.Take(10).ToList().ToArray();

            if (top10ScratchCards == null)
                throw new Exception("Initialization. No scratch cards found");

            if (top10ScratchCards != null)
                if (top10ScratchCards.Count()==0)
                    throw new Exception("Initialization. No scratch cards found. Count is 0;");


            uomLengthId = new UomLengthDAL(_db, "").FindForEnum(UomLengthENUM.inch).Id;
            shipWeight = 1.2;
            height = 1;
            width = 0.5;
            length = 0.23;
            longDescription = "this card is used for making payments";
            productCategoryId = catDAL.FindForEnum(ProductCategory1ENUM.ScratchCard).Id;
            msrp = 500;
            mlpPrice = 499;
            buyPrice = 5;
            IsChild = false;
            parentID = null;
            uomPurchaseID = new UomQtyDAL(_db, _user).FindForEnum(UomQtyENUM.Count).Id;
            uomStockID = new UomQtyDAL(_db, _user).FindForEnum(UomQtyENUM.Dz).Id;
            uomLengthId = new UomLengthDAL(_db, _user).FindForEnum(UomLengthENUM.inch).Id;
            uomShipWeightId = new UomWeightDAL(_db, _user).FindForEnum(UomWeightENUM.gm).Id;

            productId = "";
            number = top10ScratchCards[0].Number16DigitFormat;
            //productId = string.Format(" Face Value: {1}U - Serial#:{0}", top10ScratchCards[0].id,top10ScratchCards[0].TotalNumberOfUnits.ToString());
            expiryDate = top10ScratchCards[0].ExpiryDate;
            name = top10ScratchCards[0].Name.ToString();
            shortDescription = top10ScratchCards[0].FullName();



            Add(number, productId, expiryDate, name, shortDescription, longDescription, msrp, mlpPrice, buyPrice, productCategoryId, IsChild, parentID, uomPurchaseID, uomStockID, uomShipWeightId, shipWeight, uomLengthId, height, width, length);

            //==============================================================================            

            number = top10ScratchCards[1].Number16DigitFormat;
            //productId = string.Format(" Face Value: {1}U - Serial#:{0}", top10ScratchCards[1].id, top10ScratchCards[0].TotalNumberOfUnits.ToString());
            productId = top10ScratchCards[1].FullName();
            name = top10ScratchCards[1].Name.ToString();
            shortDescription = top10ScratchCards[1].FullName();



            Add(number, productId, expiryDate, name, shortDescription, longDescription, msrp, mlpPrice, buyPrice, productCategoryId, IsChild, parentID, uomPurchaseID, uomStockID, uomShipWeightId, shipWeight, uomLengthId, height, width, length);


            //==============================================================================            


            number = top10ScratchCards[2].Number16DigitFormat;
            //productId = top10ScratchCards[2].id;
            //productId = string.Format(" Face Value: {1}U - Serial#:{0}", top10ScratchCards[2].id, top10ScratchCards[0].TotalNumberOfUnits.ToString());
            //productId = top10ScratchCards[2].FullName();
            expiryDate = top10ScratchCards[2].ExpiryDate;
            name = top10ScratchCards[2].Name.ToString();
            shortDescription = top10ScratchCards[2].FullName();


            Add(number, productId, expiryDate, name, shortDescription, longDescription, msrp, mlpPrice, buyPrice, productCategoryId, IsChild, parentID, uomPurchaseID, uomStockID, uomShipWeightId, shipWeight, uomLengthId, height, width, length);





            //==============================================================================            



            number = top10ScratchCards[3].Number16DigitFormat;
            //productId = top10ScratchCards[3].id;
            //productId = string.Format(" Face Value: {1}U - Serial#:{0}", top10ScratchCards[3].id, top10ScratchCards[0].TotalNumberOfUnits.ToString());
            productId = top10ScratchCards[3].FullName();
            expiryDate = top10ScratchCards[3].ExpiryDate;
            name = top10ScratchCards[3].Name.ToString();
            shortDescription = top10ScratchCards[3].FullName();



            Add(number, productId, expiryDate, name, shortDescription, longDescription, msrp, mlpPrice, buyPrice, productCategoryId, IsChild, parentID, uomPurchaseID, uomStockID, uomShipWeightId, shipWeight, uomLengthId, height, width, length);

            //==============================================================================            

            number = top10ScratchCards[4].Number16DigitFormat;
            //productId = top10ScratchCards[4].id;
            //productId = string.Format(" Face Value: {1}U - Serial#:{0}", top10ScratchCards[4].id, top10ScratchCards[0].TotalNumberOfUnits.ToString());
            productId = top10ScratchCards[4].FullName();
            expiryDate = top10ScratchCards[4].ExpiryDate;
            name = top10ScratchCards[4].Name.ToString();
            shortDescription = top10ScratchCards[4].FullName();



            Add(number, productId, expiryDate, name, shortDescription, longDescription, msrp, mlpPrice, buyPrice, productCategoryId, IsChild, parentID, uomPurchaseID, uomStockID, uomShipWeightId, shipWeight, uomLengthId, height, width, length);

            
            //==============================================================================            
            number = top10ScratchCards[5].Number16DigitFormat;
            productId = top10ScratchCards[5].FullName();
            //productId = top10ScratchCards[5].id;
            expiryDate = top10ScratchCards[5].ExpiryDate ;
            name = top10ScratchCards[5].Name.ToString();
            shortDescription = top10ScratchCards[5].FullName();



            Add(number, productId, expiryDate, name, shortDescription, longDescription, msrp, mlpPrice, buyPrice, productCategoryId, IsChild, parentID, uomPurchaseID, uomStockID, uomShipWeightId, shipWeight, uomLengthId, height, width, length);
        }


        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                _db.Products.Remove(item);
            }
            _db.SaveChanges();


            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                _db.Products.Remove(item);
            }
            _db.SaveChanges();
        }
        public void Edit()
        {

            List<Product> lst =_dal.FindAll().ToList();

            if (lst == null)
                throw new Exception(string.Format("No '{0}' found to edit.",_dal.GetSelfClassName()));

            foreach (var item in lst)
            {
                item.Name += "*";
               _dal.Update(item);
                try
                {
                   _dal.Save();
                }
                catch (Exception e)
                {
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}