using AliKuli.Extentions;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{
    //ToDo Add stuff
    /// <summary>
    /// This class was made so as to keep similar fields in players and person in once spot
    /// to keep the model cleaner.
    /// The name will be the UserName of the user... always.
    /// </summary>
    public abstract class PlayerAbstract : CommonWithId, IPlayer
    {



        //public virtual ApplicationUser User { get; set; }

        [Display(Name = "Person")]
        [MaxLength(128)]
        public string PersonId { get; set; }
        public Person Person { get; set; }


        [Display(Name = "Default Bill Address")]
        [MaxLength(128)]
        public virtual string DefaultBillAddressId { get; set; }
        public virtual AddressMain DefaultBillAddress { get; set; }



        [Display(Name = "Ship To")]
        [MaxLength(128)]
        public virtual string DefaultShipAddressId { get; set; }
        public virtual AddressMain DefaultShipAddress { get; set; }


        //this will hide the name for all players.
        //the name will be always the userName
        public override bool HideNameInView()
        {
            return true;
        }





        public override string MakeUniqueName()
        {
            Name = FullName() + " - " + DateTime.Now.Ticks.ToString();
            return Name;

        }

        public void LoadFrom(IPlayer p)
        {
            base.LoadFrom(p as CommonWithId);
            //User = p.User;
            //UserId = p.UserId;
        }

        [NotMapped]
        public SelectList SelectListPeople { get; set; }




        //[NotMapped]
        //public SelectList SelectListUser { get; set; }


        [NotMapped]
        public SelectList SelectListBillAddress { get; set; }

        [NotMapped]
        public SelectList SelectListShipAddress { get; set; }




        public override void UpdatePropertiesDuringModify(ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);

            PlayerAbstract p = UnboxPlayerAbstract(ic);

            PersonId = p.PersonId;
            //AddressId = p.AddressId;

        }


        public static PlayerAbstract UnboxPlayerAbstract(ControllerCreateEditParameter parm)
        {
            return UnboxPlayerAbstract(parm.Entity);
        }




        public static PlayerAbstract UnboxPlayerAbstract(ICommonWithId icommonWithId)
        {
            PlayerAbstract playerAbstract = icommonWithId as PlayerAbstract;
            playerAbstract.IsNullThrowException("Unable to box player Abstract.");
            return playerAbstract;
        }


        //public override string FullName()
        //{
        //    return User.PersonComplex.PersonFullName();
        //}

        //#region Is...
        //[NotMapped]
        //public bool IsSuspended { get; set; }

        //[NotMapped]
        //public bool IsBlackListed { get; set; }

        //[NotMapped]
        //public bool IsAllowedToShip { get; set; }


        //#endregion

    }
}