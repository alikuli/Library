using System;
using AliKuli.Extentions;
using InterfacesLibrary.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UserModels;
using ModelsClassLibrary.ModelsNS.AddressNS;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{
    //ToDo Add stuff
    /// <summary>
    /// This class was made so as to keep similar fields in players and person in once spot
    /// to keep the model cleaner.
    /// </summary>
    public abstract class PlayerAbstract : CommonWithId, IPlayer
    {

        #region User
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }

        #endregion

        #region SelfErrorCheck
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            //user is required.
            Check_UserExists();
        }

        private void Check_UserExists()
        {
            if (User == null)
            {
                if (UserId.IsNullOrWhiteSpace())
                {
                    throw new Exception("User not found. PersonAbstractWithCommonFields.SelfErrorCheck");
                }
            }
        }

        #endregion

        public override string FullName()
        {
            Check_UserExists();
            return User.PersonComplex.PersonFullName();
        }

        #region Is...

        public bool IsSuspended
        {
            get
            {
                Check_UserExists();
                return User.IsSuspended.Value;
            }
            set
            {
                Check_UserExists();
                User.IsSuspended.Value = value;
            }
        }

        public bool IsBlackListed
        {
            get
            {
                Check_UserExists();
                return User.IsBlackListed.Value;
            }
            set
            {
                Check_UserExists();
                User.IsBlackListed.Value = value;
            }

        }

        public bool IsAllowedToShip
        {
            get
            {
                return !IsBlackListed && !IsSuspended;
            }
            set
            {
                Check_UserExists();
                User.IsBlackListed.Value = value;
                User.IsSuspended.Value = value;

            }
        }

        #endregion

        public override string MakeUniqueName()
        {
            Name = FullName() + " - " + DateTime.Now.Ticks.ToString();
            return Name;

        }

        public void LoadFrom(IPlayer p)
        {
            base.LoadFrom(p as CommonWithId);
            User = p.User;
            UserId = p.UserId;
        }


        public Guid AddressId{get;set;}
        public virtual AddressWithId Address { get; set; }

        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);

            PlayerAbstract p = ic as PlayerAbstract;

            if (p == null)
                throw new Exception("Unable to box PlayerAbstract. PlayerAbstract.UpdatePropertiesDuringModify");

            UserId = p.UserId;
            AddressId = p.AddressId;

        }

    }
}