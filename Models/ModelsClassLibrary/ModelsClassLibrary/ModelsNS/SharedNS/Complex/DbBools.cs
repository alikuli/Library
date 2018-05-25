
//using System;
//using System.ComponentModel.DataAnnotations.Schema;
//using ModelsClassLibrary.ModelsNS.SharedNS.Common;
//namespace ModelsClassLibrary.ModelsNS.SharedNS
//{
//    /// <summary>
//    /// These control how the record is stored in the db
//    /// </summary>
//    [ComplexType]
//    [Serializable]
//    public class DbBoolsStorageComplex : ICopyMe
//    {
//        public DbBoolsStorageComplex()
//        {
//            IsEncrypted = true;
//            IsUniqueName = true;
//        }
//        /// <summary>
//        /// When this is true, then the Name is Unique
//        /// </summary>
//        public bool IsUniqueName { get; set; }

//        /// <summary>
//        /// If this is true, then overall encryption for the record is on.
//        /// </summary>
//        public bool IsEncrypted { get; set; }

//        #region ICopyMe Members

//        public void CopyMeInto(object obj)
//        {
//            DbBoolsStorageComplex dbcmplx = obj as DbBoolsStorageComplex;

//            if (dbcmplx == null)
//            {
//                throw new Exception("The obj variable in DbBoolsStorageComplex.CopyMeInto is null");
//            }

//            dbcmplx = this.MemberwiseClone() as DbBoolsStorageComplex;

//            if (dbcmplx == null)
//            {
//                throw new Exception("Copy is null in DbBoolsStorageComplex");
//            }

//        }

//        #endregion
//    }
//}
