using System;
using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS.RandomNumberGeneratorNS;

using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.CommonAndSharedNS;

using ModelsClassLibrary.ModelsNS.ProductNS;

using UserModels.Models;

namespace DalLibrary.DalNS
{
    /// <summary>
    /// Note. Available units should be automatically calcultated for all scratch cards, regardless.
    /// </summary>
    public class ScratchCardDAL : Repositry<ScratchCard>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public ScratchCardDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }


        #region CRUD Methods



        public ScratchCard FindByNumber(string number)
        {

            ScratchCard sc = FindForName(number);
            return sc;


        }



        /// <summary>
        /// This adds new scratch cards to the database.
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="numberLength"></param>
        public List<string> AddNewScratchNumbersToDb(long qty, int numberLength, int NoOfUnitsInCard)
        {
            var listOfScratchCards = CreateScratchCardListFromGeneratedNumbersList(
                GenerateRandomNumbers(
                qty,
                numberLength))
                .ToList();

            long nextBatchCreateNumber = GetMaxBatchCreateNo() + 1;

            List<string> duplicateNumbers = new List<string>(); //This holds any duplicate clashed numbers

            if (listOfScratchCards != null)
            {
                if (listOfScratchCards.Count() > 0)
                {
                    foreach (var item in listOfScratchCards)
                    {
                        item.BatchNumberCreation = nextBatchCreateNumber;
                        item.TotalNumberOfUnits = NoOfUnitsInCard;
                        item.MetaData.Created.SetToTodaysDateStart();
                        try
                        {
                            Create(item);
                        }
                        catch (ErrorHandlerLibrary.ExceptionsNS.DuplicateScratchCardNumberException)
                        {
                            InternalMessage im = new InternalMessage(null, string.Format("oops! Clashing Scratchcard numbers {0}", item.ToString()));
                            duplicateNumbers.Add(item.Name);
                            continue; //continue adding after this
                        }
                    }
                }
            }

            return duplicateNumbers;
        }



        #endregion


        public override void Fix(ScratchCard entity)
        {
            base.Fix(entity);


        }


        #region Internal Helpers

        private void CheckErrors(ScratchCard entity)
        {
            if (entity.IssueDate > entity.ExpiryDate)
                throw new Exception("Issue date cannot be after Expiry date. Please try again.");

            if (entity.Name.IsNullOrEmpty())
                throw new Exception("Card Number is missing. This is required. Try again.");

        }


        //--------------------------------------------------------------------------------------------------------


        private string MakeNameForIndexMethod(string name)
        {
            return string.Format("{0}", name);
        }


        //--------------------------------------------------------------------------------------------------------




        /// <summary>
        /// This gets all the current list of card numbers from the data base
        /// </summary>
        /// <returns>List<string> of Numbers</returns>
        private List<string> GetCurrentListOfNumberStrings()
        {
            return this.FindAll().Select(x => x.Name).Where(y => y.Trim().Length > 0).ToList();
        }



        //--------------------------------------------------------------------------------------------------------


        /// <summary>
        /// This generates unique random numbers for 
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="numberLength"></param>
        /// <returns></returns>
        private List<string> GenerateRandomNumbers(long qty, int numberLength)
        {
            var rndmg = new RandomNoGenerator(numberLength);
            long maxNumber = rndmg.MaxNumberForLength(numberLength);
            var rgStringList = rndmg.GetStringListOfRandomNumbersWithExclusionList(
                qty,
                0,
                maxNumber,
                GetCurrentListOfNumberStrings()); //this is the current list of scratch card numbers in the system.

            return rgStringList;
        }

        //--------------------------------------------------------------------------------------------------------



        /// <summary>
        /// Finds the max number
        /// </summary>
        /// <returns></returns>
        private long GetMaxBatchCreateNo()
        {
            var allData = this.FindAll();

            if (allData != null)
                if (allData.Count() != 0)
                    return allData.Select(x => x.BatchNumberCreation).Max();
            return 0;

        }


        //--------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Finds the max number
        /// </summary>
        /// <returns></returns>
        private long GetMaxBatchPrintNo()
        {
            var allData = this.FindAll();

            if (allData != null)
                if (allData.Count() != 0)
                    return allData.Select(x => x.BatchNumberPrinting).Max();
            return 0;

        }



        //--------------------------------------------------------------------------------------------------------


        private List<ScratchCard> CreateScratchCardListFromGeneratedNumbersList(List<string> listOfRandomNumbers)
        {
            if (listOfRandomNumbers != null)
            {
                if (listOfRandomNumbers.Count() > 0)
                {
                    List<ScratchCard> listScratchCards = new List<ScratchCard>();
                    //                        long maxPinNumber = GetMaxPinNumber();
                    foreach (var item in listOfRandomNumbers)
                    {
                        ScratchCard sc = this.Factory();
                        sc.Name = item;

                        //maxPinNumber++;
                        //sc.Pin = item;

                        listScratchCards.Add(sc);
                    }
                    return listScratchCards;
                }
            }
            return null;
        }

        #endregion






    }
}
