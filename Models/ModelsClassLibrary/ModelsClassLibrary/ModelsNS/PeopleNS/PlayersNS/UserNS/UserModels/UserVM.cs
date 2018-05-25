using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Web.Mvc;
using AliKuli.Extentions;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using TestCheckBoxList.Models;
using System.Web.Security;
namespace UserModelsLibrary.ModelsNS
{

    [NotMapped]
    public class UserVM : User
    {
        List<IdentityRole> _allRoles;
        public UserVM(List<IdentityRole> allRoles)
        {
            Roles = new List<IdentityUserRole>();
            Claims = new List<IdentityUserClaim>();
            ClaimsCheckBox = new List<CheckBoxItem>();
            RolesCheckBox = new List<CheckBoxItem>();
            _allRoles = allRoles;
        }

        public new List<IdentityUserRole> Roles { get; set; }
        public new List<IdentityUserClaim> Claims { get; set; }

        public List<CheckBoxItem> RolesCheckBox { get; set; }
        public List<CheckBoxItem> ClaimsCheckBox { get; set; }

        public SelectList RolesSelectList { get; set; }
        public SelectList CountriesSelectList { get; set; }

        /// <summary>
        /// Loads data from user into this.
        /// </summary>
        /// <param name="user"></param>
        //public void LoadFrom(User user)
        //{
        //    this.Id = user.Id;

        //    this.AccessFailedCount = user.AccessFailedCount;

        //    //this.Address = user.Address;
        //    this.AddressComplex = user.AddressComplex;
        //    this.PersonInfo = user.PersonInfo;
        //    this.MetaData = user.MetaData;


        //    this.AddressId = user.AddressId;
        //    this.Email = user.Email;
        //    this.EmailConfirmed = user.EmailConfirmed;

        //    this.IsEncrypted = user.IsEncrypted;
        //    this.IsAdmin = user.IsAdmin;
        //    this.IsBlackListed = user.IsBlackListed;
        //    this.IsSnailMailAddressConfirmed = user.IsSnailMailAddressConfirmed;
        //    this.IsSuspended = user.IsSuspended;
        //    this.SnailMailDetail = user.SnailMailDetail;
        //    this.TwoFactorEnabled = user.TwoFactorEnabled;
        //    this.PhoneNumberConfirmed = user.PhoneNumberConfirmed;

        //    this.LockoutEnabled = user.LockoutEnabled;
        //    this.Name = user.Name;
        //    this.PhoneNumber = user.PhoneNumber;
        //    this.SnailMailConfirmationNumber = user.SnailMailConfirmationNumber;
        //    this.UserName = user.UserName;
        //}

        /// <summary>
        /// Loads data from this into user... except ID
        /// </summary>
        /// <param name="user"></param>
        public void LoadTo(User user)
        {
            user.AccessFailedCount = this.AccessFailedCount;
            user.Address = this.Address;
            user.AddressComplex = this.AddressComplex;
            user.AddressId = this.AddressId;
            user.Email = this.Email;
            user.EmailConfirmed = this.EmailConfirmed;
            //user.Id = this.Id;
            user.IsAdmin = this.IsAdmin;
            user.IsBlackListed = this.IsBlackListed;
            user.IsSnailMailAddressConfirmed = this.IsSnailMailAddressConfirmed;
            user.IsSuspended = this.IsSuspended;
            user.LockoutEnabled = this.LockoutEnabled;
            user.LockoutEndDateUtc = this.LockoutEndDateUtc;
            user.MetaData = this.MetaData;
            user.Name = this.Name;
            user.PersonInfo = this.PersonInfo;
            user.PhoneNumber = this.PhoneNumber;
            user.PhoneNumberConfirmed = this.PhoneNumberConfirmed;
            user.SnailMailConfirmationNumber = this.SnailMailConfirmationNumber;
            user.SnailMailDetail = this.SnailMailDetail;
            user.TwoFactorEnabled = this.TwoFactorEnabled;
            user.UserName = this.UserName;

        }

        public override void LoadFrom(User user)
        {
            base.LoadFrom(user);


            if (!user.Roles.IsNullOrEmpty())
            {
                foreach (var r in user.Roles)
                {
                    this.Roles.Add(r);

                }
            }

            if (!user.Claims.IsNullOrEmpty())
            {
                foreach (var c in user.Claims)
                {
                    this.Claims.Add(c);
                }

            }
            //First add all the Roles into the RolesCheckedBox
            if(! _allRoles.IsNullOrEmpty())
            {
                foreach (var role in _allRoles)
                {
                    CheckBoxItem cbt = new CheckBoxItem();
                    cbt.Id = role.Id;
                    cbt.Label = role.Name;
                    RolesCheckBox.Add(cbt);
                }
            }

            //now mark all the roles user is using as true.
            if(!RolesCheckBox.IsNullOrEmpty())
            {
                if(!Roles.IsNullOrEmpty())
                {
                    foreach (var role in Roles)
                    {
                        var rcb = RolesCheckBox.Find(x => x.Id == role.RoleId);
                        
                        if(!rcb.IsNull())
                        {
                            rcb.IsTrue = true;
                        }

                    }
                }
            }

            //this.Roles = user.Roles.IsNullOrEmpty() ? new List<IdentityRoleGuid>() : user.Roles;
            //this.Claims = user.Claims.IsNullOrEmpty() ? new List<Claim>() :  user.Claims.ToList();

        }
    }
}