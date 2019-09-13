using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using System;


namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {


        //public override void ErrorCheck(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        //{
        //    base.ErrorCheck(parm);
        //    //BuySellDoc buySellDoc = parm.Entity as BuySellDoc;
        //    //buySellDoc.IsNullThrowException("Could not unbox BuySellDoc");
        //    //saleToSelfNotAllowed(buySellDoc);

        //    //switch (buySellDoc.BuySellDocumentTypeEnum)
        //    //{

        //    //    case BuySellDocumentTypeENUM.Delivery:
        //    //        switch (buySellDoc.BuySellDocStateEnum)
        //    //        {
        //    //            case BuySellDocStateENUM.ReadyForPickup:
        //    //                //handleFreightOffer(buySellDoc);
        //    //                throwExceptionIfVehicalTypeNotAdded(buySellDoc);
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;

        //    //            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;

        //    //            case BuySellDocStateENUM.CourierComingToPickUp:
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;

        //    //            case BuySellDocStateENUM.PickedUp:
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;

        //    //            case BuySellDocStateENUM.Enroute:
        //    //                break;
        //    //            case BuySellDocStateENUM.Delivered:
        //    //                break;
        //    //            case BuySellDocStateENUM.Rejected:
        //    //                break;
        //    //            case BuySellDocStateENUM.Problem:
        //    //                break;
        //    //            case BuySellDocStateENUM.Unknown:
        //    //            case BuySellDocStateENUM.InProccess:
        //    //            case BuySellDocStateENUM.BackOrdered:
        //    //            case BuySellDocStateENUM.All:
        //    //            case BuySellDocStateENUM.RequestUnconfirmed:
        //    //            case BuySellDocStateENUM.RequestConfirmed:
        //    //            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
        //    //            default:
        //    //                break;
        //    //        }
        //    //        break;
        //    //    case BuySellDocumentTypeENUM.Sale:
        //    //        switch (buySellDoc.BuySellDocStateEnum)
        //    //        {
        //    //            case BuySellDocStateENUM.Unknown:
        //    //                break;
        //    //            case BuySellDocStateENUM.InProccess:
        //    //                break;
        //    //            case BuySellDocStateENUM.BackOrdered:
        //    //                break;
        //    //            case BuySellDocStateENUM.All:
        //    //                break;
        //    //            case BuySellDocStateENUM.RequestUnconfirmed:
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;
        //    //            case BuySellDocStateENUM.RequestConfirmed:
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;
        //    //            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;
        //    //            case BuySellDocStateENUM.ReadyForPickup:
        //    //                //acceptCourier(buySellDoc);
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;
        //    //            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;

        //    //            case BuySellDocStateENUM.CourierComingToPickUp:
        //    //                not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                break;
        //    //            case BuySellDocStateENUM.PickedUp:
        //    //                break;
        //    //            case BuySellDocStateENUM.Enroute:
        //    //            case BuySellDocStateENUM.Delivered:
        //    //                break;
        //    //            case BuySellDocStateENUM.Rejected:

        //    //                break;
        //    //            case BuySellDocStateENUM.Problem:
        //    //                break;
        //    //            default:
        //    //                break;
        //    //        }
        //    //        break;




        //    //    case BuySellDocumentTypeENUM.Purchase:
        //    //        switch (buySellDoc.BuySellDocStateEnum)
        //    //        {
        //    //            case BuySellDocStateENUM.Unknown:
        //    //                break;
        //    //            case BuySellDocStateENUM.InProccess:
        //    //                break;
        //    //            case BuySellDocStateENUM.BackOrdered:
        //    //                break;
        //    //            case BuySellDocStateENUM.All:
        //    //                break;
        //    //            case BuySellDocStateENUM.RequestUnconfirmed:
        //    //                if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Unknown)
        //    //                {
        //    //                    not_Within_Shipping_Window_Throw_Exception(buySellDoc);
        //    //                }
        //    //                else
        //    //                {
        //    //                    not_Within_Shipping_Window_Throw_Message(buySellDoc);

        //    //                }
        //    //                break;
        //    //            case BuySellDocStateENUM.RequestConfirmed:
        //    //                //Buyer can still delete this request.
        //    //                not_Within_Shipping_Window_Throw_Message(buySellDoc);
        //    //                break;
        //    //            case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
        //    //                not_Within_Shipping_Window_Throw_Message(buySellDoc);
        //    //                break;
        //    //            case BuySellDocStateENUM.ReadyForPickup:
        //    //                not_Within_Shipping_Window_Throw_Message(buySellDoc);
        //    //                //acceptCourier(buySellDoc);
        //    //                break;

        //    //            case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
        //    //                not_Within_Shipping_Window_Throw_Message(buySellDoc);
        //    //                break;

        //    //            case BuySellDocStateENUM.CourierComingToPickUp:
        //    //                not_Within_Shipping_Window_Throw_Message(buySellDoc);
        //    //                break;

        //    //            case BuySellDocStateENUM.PickedUp:
        //    //                not_Within_Shipping_Window_Throw_Message(buySellDoc);
        //    //                break;
        //    //            case BuySellDocStateENUM.Delivered:
        //    //                break;
        //    //            case BuySellDocStateENUM.Rejected:
        //    //                buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.RequestUnconfirmed;
        //    //                break;
        //    //            case BuySellDocStateENUM.Problem:
        //    //                break;
        //    //            default:
        //    //                break;
        //    //        }
        //    //        break;

        //    //    default:
        //    //        break;
        //    //}


        //    ////empty address not allowed while saving.
        //    //ThrowExceptionIfBillToIdOrShipToIdIsEmptyOrNull(buySellDoc);

        //}

        //private void throwExceptionIfVehicalTypeNotAdded(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.VehicalTypeOfferedId.IsNullOrWhiteSpace())
        //        throw new Exception("No Vehical type added. Please add one");
        //}

        //private static void ThrowExceptionIfBillToIdOrShipToIdIsEmptyOrNull(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
        //    {
        //        buySellDoc.AddressBillToId.IsNullOrWhiteSpaceThrowException("You must fill in the bill to address");
        //        buySellDoc.AddressShipToId.IsNullOrWhiteSpaceThrowException("You must fill in the ship to address");
        //    }
        //}

        //private void saleToSelfNotAllowed(BuySellDoc buySellDoc)
        //{

        //    string personCustomerId = buySellDoc.Customer.PersonId;
        //    string personSellerId = buySellDoc.Owner.PersonId;
        //    if (personCustomerId == personSellerId)
        //    {
        //        ErrorsGlobal.Add(string.Format("This product belongs to you! You cannot sell to you self!"), "saleToSelfNotAllowed");
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }
        //}
    }
}
