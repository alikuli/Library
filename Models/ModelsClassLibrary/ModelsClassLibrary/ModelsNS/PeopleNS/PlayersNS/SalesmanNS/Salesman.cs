using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{

    /// <summary>
    /// Salesman is owner who has privilages to:
    ///     Invoice
    ///     Receive Payments against Invoice
    ///     Etc
    ///     Salesman = VENDOR
    /// </summary>
    public class Salesman : PlayerAbstract
    {


        public static Salesman Unbox(ICommonWithId iCommonWithId)
        {
            Salesman salesman = iCommonWithId as Salesman;
            salesman.IsNullThrowException("Unable to unbox salesman");
            return salesman;
        }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Salesman;
        }



        [Display(Name = "Cateogry")]
        [MaxLength(128)]
        public virtual string SalesmanCategoryId { get; set; }
        public virtual SalesmanCategory SalesmanCategory { get; set; }

        //public virtual ICollection<BuySellDoc> BuySellDocs { get; set; }


        //https://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table


        [Display(Name = "Super Salesman")]
        public string ParentSalesmanId { get; set; }

        [Display(Name = "Super Salesman")]
        public virtual Salesman ParentSalesman { get; set; }

        [Display(Name = "Super Salesmen")]
        [InverseProperty("ParentSalesman")]
        public virtual ICollection<Salesman> ParentSalesmen { get; set; }

        [NotMapped]
        public SelectList SelectListParentSalesmen { get; set; }



        [Display(Name = "Customer Salesmen")]
        [InverseProperty("CustomerSalesman")]
        public virtual ICollection<BuySellDoc> CustomerSalesmenBuySellDocs { get; set; }

        [Display(Name = "Super Customer Salesmen")]
        [InverseProperty("SuperCustomerSalesman")]
        public virtual ICollection<BuySellDoc> SuperCustomerSalesmanBuySellDocs { get; set; }




        [Display(Name = "Super Super Customer Salesmen")]
        [InverseProperty("SuperSuperCustomerSalesman")]
        public virtual ICollection<BuySellDoc> SuperSuperCustomerSalesmanBuySellDocs { get; set; }


        [Display(Name = "Owner Salesmen")]
        [InverseProperty("OwnerSalesman")]
        public virtual ICollection<BuySellDoc> OwnerSalesmanBuySellDocs { get; set; }


        [Display(Name = "Super Owner Salesmen")]
        [InverseProperty("SuperOwnerSalesman")]
        public virtual ICollection<BuySellDoc> SuperOwnerSalesmanBuySellDocs { get; set; }



        [Display(Name = "Super Super Owner Salesmen")]
        [InverseProperty("SuperSuperOwnerSalesman")]
        public virtual ICollection<BuySellDoc> SuperSuperOwnerSalesmanBuySellDocs { get; set; }











        [Display(Name = "Deliveryman Salesmen")]
        [InverseProperty("DeliverymanSalesman")]
        public virtual ICollection<BuySellDoc> DeliverymanSalesmanBuySellDocs { get; set; }


        [Display(Name = "Super Deliveryman Salesmen")]
        [InverseProperty("SuperDeliverymanSalesman")]
        public virtual ICollection<BuySellDoc> SuperDeliverymanSalesmanBuySellDocs { get; set; }



        [Display(Name = "Super Super Deliveryman Salesmen")]
        [InverseProperty("SuperSuperDeliverymanSalesman")]
        public virtual ICollection<BuySellDoc> SuperSuperDeliverymanSalesmanBuySellDocs { get; set; }
















        /// <summary>
        /// If true, then this user can hire other salesmen
        /// </summary>
        public bool IsSuperSalesman { get; set; }


        [NotMapped]
        public SelectList SelectListSalesmanCategory { get; set; }

        public override void UpdatePropertiesDuringModify(ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);
            Salesman sm = Salesman.Unbox(ic);
            ParentSalesmanId = sm.ParentSalesmanId;
            SalesmanCategoryId = sm.SalesmanCategoryId;
            IsSuperSalesman = sm.IsSuperSalesman;
        }

        //[NotMapped]
        //public SelectList SelectListBillAddress { get; set; }


        //[NotMapped]
        //public SelectList SelectListCashFromAddress { get; set; }



        public static decimal CommissionPct_CustomerSalesman
        {
            get
            {
                return CommissionPct_CustomerSalesman_String().ToDecimal();
            }
        }
        public static decimal CommissionPct_Customer_Super_Salesman
        {
            get
            {
                return CommissionPct_Customer_Super_Salesman_String().ToDecimal();
            }
        }
        public static decimal CommissionPct_Customer_Super_Super_Salesman
        {
            get
            {
                return CommissionPct_Customer_Super_Super_Salesman_String().ToDecimal();
            }
        }

        public static decimal GetCostToBecomeASalesman()
        {
            string pctAmountString = ConfigurationManager.AppSettings["salesman.cost_to_become_Salesman.amount"];
            decimal pctAmount = pctAmountString.ToDecimal();

            return pctAmount;

        }
        public static int GetNumberOfBsdToBecomeSuperSalesman()
        {
            string amountStr = ConfigurationManager.AppSettings["salesman.number_of_bsd_to_become_SuperSalesman.amount"];
            int amount = amountStr.ToInt();

            return amount;

        }
        static string CommissionPct_CustomerSalesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Salesman.Customer"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Salesman.Customer in WebConfig");
            }

            return commission;
        }
        static string CommissionPct_Customer_Super_Salesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Super.Salesman.Customer"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Salesman.Customer in WebConfig");
            }

            return commission;
        }
        static string CommissionPct_Customer_Super_Super_Salesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Super.Super.Salesman.Customer"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Super.Super.Salesman.Customer in WebConfig");
            }

            return commission;
        }

        public static decimal CommissionPct_OwnerSalesman
        {
            get
            {
                return CommissionPct_OwnerSalesman_String().ToDecimal();
            }
        }
        public static decimal CommissionPct_Owner_Super_Salesman
        {
            get
            {
                return CommissionPct_Owner_Super_Salesman_String().ToDecimal();
            }
        }
        public static decimal CommissionPct_Owner_Super_Super_Salesman
        {
            get
            {
                return CommissionPct_Owner_Super_Super_Salesman_String().ToDecimal();
            }
        }

        static string CommissionPct_OwnerSalesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Salesman.Owner"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Salesman.Owner in WebConfig");
            }

            return commission;
        }

        static string CommissionPct_Owner_Super_Salesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Super.Salesman.Owner"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Super.Salesman.Owner in WebConfig");
            }

            return commission;
        }

        static string CommissionPct_Owner_Super_Super_Salesman_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Super.Super.Salesman.Owner"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Super.Super.Salesman.Owner in WebConfig");
            }

            return commission;
        }


        /// <summary>
        /// this is for both delivey and sales
        /// </summary>
        /// <returns></returns>
        static string CommissionPct_System_String()
        {
            string commission = ConfigurationManager.AppSettings["PercentOfSale.Salesman.System"];
            if (commission.IsNullOrWhiteSpace())
            {
                throw new Exception("Please set PercentOfSale.Salesman.System in WebConfig");
            }

            return commission;
        }
        public static decimal CommissionPct_System
        {
            get
            {
                return CommissionPct_System_String().ToDecimal();
            }
        }

    }
}