
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class PaymentsAppliedSalesOrderDAL : Repositry<PaymentAppliedSalesOrder>
    {

        //private ApplicationDbContext db;
        //private string user;

        public PaymentsAppliedSalesOrderDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }



    }
}
