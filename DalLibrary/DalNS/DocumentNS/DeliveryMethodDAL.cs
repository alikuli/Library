using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;


using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class DeliveryMethodDAL : Repositry<DeliveryMethod>
    {


        public DeliveryMethodDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }
    }
}
