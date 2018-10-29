using EnumLibrary.EnumNS;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;
using System;
using System.Collections.Generic;

namespace VerificationLetterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program start");


            //data
            string addressMailFrom = "Kara Enterprises, 2 The Mall Road, Peshawar";
            string letter_Instructgions = "Log in to you account. The instructions are longer than this but I don't know what to write now. So I am just adding stuff to see how everything will turn out. Then go To Verification and enter the verification number listed below:";
            //string title = "Verification Request";
            string welcomePara = "Welcome to verification! Thank you for verifying your address. You will reap great benfits from this!";
            string website = "Dhoond-Nokar.com";
            string letter_body = "With a verified address people will feel safer to do business with you. You can sell or buy easier. People will trust you. Will not show your address on the screen. It can only be used to get or send orders.";
            string printDate = "11/11/2018";
            string logoAddressAbsolute = @"C:\Users\ALI\Documents\Visual Studio 2013\Projects\Libraries\MarketPlace.Web6\MarketPlace.Web6\Content\Icons\ShoppingCart.png";
            string batchNumber = "53";

            string verf_WelcomePara = welcomePara;
            string verf_Instructions = letter_Instructgions;
            string verf_Body = letter_body;
            string verf_Title = "Verification Request";
            string sum_Title = "Verification Summary";
            string sum_body = "Summary";
            string sum_Instructions = " You will need to go to the post office and mail these immediately. We have recorded the print date and the delivery time has started to tick. Once the window for the delivery time closes, you will not get paid for the mail. So please, mail immediaely!";
            string sum_Welcome = "Welcome to the Summary";
            string mailerName = "Alamgeer";
            string userName = "UserNameAlamgeer";
            MailLocalOrForiegnENUM mailLocalOrForiegnEnum = MailLocalOrForiegnENUM.InPakistan;
            MailServiceENUM mailServiceENUM = MailServiceENUM.Post;




            //create letter data

            AddressVerificationModel letter1 = new AddressVerificationModel();

            string inProcessDate = "10/10/2018";
            string addressMailTo = "Ali Kuli, House No. 1, Gulkali, Main Harbanspura Road Lahore";
            string letterNumber = "69";
            string requestDate = "12/11/2018";
            string verificationNumber = "203923";
            letter1.Load(verificationNumber, addressMailTo, requestDate, inProcessDate, letterNumber);



            AddressVerificationModel letter2 = new AddressVerificationModel();

            //param1.Website = website;
            addressMailTo = "Aila, House No. 1, Gulkali, Main Harbanspura Road Lahore";
            inProcessDate = "10/10/2018";
            letterNumber = "88";
            requestDate = "12/11/2018";
            verificationNumber = "69696969";
            letter2.Load(verificationNumber, addressMailTo, requestDate, inProcessDate, letterNumber);


            List<AddressVerificationModel> letterList = new List<AddressVerificationModel>();
            letterList.Add(letter1);
            letterList.Add(letter2);





            AddressVerificationModel_Header header = new AddressVerificationModel_Header();


            LetterClass summaryLetter = new LetterClass();
            LetterClass verificationLetter = new LetterClass();

            summaryLetter.Load(sum_Title, sum_Welcome, sum_body, sum_Instructions);
            verificationLetter.Load(verf_Title, verf_WelcomePara, verf_Body, verf_Instructions);




            header.Load(
                batchNumber,
                logoAddressAbsolute,
                addressMailFrom,
                website,
                mailLocalOrForiegnEnum,
                mailServiceENUM,
                printDate,
                mailerName,
                userName,
                summaryLetter,
                verificationLetter,
                letterList);


            var cavf = new CustomerAddressVerificationFactory();
            cavf.Build(header);

            Console.WriteLine("*** Program End");
            Console.ReadLine();

        }
    }
}
