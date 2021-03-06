﻿using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;


namespace ModelsClassLibrary.ModelsNS.ProductChildNS
{
    public partial class ProductChild
    {

        /// <summary>
        /// This is the User who owns this product. This needs to be set at the time of creating because we may want to upload all this stuff in their own directory
        /// </summary>
        //[Display(Name = "Owner")]
        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }


        public virtual ICollection<ProductChildFeature> ProductChildFeatures { get; set; }
        [NotMapped]
        public List<ProductChildFeature> ProductChildFeatures_Fixed
        {
            get
            {
                if (ProductChildFeatures.IsNullOrEmpty())
                    return new List<ProductChildFeature>();

                List<ProductChildFeature> lst = ProductChildFeatures.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }


        /// <summary>
        /// This contains all the features ie. from MenuPath1, Menupath2, MenuPath3, Product and ofcours, ProductChildFeatures
        /// </summary>
        [NotMapped]
        public virtual List<ProductChildFeature> AllFeatures { get; set; }


        /// <summary>
        /// This is the owning product.
        /// </summary>
        [Display(Name = "Product")]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }





        public virtual ICollection<GlobalComment> GlobalComments { get; set; }
        public List<GlobalComment> GlobalComments_Fixed
        {
            get
            {
                if (GlobalComments.IsNullOrEmpty())
                    return new List<GlobalComment>();

                List<GlobalComment> lst = GlobalComments.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }


        public virtual ICollection<LikeUnlike> LikeUnlikes { get; set; }
        public List<LikeUnlike> LikeUnlikes_Fixed
        {
            get
            {
                if (LikeUnlikes.IsNullOrEmpty())
                    return new List<LikeUnlike>();

                List<LikeUnlike> lst = LikeUnlikes.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }


        /// <summary>
        /// These are the messages being advertised by the user.
        /// </summary>
        //public virtual ICollection<Message> MessagesAdvertisment { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public List<Message> Messages_Fixed
        {
            get
            {
                if (Messages.IsNullOrEmpty())
                    return new List<Message>();

                List<Message> lst = Messages.Where(x => x.MetaData.IsDeleted == false).ToList();
                return lst;
            }
        }



        public string ShipFromAddressId { get; set; }
        public virtual AddressMain ShipFromAddress { get; set; }

        [NotMapped]
        public SelectList SelectListShipFromAddress { get; set; }


        [Display(Name = "Ship From Address")]
        [NotMapped]
        public AddressComplex ShipFromAddressComplex { get; set; }

        public AddressComplex SystemAddress_Complex()
        {
            return AddressComplex.SystemAddress_Complex();
        }

    }

}
