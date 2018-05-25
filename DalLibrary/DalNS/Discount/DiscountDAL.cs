using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DiscountNS;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class DiscountDAL : Repositry<Discount>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public DiscountDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass("DiscountDAL");
        }

        public override void ErrorCheck(Discount entity)
        {

            entity.SelfErrorCheck();

            //Checking for duplicate
            var foundDiscountKey = FindForDiscountKey(entity.DiscountKey);
            if (foundDiscountKey != null && !entity.Equals(foundDiscountKey))
                Errors.Add("This discount already exists.", "ErrorCheck");
            //throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException("This discount already exists.");

            base.ErrorCheck(entity);
        }

        private Discount FindForDiscountKey(string discountKey)
        {
            Discount discount = SearchFor(x => x.DiscountKey == discountKey).FirstOrDefault();
            return discount;
        }


        public IEnumerable<SelectListItem> SelectListDiscountENUM()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            var listOfEnumValues = Enum.GetValues(typeof(DiscountRuleENUM));
            if (listOfEnumValues != null)
                if (listOfEnumValues.Length > 0)
                {
                    foreach (var item in listOfEnumValues)
                    {
                        SelectListItem sVM = new SelectListItem();
                        sVM.Value = item.ToString();
                        sVM.Text = Enum.GetName(typeof(DiscountRuleENUM), item)
                            .ToString()
                            .ToSentence()
                            .ToTitleCase();

                        selectList.Add(sVM);
                    }

                }
            return selectList.OrderBy(x => x.Text).AsEnumerable();

        }



        /// <summary>
        /// Find a discount for a purchase. Remember to fully load the Discount first.
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public decimal GetDiscountedPurchase(Discount discount)
        {

            Discount discountFound = this.FindGivenDiscount(discount, DiscountTypeENUM.Purchase);
            return discountFound.FinalDiscountAmountForPurchase;
        }

        /// <summary>
        /// Find a discount for a sale. Remember to fully load the Discount first.
        /// </summary>
        /// <param name="discount"></param>
        /// <returns></returns>
        public Discount GetDiscountedSale(Discount discount)
        {
            Discount discountFound = this.FindGivenDiscount(discount, DiscountTypeENUM.Sale);
            return discountFound;
        }


        /// <summary>
        /// This locates the required discount, Purchase or Sale.
        /// </summary>
        /// <param name="discountFullyLoaded"></param>
        /// <param name="discountType"></param>
        /// <returns></returns>
        private Discount FindGivenDiscount(Discount discountFullyLoaded, DiscountTypeENUM discountType)
        {
            //Initialize the DALs
            //DiscountDAL discountDAL = new DiscountDAL(_db, _user);
            DiscountPrecedenceDAL discountPrecedenceDAL = new DiscountPrecedenceDAL(_db, _user);
            List<DiscountPrecedence> discountPrecedenceList = new List<DiscountPrecedence>();

            switch (discountType)
            {
                case DiscountTypeENUM.Unknown:
                    return null;

                case DiscountTypeENUM.Sale:
                    discountPrecedenceList = discountPrecedenceDAL.FindAllRulesByRankSale().ToList();
                    break;

                case DiscountTypeENUM.Purchase:
                    discountPrecedenceList = discountPrecedenceDAL.FindAllRulesByRankPurchase().ToList();
                    break;

                default:
                    return null;
            }



            //now search them according to the precede
            if (!discountPrecedenceList.IsNullOrEmpty())
            {
                //Break out of this loop if a discount is found
                foreach (var rule in discountPrecedenceList)
                {
                    //Load the rule into the fully loaded Discount for which we are checking
                    KeyTool k = new KeyTool(discountFullyLoaded, rule.DiscountRuleEnum);
                    Discount discountFound = FindForDiscountKey(k.Key);

                    return discountFound;

                }
            }
            return null;
        }




    }
}
