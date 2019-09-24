using AliKuli.Extentions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS
{
    /// <summary>
    /// This holds the users various identities and is passed around as a neat parameter.
    /// </summary>
    [NotMapped]
    public class UserParameter
    {
        public UserParameter()
        {

        }
        public UserParameter(string userId, string userName, string customerId, string ownerId, string deliverymanId, string salesmanId, string personId, string personName, bool isAdmin, string personAcceptingPaymentForSystemId, string mailerId, string bankId, bool isSuperSalesman, bool isSuperSuperSalesman)
        {
            UserId = userId;
            CustomerId = customerId;
            OwnerId = ownerId;
            DeliverymanId = deliverymanId;
            SalesmanId = salesmanId;
            PersonId = personId;
            UserName = userName;
            IsAdmin = isAdmin;
            PersonName = personName;
            SystemPersonId = personAcceptingPaymentForSystemId;
            MailerId = mailerId;
            BankId = bankId;
            IsSuperSalesman = isSuperSalesman;
            IsSuperSuperSalesman = isSuperSuperSalesman;

        }
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string OwnerId { get; set; }
        public string DeliverymanId { get; set; }
        public string SalesmanId { get; set; }
        public string MailerId { get; set; }
        public string PersonId { get; set; }
        public string PersonName { get; set; }
        public string UserName { get; set; }
        public string SystemPersonId { get; set; }
        public string BankId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCustomer
        {
            get
            {
                return !CustomerId.IsNullOrWhiteSpace();
            }
        }
        public bool IsOwner
        {
            get
            {
                return !OwnerId.IsNullOrWhiteSpace();
            }
        }
        public bool IsDeliveryman
        {
            get
            {
                return !DeliverymanId.IsNullOrWhiteSpace();
            }
        }
        public bool IsSalesman
        {
            get
            {
                return !SalesmanId.IsNullOrWhiteSpace();
            }
        }
        public bool IsSuperSalesman { get; private set; }
        public bool IsSuperSuperSalesman { get; private set; }
        public bool IsBank { get { return !BankId.IsNullOrWhiteSpace(); } }

        public bool IsMailer { get { return !MailerId.IsNullOrWhiteSpace(); } }

        public bool IsLoggedIn { get { return !UserId.IsNullOrWhiteSpace(); } }


    }
}
