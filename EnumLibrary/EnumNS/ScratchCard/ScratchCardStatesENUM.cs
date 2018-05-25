using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnumLibrary.EnumNS

{
    public enum ScratchCardStatesENUM
    {
        New,                    //This is the state when the cards have been generated
        IssuedForPrinting,      //This is when cards have gone to the printer
        ReceivedFromPrinting,   //This is when the cards have returned from printer but not made live
        Activate,                   //This card is ready to be used
        UsedUp,                 //Once the card is used completly, it is marked used up
        BlackListed             //In case a card is not used or used, but black listed.

    }
}