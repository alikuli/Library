using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.UserMoneyAccountNS
{
    public class UserMoneyAccountHelperClass
    {
        public UserMoneyAccountHelperClass(decimal amount, string menuItem, string toolTup)
        {

        }
        public virtual string ToolTip {get;set;}
        public MenuItemHelperClass Menu { get { return new MenuItemHelperClass(menuItem, ToolTip); } }
        
        
        public decimal amount { get; set; }
        string amountStr
        {
            get
            {
                string str = string.Format("R: Rs{0:n2}", amount);
                return str;
            }
        }
        public virtual string menuItem {get;set;}
    }
}
