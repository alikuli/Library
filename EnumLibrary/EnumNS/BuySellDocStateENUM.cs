
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
        BeingPreparedForShipmentBySeller, //The seller is preparing the order
        ReadyForPickup,  //Once package is ready for shipping, seller confirms that package is ready for pickup. courier begin to place offers
        CourierAcceptedByBuyerAndSeller, //The courier is accepted by vendor. The courier can still be changed
        CourierComingToPickUp, //Courier is coming to pick up the product
        PickedUp, //the package has been picked up by the courier
        Enroute,  //The package is  with the courier and enroute
        Delivered, //the package has been delivered to the buyer
        Rejected, //the order has been rejected for some reason
        Problem, //there is a problem with the order.
        CashTransaction, //used for cash trx when converting to cashtrxVm2.
        CashEncashment,  //used for cash trx when converting to cashtrxVm2.
        OptedOutOfSystem, //this buysell order will
        //New, //Depreciated to ConfirmedBySeller
        //Closed,      //Depreciated to Delivered or Rejected
        //Canceled,   //Depreciated to rejected
        //Quotation,  //Depreciated to request
        //Credit,// Depreciated to not being used. This will be done by cash.

    }
}
