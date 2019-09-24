using InterfacesLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [ComplexType]

    public class DateAndByComplex : IDateAndBy, IFieldsToLoadFromView
    {

        //[Column(TypeName = "DateTime2")]
        //[Display(Name = "Date Start (UTC)")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        //public DateTime? DateStart { get; set; }



        [Display(Name = "Date (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }


        [Display(Name = "Date (UTC)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public DateTime Date_NotNull_Min { get { return Date ?? DateTime.MinValue; } }


        [Display(Name = "Date (UTC)")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [NotMapped]
        public DateTime Date_NotNull_Max { get { return Date ?? DateTime.MaxValue; } }


        [Display(Name = "By")]
        [MaxLength(50)]
        public string By { get; set; }
        public string ByUserId { get; set; }


        #region Methods

        public virtual void SetToTodaysDate(string byUser, string byUserId)
        {
            Date = DateTime.UtcNow;
            By = byUser;
            ByUserId = byUserId;
        }
        //public void SetToTodaysDateStart(string byUser, string byUserId)
        //{
        //    DateStart = DateTime.UtcNow;
        //    By = byUser;
        //    ByUserId = byUserId;

        //}

        public void SetDateTo(string byUser, int noOfDays)
        {
            Date = DateTime.UtcNow.AddDays(noOfDays);
            By = byUser;
        }

        public void AddDate(DateTime date)
        {
            Date = date;
        }
        //public void CopyMeInto(object obj)
        //{
        //    DateAndByComplex dabc = obj as DateAndByComplex;

        //    if (dabc == null)
        //    {
        //        throw new Exception("DateAndByComplex is null in CopyMeInto");
        //    }

        //    obj = this.MemberwiseClone();

        //}


        public virtual void Clear()
        {
            //DateStart = null;
            Date = null;
            By = "";
            ByUserId = "";

        }

        public virtual List<string> FieldsToLoadFromView()
        {
            List<string> lst = new List<string>();
            lst.Add("DateStart");
            return lst;
        }

        #endregion
    }
}