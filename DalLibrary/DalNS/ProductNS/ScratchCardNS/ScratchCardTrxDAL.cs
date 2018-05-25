using System;
using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;

using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS;

using UserModels.Models;

using ModelsClassLibrary.ModelsNS.ProductNS;

namespace DalLibrary.DalNS
{
    public class ScratchCardTrxDAL : Repositry<ScratchCardTrx>
    {


        public ScratchCardTrxDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }
        public override string MakeNameForIndexMethod(ScratchCardTrx entity)
        {
            return entity.FullName();
        }

        //private string MakeNameForSelectList(ScratchCardTrx entity)
        //{
        //    return string.Format("Scratch Card No: {0}, Used By: {1} on {2:dd-MMM-yyyy} ",
        //        entity.ScratchCard.Name,
        //        entity.UsedByCustomer.FullName(),
        //        entity.CreatedDate);
        //}


        public override void ErrorCheck(ScratchCardTrx scTrx)
        {
            if (scTrx == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("No Scratchcard Transaction passed. Please try again.");
            //Make sure card is active

            ScratchCard scratchCard = new ScratchCardDAL(_db, _user).FindFor(scTrx.ScratchCardID);

            if (scratchCard == null)
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDataException("No Scratchcard found. Please try again.");



            //Make sure that there is a scratch card
            if (scTrx.ScratchCard == null)
            {
                if (scTrx.ScratchCardID.IsNullOrEmpty())
                    throw new Exception("No scratch card loaded. Please try again.");
                else
                    scTrx.ScratchCard = new ScratchCardDAL(_db, _user).FindFor(scTrx.ScratchCardID);
            }


        }


        /// <summary>
        /// Gets the total number of units available for the scratch card. Note: This does not mean that the units can be used. It just means that
        /// these are how many are left after subtracting all the transactions.
        /// </summary>
        /// <param name="scratchCard"></param>
        /// <returns></returns>
        //public int GetTotalUnitsAvailableForScratchCardID(ScratchCard scratchCard)
        //{

        //    if (scratchCard == null)
        //        throw new ArgumentNullException("No Scratch Card Passed! error in GetTotalUnitsAvailableForScratchCardID.");

        //    var allScratchCardTrxs = this.FindByScratchcardID(scratchCard.Id).ToList();

        //    if (allScratchCardTrxs.Any())
        //        return scratchCard.AvailableUnits;
        //    int totalUnits = 0;

        //    if (allScratchCardTrxs != null && allScratchCardTrxs.Count() > 0)
        //    {
        //        foreach (var item in allScratchCardTrxs)
        //        {
        //            if (item.DebitOrCreditEnum == DebitCreditENUM.Debit)
        //            {
        //                totalUnits += item.UnitsForTrx;
        //            }

        //            if (item.DebitOrCreditEnum == DebitCreditENUM.Credit)
        //            {
        //                totalUnits -= item.UnitsForTrx;
        //            }

        //        }

        //        return totalUnits;
        //    }


        //    return scratchCard.TotalNumberOfUnits - unit;
        //}

        public override void Fix(ScratchCardTrx entity)
        {
            base.Fix(entity);

            //Fix scratch card 

            if (entity.ScratchCardID.IsNullOrEmpty())
            {
                if (entity.ScratchCard == null)
                {
                    throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("ScratchCard is missing. ScratchCardTrxDAL.Fix");
                }
                else
                {
                    entity.ScratchCardID = entity.ScratchCard.Id;
                }
            }
            else
            {
                if (entity.ScratchCard == null)
                {
                    entity.ScratchCard = new ScratchCardDAL(_db, _user).FindFor(entity.ScratchCardID);

                    if (entity.ScratchCard == null)
                    {
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Scratch Card not found. ScratchCardTrxDAL.Fix");
                    }
                }
            }

            //fix Person
            if (entity.UsedByUserId.IsNullOrEmpty())
            {
                if (entity.UsedByUser == null)
                {
                    throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Person is missing. ScratchCardTrxDAL.Fix");
                }
                else
                {
                    entity.UsedByUserId = entity.UsedByUser.Id;
                }
            }
            else
            {
                if (entity.UsedByUser == null)
                {
                    entity.UsedByUser = (User) new UserDAL(_db, _user).FindUserById(entity.UsedByUserId);

                    if (entity.UsedByUser == null)
                    {
                        throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Person not found. ScratchCardTrxDAL.Fix");
                    }
                }
            }


            entity.Name = entity.FullName();


        }


        ///// <summary>
        ///// This returns a list of all scratchcards Trxs with the latest dates, grouped on the scratchcard number.
        ///// This gives the following information
        /////     TotalUnitsAdded: These are units that are added (debited)
        /////     TotalUnitsUsed: Amount of Units Used.
        /////     TotalUnitsUnknown: These are error units.
        ///// </summary>
        ///// <param name="personId"></param>
        ///// <returns></returns>
        //private List<ScratchCardKeyWithUnitsUsedVM> GetTotalUnitsUsedForEachScratchCard()
        //{
        //    var AllScratchCardTrx = FindAll();

        //    var a = from p in AllScratchCardTrx
        //            group p by new {p.ScratchCardID, p.DebitOrCreditEnum} into g
        //            select new
        //            {
        //                g.Key, //This is the Scratchcard Id
        //                TotalUnitsUsed = g.Sum(b => b.UnitsForTrx)
        //            };


        //    var aList = a.ToList();

        //    //now send back the key of the ScratchCard along with units used
        //    if (aList != null && aList.Count > 0)
        //    {
        //        List<ScratchCardKeyWithUnitsUsedVM> scLst = new List<ScratchCardKeyWithUnitsUsedVM>();
        //        foreach (var item in aList)
        //        {
        //            ScratchCardKeyWithUnitsUsedVM sc = new ScratchCardKeyWithUnitsUsedVM();

        //            sc.ScratchCardId = item.Key.ScratchCardID;

        //            //If UNKNOWN
        //            if(item.Key.DebitOrCreditEnum == DebitCreditENUM.Unknown)
        //            {
        //                sc.TotalUnitsUnknwon = item.TotalUnitsUsed;
        //            }

        //            //If DEBIT
        //            if (item.Key.DebitOrCreditEnum == DebitCreditENUM.Debit)
        //            {
        //                sc.TotalUnitsAdded = item.TotalUnitsUsed;
        //            }

        //            //If CREDIT
        //            if (item.Key.DebitOrCreditEnum == DebitCreditENUM.Credit)
        //            {
        //                sc.TotalUnitsUsed = item.TotalUnitsUsed;
        //            }

        //            scLst.Add(sc);
        //        }

        //        //Now we will need to add it all up because all the debits and credits
        //        //have become seperate records for each card.


        //        if(scLst != null || scLst.Count() > 0)
        //        {

        //            //first get all the final numbers into TotalUnitsFinal so we can use linq... linq
        //            //does not work on calculated values
        //            foreach (var item in scLst)
        //            {
        //                item.TotalUnitsFinal = item.GetTotalUnitsFinal();
        //            }

        //            //Now group and add...
        //            var scListSummation = from p in scLst
        //                              group p by p.ScratchCardId into g
        //                              select new
        //                              {
        //                                  g.Key,
        //                                  TotalUnitsFinal = g.Sum(x => x.TotalUnitsFinal),
        //                                  TotalUnitsAdded = g.Sum(x => x.TotalUnitsAdded),
        //                                  TotalUnitsUsed = g.Sum(x => x.TotalUnitsUsed),
        //                                  TotalUnitsUnknown = g.Sum(x => x.TotalUnitsUnknwon),
        //                              };

        //            //now once again create the correct records for passing
        //            if(scListSummation != null && scListSummation.Count() >0)
        //            {

        //                List<ScratchCardKeyWithUnitsUsedVM> scSummationLst = new List<ScratchCardKeyWithUnitsUsedVM>();
        //                foreach (var item in scListSummation)
        //                {
        //                    ScratchCardKeyWithUnitsUsedVM sc = new ScratchCardKeyWithUnitsUsedVM();
        //                    sc.ScratchCardId = item.Key;
        //                    sc.TotalUnitsAdded = item.TotalUnitsAdded;
        //                    sc.TotalUnitsFinal = item.TotalUnitsFinal;
        //                    sc.TotalUnitsUsed = item.TotalUnitsUsed;
        //                    sc.TotalUnitsUnknwon = item.TotalUnitsUnknown;

        //                    scSummationLst.Add(sc);
        //                }

        //                return scSummationLst;
        //            }

        //        }

        //        //return scLst.ToList();
        //    }

        //    return null;
        //}

        /// <summary>
        /// This returns the Id of the last ScratchCardTrx, its date, the ScratchCardId, the last Owner,
        /// </summary>
        /// <returns></returns>
        private List<ScratchCardWithLastTrxVM> GetScratchCardOwnersForEachScratchCard()
        {
            var AllScratchCardTrx = FindAll();

            var lastTrxOfEachScratchCardByMaxId = from p in AllScratchCardTrx
                                                  group p by p.ScratchCardID into g
                                                  select new
                                                  {
                                                      g.Key,
                                                      MaxIdInSeries = g.Max(x => x.Id)
                                                  };
            var aList = lastTrxOfEachScratchCardByMaxId.ToList();





            if (aList != null && aList.Count > 0)
            {

                List<ScratchCardWithLastTrxVM> scLst = new List<ScratchCardWithLastTrxVM>();
                foreach (var item in aList)
                {
                    ScratchCardWithLastTrxVM sc = new ScratchCardWithLastTrxVM();


                    var scTrx = AllScratchCardTrx.FirstOrDefault(c => c.Id == item.MaxIdInSeries);

                    if (scTrx == null)
                    {
                        throw new Exception("No Scratch card trx found. ScratchCardTrxDAL.GetLastTrxForEachScr.");
                    }

                    if (scTrx.ScratchCard == null)
                    {
                        throw new Exception("No Scratch card found in Scratch Card Trx. ScratchCardTrxDAL.GetLastTrxForEachScr.");

                    }


                    //Transfer data...
                    sc.ScratchCardId = scTrx.ScratchCard.Id;
                    sc.LastOwnerPersonId = scTrx.UsedByUserId;
                    sc.ScratchCardTrxId = item.MaxIdInSeries;
                    sc.LastTrxDate = scTrx.MetaData.Created.Date;
                    sc.TotalUnits = scTrx.ScratchCard.TotalNumberOfUnits;

                    scLst.Add(sc);



                }
                return scLst;
            }
            return null;

        }


        ///// <summary>
        ///// This returns all the neccessary information about all the scratch cards.
        /////     ScratchCardTrxId: The Scratch Card Trx Id from which last owner was found
        /////     LastTrxDate: This is the date the last Owner got the card or had the card.
        /////     ScratchCardIdFmOwners: This is the Scratch Card Id from which Owner was found.
        /////     ScratchCardIdFmTtls: This is Scratch Card Id for which Totals were found. This is here as a check. Always ScratchCardIdFmOwners = ScratchCardIdFmTtls. If not, error is thrown.
        /////     LastOwnerPersonId: This is the Person Id of the last Owner
        /////     TotalUnitsUsed: These are the Units used in the scratch card.
        ///// </summary>
        ///// <returns></returns>
        //public List<ScratchCardFullInfoVM> GetScratchCardTotals()
        //{
        //    var scratchCardTotalUnitsUsed = GetTotalUnitsUsedForEachScratchCard();
        //    var scratchCardOwners = GetScratchCardOwnersForEachScratchCard();

        //    if (scratchCardTotalUnitsUsed == null || scratchCardTotalUnitsUsed.Count == 0)
        //    {
        //        throw new Exception("No scratch card Trxs found. ScratchCardTrxDAL.GetAllScratchCardInfo");
        //    }
        //    if (scratchCardOwners == null || scratchCardOwners.Count == 0)
        //    {
        //        throw new Exception("No scratch card Owners found. ScratchCardTrxDAL.GetAllScratchCardInfo");

        //    }

        //    //create the new record with all the information joined.
        //    var AllOwnersWithTotalUsed = from owners in scratchCardOwners
        //                                 join ttls in scratchCardTotalUnitsUsed on owners.ScratchCardId equals ttls.ScratchCardId
        //                                 select new ScratchCardFullInfoVM
        //                                 {
        //                                     LastOwnerPersonId = owners.LastOwnerPersonId,
        //                                     LastTrxDate = owners.LastTrxDate,
        //                                     ScratchCardIdFmOwners = owners.ScratchCardId,
        //                                     ScratchCardIdFmTtls = ttls.ScratchCardId,
        //                                     ScratchCardTrxId = owners.ScratchCardTrxId,
        //                                     TotalUnitsAdded = ttls.TotalUnitsAdded,  //Units added
        //                                     TotalUnitsUsed = ttls.TotalUnitsUsed,    //Units used
        //                                     TotalUnitsUnknown = ttls.TotalUnitsUnknwon, //Units Unkown
        //                                     TotalUnits = owners.TotalUnits
        //                                 };
        //    var AllOwnersWithTotalUsedList = AllOwnersWithTotalUsed.ToList();


        //    //Now a final check on the data, we will see if the Scratch cards received from both the data match... if not, throw an exception.

        //    if(AllOwnersWithTotalUsedList != null && AllOwnersWithTotalUsedList.Count > 0)
        //    {
        //        foreach (var item in AllOwnersWithTotalUsedList)
        //        {
        //            if(item.ScratchCardIdFmOwners != item.ScratchCardIdFmTtls)
        //            {
        //                throw new Exception("Scratch Card Data Corruption. ScratchCardTrxDAL.GetAllScratchCardInfo");
        //            }
        //        }

        //        return AllOwnersWithTotalUsedList;
        //    }

        //    return null;

        //}

        /// <summary>
        /// This returns all the transactions for a scratchcard.
        /// </summary>
        /// <param name="scratchCardId"></param>
        /// <returns></returns>
        public List<ScratchCardTrx> GetAllTrxsForScratchCard(Guid? scratchCardId)
        {
            if (scratchCardId.IsNullOrEmpty())
            {
                throw new Exception("No scratch card received. ScratchCardTrxDAL.ScratchCardTrxsForScratchCard");
            }

            List<ScratchCardTrx> scTrxs = FindAll().Where(x => x.ScratchCardID == scratchCardId).ToList();
            return scTrxs;

        }

        //public List<ScratchCardTrxForStatement> GetAllScratchCardTrxsForPersonStatement(long personId)
        //{

        //}

        public List<ScratchCardTrx> GetAllScratchCardTrxsForUser(Guid userId)
        {
            if (userId.IsNullOrEmpty())
            {
                throw new Exception("No person found. ScratchCardTrxDAL.GetAllScratchCardTrxsForPerson");
            }

            List<ScratchCardTrx> scTrxs = FindAll().Where(x => x.UsedByUserId == userId).ToList();
            return scTrxs;
        }




        #region Find All Region
        public IQueryable<ScratchCardTrx> FindByScratchcardID(Guid? scratchCardId)
        {
            //For IQueryable we need to return an empty list or we get an exception
            if (scratchCardId.IsNullOrEmpty())
                return new List<ScratchCardTrx>().AsQueryable();

            var ScratchCardTrxs = this.SearchFor(x => x.ScratchCardID == scratchCardId);
            return ScratchCardTrxs;

        }



        #endregion

    }
}
