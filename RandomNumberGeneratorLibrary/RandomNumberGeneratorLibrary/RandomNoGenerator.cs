using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ErrorHandlerLibrary.ExceptionsNS;
using AliKuli.Extentions;
namespace AliKuli.UtilitiesNS.RandomNumberGeneratorNS
{

    public class HashCustom<T> where T : class
    {
        private Hashtable h = new Hashtable();

        public void Add(T o)
        {
            h[o] = null;
        }

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return h.Keys.GetEnumerator();
        }


        public long Count
        {
            get
            {
                return h.Count;
            }
        }

        public List<T> ToList()
        {
            return h.Cast<T>().ToList();

        }

        public void Remove(T o)
        {
            h.Remove(o);
        }


        #endregion
    }



    public class Hashs
    {
        private HashCustom<string> _old = new HashCustom<string>();
        private HashCustom<string> _new = new HashCustom<string>();
        private HashCustom<string> _all = new HashCustom<string>();
        private long _newQtyRequred;

        public Hashs()
        {
            _old = new HashCustom<string>();
            _new = new HashCustom<string>();
            _all = new HashCustom<string>();

        }


        public List<string> NewToList()
        {
            return _new.ToList();
        }

        public List<string> OldToList()
        {
            return _old.ToList();
        }


        public List<string> AllToList()
        {
            return _all.ToList();
        }

        /// <summary>
        /// Add to the old list
        /// </summary>
        /// <param name="o"></param>
        public void AddToOld(string o)
        {
            _old.Add(o);
            AddToAll(o);
        }



        /// <summary>
        /// Add to the new list
        /// </summary>
        /// <param name="o"></param>
        public void AddToNew(string o)
        {
            _new.Add(o);
            AddToAll(o);

        }

        /// <summary>
        /// Add to the old + new 
        /// </summary>
        /// <param name="o"></param>
        private void AddToAll(string o)
        {
            _all.Add(o);

        }

        private List<string> ListOfDuplicatesInNewAndOld()
        {

            var lstOld = _old.ToList();
            var lstNew = _new.ToList();

            if (lstOld.IsNullOrEmpty())
            {
                if (lstNew.IsNullOrEmpty())
                    return null;
                else
                    lstOld = lstNew;
            }
            else
            {
                if (lstNew.IsNullOrEmpty())
                {
                    //do nothing
                }
                else
                {
                    lstOld.Concat(lstNew);
                }

            }


            var lstOfDuplicates = lstOld.GroupBy(s => s)
                .Where(s => s.Count() > 1)
                .Select(g => g.Key.ToString())
                .ToList();


            return lstOfDuplicates;


        }
        /// <summary>
        /// Check to see if duplicates were generated.
        /// </summary>
        public bool HasDuplicates
        {
            get
            {
                return !(OldQty + NewQty == AllQty);
            }
        }

        /// <summary>
        /// This is her new Qty
        /// </summary>
        public long AllQty
        {
            get
            {
                return _all.Count;
            }
        }

        /// <summary>
        /// The old quantity of random numbers
        /// </summary>
        public long OldQty
        {
            get
            {
                return _old.Count;
            }
        }

        /// <summary>
        /// Newly generated random number quantity
        /// </summary>
        public long NewQty
        {
            get
            {
                return _new.Count;
            }
        }

        /// <summary>
        /// This is the the new qty required.
        /// </summary>
        public long NewQtyRequired
        {
            get { return (_newQtyRequred + OldQty) - AllQty; }
            set { _newQtyRequred = value; }
        }

        public long BalanceRequired
        {
            get
            {
                if (HaveRequiredTotal)
                    return 0;
                return (NewQtyRequired - NewQty);
            }
        }

        /// <summary>
        /// This is the final wanted qty i.e. new qty + old qty
        /// </summary>
        public long FinalQtyWanted
        {
            get { return NewQtyRequired + OldQty; }
        }

        public bool HaveRequiredTotal
        {
            get
            {
                RemoveDuplicatesFromNewList();
                return NewQty >= NewQtyRequired;
            }
        }


        public void RemoveDuplicatesFromNewList()
        {
            var duplicates = ListOfDuplicatesInNewAndOld();
            if (duplicates.IsNullOrEmpty())
                return;

            foreach (var dup in duplicates)
            {
                _new.Remove(dup);
                //we do not have to remove it from All, because it never made it there.
                //it is a key so duplicaes cannot exist in all.
            }
        }
    }






    public class RandomNoGenerator
    {

        private readonly ErrorSet _err;
        private readonly long _maxNoLength;
        public RandomNoGenerator(long maxNoLength)
        {
            _maxNoLength = maxNoLength;

            _err = new ErrorSet();
            _err.SetLibAndClass("RandomNumberGeneratorLibrary", "RandomNoGenerator");
        }
        /// <summary>
        /// This creates a set of generated numbers. This will need to be checked with
        /// </summary>
        /// <param name="Quantity"></param>
        /// <param name="minNumber"></param>
        /// <param name="maxNumber"></param>
        /// <returns></returns>
        public ICollection<long> GenerateNumbers(long Quantity, Int64 minNumber, Int64 maxNumber)
        {
            HashSet<long> hashSetOfRandomNumbers = new HashSet<long>();
            //long totalProcessed = 0;

            using (RNGCryptoServiceProvider randonNoProvider = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[8];
                //Console.WriteLine("Entering loop...");
                while (true)
                {
                    //fill buffer
                    randonNoProvider.GetBytes(data);

                    //convert to int 64
                    long longNumber = BitConverter.ToInt64(data, 0) % maxNumber;

                    if (longNumber >= minNumber && longNumber <= maxNumber)
                    {
                        hashSetOfRandomNumbers.Add(longNumber);
                        if (hashSetOfRandomNumbers.Count() > Quantity - 1)
                            break;
                    }
                }

            }
            return hashSetOfRandomNumbers;
        }





        public List<string> GetStringListOfRandomNumbers(long quantity, Int64 minNumber, Int64 maxNumber)
        {
            List<long> lstOfRandomNumber = GenerateNumbers(quantity, minNumber, maxNumber).ToList();

            if (lstOfRandomNumber == null)
                return null;

            List<string> lstStringOfRandomNumbers = new List<string>();
            int numberLength = maxNumber.ToString().Length;

            foreach (var item in lstOfRandomNumber)
            {
                string s = PrintCorrectFormatFor(item);
                lstStringOfRandomNumbers.Add(s);
            }

            return lstStringOfRandomNumbers;
        }

        /// <summary>
        /// This method fi
        /// nds the duplicates in the List of strings.
        /// </summary>
        /// <param name="incomingList"></param>
        /// <returns></returns>
        private List<string> GetDuplicates(List<string> incomingList)
        {
            if (incomingList.IsNullOrEmpty())
                return null;

            return incomingList.GroupBy(x => x)
                .Where(g => g.LongCount() > 1)
                .Select(y => y.Key)
                .ToList();

        }

        public List<string> GetStringListOfRandomNumbersWithExclusionList(long quantity, Int64 minNumber, Int64 maxNumber, List<string> exclusionList)
        {
            try
            {
                //This list will hold the final values                  

                Hashs cardNumbers = new Hashs();
                cardNumbers.NewQtyRequired = quantity;


                if (!exclusionList.IsNullOrEmpty())
                {
                    foreach (var item in exclusionList)
                    {
                        cardNumbers.AddToOld(item);
                    }
                }

                var numberLength = maxNumber.ToString().Length;

                do
                {
                    //This is a temporary list
                    var rgStringList = GetStringListOfRandomNumbers(cardNumbers.NewQtyRequired, 0, MaxNumberForLength(numberLength));

                    //Add the values to the final list
                    foreach (var item in rgStringList)
                    {
                        cardNumbers.AddToNew(item);
                    }

                } while (cardNumbers.BalanceRequired > 0);

                return cardNumbers.NewToList();
            }
            catch
            {
                throw;
            }

        }
        public string PrintCorrectFormatFor(long number)
        {
            switch (_maxNoLength)
            {
                case 1:
                    return number.ToString("0");

                case 2:
                    return number.ToString("00");

                case 3:
                    return number.ToString("000");

                case 4:
                    return number.ToString("0000");

                case 5:
                    return number.ToString("00000");

                case 6:
                    return number.ToString("000000");

                case 7:
                    return number.ToString("0000000");

                case 8:
                    return number.ToString("00000000");

                case 9:
                    return number.ToString("000000000");

                case 10:
                    return number.ToString("0000000000");

                case 11:
                    return number.ToString("00000000000");

                case 12:
                    return number.ToString("000000000000");

                case 13:
                    return number.ToString("0000000000000");

                case 14:
                    return number.ToString("00000000000000");

                case 15:
                    return number.ToString("000000000000000");

                case 16:
                    return number.ToString("0000000000000000");

                default: throw new Exception(string.Format("The number size of length '{0}' is not supported. Try again", _maxNoLength));

            }

        }

        public long MaxNumberForLength(int noLength)
        {
            switch (noLength)
            {
                case 1: return 9;
                case 2: return 99;
                case 3: return 999;
                case 4: return 9999;
                case 5: return 99999;
                case 6: return 999999;
                case 7: return 9999999;
                case 8: return 99999999;
                case 9: return 999999999;
                case 10: return 9999999999;
                case 11: return 99999999999;
                case 12: return 999999999999;
                case 13: return 9999999999999;
                case 14: return 99999999999999;
                case 15: return 999999999999999;
                case 16: return 9999999999999999;
                case 17: return 99999999999999999;
                case 18: return 999999999999999999;
                default: throw new Exception(string.Format("The number size of length '{0}' is not supported. Try again", _maxNoLength));
            }
        }
    }


}

