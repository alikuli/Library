//using AliKuli.UtilitiesNS;
//using ApplicationDbContextNS;
//using DalLibrary.Interfaces;
//using EnumLibrary.EnumNS;
//using ErrorHandlerLibrary.ExceptionsNS;
//using ModelsClassLibrary.ModelsNS.SharedNS;
//using ModelsClassLibrary.ViewModels;
//using UserModels;
//using WebLibrary.Programs;
//using System;
//using ModelsClassLibrary.RightsNS;
//namespace UowLibrary.PlayersNS
//{
//    public partial class RightBiz : BusinessLayer<Right>
//    {




//        /// <summary>
//        /// This is the base method to create rights
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <param name="classesWithRightsEnum"></param>
//        /// <param name="allowCreate"></param>
//        /// <param name="allowRetrieve"></param>
//        /// <param name="allowUpdate"></param>
//        /// <param name="allowDelete"></param>
//        /// <param name="allowDeleteAll"></param>
//        private Right factoryCreateRightFor(string userId, ClassesWithRightsENUM classesWithRightsEnum, bool allowCreate = false, bool allowRetrieve = true, bool allowUpdate = false, bool allowDelete = false, bool allowDeleteAll = false)
//        {

//            Right r = Dal.Factory();
//            r.UserId = userId;
//            r.RightsFor= classesWithRightsEnum;
//            r.Create = allowCreate;
//            r.Retrieve = allowRetrieve;
//            r.Update = allowUpdate;
//            r.Delete = allowDelete;
//            r.DeleteActually = allowDeleteAll;
//            return r;

//        }

//        public Right  FactoryCountryRightDefault (string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.Country;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }

//        public Right FactoryStateRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.State;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }

//        public Right FactoryCityRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.City;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryTownRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.Town;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryLanguageRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.Language;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryDiscountRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.Discount;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryDiscountPrecedenceRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.DiscountPrecedence;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }

//        public Right FactoryCustomerRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.Customer;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryCustomerCategoryRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.CustomerCategory;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryOwnerRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.Owner;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryOwnerCategoryRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.OwnerCategory;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }



//        public Right FactoryPaymentMethodRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.PaymentMethod;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryPaymentTermRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.PaymentTerm;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryProductCategory1RightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.ProductCategory1;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }
//        public Right FactoryProductCategory2RightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.ProductCategory2;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }
//        public Right FactoryProductCategory3RightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.ProductCategory3;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryFileDocRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.FileDoc;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryUserRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.ApplicationUser;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//        public Right FactoryRightDefault(string userId)
//        {
//            ClassesWithRightsENUM classesWithRightsEnum = ClassesWithRightsENUM.Right;
//            Right r = factoryCreateRightFor(userId, classesWithRightsEnum);

//            return r;

//        }


//    }
//}
