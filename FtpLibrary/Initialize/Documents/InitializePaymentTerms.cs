using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.DeliveryMethodNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializePaymentTerms
    {
        private static PaymentTermDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializePaymentTerms(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new PaymentTermDAL(_db, user);
        }

        private void Add(string theName, decimal earlyPaymentDiscount, int noOfDaysCredit, int noOfDaysEarlyPayment)
        {
            PaymentTerm entity = dal.Factory();
            
            entity.Name = theName;
            entity.EarlyPaymentDiscount = earlyPaymentDiscount;
            entity.NoOfDaysCredit = noOfDaysCredit;
            entity.NoOfDaysEarlyPayment = noOfDaysEarlyPayment;
            entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(),DateTime.UtcNow.ToLongDateString());

            try
            {
                dal.Create(entity);
                dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            { }

            catch
            {
                throw;
            }
        }
        

        public void Initialize()
        {
            Add("Net 30",0.03m,30,0);
            Add("Cash",0,0,0);
            Add("Net 45",0.04m,45,10);
        }
        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.PaymentTerms.Remove(item);
            }
            db.SaveChanges();
            
            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.PaymentTerms.Remove(item);
            }
            db.SaveChanges();
        }

        public void Edit()
        {

            List<PaymentTerm> lst = dal.FindAll().ToList();

            if (lst == null)
                throw new Exception(string.Format("No '{0}' found to edit.", dal.GetSelfClassName()));

            foreach (var item in lst)
            {
                item.Name += "*";
                dal.Update(item);
                try
                {
                    dal.Save();
                }
                catch (Exception e)
                {
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}