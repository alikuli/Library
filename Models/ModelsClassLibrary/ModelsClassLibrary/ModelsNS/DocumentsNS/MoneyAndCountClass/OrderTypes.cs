
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.MoneyAndCountClass
{
    public class OrderTypes
    {
        public OrderTypes()
        {
            InProccess = new MoneyCountItemClass();
            BackOrdered = new MoneyCountItemClass();
            All = new MoneyCountItemClass();
            RequestUnconfirmed = new MoneyCountItemClass();
            RequestConfirmed = new MoneyCountItemClass();
            ConfirmedBySeller = new MoneyCountItemClass();
            ReadyForShipment = new MoneyCountItemClass();
            ConfirmedByCourier = new MoneyCountItemClass();
            PickedUp = new MoneyCountItemClass();
            Delivered = new MoneyCountItemClass();
            Rejected = new MoneyCountItemClass();
            Problem = new MoneyCountItemClass();
        }
        //These are all open order that are ConfirmedBySeller + ReadyForShipment + ConfirmedByCourier + PickedUp + Problem
        public MoneyCountItemClass InProccess { get; set; }

        //This will show all orders with an incomplete delivery
        public MoneyCountItemClass BackOrdered { get; set; }

        //all orders Request + ConfirmedBySeller + ReadyForShipment + ConfirmedByCourier + PickedUp + Delivered + Rejected + Problem
        public MoneyCountItemClass All { get; set; }

        //This is the request created by thebuyer by pressing shopping cart. 
        //sometimes the address will not be there, or it may not be correct, therefore,
        //all requests have to be confirmed by the buyer
        public MoneyCountItemClass RequestUnconfirmed { get; set; }

        /// <summary>
        /// The buyer has checked the request and it is in order. The buyer confirms it.
        /// </summary>
        public MoneyCountItemClass RequestConfirmed { get; set; }

        //Once seller agrees to the request, he either confirs or rejects.
        public MoneyCountItemClass ConfirmedBySeller { get; set; }

        //Once package is ready for shipping, seller confirms that package is ready for pickup
        public MoneyCountItemClass ReadyForShipment { get; set; }

        //Courier confirms they are going to pick up the package
        public MoneyCountItemClass ConfirmedByCourier { get; set; }

        //the package has been picked up by the courier
        public MoneyCountItemClass PickedUp { get; set; }

        //the package has been delivered to the buyer
        public MoneyCountItemClass Delivered { get; set; }

        //the order has been rejected for some reason
        public MoneyCountItemClass Rejected { get; set; }

        //there is a problem with the order.
        public MoneyCountItemClass Problem { get; set; }

    }
}
