using ModelsClassLibrary.ModelsNS.AddressNS;

namespace InvoiceNS
{
    public class Addresses
    {
        public Addresses() :
            this(new Address(), new Address(), new Address(), new Address(), new Address())
        {

        }
        public Addresses(AddressStringWithNames seller, AddressStringWithNames customer, AddressStringWithNames shipTo, AddressStringWithNames informTo, AddressStringWithNames webCompany)
        {
            Seller = seller;
            Customer = customer;
            WebCompany = webCompany;
            ShipTo = shipTo;
            InformTo = informTo;
        }
        public AddressStringWithNames Seller { get; set; }
        public AddressStringWithNames Customer { get; set; }
        public AddressStringWithNames WebCompany { get; set; }
        public AddressStringWithNames ShipTo { get; set; }
        public AddressStringWithNames InformTo { get; set; }


    }
}
