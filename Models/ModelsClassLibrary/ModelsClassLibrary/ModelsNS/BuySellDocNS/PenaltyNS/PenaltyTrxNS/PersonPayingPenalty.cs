using ModelsClassLibrary.ModelsNS.CashNS.PenaltyTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS
{
    /// <summary>
    /// This class is used to collect and fix the data for penalties.
    /// from here we pass it on and formalize it.
    /// </summary>
    [NotMapped]
    public class PersonPayingPenalty
    {
        public PersonPayingPenalty(BuySellDoc buysSellDoc)
        {
            From = new PersonPayingPenaltyDetail();
            Owner = new PersonPayingPenaltyDetail();
            Customer = new PersonPayingPenaltyDetail();
            Deliveryman = new PersonPayingPenaltyDetail();
            Salesman_Owner = new PersonPayingPenaltyDetail();
            Salesman_Customer = new PersonPayingPenaltyDetail();
            Salesman_Deliveryman = new PersonPayingPenaltyDetail();

            //we need these to be null...means they dont have a value
            System_Freight = new PersonPayingPenaltyDetail();
            System_Sale = new PersonPayingPenaltyDetail();
            System_ExtraCommission = new PersonPayingPenaltyDetail();

        }

        public bool IsCustomer { get; set; }
        public bool IsOwner { get; set; }
        public bool IsDeliveryman { get; set; }
        public bool IsSalesman { get; set; }

        public PersonPayingPenaltyDetail From { get; set; }
        public PersonPayingPenaltyDetail Customer { get; set; }
        public PersonPayingPenaltyDetail Owner { get; set; }
        public PersonPayingPenaltyDetail Deliveryman { get; set; }
        public PersonPayingPenaltyDetail Salesman_Owner { get; set; }
        public PersonPayingPenaltyDetail Salesman_Customer { get; set; }
        public PersonPayingPenaltyDetail Salesman_Deliveryman { get; set; }
        public PersonPayingPenaltyDetail System_Freight { get; set; }
        public PersonPayingPenaltyDetail System_Sale { get; set; }
        public PersonPayingPenaltyDetail System_ExtraCommission { get; set; }

        public PersonPayingPenaltyDetail Super_Salesman_Owner { get; set; }
        public PersonPayingPenaltyDetail Super_Super_Salesman_Owner { get; set; }

        public PersonPayingPenaltyDetail Super_Salesman_Customer { get; set; }
        public PersonPayingPenaltyDetail Super_Super_Salesman_Customer { get; set; }


        public PersonPayingPenaltyDetail Super_Salesman_Deliveryman { get; set; }
        public PersonPayingPenaltyDetail Super_Super_Salesman_Deliveryman { get; set; }

        public void SelfErrorCheck()
        {

        }

        //public Person PersonFrom { get; set; }
        //public Decimal PersonFrom_PaymentAmount { get; set; }
        //public string PersonFrom_Comment { get; set; }



        //public Person Customer { get; set; }
        //public Decimal Customer_RecivedAmount { get; set; }
        //public string Customer_Comment { get; set; }
        //public decimal Customer_Pct_Of_Total { get; set; }
        //public void ClearCustomer()
        //{
        //    Customer = null;
        //    Customer_RecivedAmount = 0;
        //    Customer_Comment = "";
        //    Customer_Pct_Of_Total = 0;
        //}


        //public Person Owner { get; set; }
        //public Decimal Owner_RecivedAmount { get; set; }
        //public string Owner_Comment { get; set; }
        //public decimal Owner_Pct_Of_Total { get; set; }
        //public void ClearOwner()
        //{
        //    Owner = null;
        //    Owner_RecivedAmount = 0;
        //    Owner_Comment = "";
        //    Owner_Pct_Of_Total = 0;
        //}


        //public Person Deliveryman { get; set; }
        //public Decimal Deliveryman_RecivedAmount { get; set; }
        //public string Deliveryman_Comment { get; set; }
        //public decimal Deliveryman_Pct_Of_Total { get; set; }
        //public void ClearDeliveryman()
        //{
        //    Deliveryman = null;
        //    Deliveryman_RecivedAmount = 0;
        //    Deliveryman_Comment = "";
        //    Deliveryman_Pct_Of_Total = 0;
        //}


        //public Person Salesman_Owner { get; set; }
        //public Decimal Salesman_Owner_RecivedAmount { get; set; }
        //public string Salesman_Owner_Comment { get; set; }
        //public decimal Salesman_Owner_Pct_Of_Total { get; set; }
        //public void ClearSalesman_Owner()
        //{
        //    Salesman_Owner = null;
        //    Salesman_Owner_RecivedAmount = 0;
        //    Salesman_Owner_Comment = "";
        //    Salesman_Owner_Pct_Of_Total = 0;
        //}


        //public Person Salesman_Customer { get; set; }
        //public Decimal Salesman_Customer_RecivedAmount { get; set; }
        //public string Salesman_Customer_Comment { get; set; }
        //public decimal Salesman_Customer_Pct_Of_Total { get; set; }
        //public void ClearSalesman_Customer()
        //{
        //    Salesman_Customer = null;
        //    Salesman_Customer_RecivedAmount = 0;
        //    Salesman_Customer_Comment = "";
        //    Salesman_Customer_Pct_Of_Total = 0;
        //}



        //public Person Salesman_Deliveryman { get; set; }
        //public Decimal Salesman_Deliveryman_RecivedAmount { get; set; }
        //public string Salesman_Deliveryman_Comment { get; set; }
        //public decimal Salesman_Deliveryman_Pct_Of_Total { get; set; }
        //public void ClearSalesman_Deliveryman()
        //{
        //    Salesman_Deliveryman = null;
        //    Salesman_Deliveryman_RecivedAmount = 0;
        //    Salesman_Deliveryman_Comment = "";
        //    Salesman_Deliveryman_Pct_Of_Total = 0;
        //}

        //public Person System_Freight { get; set; }
        //public Decimal System_Freight_RecivedAmount { get; set; }
        //public string System_Freight_Comment { get; set; }
        //public decimal System_Freight_Pct_Of_Total { get; set; }
        //public void ClearSystem_Freight()
        //{
        //    System_Freight = null;
        //    System_Freight_RecivedAmount = 0;
        //    System_Freight_Comment = "";
        //    System_Freight_Pct_Of_Total = 0;
        //}


        //public Person System_Sale { get; set; }
        //public Decimal System_Sale_RecivedAmount { get; set; }
        //public string System_Sale_Comment { get; set; }
        //public decimal System_Sale_Pct_Of_Total { get; set; }
        //public void ClearSystem_Sale()
        //{
        //    System_Sale = null;
        //    System_Sale_RecivedAmount = 0;
        //    System_Sale_Comment = "";
        //    System_Sale_Pct_Of_Total = 0;
        //}


        //public Person System_ExtraCommission { get; set; }
        //public Decimal System_ExtraCommission_RecivedAmount { get; set; }
        //public string System_ExtraCommission_Comment { get; set; }
        //public decimal System_ExtraCommission_Pct_Of_Total { get; set; }
        //public void ClearSystem_ExtraCommission()
        //{
        //    System_ExtraCommission = null;
        //    System_ExtraCommission_RecivedAmount = 0;
        //    System_ExtraCommission_Comment = "";
        //    System_ExtraCommission_Pct_Of_Total = 0;
        //}

        //public void SelfErrorCheck()
        //{
        //    decimal total =
        //        Owner_RecivedAmount +
        //        Deliveryman_RecivedAmount +
        //        Salesman_Owner_RecivedAmount +
        //        Salesman_Customer_RecivedAmount +
        //        Salesman_Deliveryman_RecivedAmount +
        //        System_Freight_RecivedAmount +
        //        System_Sale_RecivedAmount +
        //        System_ExtraCommission_RecivedAmount;

        //    if (PersonFrom_PaymentAmount != total)
        //        throw new Exception(string.Format("The money does not match. Deducted amount = Rs{0:N2}, distributed amount = Rs{1:N2}", PersonFrom_PaymentAmount, total));
        //}

    }
}
