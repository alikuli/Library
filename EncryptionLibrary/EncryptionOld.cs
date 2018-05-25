using AliKuli.UtilitiesNS;

namespace AliKuli.UtilitiesNS.Encryption
{
   public static class EncryptionOld
    {

        #region Constants
        const string PURPOSE_NATIONALID_CARD = "This is the user Id card 1 2 3 4 5 6 7 8";
        const string PURPOSE_FNAME = "This is the firstName 1 2 3 4 5 6 7 8";
        const string PURPOSE_MNAME = "This is the middle Name 1 2 3 4 5 6 7 8";
        const string PURPOSE_LNAME = "This is the Last Name 1 2 3 4 5 6 7 8";
        const string PURPOSE_NAME_FATHER_HUSBAND = "This is the Name of father or husband 1 2 3 4 5 6 7 8";
        const string PURPOSE_USERNAME = "This is the User Name 1 2 3 4 5 6 7 8";
        const string PURPOSE_ROAD = "This is the Address1 1 2 3 4 5 6 7 8";
        const string PURPOSE_CONTACT_NUMBER = "This is the contact number 1 2 3 4 5 6 7 8";
        const string PURPOSE_EMAIL = "This is the User email 1 2 3 4 5 6 7 8";
        const string PURPOSE_WEBADDRESS = "This is the Webaddress 1 2 3 4 5 6 7 8";
        const string PURPOSE_NAME = "This is the name 1 2 3 4 5 6 7 8";
        const string PURPOSE_ADDRESS2 = "This is the Address2 1 2 3 4 5 6 7 8";
        const string PURPOSE_HOUSE = "This is the House 1 2 3 4 5 6 7 8";
        const string PURPOSE_ZIP = "This is the Zip 1 2 3 4 5 6 7 8";
        const string PURPOSE_ATTENTION = "This is the Attention 1 2 3 4 5 6 7 8"; 
        #endregion

       #region Purpose  Helpers
        //private static string[] Purpose_NationalIdCardArray
        //{
        //    get
        //    {
        //        return MakePurpose(PURPOSE_NATIONALID_CARD);
        //    }
        //}
        //private static string[] Purpose_FName
        //{
        //    get
        //    {
        //        return MakePurpose(PURPOSE_FNAME);
        //    }
        //}
        //private static string[] Purpose_Road
        //{
        //    get
        //    {
        //        return MakePurpose(PURPOSE_ROAD);
        //    }
        //}

        private static string[] MakePurpose(string purpose)
       {
           string[] stringArray = { purpose };
           return stringArray;
       }


       #endregion




        #region IdentificationNumber

		public static string Encrypt_IdentificationNumber(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_NATIONALID_CARD);
        }


        public static string Decrypt_IdentificationNumber(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_NATIONALID_CARD);

        }
 
	    #endregion
        #region FName

	    public static string Encrypt_FName(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_FNAME);

        }
        
       
        public static string Decrypt_FName(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_FNAME);

        }
 
	    #endregion
        #region MName

        public static string Encrypt_MName(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_MNAME);

        }


        public static string Decrypt_MName(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_MNAME);

        }

        #endregion

        #region NameOfFatherOrHusband

        public static string Encrypt_NameOfFatherOrHusband(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_NAME_FATHER_HUSBAND);

        }


        public static string Decrypt_NameOfFatherOrHusband(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_NAME_FATHER_HUSBAND);

        }

        #endregion

        #region LName
		
        public static string Encrypt_LName(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_LNAME);
        }
        public static string Decrypt_LName(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_LNAME);
        }
 
	    #endregion
        #region Road

        public static string Encrypt_Road(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_ROAD);

        }
        public static string Decrypt_Road(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_ROAD);

        }

        #endregion
        #region Encrypt_Address2
        public static string Encrypt_Address2(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_ADDRESS2);
        }
        public static string Decrypt_Address2(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_ADDRESS2);
        }

        #endregion
        #region UserName
		
        public static string Encrypt_UserName(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_USERNAME);
        }
        public static string Decrypt_UserName(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_USERNAME);
        }
 
	    #endregion
        #region ContactNo
		
        public static string Encrypt_ContactNo(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_CONTACT_NUMBER);
        }
        public static string Decrypt_ContactNo(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_CONTACT_NUMBER);
        }
 
	    #endregion
        #region Email
		        
       public static string Encrypt_Email(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_EMAIL);
        }
        public static string Decrypt_Email(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_EMAIL);
        } 
	    #endregion
        #region Webaddress
		public static string Encrypt_Webaddress(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_WEBADDRESS);
        }
        public static string Decrypt_Webaddress(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_WEBADDRESS);
        } 
	    #endregion
        #region Name
        public static string Encrypt_Name(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_NAME);
        }
        public static string Decrypt_Name(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_NAME);
        } 
	    #endregion
        #region HouseNoEncryptDecrypt

        public static string Encrypt_HouseNo(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_HOUSE);
        }
        public static string Decrypt_HouseNo(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_HOUSE);
        }


        #endregion
        #region ZipEncryptDecrypt

        public static string Encrypt_Zip(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_ZIP);
        }
        public static string Decrypt_Zip(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_ZIP);
        }
        
        
        #endregion
        #region ContactPhoneEncryptDecrypt

        public static string Encrypt_ContactPhone(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_CONTACT_NUMBER);
        }
        public static string Decrypt_ContactPhone(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_CONTACT_NUMBER);
        }
        
        
        #endregion
        #region AttentionEncryptDecrypt


        public static string Encrypt_Attention(this string expression)
        {
            return EncryptionEngine.Encrypt(expression, PURPOSE_ATTENTION);
        }
        public static string Decrypt_Attention(this string expression)
        {
            return EncryptionEngine.Decrypt(expression, PURPOSE_ATTENTION);
        }
        
        
        #endregion


    }
}
