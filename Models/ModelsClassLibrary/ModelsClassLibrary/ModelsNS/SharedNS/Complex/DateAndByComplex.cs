﻿using InterfacesLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [ComplexType]

    public class DateAndByComplex : IDateAndBy, IFieldsToLoadFromView
    {

        [Column(TypeName = "DateTime2")]
        [Display(Name = "Date Start (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateStart { get; set; }



        [Display(Name = "Created End (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }


        [NotMapped]
        public DateTime Date_NotNull_Min { get { return Date ?? DateTime.MinValue; } }
        [NotMapped]
        public DateTime Date_NotNull_Max { get { return Date ?? DateTime.MaxValue; } }


        [Display(Name = "By")]
        [MaxLength(50)]

        public string By { get; set; }



        #region Methods

        public void SetToTodaysDate(string byUser)
        {
            Date = DateTime.UtcNow;
            By = byUser;
        }
        public void SetToTodaysDateStart(string byUser)
        {
            DateStart = DateTime.UtcNow;
            By = byUser;

        }

        public void SetDateTo(string byUser, int noOfDays)
        {
            Date = DateTime.UtcNow.AddDays(noOfDays);
            By = byUser;
        }

        public void CopyMeInto(object obj)
        {
            DateAndByComplex dabc = obj as DateAndByComplex;

            if (dabc == null)
            {
                throw new Exception("DateAndByComplex is null in CopyMeInto");
            }

            obj = this.MemberwiseClone();

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