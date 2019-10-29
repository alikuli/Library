using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    /// <summary>
    /// Note. Scratch card 16 digit serial number is placed in Name AND in ProductsOwnNumber. I believe thqt ProductsOwnNumber
    /// needs to be removed. No need for that. Name is fine because it will not duplicate intrinsically.
    /// </summary>
    public abstract partial class ProductAbstract
    {



        public virtual ICollection<LikeUnlike> LikeUnlikes { get; set; }


        [NotMapped]
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




    }
}