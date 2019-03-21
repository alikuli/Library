//using AppDbx.Models;


namespace AliKuli.ConstantsNS
{



    public static class MyConstants
    {
        /// <summary>
        /// This class carries the names of all the fields for the GlobalVariables
        /// </summary>

        //public const string ADMIN_COUNTRY_ID_NUMBER_LENGTH = "LengthOfCountryIdNumber";
        // 

        public const string STOP_WORDS_PATH = @"\ContentMine\SetupData\stop-word-list.csv";

        public const string ADMIN_USER_PHONE = "AdminUserPhone";
        public const string ADMIN_NAME = "AdminName";
        public const string ADMIN_PASSWORD = "AdminPassword";
        public const string ADMIN_EMAIL = "AdminEmail";
        public const string ADMIN_COUNTRY = "AdminCountry";
        public const string ADMIN_COUNTRY_ABBREVIATION = "AdminCountryAbbreviation";
        public const string ADMIN_ROLE = "AdministerRole";

        public const string IS_SEND_BCC = "AlwaysSendBcc";

        public const string SERVICEMAN_ROLE = "ServiceMen";
        public const string HIRING_MAN_ROLE = "HiringMan";
        public const string MANAGER_ROLE = "Managers";

        public const int DISCOUNT_KEY_LENGTH = 36;
        public const string SPLITTING_SEQUANCE = "#@$*";
        public const string COMPANY_NAME = "DefaultCompanyName";

        public const string NO_OF_UNITS_REQUIRED_TO_SETUP_A_SERVICEMAN = "NumberOfUnitsRequiredToSetUpAServiceman";
        public const string NO_OF_UNITS_REQUIRED_TO_SETUP_A_CUSTOMER = "NumberOfUnitsRequiredToSetUpACustomer";


        public const string EMAIL_TESTING_DIRECTORY = "EmailTestingDirectory";
        public const string SMS_TESTING_DIRECTORY = "SmsTestingDirectory";

        public const string IS_SMTP_SELECTED_SERVICE = "IsSmtpSelectedService";
        public const string SMTP_DOMAIN = "Smtp_Domain";
        public const string SMTP_PASSWORD = "Smtp_Password";
        public const string SMTP_PORT = "Smtp_Port";
        public const string SMTP_SERVER = "SmtpServer";
        public const string SMTP_USER = "Smtp_User";
        public const string SMTP_FROM_EMAIL_ADDRESS = "fromEmailAddress";

        public const string DEFAULT_SCRATCHCARD_BATCH_SIZE = "DefaultScractchCardBatchSize";
        public const string DEFAULT_PAGE_SIZE = "DefaultPageSize";
        public const string WEBSITE_ANCHOR_LINK = "WebsiteAnchorLink";

        public const string IS_VERBOSE = "IsVerbose";
        public const string IS_ENCRYPTED = "IsEncrypted";
        public const string IS_FORCE_INITIALIZATION = "IsForceInitialization";

        public const string USERNAME_ENCRPTION_SEED = "2394892Laillahaillallah!2045md48d302";

        // *** Saving locations
        public const string SAVE_ROOT_DIRECTORY = @"~\ContentMine\Uploads";


        public const string SAVE_INITIALIZATION_DIRECTORY = @"~\ContentMine\Initialization\";

        public const string LOGO_LOCATION = @"~/ContentMine/MyImages/Logo.jpg";

        public const string MINDATE = "01/Jan/2010";
        public const string MAXDATE = "31/Dec/2050";



        //save locations for products
        //public const string SAVE_LOCATION_PRODUCT_CATEGORY1 = SAVE_LOCATION_ROOT_DIRECTORY + @"Category1";
        //public const string SAVE_LOCATION_PRODUCT_CATEGORY2 = SAVE_LOCATION_ROOT_DIRECTORY + @"Category2";
        //public const string SAVE_LOCATION_PRODUCT_CATEGORY3 = SAVE_LOCATION_ROOT_DIRECTORY + @"Category3";
        //public const string SAVE_LOCATION_PRODUCT_MAIN_CATEGORY = SAVE_LOCATION_ROOT_DIRECTORY + @"MainCategory";
        //public const string SAVE_LOCATION_PRODUCT_UOM_LENGTH = @"UomLength";
        //public const string SAVE_LOCATION_PRODUCT_UOM_VOLUME = @"UomVolume";
        //public const string SAVE_LOCATION_PRODUCT_UOM_WEIGHT = @"UomWeight";

        //public const string SAVE_LOCATION_PRODUCT_PICTURE_BIG = @"ProductPictureBig";
        //public const string SAVE_LOCATION_PRODUCT_PICTURE_MEDIUM = @"ProductPictureMedium";
        //public const string SAVE_LOCATION_PRODUCT_PICTURE_SMALL = @"ProductPictureSmall";

        //public const string SAVE_LOCATION_FILE_DOC_IMAGES = @"FileDocImages";

        //public const string SAVE_LOCATION_USER_IMAGES = @"UserImages";

        //public const string SAVE_LOCATION_USER_SELFIE = @"UserSelfie";

        //public const string SAVE_LOCATION_USER_ID_CARD_FRONT = @"UserIdCardFront";
        //public const string SAVE_LOCATION_USER_ID_CARD_BACK = @"UserIdCardBack";

        //public const string SAVE_LOCATION_USER_PASSPORT_FIRSTPAGE = @"UserPassportFirstPage";
        //public const string SAVE_LOCATION_USER_PASSPORT_VISA = @"UserPassportVisa";


        //public const string SAVE_LOCATION_USER_LISCENSE_FRONT = @"UserLiscenseFront";
        //public const string SAVE_LOCATION_USER_LISCENSE_BACK = @"UserLiscenseBack";


        //Controled pic size
        public const string MAX_UPLOAD_SIZE_ALLOWED_IN_BYTES = "3000000";
        public const string SIZE_PIC_BYTES_TO_SAVE = "1000000";
        public const string SIZE_PIC_HEIGHT_IN_PTS_MAX = "1000";
        public const string SIZE_PIC_WIDTH_IN_PTS_MAX = "1000";
        public const string DEFAULT_IMAGE_LOCATION = @"~\ContentMine\MyImages\BlankImage.jpg";

    }



}
