using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class PaymentTermDAL : Repositry<PaymentTerm>
    {


        public PaymentTermDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }





    }
}
