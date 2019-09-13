using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    /// <summary>
    /// Make sure you load the BuySellDocumentTypeEnum and BuySellDocStateModifierEnum into buySelllDoc before
    /// putting it in here
    /// </summary>
    [NotMapped]
    public class RejectCancelDeleteInbetweenClass
    {
        public RejectCancelDeleteInbetweenClass()
        {

        }
        public RejectCancelDeleteInbetweenClass(string returnUrl, string text, BuySellDoc buySellDoc)
        {
            ReturnUrl = returnUrl;
            Text = text;
            BuySellDoc = buySellDoc;
            BuySellDoc.IsNullThrowException();
            BuySellDocId = buySellDoc.Id;
            BuySellDocumentTypeEnum = buySellDoc.BuySellDocumentTypeEnum;
            BuySellDocStateModifierEnum = buySellDoc.BuySellDocStateModifierEnum;
        }


        //public string Heading
        //{
        //    get
        //    {
        //        string heading = string.Format("");
        //        BuySellDoc.IsNullThrowException();

        //        switch (BuySellDoc.BuySellDocumentTypeEnum)
        //        {
        //            case BuySellDocumentTypeENUM.Sale:
        //                heading = string.Format("{0} to {1} (Rs{2}) [Insurance Rs{3}]",
        //                    BuySellDocStateModifierEnum.ToString().ToUpper(),
        //                    BuySellDoc.AddressShipTo.ToAddressComplex().ToStringOnlyNameAndCityAndCountry,
        //                    BuySellDoc.TotalInvoice,
        //                    BuySellDoc.InsuranceRequired_ToSring);
        //                break;

        //            case BuySellDocumentTypeENUM.Purchase:
        //                throw new NotImplementedException();

        //            case BuySellDocumentTypeENUM.Delivery:
        //                throw new NotImplementedException();
        //            case BuySellDocumentTypeENUM.Unknown:
        //            default:
        //                throw new Exception("Document unknown");
        //        }


        //        return heading;
        //    }
        //}

        public string BuySellDocId { get; set; }
        public BuySellDoc BuySellDoc { get; set; }

        public string ReturnUrl { get; set; }
        public string Text { get; set; }
        public BuySellDocStateModifierENUM BuySellDocStateModifierEnum { get; set; }
        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }
        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }
        /// <summary>
        /// Used in the message
        /// </summary>
        public string Subject { get; set; }
        public string Comment { get; set; }

        public override string ToString()
        {
            string subject = string.Format("{1} {2} {0}",
                    BuySellDocStateModifierEnum.ToString().ToUpper(),
                    BuySellDocumentTypeEnum,
                    BuySellDocStateEnum.ToString().ToTitleSentance());

            return subject;
        }



    }
}
