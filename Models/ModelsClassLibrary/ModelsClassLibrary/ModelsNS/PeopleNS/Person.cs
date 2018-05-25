//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using Bearer6.MyPrograms.Utilities;
//using ModelsClassLibrary.Models.CommonAndShared;
//using ModelsClassLibrary.Models.People;
//using AliKuli.Extentions;

//namespace ModelsClassLibrary.Models.PeopleNS.SupportingTables
//{
//    public abstract class Person:IPerson
//    {
//        public bool IsEncrypted { get; set; }

//        #region IPerson Members

//        public string IdentificationNo { get; set; }

//        public string FName { get; set; }

//        public string LName { get; set; }

//        public string MName { get; set; }

//        public SexENUM Sex { get; set; }

//        public SonOfWifeOfDotOfENUM SonOfOrWifeOf { get; set; }
//        public string NameOfFatherOrHusband { get; set; }


//        public void LoadFrom(IPerson p)
//        {
//            IdentificationNo = p.IdentificationNo;
//            FName = p.FName;
//            LName = p.LName;
//            MName = p.MName;
//            Sex = p.Sex;
//            SonOfOrWifeOf = p.SonOfOrWifeOf;
//            NameOfFatherOrHusband = p.NameOfFatherOrHusband;
//        }

//        #region EncryptDecript Fields

//        #region LName

//        public virtual string LNameDecrypted { get; set; }

//        public string LName
//        {
//            get
//            {
//                if (IsEncrypted)
//                    LNameDecrypted = Encryption.Decrypt_LName(LName);
//                else
//                    LNameDecrypted = LName;
//                return LNameDecrypted;
//            }
//            set
//            {
//                LNameDecrypted = value;

//                if (IsEncrypted)
//                    LName = Encryption.Encrypt_LName(LNameDecrypted);
//                else
//                    LName = LNameDecrypted;
//            }
//        }


//        #endregion
//        #region IdentificationNoEncryptDecrypt

//        public string IdentificationNoDecrypted { get; set; }
//        public string IdentificationNoEncryptDecrypt
//        {
//            get
//            {
//                if (IsEncrypted)
//                    IdentificationNoDecrypted = Encryption.Decrypt_IdentificationNumber(IdentificationNo);
//                else
//                    IdentificationNoDecrypted = IdentificationNo;

//                return IdentificationNoDecrypted;
//            }
//            set
//            {
//                IdentificationNoDecrypted = value;
//                if (IsEncrypted)
//                    IdentificationNo = Encryption.Encrypt_IdentificationNumber(IdentificationNoDecrypted);
//                else
//                    IdentificationNo = IdentificationNoDecrypted;
//            }
//        }


//        #endregion
//        //#region ContactPhoneEncryptDecrypt


//        //public virtual string ContactPhoneDecrypted { get; set; }

//        ////public string ContactPhoneEncryptDecrypt
//        ////{
//        ////    get
//        ////    {
//        ////        if (IsEncrypted)
//        ////            ContactPhoneDecrypted = Encryption.Decrypt_ContactNo(ContactPhone);
//        ////        else
//        ////            ContactPhoneDecrypted = ContactPhone;

//        ////        return ContactPhoneDecrypted;
//        ////    }
//        ////    set
//        ////    {
//        ////        ContactPhoneDecrypted = value;
//        ////        if (IsEncrypted)
//        ////            ContactPhone = Encryption.Encrypt_ContactNo(ContactPhoneDecrypted);
//        ////        else
//        ////            ContactPhone = ContactPhoneDecrypted;
//        ////    }
//        ////}

//        //#endregion
//        #region FName

//        public string FNameDecrypted { get; set; }


//        public string FName
//        {
//            get
//            {
//                if (IsEncrypted)
//                    FNameDecrypted = Encryption.Decrypt_FName(FName);
//                else
//                    FNameDecrypted = FName;

//                return FNameDecrypted;
//            }
//            set
//            {
//                FNameDecrypted = value;
//                if (IsEncrypted)
//                    FName = Encryption.Encrypt_FName(FNameDecrypted);
//                else
//                    FName = FNameDecrypted;
//            }
//        }


//        #endregion
//        //#region WebAddressEncryptDecrypt


//        //public virtual string WebAddressDecrypted { get; set; }

//        //public string WebAddressEncryptDecrypt
//        //{
//        //    get
//        //    {
//        //        if (IsEncrypted)
//        //            WebAddressDecrypted = Encryption.Decrypt_Webaddress(WebAddress);
//        //        else
//        //            WebAddressDecrypted = WebAddress;

//        //        return WebAddressDecrypted;
//        //    }
//        //    set
//        //    {
//        //        WebAddressDecrypted = value;
//        //        if (IsEncrypted)
//        //            WebAddress = Encryption.Encrypt_Webaddress(WebAddressDecrypted);
//        //        else
//        //            WebAddress = WebAddressDecrypted;
//        //    }
//        //}


//        //#endregion
//        //#region RoadEncryptDecrypt


//        //public virtual string RoadDecrypted { get; set; }
//        //public string RoadEncryptDecrypt
//        //{
//        //    get
//        //    {
//        //        if (IsEncrypted)
//        //            RoadDecrypted = Encryption.Decrypt_Road(Road);
//        //        else
//        //            RoadDecrypted = Road;

//        //        return RoadDecrypted;
//        //    }
//        //    set
//        //    {
//        //        RoadDecrypted = value;
//        //        if (IsEncrypted)
//        //            Road = Encryption.Encrypt_Road(RoadDecrypted);
//        //        else
//        //            Road = RoadDecrypted;
//        //    }
//        //}



//        //#endregion





//        #endregion

//        public string Helper_CreateFullName()
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append(FName.ToTitleCase());

//            if (!MName.IsNullOrEmpty())
//            {
//                sb.Append(" ");
//                sb.Append(MName.ToTitleCase());
//            }
//            if (!LName.IsNullOrEmpty())
//            {
//                sb.Append(" ");
//                sb.Append(LName.ToTitleCase());
//            }

//            if (SonOfOrWifeOf != SonOfWifeOfDotOfENUM.Unknown)
//            {

//                sb.Append(" ");
//                sb.Append(AliKuli.SonOfWifeOfDotOfStringValues.StringValues(SonOfOrWifeOf));
//                sb.Append(" ");
//                sb.Append(NameOfFatherOrHusband.ToTitleCase());
//            }

//            //Add the Identity Card Number to make sure there are no unneccessary duplicates in the names
//            //The Id card will be the item tjhat will bring about the uniqueness
//            if (!IdentificationNo.IsNullOrEmpty())
//            {
//                sb.Append(" ");
//                sb.Append(string.Format("- National ID: {0}", IdentificationNoEncryptDecrypt.ToPakistanCnicFormat()));
//            }

//            return sb.ToString();

//        }
//        #endregion
//    }
//}