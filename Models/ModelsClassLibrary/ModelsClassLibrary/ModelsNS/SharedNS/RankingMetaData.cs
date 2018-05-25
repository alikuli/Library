using AliKuli.Extentions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.UploadedFileNS
{
    /// <summary>
    /// This is used in Uploads. Use the following to update:
    ///     Rank: updates RankCurrent, RankPrevious and RankBeforePrevious. It returns RankCurrent. Not presisted.
    ///     ViewUserId: Updates ViewUserIdCurr, ViewUserIdLast, ViewUserIdBeforeLast, NoOfViews. Update this when Viewed. Not presisted.
    ///     SelectUserId: Updates SelectUserIdCurr, SelectUserIdLast, SelectUserIdBeforeLast, NoOfSelects. Update this when Selected. Not presisted.
    /// </summary>
    [ComplexType]

    public class RankingMetaData
    {


        #region Ranking
        /// <summary>
        /// This is the current rank. Use Rank to load this
        /// </summary>
        public int RankCurrent { get; set; }

        /// <summary>
        /// Use Rank to load this
        /// </summary>
        public int RankPrevious { get; set; }

        /// <summary>
        /// Use Rank to load this
        /// </summary>
        public int RankBeforePrevious { get; set; }

        /// <summary>
        /// This is not saved. Use this to load rank
        /// </summary>
        [NotMapped]
        public int Rank
        {
            get
            {
                return RankCurrent;
            }
            set
            {
                RankBeforePrevious = RankPrevious;
                RankPrevious = RankCurrent;
                RankCurrent = value;
            }
        }

        #endregion


        #region Views



        [NotMapped]
        public string ViewUserId
        {
            get
            {
                return ViewUserIdCurr;
            }

            set
            {
                string dataIn = value;
                AddOneNoOfViews();
                if (!dataIn.IsNullOrWhiteSpace())
                {
                    ViewUserIdBeforeLast = ViewUserIdLast;
                    ViewUserIdLast = ViewUserIdCurr;
                    ViewUserIdCurr = dataIn;
                }
            }
        }

        public int NoOfViews { get; set; }
        public DateTime FirstViewDate { get; set; }
        public DateTime LastViewDate { get; set; }

        /// <summary>
        /// Sets the date if it is Min Max or Null
        /// </summary>
        private void SetFirstViewDate()
        {
            if (FirstViewDate.IsNull())
            {
                FirstViewDate = DateTime.UtcNow;
                return;
            }

            if (FirstViewDate == DateTime.MaxValue)
            {
                FirstViewDate = DateTime.UtcNow;
                return;
            }

            if (FirstViewDate == DateTime.MinValue)
            {
                FirstViewDate = DateTime.UtcNow;
                return;
            }
        }



        private void AddOneNoOfViews()
        {
            NoOfViews++;
            LastViewDate = DateTime.UtcNow;
            SetFirstViewDate();
        }
        private void AddOneNoOfSelects()
        {
            NoOfSelects++;
            LastSelectDate = DateTime.UtcNow;
            SetFirstSelectDate();
        }


        public string ViewUserIdCurr { get; set; }
        public string ViewUserIdLast { get; set; }
        public string ViewUserIdBeforeLast { get; set; }



        public double AverageViewsThisMonth { get; set; }
        public double AverageViewsLastMonth { get; set; }
        public double AverageViewsB4LastMonth { get; set; }

        #endregion

        #region Selects

        [NotMapped]
        public string SelectUserId
        {
            get
            {
                return SelectUserIdCurr;
            }

            set
            {
                string dataIn = value;
                AddOneNoOfSelects();
                if (!dataIn.IsNullOrWhiteSpace())
                {
                    SelectUserIdBeforeLast = SelectUserIdLast;
                    SelectUserIdLast = SelectUserIdCurr;
                    SelectUserIdCurr = dataIn;
                }
            }
        }


        /// <summary>
        /// Sets the date if it is Min Max or Null
        /// </summary>
        private void SetFirstSelectDate()
        {
            if (FirstSelectDate.IsNull())
            {
                FirstSelectDate = DateTime.UtcNow;
                return;
            }

            if (FirstSelectDate == DateTime.MaxValue)
            {
                FirstSelectDate = DateTime.UtcNow;
                return;
            }

            if (FirstSelectDate == DateTime.MinValue)
            {
                FirstSelectDate = DateTime.UtcNow;
                return;
            }
        }


        //presisted data

        public DateTime FirstSelectDate { get; set; }

        public DateTime LastSelectDate { get; set; }

        public int NoOfSelects { get; set; }

        public string SelectUserIdCurr { get; set; }
        public string SelectUserIdLast { get; set; }
        public string SelectUserIdBeforeLast { get; set; }

        #endregion


    }
}
