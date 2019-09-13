using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Complex;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS
{
    public class CashEncashmentTrx : CommonWithId
    {

        public CashEncashmentTrx()
        {
            IsPaymentMade = new BoolDateAndByComplex();
            IsApproved = new BoolDateAndByComplex();
            SecretNumberEntered = new StringDateAndByComplex();
        }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.CashEncashmentTrx;
        }

        public CashEncashmentTrx(decimal currentBalance_Refundable, string systemMessageToApplicant, SelectList selectListPaymentType)
        {
            //PersonRequestingPaymentId = personRequestingPaymentId;
            //SelectListPersonRequestingPayment = selectListPersonRequestingPayment;
            CurrentBalance_Refundable = currentBalance_Refundable;
            SystemMessageToApplicant = systemMessageToApplicant;
            SelectListPaymentMethod = selectListPaymentType;

        }

        public override string MakeUniqueName()
        {
            IsApproved.By.IsNullOrWhiteSpaceThrowException("First add the IsApproved.");
            if (DocumentNo == 0)
                throw new Exception("Document number not initialized");

            string name = string.Format("{0}-{1}", IsApproved.By, DocumentNo);
            return name;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            CashEncashmentTrx cp = CashEncashmentTrx.Unbox(icommonWithId);

            PersonRequestingPaymentId = cp.PersonRequestingPaymentId;
            Amount = cp.Amount;
            PaymentDetail = cp.PaymentDetail;
            SystemMessageToApplicant = cp.SystemMessageToApplicant;
            IsPaymentMade = cp.IsPaymentMade;
            IsApproved = cp.IsApproved;
            SecretNumber = cp.SecretNumber;


        }
        public long DocumentNo { get; set; }
        public static CashEncashmentTrx Unbox(ICommonWithId icommonWithId)
        {
            CashEncashmentTrx cashPayment = icommonWithId as CashEncashmentTrx;
            cashPayment.IsNullThrowException();
            return cashPayment;
        }



        /// <summary>
        /// This is the person requesting the payment from the system
        /// </summary>
        /// 
        [Display(Name = "Person Requesting Payment")]
        public string PersonRequestingPaymentId { get; set; }


        [Display(Name = "Person Requesting Payment")]
        public virtual Person PersonRequestingPayment { get; set; }



        [NotMapped]
        public SelectList SelectListPersonRequestingPayment { get; set; }


        [Display(Name = "Encashment Amount")]
        [NotMapped]
        public string AmountString { get; set; }


        public decimal Amount { get; set; }



        [NotMapped]
        [Display(Name = "Curr Balance (Refundable)")]
        public string CurrentBalance_Refundable_String
        {
            get
            {

                return CurrentBalance_Refundable.ToString().ToRuppeeFormat();
            }
        }



        [NotMapped]
        [Display(Name = "Curr Balance (Refundable)")]
        public decimal CurrentBalance_Refundable { get; set; }



        /// <summary>
        /// This will hold the cheque number or any other reference numbers for payment detail
        /// </summary>
        [Display(Name = "Payment Detail")]
        public string PaymentDetail { get; set; }


        /// <summary>
        /// This will hold the system message such as payment will be made within 3 working days.
        /// </summary>
        /// 
        [Display(Name = "Terms")]
        public string SystemMessageToApplicant { get; set; }


        [Display(Name = "Secret Code")]
        public string SecretNumber { get; set; }


        [Display(Name = "Secret Code Entered")]
        public StringDateAndByComplex SecretNumberEntered { get; set; }

        [Display(Name = "Id Number")]
        public string ReceiversIdentificationCardNumber { get; set; }
        public bool IsPaid()
        {
            if (SecretNumber.IsNullOrWhiteSpace())
                return false;

            if (SecretNumberEntered.Value.IsNullOrWhiteSpace())
                return false;

            return SecretNumber.Trim() == SecretNumberEntered.Value.Trim();
        }

        [Display(Name = "No. Of trys")]
        public int NoOfTriesToEnterSecretNumber { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }




        [Display(Name = "Payment Type")]
        [NotMapped]
        public SelectList SelectListPaymentMethod { get; set; }




        /// <summary>
        /// If approved, payment can be made.
        /// </summary>
        [Display(Name = "Approved")]
        public BoolDateAndByComplex IsApproved { get; set; }




        [Display(Name = "Payment Made")]
        public BoolDateAndByComplex IsPaymentMade { get; set; }


        /// <summary>
        /// IF cashTrx is not to be made this will return null.
        /// </summary>
        /// <param name="cashTrxFromFactory"></param>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public CashTrx ToCashTrx(CashTrx cashTrxFromFactory, UserParameter userParameter)
        {

            CashStateENUM cashStateEnum = CashStateENUM.Allocated;
            if (IsApproved.Selected)
            {
                if (IsPaid())
                {
                    cashStateEnum = CashStateENUM.Available;

                }
            }
            else
            {
                return null;
            }
            string comment = string.Format("Encashment - {0}", FullName());
            string name = string.Format("{0} {1}", "Encashment", Name);
            cashTrxFromFactory.SetupCashTrx(PersonRequestingPayment, null, Amount, cashStateEnum, CashTypeENUM.Refundable, userParameter, comment, name, Id);

            return cashTrxFromFactory;

        }

        public CashTrxVM2 ToCashTrxVM2(string comment, Person fakeBankPerson)
        {
            string fixedComment = "";

            if (comment.IsNullOrWhiteSpace())
            {
                if (Comment.IsNullOrWhiteSpace())
                {

                }
                else
                {
                    fixedComment = Comment;
                }
            }
            else
            {
                if (Comment.IsNullOrWhiteSpace())
                {
                    fixedComment = comment;
                }
                else
                {
                    fixedComment = comment + " - " + Comment;
                }
            }

            CashStateENUM cashStateEnum = CashStateENUM.Allocated;
            if (IsApproved.Selected)
            {
                if (IsPaid())
                {
                    cashStateEnum = CashStateENUM.Available;

                }
            }
            else
            {
                return null;
            }


            CashTrxVM2 cashTrxVm2 = new CashTrxVM2(
                Id, 
                MetaData.Created.Date_NotNull_Min, 
                Amount, 
                0, 
                comment, 
                PersonRequestingPayment, 
                fakeBankPerson, 
                CashTypeENUM.Refundable, 
                cashStateEnum, 
                CashTrxVmDocumentTypeENUM.EncashmentRequest, 
                DocumentNo.ToString("N0"), 
                BuySellDocStateENUM.CashEncashment);

            return cashTrxVm2;
        }

    }
}
