
namespace EnumLibrary.EnumNS
{

    /// <summary>
    /// This enum is used for selling/buying
    /// </summary>
    public enum BuySellDocStateENUM
    {
        Unknown, //This is the starting value
        InProccess,  //These are all open order that are ConfirmedBySeller + ReadyForShipment + ConfirmedByCourier + PickedUp + Problem
        BackOrdered, //This will show all orders with an incomplete delivery
        All,  //all orders RequestUnConfirmed + RequestConfirmed + ConfirmedBySeller + ReadyForShipment + ConfirmedByCourier + PickedUp + Delivered + Rejected + Problem

        RequestUnconfirmed, //This is the request created by the buyer by pressing shopping cart. Before activating, buyer has a chance to fix address etc
        RequestConfirmed, //This is the request after it has been checked and confirmed.
        ConfirmedBySeller, //Once seller agrees to the request, he either confirs or rejects.
        ReadyForShipment,  //Once package is ready for shipping, seller confirms that package is ready for pickup
        ConfirmedByCourier,//Courier confirms they are going to pick up the package
        PickedUp, //the package has been picked up by the courier
        Delivered, //the package has been delivered to the buyer
        Rejected, //the order has been rejected for some reason
        Problem, //there is a problem with the order.


        New, //Depreciated to ConfirmedBySeller
        Closed,      //Depreciated to Delivered or Rejected
        Canceled,   //Depreciated to rejected
        Quotation,  //Depreciated to request
        Credit,// Depreciated to not being used. This will be done by cash.

    }
}
