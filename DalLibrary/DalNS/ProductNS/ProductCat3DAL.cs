using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class ProductCat3DAL:Repositry<ProductCat3>
    {



        public ProductCat3DAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }




        /// <summary>
        /// This will not actually delete, but mark the item as deleted=true
        /// Special consideration has to be taken when deleting these as they make the ProductCategoryMain. Changing the name is no big deal, because that is created on the fly.
        /// The plan is that if the category is deleted, then it should be removed from the database, i.e. made into null. If the resulting item is a duplicate, 
        /// then that too needs to be removed.
        /// However, then we get into problems with discounts it rules exist there, thereofore we will need to remove the rules as well. Before doing all this, the 
        /// result need to be shown BEFORE the delete is given.
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(ProductCat3 entity)
        {
            //Check to see it it exists in the main category. If it does, remove it.
            //Check to see if after removing it, the main category becomes a duplicate. It it does, Remove it.
            //Check to see if there are any rules for discounts for the main category which is getting removed, if there are remove them.

            //If we remove the main categories, then we will also have to fix the discounts as well. Suggestion is to substitute it with the productCategory
            //it is duplicating

            //If we remove the main categories, then we will also have to fix the Products as well. Suggestion, substitute it with the productCategory that
            //it is duplicating.


            //find these categories discounts and replace them with the category that will duplicate them
            //find these categories products and replace them with the category that will duplicate them


            //Just to be safe now, I will not allow deletion if the category is being used, I will respond with list of Categories being affected by it along with list of products and discounts.

            //this is the list of categories that that contain Cat1
            base.Delete(entity);
        }




    }
}
