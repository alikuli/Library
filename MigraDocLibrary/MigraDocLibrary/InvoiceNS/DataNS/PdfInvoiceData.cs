using MigraDocLibrary.InvoiceNS;
using ModelClassLibrary.MigraDocNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.SharedNS;
using System;
using System.Collections.Generic;

namespace InvoiceNS
{
    public class PdfInvoiceData
    {
        AddressStringWithNames _buyer;
        AddressStringWithNames _seller;
        AddressStringWithNames _shipTo;
        AddressStringWithNames _informTo;
        AddressStringWithNames _webCompany;
        public InvoicPdfParameter Load(string imageAddress)
        {
            loadAddresses();

            InvoicPdfParameter param = new InvoicPdfParameter(
                loadLogo(imageAddress),
                new PdfHeaderInfo("Invoice", "Invoice Test", "Ali Kuli Aminuddin"),
                new Addresses(_seller, _buyer, _shipTo, _informTo, _webCompany), loadDocumentInfo(),
                "Nida.Com");

            return param;
        }

        private Logo loadLogo(string imageAddress)
        {
            string a = imageAddress;
            var logo = new Logo(a);
            return logo;
        }

        private static PdfHeaderInfo loadDocumentHeaderInfo()
        {
            PdfHeaderInfo info = new PdfHeaderInfo();
            info.Author = "Ali Kuli Aminuddin";
            info.Subject = "Invoice Test";
            info.Title = "Invoice";

            return info;
        }

        private static DocumentInfo loadDocumentInfo()
        {
            DocumentInfo info = new DocumentInfo();
            info.DocType = EnumLibrary.EnumNS.DocumentTypeENUM.Credit;
            info.Date = DateTime.Now.ToShortDateString();
            info.DocumentNo = "1234";

            info.PurchaseOrderNumber = "9876";
            info.PurchaseOrderDate = DateTime.Now.AddDays(-3).ToShortDateString();
            info.ShipDate = DateTime.Now.AddDays(-1).ToShortDateString();
            info.ShipWeight = "98KG";
            info.ShippingCarrier = "TCS EXP";

            info.Comment = "I am praying this invoice works well.";


            info.Tax = 101;
            info.ShippingHandling = 5;
            info.Misc = 100;
            info.AdvancePayment = 1000;

            info.LineItems = new List<LineItem>()
            {
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("16","item with no comment. ",93,22,1300,0.1),
                new LineItem("17","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),
                new LineItem("10","Airplane",100,22,100000,0.5,"This is a test comment"),
                new LineItem("11","Airplane with 3 wings",93,22,134300,0.5,"This is a another text"),
                new LineItem("12","Boat - Titanic copy. I am going to make this a very long description so that I cn see what happens.",24,24,4500,0.5,"This is YET a another text."),
                new LineItem("13","Car- Black. ",93,22,1300,0.2,"This is YET a another text. I am going to make this a very long description so that I cn see what happens."),
                new LineItem("14","item with no comment. ",93,22,1300,0.1),


            };
            return info;
        }
        private void loadAddresses()
        {
            _buyer = new AddressStringWithNames();
            _buyer.HouseNo = "2";
            _buyer.Road = "The Mall";
            _buyer.Address2 = "Peshawar Cantt.";
            _buyer.TownName = "";
            _buyer.CityName = "Peshawar";
            _buyer.StateName = "Puktun Khwa";
            _buyer.CountryName = "pakistan";
            _buyer.Phone = "03318783120";
            _buyer.Attention = "ali KULI aMinuddin";
            //_buyer.Name = "ali KULI aMinuddin";

            _seller = new AddressStringWithNames();
            _seller.HouseNo = "Ali House";
            _seller.Road = "Harbanspura Road";
            _seller.Address2 = "";
            _seller.TownName = "Harbanspura";
            _seller.CityName = "Lahore";
            _seller.StateName = "Punjab";
            _seller.CountryName = "pakistan";
            _seller.Phone = "03318783122";
            _seller.Attention = "Aila azhar/Azhar";
            //_seller.Name = "Aila azhar";

            _shipTo = new AddressStringWithNames();
            _shipTo.HouseNo = "7";
            _shipTo.Road = "The Mall";
            _shipTo.Address2 = "Peshawar Cantt.";
            _shipTo.TownName = "";
            _shipTo.CityName = "Peshawar";
            _shipTo.StateName = "Puktun Khwa";
            _shipTo.CountryName = "pakistan";
            _shipTo.Phone = "03318783120";
            _shipTo.Attention = "Nida/Ali";
            //_shipTo.Name = "Nida";

            _informTo = new AddressStringWithNames();
            _informTo.HouseNo = "7";
            _informTo.Road = "The Mall";
            _informTo.Address2 = "Peshawar Cantt.";
            _informTo.TownName = "";
            _informTo.CityName = "Peshawar";
            _informTo.StateName = "Puktun Khwa";
            _informTo.CountryName = "pakistan";
            _informTo.Phone = "03318783120";
            _informTo.Attention = "Mirnawaz/occupant";

            _webCompany = new AddressStringWithNames();
            _webCompany.HouseNo = "2-H";
            _webCompany.Road = "";
            _webCompany.Address2 = "";
            _webCompany.TownName = "Gulberg";
            _webCompany.CityName = "Lahore";
            _webCompany.StateName = "Punjab";
            _webCompany.CountryName = "pakistan";
            _webCompany.Phone = "03314474120";
            _webCompany.WebAddress = "alikuli@gmail.com";
            //_webCompany.Name = "Nida.com";
        }
    }
}
