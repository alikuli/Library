﻿
namespace ModelsClassLibrary.ModelsNS.ServiceRequestNS.ServiceRequest_StatePatternNS.ServiceRequest_StatesNS
{
    public class BecomeMailerState : ServiceRequestDetail_Aabstract
    {
        public override string Text
        {
            get
            {
                string str = string.Format("You will be charged a non-refundable nominal fee of {0}. In case you have tokens, tokens will be used first.", Amount);
                return str;
            }
        }

        public override decimal Amount
        {
            get
            {
                return ServiceRequestDetail_Aabstract.BecomeMailerFee();
            }
        }
    }
}
