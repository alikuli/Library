
using System;
using System.Collections.Generic;
using AliKuli.Extentions;

using UserModels.Models;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;

using ModelsClassLibrary.ModelsNS.ProductNS;

namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class ProductCat1DAL : Repositry<ProductCat1>, IEntityWithEnum
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public ProductCat1DAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }




        public void InitializeFromEnumAndSave()
        {

            var listOfEnums = EnumExtention<ProductCategory1ENUM>.ToList();

            if (listOfEnums.IsNullOrEmpty())
                return;

            listOfEnums.Remove(ProductCategory1ENUM.Unknown.ToString());
            CreateEntitiesFromListOfStrings(listOfEnums as IList<string>);
            Save();

            ProductCategoryMainDAL pCatMainDal = new ProductCategoryMainDAL(_db, _user);

            //remember to create the MainProduct also for these categories
            foreach (var item in listOfEnums)
            {
                try
                {
                    ProductCategoryMain c = pCatMainDal.Factory();
                    c.ProductCat1 = (ICategory)FindForName(item);
                    pCatMainDal.Create(c);
                    pCatMainDal.Save();
                }
                catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
                {
                    continue;
                }
            }

            Save();
        }

        public ProductCat1 FindForEnum(ProductCategory1ENUM theEnum)
        {
            if (theEnum == ProductCategory1ENUM.Unknown)
                return null;

            string name = Enum.GetName(theEnum.GetType(), theEnum);

            return FindForName(name.ToSentence());
        }

        public override void ErrorCheck(ProductCat1 entity)
        {
            base.ErrorCheck(entity);
        }

        public override void Fix(ProductCat1 entity)
        {
            base.Fix(entity);
        }


    }
}
