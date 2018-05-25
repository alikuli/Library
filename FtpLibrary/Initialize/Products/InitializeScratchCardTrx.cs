using AliKuli.Extentions;
using Bearer.DAL;
using Bearer.Models;
using Bearer6.Models.PlayersNS;
using ModelsClassLibrary.Models;
using ModelsClassLibrary.Models.People;
using ModelsClassLibrary.Models.ProductNS.ScratchCard;
using ModelsClassLibrary.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeScratchCardTrx
    {
        private static ScratchCardTrxDAL dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeScratchCardTrx(ApplicationDbContext db, string user)
        {
            _db = db;
            _user=user;
            dal = new ScratchCardTrxDAL(db, _user);
        }

        private void Add(string scratchCardnumber, string personIdentificationNo, int NoOfUnits, DebitCreditENUM debitOrCredit)
        {
            //ScratchCard entity = new ScratchCard();
            ScratchCardTrx entity = dal.Factory();
            

            try
            {
                ScratchCard sc = new ScratchCardDAL(_db, _user).FindForName(scratchCardnumber);

                if (sc == null)
                    throw new Exception(string.Format("Scratch card Number {0} not found.", scratchCardnumber.ConvertStrNumTo16DigitFormat()));

                entity.ScratchCard = sc;
                entity.ScratchCardID = sc.Id;


                User u = new UserDAL(_db, _user).FindForIdentityCard(personIdentificationNo);

                if (u == null)
                    throw new Exception(string.Format("User with idenification number '{0}' was not found.",personIdentificationNo.ConvertPakistanIdFormat()));


                entity.UsedByUser = u;
                entity.UsedByUserId = u.Id;
                entity.DebitOrCreditEnum = debitOrCredit;
                entity.UnitsForTrx = NoOfUnits;
                entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

                dal.Create(entity);
                dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            { }
            catch (AliKuli.ExceptionsNS.ScratchCardStateException)
            { }

            catch
            {
                throw;
            }
        }
        

        public void Initialize()
        {

            ScratchCardDAL scDAL = new ScratchCardDAL(_db, _user);
            var scratchCardList = scDAL.FindAll().ToList();

            if (scratchCardList == null)
                throw new Exception("There are no scratch cards found");

            if (scratchCardList.Count==0)
                throw new Exception("There are no scratch cards found");
            
            var scratchCardArray = scratchCardList.Take(10).ToArray();
            
            Add(scratchCardArray[0].Name, "1234567890123", 10, DebitCreditENUM.Credit);
            Add(scratchCardArray[0].Name, "1234567890123", 5, DebitCreditENUM.Debit);
            Add(scratchCardArray[0].Name, "1234567890123", 2, DebitCreditENUM.Credit);
            Add(scratchCardArray[0].Name, "1234567890123", 12, DebitCreditENUM.Credit);
            Add(scratchCardArray[0].Name, "1234567890123", 16, DebitCreditENUM.Credit);

            Add(scratchCardArray[1].Name, "1234567890125", 3, DebitCreditENUM.Credit);
            Add(scratchCardArray[1].Name, "1234567890125", 4, DebitCreditENUM.Unknown);
            Add(scratchCardArray[1].Name, "1234567890125", 6, DebitCreditENUM.Credit);
            Add(scratchCardArray[2].Name, "1234567890125", 15, DebitCreditENUM.Credit);
            Add(scratchCardArray[2].Name, "1234567890125", 14, DebitCreditENUM.Credit);

            Add(scratchCardArray[3].Name, "1234567890120", 10, DebitCreditENUM.Credit);
            Add(scratchCardArray[3].Name, "1234567890120", 5, DebitCreditENUM.Debit);
            Add(scratchCardArray[4].Name, "1234567890120", 2, DebitCreditENUM.Credit);
            Add(scratchCardArray[4].Name, "1234567890120", 12, DebitCreditENUM.Credit);
            Add(scratchCardArray[4].Name, "1234567890120", 16, DebitCreditENUM.Credit);
}
        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                _db.ScratchCardTrxs.Remove(item);
            }
            _db.SaveChanges();
            
            list = dal.FindAll(true);
            foreach (var item in list)
            {
                _db.ScratchCardTrxs.Remove(item);
            }
            _db.SaveChanges();
        }


        public void Edit()
        {

            List<ScratchCardTrx> lst = dal.FindAll().ToList();

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