using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using System;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS
{
    public class AddressVerificationModel_Header
    {
        public AddressVerificationModel_Header()
        {
            Summary = new LetterClass();
            Letter = new LetterClass();
        }

        public string BatchNumber { get; set; }
        public string Logo_AddressAbsolute { get; set; }
        public string AddressMailFrom { get; set; }
        public string WebsiteAddress { get; set; }
        public MailLocalOrForiegnENUM MailLocalOrForiegnEnum { get; set; }
        public MailServiceENUM MailServiceEnum { get; set; }


        public LetterClass Summary { get; set; }
        public LetterClass Letter { get; set; }

        public string PrintDate { get; set; }
        public string PrintDate_Str { get { return DateTime.Now.ToShortDateString(); } }


        public List<AddressVerificationModel> LetterList { get; set; }

        private int NumberOfVerificationLetters
        {
            get
            {
                if (LetterList.IsNullOrEmpty())
                    return 0;
                return LetterList.Count;
            }
        }

        public string NumberOfLetters_Str
        {
            get
            {
                string str = string.Format("{0}", NumberOfVerificationLetters);
                return str;
            }
        }

        public string MailerName { get; set; }
        public string UserName { get; set; }

        public string LetterRefNumbers { get { return getLetterRefNumbers(); } }

        private string getLetterRefNumbers()
        {
            string str = "";

            if (LetterList.IsNullOrEmpty())
                return str;

            foreach (var item in LetterList)
            {
                str += item.LetterNumber + ", ";
            }

            str = str.Substring(0, str.Length - 2);
            str += ".";

            return str;
        }

        public void Load(string batchNumber, string logo_AddressAbsolute, string addressMailFrom, string websiteAddress, MailLocalOrForiegnENUM mailLocalOrForiegnEnum, MailServiceENUM mailServiceEnum, string printDate, string mailerName, string userName, LetterClass summaryLetter, LetterClass verificationLetter, List<AddressVerificationModel> letterList)
        {
            BatchNumber = batchNumber;
            Logo_AddressAbsolute = logo_AddressAbsolute;
            AddressMailFrom = addressMailFrom;
            WebsiteAddress = websiteAddress;
            MailLocalOrForiegnEnum = mailLocalOrForiegnEnum;
            MailServiceEnum = mailServiceEnum;
            PrintDate = printDate;
            MailerName = mailerName;
            UserName = userName;
            Summary = summaryLetter;
            Letter = verificationLetter;
            LetterList = letterList;
        }

    }


 
    public class LetterClass
    {

        public void Load (string title, string welcomePara, string body, string instructions)
        {
            Title = title;
            WelcomePara = welcomePara;
            Body = body;
            Instructions = instructions;
        }
        public string Title { get; set; }
        public string WelcomePara { get; set; }
        public string Body { get; set; }
        public string Instructions { get; set; }

    }
}
