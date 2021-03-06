﻿using System.Configuration;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class PersonOpenPurchaseOrders : IMenuItemHelper
    {
        public PersonOpenPurchaseOrders()
        {

        }
        public PersonOpenPurchaseOrders(decimal money, double quantity)
        {
            Money = money;
            Quantity = quantity;
        }

        decimal Money { get; set; }
        double Quantity { get; set; }
        public string MenuItem
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Open.MenuItem"];
                string str = string.Format(content, Money, Quantity);
                return str;

            }
        }

        public string ToolTip
        {
            get
            {
                string content = ConfigurationManager.AppSettings["menu.person.PurchaseOrders.Open.ToolTip"];
                string str = string.Format(content, Money, Quantity);
                return str;
            }
        }



    }
}
