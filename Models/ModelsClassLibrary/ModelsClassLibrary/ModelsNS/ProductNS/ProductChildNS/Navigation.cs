using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual ICollection<LikeUnlike> LikeUnlikes { get; set; }
        //public virtual ICollection<Feature> Features { get; set; }

        /// <summary>
        /// These are the messages being advertised by the user.
        /// </summary>
        //public virtual ICollection<Message> MessagesAdvertisment { get; set; }
        public virtual ICollection<Message> Messages { get; set; }


    }





}
