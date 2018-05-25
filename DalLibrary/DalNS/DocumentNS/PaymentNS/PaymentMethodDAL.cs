
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class PaymentMethodDAL : Repositry<PaymentMethod>
    {

        //private ApplicationDbContext db;
        //private string user;

        public PaymentMethodDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass("PaymentMethodDAL");

        }



    }
}
