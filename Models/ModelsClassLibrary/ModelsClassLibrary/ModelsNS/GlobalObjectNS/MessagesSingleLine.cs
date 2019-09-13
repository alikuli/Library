using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.GlobalObjectNS
{
    public class MessagesSingleLine
    {
        public string CustomerMsg(decimal customerBalance, bool isCustomer)
        {
            if(isCustomer)
            {

            }

            return "You are not a customer. You cannot buy anything! To buy click this";
        }
    }
}
