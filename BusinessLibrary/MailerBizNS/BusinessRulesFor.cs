﻿
using AliKuli.ConstantsNS;
using AliKuli.Extentions;
using ConfigManagerLibrary;
using EnumLibrary.EnumNS;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;
using System;
using System.Collections.Generic;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {



        //public override void BusinessRulesFor(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        //{

        //    base.BusinessRulesFor(parm);

        //    Mailer mailer = parm.Entity as Mailer;
        //    mailer.IsNullThrowException("Could not box mailer");
        //    mailer.Name.IsNullOrWhiteSpaceThrowException("mailer Name is empty");

        //}


        public byte[] PrintVerificationLetters(string id)
        {
            //AddressVerificationModel_Header hdr = getVerificationLetterData(addressVerificationHdr);
            id.IsNullOrWhiteSpaceThrowException("id");
            AddressVerificationHdr addressVerificationHdr = AddressVerificationHdrBiz.Find(id);
            addressVerificationHdr.IsNullThrowException("addressVerificationHdr");
            addressVerificationHdr.Mailer.IsNullThrowException("Mailer");


            //gather data
            string mailerName = addressVerificationHdr.Mailer.Name;
            string logoAddressAbsolute = System.Web.HttpContext.Current.Server.MapPath(MyConstants.LOGO_LOCATION);
            string addressMailFrom = CompanyConfig.CompanyAddress;
            string website = CompanyConfig.CompanyNameForWebPage;
            string userName = UserName;
            string printDate = DateTime.Now.ToShortDateString();
            string letter_Instructions = VerificationConfig.Letter_Instruction_Para;
            string letter_welcomePara = VerificationConfig.Letter_Welcome_Para;
            string letter_body = VerificationConfig.Letter_Body;
            string letter_Title = VerificationConfig.Letter_Title;
            string sum_Title = VerificationConfig.Summary_Title;
            string sum_body = VerificationConfig.Summary_Body;
            string sum_Instructions = VerificationConfig.Summary_Instruction_Para;
            string sum_Welcome = VerificationConfig.Summary_Welcome_Para;

            //need to initialize
            string batchNumber = addressVerificationHdr.BatchNo.ToString();
            MailLocalOrForiegnENUM mailLocalOrForiegnEnum = addressVerificationHdr.MailLocalOrForiegnEnum;
            MailServiceENUM mailServiceENUM = addressVerificationHdr.MailServiceEnum;

            byte[] hdr = createTheHeaderForPdfPrinting(addressVerificationHdr,
                                                        mailerName,
                                                        logoAddressAbsolute,
                                                        addressMailFrom,
                                                        website,
                                                        userName,
                                                        printDate,
                                                        letter_Instructions,
                                                        letter_welcomePara,
                                                        letter_body,
                                                        letter_Title,
                                                        sum_Title,
                                                        sum_body,
                                                        sum_Instructions,
                                                        sum_Welcome,
                                                        batchNumber,
                                                        mailLocalOrForiegnEnum,
                                                        mailServiceENUM);

            return hdr;
        }

        private static byte[] createTheHeaderForPdfPrinting(AddressVerificationHdr addressVerificationHdr,
                                                            string mailerName,
                                                            string logoAddressAbsolute,
                                                            string addressMailFrom,
                                                            string website,
                                                            string userName,
                                                            string printDate,
                                                            string letter_Instructions,
                                                            string letter_welcomePara,
                                                            string letter_body,
                                                            string letter_Title,
                                                            string sum_Title,
                                                            string sum_body,
                                                            string sum_Instructions,
                                                            string sum_Welcome,
                                                            string batchNumber,
                                                            MailLocalOrForiegnENUM mailLocalOrForiegnEnum,
                                                            MailServiceENUM mailServiceENUM)
        {

            LetterClass summaryLetter;
            LetterClass verificationLetter;

            initializeTheLetterCommonText(letter_Instructions,
                                            letter_welcomePara,
                                            letter_body,
                                            letter_Title,
                                            sum_Title,
                                            sum_body,
                                            sum_Instructions,
                                            sum_Welcome,
                                            out summaryLetter,
                                            out verificationLetter);


            //create the letters            
            List<AddressVerificationModel> letterList = createLetters(addressVerificationHdr);

            //initialize the header
            AddressVerificationModel_Header header = new AddressVerificationModel_Header();

            //load the header
            header.Load(batchNumber,
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

            var factory = new CustomerAddressVerificationFactory();
            byte[] pdf = factory.Build(header);

            return pdf;
        }

        private static void initializeTheLetterCommonText(string letter_Instructions,
                                                            string letter_welcomePara,
                                                            string letter_body,
                                                            string letter_Title,
                                                            string sum_Title,
                                                            string sum_body,
                                                            string sum_Instructions,
                                                            string sum_Welcome,
                                                        out LetterClass summaryLetter,
                                                        out LetterClass verificationLetter)
        {
            //set up the letter classes for printing
            summaryLetter = new LetterClass();
            verificationLetter = new LetterClass();

            summaryLetter.Load(sum_Title,
                                sum_Welcome,
                                sum_body,
                                sum_Instructions);

            verificationLetter.Load(letter_Title,
                                        letter_welcomePara,
                                        letter_body,
                                        letter_Instructions);
        }

        private static List<AddressVerificationModel> createLetters(AddressVerificationHdr addressVerificationHdr)
        {
            List<AddressVerificationModel> letterList = new List<AddressVerificationModel>();
            foreach (var trx in addressVerificationHdr.AddressVerificationTrxs)
            {

                string inProcessDate = addressVerificationHdr.BeginDate.Date_NotNull.ToShortDateString();
                string addressMailTo = trx.Address.ToPostal();
                string letterNumber = trx.LetterNo.ToString();
                string requestDate = trx.DateVerifcationPaymentAccepted_NotNull.ToShortDateString();
                string verificationNumber = trx.VerificationNumber.ToString();

                AddressVerificationModel letter = new AddressVerificationModel();
                letter.Load(verificationNumber, addressMailTo, requestDate, inProcessDate, letterNumber);
                letterList.Add(letter);

            }
            return letterList;
        }

    }
}
