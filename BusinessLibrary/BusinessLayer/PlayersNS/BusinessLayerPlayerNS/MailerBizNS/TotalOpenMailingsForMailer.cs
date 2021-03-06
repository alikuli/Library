﻿using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {


        //this returns a model with all the info.
        private MailCounterModel TotalOpenMailingsForMailer(Mailer mailer)
        {
            mailer.IsNullThrowExceptionArgument("Mailer");

            MailCounterModel mcm = new MailCounterModel();

            if (mailer.AddressVerificationHdrs.IsNullOrEmpty())
                return mcm;



            foreach (var hdr in mailer.AddressVerificationHdrs)
            {
                if (hdr.SuccessEnum == EnumLibrary.EnumNS.SuccessENUM.InProccess)
                    mcm.Inproccess++;

                if (hdr.SuccessEnum == EnumLibrary.EnumNS.SuccessENUM.Unknown)
                    mcm.Unknown++;
            }


            return mcm;
        }



    }
}
