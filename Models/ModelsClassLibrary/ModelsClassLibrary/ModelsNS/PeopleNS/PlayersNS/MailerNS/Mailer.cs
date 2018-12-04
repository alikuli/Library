using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using EnumLibrary.EnumNS.VerificationNS;
using InterfacesLibrary.Interfaces.PeopleNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using UserModels;
namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS
{
    public class Mailer : PlayerAbstract, IPlayer
    {
        [Display(Name = "Trust Level")]
        public TrustLevelENUM TrustLevelEnum { get; set; }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Mailer;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            Mailer mailer = icommonWithId as Mailer;
            mailer.IsNullThrowException("mailer unboxing/boxing");
            PersonId = PersonId;
            TrustLevelEnum = mailer.TrustLevelEnum;
            //User = User;


        }

        /// <summary>
        /// This holds all the mailings the mailer has done, is doing.
        /// </summary>
        public virtual ICollection<AddressVerificationHdr> AddressVerificationHdrs { get; set; }

        public double DepositFor(MailLocalOrForiegnENUM mailLocalOrForiegnEnum, MailServiceENUM mailServiceEnum)
        {
            double depositAmount = 0;
            switch (TrustLevelEnum)
            {
                case TrustLevelENUM.Unknown:
                    throw new Exception("Trust Level Unknown!");


                case TrustLevelENUM.Level1:
                    switch (mailLocalOrForiegnEnum)
                    {
                        case MailLocalOrForiegnENUM.InPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level1_Postal_Deposit_Pakistan;
                                    break;

                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level1_Courier_Deposit_Pakistan;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case MailLocalOrForiegnENUM.OutOfPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level1_Postal_Deposit_Foreign;
                                    break;
                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level1_Courier_Deposit_Foreign;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        default:
                            throw new Exception("Local or Foreign Unknown!");
                    }

                    break;

                case TrustLevelENUM.Level2:
                    switch (mailLocalOrForiegnEnum)
                    {
                        case MailLocalOrForiegnENUM.InPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level2_Postal_Deposit_Pakistan;
                                    break;

                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level2_Courier_Deposit_Pakistan;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case MailLocalOrForiegnENUM.OutOfPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level2_Postal_Deposit_Foreign;
                                    break;
                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level2_Courier_Deposit_Foreign;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        default:
                            throw new Exception("Local or Foreign Unknown!");
                    }
                    break;

                case TrustLevelENUM.Level3:
                    switch (mailLocalOrForiegnEnum)
                    {
                        case MailLocalOrForiegnENUM.InPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level3_Postal_Deposit_Pakistan;
                                    break;

                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level3_Courier_Deposit_Pakistan;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case MailLocalOrForiegnENUM.OutOfPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level3_Postal_Deposit_Foreign;
                                    break;
                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level3_Courier_Deposit_Foreign;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        default:
                            throw new Exception("Local or Foreign Unknown!");
                    }
                    break;


                case TrustLevelENUM.Level4:
                    switch (mailLocalOrForiegnEnum)
                    {
                        case MailLocalOrForiegnENUM.InPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level4_Postal_Deposit_Pakistan;
                                    break;

                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level4_Courier_Deposit_Pakistan;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case MailLocalOrForiegnENUM.OutOfPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level4_Postal_Deposit_Foreign;
                                    break;
                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level4_Courier_Deposit_Foreign;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        default:
                            throw new Exception("Local or Foreign Unknown!");
                    }
                    break;


                case TrustLevelENUM.Level5:
                    switch (mailLocalOrForiegnEnum)
                    {
                        case MailLocalOrForiegnENUM.InPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level5_Postal_Deposit_Pakistan;
                                    break;

                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level5_Courier_Deposit_Pakistan;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case MailLocalOrForiegnENUM.OutOfPakistan:
                            switch (mailServiceEnum)
                            {
                                case MailServiceENUM.Post:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level5_Postal_Deposit_Foreign;
                                    break;
                                case MailServiceENUM.Courier:
                                    depositAmount = ConfigManagerLibrary.MailersTrustLevelConfig.Level5_Courier_Deposit_Foreign;
                                    break;
                                default:
                                    break;
                            }
                            break;

                        default:
                            throw new Exception("Local or Foreign Unknown!");
                    }
                    break;


                case TrustLevelENUM.BlackListed:
                    throw new Exception("Black Listed!");

                default:
                    break;
            }

            return 0;
        }


        [NotMapped]
        public SelectList SelectListTrustLevel { get; set; }
        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }

        //[Display(Name="Person")]
        //public string PersonId { get; set; }
        //public virtual Person Person { get; set; }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Name.IsNullOrWhiteSpaceThrowException("Name is empty");
            //UserId.IsNullOrWhiteSpaceThrowException("User Id is empty");
            //User.IsNullThrowException("User is empty");
            if(TrustLevelEnum == TrustLevelENUM.Unknown)
                throw new Exception("Trust Level is Unknown");
        }



    }

}
