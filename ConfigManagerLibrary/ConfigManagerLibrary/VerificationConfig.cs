using AliKuli.Extentions;
using ConstantsLibrary;
using System;
using System.Configuration;

namespace ConfigManagerLibrary
{

    public class Icon
    {
        public static string StarIcon
        {
            get
            {

                return ConfigurationManager.AppSettings[Icons.STAR_ICON];


            }
        }
    }
    public class VerificationConfig
    {
        public static string StarIcon
        {
            get
            {

                return ConfigurationManager.AppSettings[Icons.STAR_ICON];


            }
        }
        public static string RequestIconIcon
        {
            get
            {

                return ConfigurationManager.AppSettings[VerificationIcons.REQUESTED_ICON];


            }
        }


        public static string VerifiedIcon
        {
            get
            {

                return ConfigurationManager.AppSettings[VerificationIcons.VERIFIED_ICON];


            }
        }

        public static string InproccessIcon
        {
            get
            {

                return ConfigurationManager.AppSettings[VerificationIcons.INPROCCESS_ICON];


            }
        }

        public static string MailedIcon
        {
            get
            {

                return ConfigurationManager.AppSettings[VerificationIcons.MAILED_ICON];


            }
        }

        public static string PrintedIcon
        {
            get
            {

                return ConfigurationManager.AppSettings[VerificationIcons.PRINTED_ICON];


            }
        }



        public static string FailedIcon
        {
            get
            {

                return ConfigurationManager.AppSettings[VerificationIcons.PRINTED_ICON];


            }
        }
        public static string NotVerifiedIcon
        {
            get
            {
                return ConfigurationManager.AppSettings[VerificationIcons.NOTVERIFIED_ICON];


            }
        }

        public static double SuccessPercentage
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationConstants.MAIL_SUCCESS_PERCENTAGE];
                if (str.IsInt())
                {
                    return double.Parse(str);
                }
                throw new Exception("The success percentage is not properly set in");



            }
        }

        public static int Number_Of_Open_Mailings_Allowed
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationConstants.NUMBER_OF_OPEN_MAILINGS_ALLOWED];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("Number_Of_Open_Mailings_Allowed not set properly in web.config.");



            }
        }

        public static int Number_Of_Days_Allowed_For_Local_Post
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationConstants.NUMBER_OF_DAYS_ALLOWED_FOR_LOCAL_POST];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("Number_Of_Days_Allowed_For_Local_Post not set properly in web.config.");



            }
        }


        public static int Number_Of_Days_Allowed_For_Local_Courier
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationConstants.NUMBER_OF_DAYS_ALLOWED_FOR_LOCAL_COURIER];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("Number_Of_Days_Allowed_For_Local_Courier not set properly in web.config.");



            }
        }

        public static int Number_Of_Days_Allowed_For_Foreign_Post
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationConstants.NUMBER_OF_DAYS_ALLOWED_FOR_FOREIGN_POST];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("Number_Of_Days_Allowed_For_Foreign_Post not set properly in web.config.");
            }
        }


        public static int Number_Of_Days_Allowed_For_Foreign_Courier
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationConstants.NUMBER_OF_DAYS_ALLOWED_FOR_FOREIGN_COURIER];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("Number_Of_Days_Allowed_For_Foreign_Courier not set properly in web.config.");
            }
        }








        public static double Sale_Courier_International
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationSale.SALE_COURIER_INTERNATIONAL];
                if (str.IsDouble())
                    return str.ToDouble();
                else
                    throw new Exception("SALE_COURIER_INTERNATIONAL not set properly in web.config.");



            }
        }
        public static double Sale_Postal_International
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationSale.SALE_POSTAL_INTERNATIONAL];
                if (str.IsDouble())
                    return str.ToDouble();
                else
                    throw new Exception("SALE_POSTAL_INTERNATIONAL not set properly in web.config.");



            }
        }

        public static double Sale_Courier_Local
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationSale.SALE_COURIER_LOCAL];
                if (str.IsDouble())
                    return str.ToDouble();
                else
                    throw new Exception("SALE_COURIER_LOCAL not set properly in web.config.");



            }
        }
        public static double Sale_Postal_Local
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationSale.SALE_POSTAL_LOCAL];
                if (str.IsDouble())
                    return str.ToDouble();
                else
                    throw new Exception("Cost_Postal_Local not set properly in web.config.");



            }
        }



        //--------------------------------------------------

        /// <summary>
        /// These are the budgeted cost
        /// </summary>
        public static double Expected_Cost_Courier_International
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationCosts.BUDGETED_COST_COURIER_INTERNATIONAL];
                if (str.IsDouble())
                    return str.ToDouble();
                else
                    throw new Exception("Expected_Cost_Courier_International not set properly in web.config.");



            }
        }

        public static double Expected_Cost_Postal_International
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationCosts.BUDGETED_COST_POSTAL_INTERNATIONAL];
                if (str.IsDouble())
                    return str.ToDouble();
                else
                    throw new Exception("Expected_Cost_Postal_International not set properly in web.config.");



            }
        }

        public static double Expected_Cost_Courier_Local
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationCosts.BUDGETED_COST_COURIER_LOCAL];
                if (str.IsDouble())
                    return str.ToDouble();
                else
                    throw new Exception("Expected_Cost_Courier_Local not set properly in web.config.");



            }
        }
        public static double Expected_Cost_Postal_Local
        {
            get
            {

                string str = ConfigurationManager.AppSettings[VerificationCosts.BUDGETED_COST_POSTAL_LOCAL];
                if (str.IsDouble())
                    return str.ToDouble();
                else
                    throw new Exception("Expected_Cost_Postal_Local not set properly in web.config.");



            }
        }

        public static string Letter_Title
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationLetters.LETTER_TITLE];
                str.IsNullOrWhiteSpaceThrowException("Letter Title not set in config");
                return str;


            }
        }




        public static string Letter_Welcome_Para
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationLetters.LETTER_WELCOME_PARA];
                str.IsNullOrWhiteSpaceThrowException("LETTER_WELCOME_PARA not set in config");
                return str;


            }
        }


        public static string Letter_Body
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationLetters.LETTER_BODY];
                str.IsNullOrWhiteSpaceThrowException("Letter body not set in config");
                return str;


            }
        }




        public static string Letter_Instruction_Para
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationLetters.LETTER_INSTRUCTION_PARA];
                str.IsNullOrWhiteSpaceThrowException("Letter instruction para not set in config");
                return str;


            }
        }



        public static string Summary_Title
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationLetters.SUMMARY_TITLE];
                str.IsNullOrWhiteSpaceThrowException("SUMMARY_TITLE not set in config");
                return str;


            }
        }



        public static string Summary_Welcome_Para
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationLetters.SUMMARY_WELCOME_PARA];
                str.IsNullOrWhiteSpaceThrowException("SUMMARY_WELCOME_PARA not set in config");
                return str;


            }
        }



        public static string Summary_Body
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationLetters.SUMMARY_BODY];
                str.IsNullOrWhiteSpaceThrowException("SUMMARY_BODY not set in config");
                return str;


            }
        }



        public static string Summary_Instruction_Para
        {
            get
            {
                string str = ConfigurationManager.AppSettings[VerificationLetters.SUMMARY_INSTRUCTION_PARA];
                str.IsNullOrWhiteSpaceThrowException("SUMMARY_INSTRUCTION_PARA not set in config");
                return str;


            }
        }



    }
}
