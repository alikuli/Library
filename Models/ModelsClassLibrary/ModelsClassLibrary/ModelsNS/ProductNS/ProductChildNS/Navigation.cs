using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ComplexClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.ProductChildNS
{
    public partial class ProductChild
    {

        /// <summary>
        /// This is the User who owns this product. This needs to be set at the time of creating because we may want to upload all this stuff in their own directory
        /// </summary>
        [Display(Name = "Owner")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }



        /// <summary>
        /// This is the owning product.
        /// </summary>
        [Display(Name = "Product")]
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public virtual ICollection<GlobalComment> GlobalComments { get; set; }
        public virtual ICollection<LikeUnlike> LikeUnlikes { get; set; }
        //public virtual ICollection<Feature> Features { get; set; }


    }





}
