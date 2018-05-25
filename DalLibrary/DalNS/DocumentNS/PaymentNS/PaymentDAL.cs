using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;
using DalLibrary.DalNS.DocumentNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using UserModels;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class PaymentDAL : AbstractDocHeaderDAL<Payment>
    {

        //private ApplicationDbContext db;
        //private string user;

        public PaymentDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass("PaymentDAL");

        }


        public override long GetNextDocNumber()
        {
            var lstPayments = _db.Payments.ToList();

            if (lstPayments.IsNullOrEmpty())
                return 1;

            return lstPayments.Max(x => x.DocNo) + 1;
        }


        /// <summary>
        /// This returns list of all unapplied payments for a customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public List<Payment> FindAllUnApplliedPaymentsFor(Customer customer)
        {
            var allunappliedPayments = FindAll()
                .Where(x => x.FromCustomerId == customer.Id && x.Total_Unapplied.Amount != 0)
                .ToList();
            return allunappliedPayments;
        }

        /// <summary>
        /// This returns list of all unapplied payments for a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Payment> FindAllUnApplliedPaymentsFor(ApplicationUser user)
        {
            var allunappliedPayments = FindAll()
                .Where(x => x.FromCustomer.UserId == user.Id && x.Total_Unapplied.Amount != 0)
                .ToList();
            return allunappliedPayments;
        }



    }
}
