using AliKuli.Extentions;
using ConstantsLibrary;
using System;
using System.Configuration;

namespace ConfigManagerLibrary
{
    public class MailersTrustLevelConfig
    {

        /// <summary>
        /// This returns the number of mailers allowed to the mailers.
        /// </summary>
        public static int Level1_Postal
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL1_POSTAL];

                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("LEVEL1 not set properly in web.config.");


            }
        }


        public static int Level2_Postal
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL2_POSTAL];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("LEVEL2 not set properly in web.config.");


            }
        }


        public static int Level3_Postal
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL3_POSTAL];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("LEVEL3 not set properly in web.config.");


            }
        }


        public static int Level4_Postal
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL4_POSTAL];

                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("LEVEL4 not set properly in web.config.");

            }
        }


        public static int Level5_Postal
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL5_POSTAL];

                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("Level5 not set properly in web.config.");

            }
        }






        public static int Level1_Courier
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL1_COURIER];

                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("LEVEL1 not set properly in web.config.");


            }
        }


        public static int Level2_Courier
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL2_COURIER];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("LEVEL2 not set properly in web.config.");


            }
        }


        public static int Level3_Courier
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL3_COURIER];
                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("LEVEL3 not set properly in web.config.");


            }
        }


        public static int Level4_Courier
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL4_COURIER];

                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("LEVEL4 not set properly in web.config.");

            }
        }


        public static int Level5_Courier
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL5_COURIER];

                if (str.IsInt())
                    return str.ToInt();
                else
                    throw new Exception("Level5 not set properly in web.config.");

            }
        }




        //-----------------------------------------------------


        public static double Level1_Postal_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL1_POSTAL_DEPOSIT_PAKISTAN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL1 (Pakistan) deposit not set properly in web.config.");


            }
        }


        public static double Level2_Postal_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL2_POSTAL_DEPOSIT_PAKISTAN];
                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL2 (Pakistan) deposit not set properly in web.config.");


            }
        }


        public static double Level3_Postal_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL3_POSTAL_DEPOSIT_PAKISTAN];
                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL3 (Pakistan) deposit not set properly in web.config.");


            }
        }


        public static double Level4_Postal_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL4_POSTAL_DEPOSIT_PAKISTAN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL4 (Pakistan) deposit not set properly in web.config.");

            }
        }


        public static double Level5_Postal_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL5_POSTAL_DEPOSIT_PAKISTAN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("Level5 (Pakistan) deposit not set properly in web.config.");

            }
        }






        public static double Level1_Courier_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL1_COURIER_DEPOSIT_PAKISTAN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL1 (Pakistan) deposit not set properly in web.config.");


            }
        }


        public static double Level2_Courier_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL2_COURIER_DEPOSIT_PAKISTAN];
                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL2 (Pakistan) deposit not set properly in web.config.");


            }
        }


        public static double Level3_Courier_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL3_COURIER_DEPOSIT_PAKISTAN];
                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL3 (Pakistan) deposit not set properly in web.config.");


            }
        }


        public static double Level4_Courier_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL4_COURIER_DEPOSIT_PAKISTAN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL4 (Pakistan) deposit not set properly in web.config.");

            }
        }


        public static double Level5_Courier_Deposit_Pakistan
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL5_COURIER_DEPOSIT_PAKISTAN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("Level5 (Pakistan) deposit not set properly in web.config.");

            }
        }


        //------------------------------------------------------------------------------







        public static double Level1_Postal_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL1_POSTAL_DEPOSIT_FOREIGN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL1 (Foreign) deposit not set properly in web.config.");


            }
        }


        public static double Level2_Postal_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL2_POSTAL_DEPOSIT_FOREIGN];
                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL2 (Foreign) deposit not set properly in web.config.");


            }
        }


        public static double Level3_Postal_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL3_POSTAL_DEPOSIT_FOREIGN];
                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL3 (Foreign) deposit not set properly in web.config.");


            }
        }


        public static double Level4_Postal_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL4_POSTAL_DEPOSIT_FOREIGN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL4 (Foreign) deposit not set properly in web.config.");

            }
        }


        public static double Level5_Postal_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL5_POSTAL_DEPOSIT_FOREIGN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("Level5 (Foreign) deposit not set properly in web.config.");

            }
        }






        public static double Level1_Courier_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL1_COURIER_DEPOSIT_FOREIGN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL1 (Foreign) deposit not set properly in web.config.");


            }
        }


        public static double Level2_Courier_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL2_COURIER_DEPOSIT_FOREIGN];
                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL2 (Foreign) deposit not set properly in web.config.");


            }
        }


        public static double Level3_Courier_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL3_COURIER_DEPOSIT_FOREIGN];
                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL3 (Foreign) deposit not set properly in web.config.");


            }
        }


        public static double Level4_Courier_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL4_COURIER_DEPOSIT_FOREIGN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("LEVEL4 (Foreign) deposit not set properly in web.config.");

            }
        }


        public static double Level5_Courier_Deposit_Foreign
        {
            get
            {

                string str = ConfigurationManager.AppSettings[MailerTrustLevelConstants.MAIL_QTY_LVL5_COURIER_DEPOSIT_FOREIGN];

                if (str.IsInt())
                    return str.ToDouble();
                else
                    throw new Exception("Level5 (Foreign) deposit not set properly in web.config.");

            }
        }







    }
}
