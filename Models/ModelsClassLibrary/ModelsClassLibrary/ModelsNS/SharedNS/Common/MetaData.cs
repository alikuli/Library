using InterfacesLibrary.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    /// <summary>
    /// Any class thatcontains MetaData will do Encryption.
    /// </summary>

    public abstract class MetaData : IMetaData
    {

        public MetaData()
        {
            Created = new DateAndByComplex();
            Modified = new DateAndByComplex();
            Deleted = new DateAndByComplex();
            UnDeleted = new DateAndByComplex();
            Created.SetToTodaysDateStart("","");

            IsInactive = false;
            IsEditLocked = false;
            IsDeleteLocked = false;
            IsDeleted = false;
            //IsEncrypted = false;

        }


        #region Bools

        [Display(Name = "Auto Created?")]

        public bool IsEditLocked { get; set; }
        public bool IsDeleteLocked { get; set; }

        [Display(Name = "Inactive?")]
        public bool IsInactive { get; set; }

        #endregion





        //[MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
        //[DataType(DataType.MultilineText)]
        //public string Comment { get; set; }

        #region Methods
        public string GetCreatedTicks()
        {
            if (Created.DateStart == default(DateTime))
                return "";

            Check_CreatedTicksStart();
            DateTime dt = Created.DateStart ?? default(DateTime);
            return dt.Ticks.ToString();
        }

        private void Check_CreatedTicksStart()
        {
            if (Created.DateStart == null)
                throw new Exception("Created Date Start is null");

            if (Created.DateStart == DateTime.MaxValue)
                throw new Exception("Created Date Start is Max value");

            if (Created.DateStart == DateTime.MinValue)
                throw new Exception("Created Date Start is min value");
        }

        public virtual void SelfErrorCheck()
        {
            Check_CreatedTicksStart();
        }

        public string GetSelfMethodName()
        {
            var method = System.Reflection.MethodBase.GetCurrentMethod();
            var fullName = string.Format("{0}.{1}({2})", method.ReflectedType.FullName, method.Name, string.Join(",", method.GetParameters().Select(o => string.Format("{0} {1}", o.ParameterType, o.Name)).ToArray()));
            return fullName;
        }


        /// <summary>
        /// This is supposed to check all fields of the record.
        /// </summary>

        public string GetSelfClassName()
        {
            return this.GetType().Name;
        }

        public void LoadFrom(IMetaData icommonNoId)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region IsBool



        ///// <summary>
        ///// When this is true, then the Name is Unique
        ///// </summary>
        //public bool IsUniqueName { get; set; }

        ///// <summary>
        ///// If this is true, then overall encryption for the record is on.
        ///// </summary>
        //public bool IsEncrypted { get; set; }


        [Display(Name = "Deleted?")]

        public bool IsDeleted { get; set; }

        #endregion

        #region Complex DateAndByComplex


        [Display(Name = "Created (UTC)")]
        public DateAndByComplex Created { get; set; }

        [Display(Name = "Deleted (UTC)")]
        public DateAndByComplex Deleted { get; set; }

        [Display(Name = "Modified (UTC)")]
        public DateAndByComplex Modified { get; set; }

        [Display(Name = "Undeleted (UTC)")]
        public DateAndByComplex UnDeleted { get; set; }

        #endregion

        #region ICopyMe Members

        //public void CopyMeInto(object obj)
        //{
        //    MetaData md = obj as MetaData;

        //    if(md == null)
        //    {
        //        throw new Exception("MetaData is null in CopyMeInto");
        //    }

        //    md = this.MemberwiseClone() as MetaData;

        //    //Make deep copies
        //    this.Created.CopyMeInto(md.Created);
        //    this.Modified.CopyMeInto(md.Modified);
        //    this.Deleted.CopyMeInto(md.Deleted);
        //    this.UnDeleted.CopyMeInto(md.UnDeleted);


        //    }

        #endregion
    }
}