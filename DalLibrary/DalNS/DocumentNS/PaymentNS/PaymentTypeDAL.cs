using AliKuli.Extentions;

using EnumLibrary.EnumNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class PaymentTypeDAL : Repositry<PaymentType>
    {

        public PaymentTypeDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());

        }

        public void InitializeFromEnumAndSave()
        {

            var eList = EnumExtention<PaymentTypeENUM>.ToList();

            if (eList.IsNullOrEmpty())
                return;

            //fix the names


            foreach (var item in eList)
            {
                PaymentType c = Factory();
                c.Name = item.ToSentence();
                c.MetaData.IsAutoCreated = true;

                try
                {
                    Create(c);
                }
                catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
                {
                }
            }

            Save();


        }


    }
}
