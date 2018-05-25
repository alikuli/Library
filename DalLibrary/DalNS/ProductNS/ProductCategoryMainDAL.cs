using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;

using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class ProductCategoryMainDAL:Repositry<ProductCategoryMain>, IEntityWithEnum
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public ProductCategoryMainDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }

        #region DataIntegrityHelpers

        public override void ErrorCheck(ProductCategoryMain entity)
        {
            
            base.ErrorCheck(entity);
            entity.SelfErrorCheck();
            CheckForDuplicateRecord(entity);
            RejectIfSeniorCatEmptyAndJrCatIsFilled(entity);

        }

        private void RejectIfSeniorCatEmptyAndJrCatIsFilled(ProductCategoryMain entity)
        {
            if(entity.ProductCat3Id != null )
            {
                    if (entity.ProductCat1Id == null)
                    {
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Product Category 1 cannot be empty when Product Category 3 is filled.");
                    }

                    if (entity.ProductCat2Id == null)
                    {
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Product Category 1 cannot be empty when Product Category 2 is filled.");
                    }
            }


            if (entity.ProductCat2Id != null )
            {
                    if (entity.ProductCat1Id == null)
                    {
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Product Category 1 cannot be empty when Product Category 3 is filled.");
                    }
            }

        }

        /// <summary>
        /// This makes sure that a previous exact same category does not exist.
        /// </summary>
        /// <param name="entity"></param>
        private void CheckForDuplicateRecord(ProductCategoryMain entity)
        {
            ProductCategoryMain pmFound = FindForMinorCategories(entity.ProductCat1Id, entity.ProductCat2Id, entity.ProductCat3Id);

            //if creating check to see if a duplicate exists...
            if (pmFound != null && entity.Id != pmFound.Id)
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException("This main category already exists. ProductCategoryMainDAL.ErrorCheck");
        }
        
        public override void Fix(ProductCategoryMain entity)
        {
            //this needs to come first because it is used in the base.Fix
            entity.Name = Make_Name(entity);
            
            base.Fix(entity);
            
            Fix_ProductCat1(entity);
            Fix_ProductCat2(entity);
            Fix_ProductCat3(entity);
            
            
        }

        public string Make_Name(ProductCategoryMain entity)
        {
            string name = "";
            if (entity.Is_Level_1_Category())
            {
                name = string.Format("{0}",
                    entity.ProductCat1.Name);
                return name;;
            }

            if (entity.Is_Level_2_Category())
            {
                name = string.Format("{0}-{1}",
                    entity.ProductCat1.Name,
                    entity.ProductCat2.Name);
                return name;
            }

            if (entity.Is_Level_3_Category())
            {
                name = string.Format("{0}-{1}-{3}",
                    entity.ProductCat1.Name,
                    entity.ProductCat2.Name,
                    entity.ProductCat3.Name);
                return name;


            }

            throw new ErrorHandlerLibrary.ExceptionsNS.ProductCategoryException("There is an error in the Categories. ProductCategoryMainDAL.Fix_Name");

        }

        #region Fix Helpers
        private static void Fix_ProductCat3(ProductCategoryMain entity)
        {
            if (entity.ProductCat3 == null)
            {
                if (entity.ProductCat3Id == null)
                {
                    //user doesnt want ProductCat3
                }
                else
                {
                    entity.ProductCat3 = (ICategory) new ProductCat3DAL(_db, _user).FindFor(entity.ProductCat3Id);

                    if (entity.ProductCat3 == null)
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Cat 3 Not found. ProductCategoryMainDAL.Fix_ProductCat3");
                }


            }
            else
            {
                if (entity.ProductCat3Id == null)
                {
                    entity.ProductCat3Id = entity.ProductCat3.Id;
                }
            }
        }

        private static void Fix_ProductCat2(ProductCategoryMain entity)
        {
            if (entity.ProductCat2 == null)
            {
                if (entity.ProductCat2Id == null)
                {
                    //user doesnt want ProductCat2
                }
                else
                {
                    entity.ProductCat2 = (ICategory)new ProductCat2DAL(_db, _user).FindFor(entity.ProductCat2Id);

                    if (entity.ProductCat2 == null)
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Cat 2 Not found. ProductCategoryMainDAL.Fix_ProductCat2");
                }

            }
            else
            {
                if (entity.ProductCat2Id == null)
                {
                    entity.ProductCat2Id = entity.ProductCat2.Id;
                }
            }
        }

        private static void Fix_ProductCat1(ProductCategoryMain entity)
        {
            if (entity.ProductCat1 == null)
            {
                if(entity.ProductCat1Id.IsNullOrEmpty())
                    throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Cat 1 is required. ProductCategoryMainDAL.Fix_ProductCat1");
                else
                {
                    entity.ProductCat1 = (ICategory) new ProductCat1DAL(_db, _user).FindFor(entity.ProductCat1Id);
                }

                if (entity.ProductCat1 == null)
                    throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Cat 1 Not found. ProductCategoryMainDAL.Fix_ProductCat1");
            }
            else
            {
                if (entity.ProductCat1Id == null)
                {
                    entity.ProductCat1Id = entity.ProductCat1.Id;
                }
            }
        }

        #endregion

        protected ProductCategoryMain FindForMinorCategories(Guid? cat1Id, Guid? cat2Id, Guid? cat3Id)
        {
            Guid? productCat1Id = cat1Id ?? null;
            Guid? productCat2Id = cat2Id ?? null;
            Guid? productCat3Id = cat3Id ?? null;

            ProductCategoryMain foundProductCategoryMain = this.SearchFor(x =>
                x.ProductCat1Id == productCat1Id &&
                x.ProductCat2Id == productCat2Id &&
                x.ProductCat3Id == productCat3Id 
                ).FirstOrDefault();

            return foundProductCategoryMain;

        }



        #endregion

        public ProductCategoryMain FindForEnum(ProductCategory1ENUM theEnum)
        {
            if (theEnum == ProductCategory1ENUM.Unknown)
                return null;

            string name = Enum.GetName(theEnum.GetType(), theEnum);
            name = name.ToSentence();
            return FindForName(name);
        }



        public void InitializeFromEnumAndSave()
        {
            ProductCategoryMain pc = Factory();
            pc.ProductCat1Id = new ProductCat1DAL(_db, _user).FindForEnum(ProductCategory1ENUM.Product).Id;

        }
    }
}
