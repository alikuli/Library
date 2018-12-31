using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Linq;
using System.Collections.Generic;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Threading.Tasks;
using InterfacesLibrary.SharedNS;
using System.Data.Entity;

namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {
        /// <summary>
        /// Every Owner can add only One product of a specific name.
        /// We need to load the product in this at a higher level because we are unable to access ProductBiz here
        /// </summary>
        /// <param name="parm"></param>
        public override void Fix(ControllerCreateEditParameter parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");

            ProductChild pc = parm.Entity as ProductChild;
            pc.IsNullThrowException("Unable to unbox Product Child");

            //Product comes from the controller.
            pc.ProductId.IsNullOrWhiteSpaceThrowException("There is no parent Product");
            pc.Product.IsNullThrowException("Product Has not been loaded!");

             
            if (pc.Name.IsNullOrWhiteSpace())
                pc.Name = pc.Product.Name;

            //we need to load the owner in because the name is used to create
            //a directory for the uploads.
            //first get the Owners Id
            Owner owner = OwnerBiz.GetOwnerForUser(UserId);
            owner.IsNullThrowException("Owner not found!");
            pc.OwnerId = owner.Id;
            pc.Owner = owner;

            addProductFeatures(pc);

            base.Fix(parm);
        }

        private void addProductFeatures(ProductChild pc)
        {
            //get all the productFeaures from the product.
            pc.Product.IsNullThrowException("Product");
            if (pc.Product.ProductFeatures.IsNullOrEmpty())
                return;
            
            List<ProductFeature> productFeaturesList = pc.Product.ProductFeatures.ToList();
            
            //add them to productChild
            //
            foreach (ProductFeature pf in productFeaturesList)
            {
                if (pc.ProductChildFeatures.IsNullOrEmpty())
                    continue;
                //see if the entity exists, if not, then add
                bool productChildFeatureExists = pc.ProductChildFeatures.Any(x => 
                    x.MetaData.IsDeleted == false && 
                    x.ProductChildId == pc.Id && 
                    x.Name.ToLower() == pf.Name.ToLower());
                
                if (productChildFeatureExists)
                { }
                else
                {
                    ProductChildFeature pcf = ProductChildFeatureBiz.Factory() as ProductChildFeature;
                    pcf.ProductChildId = pc.Id;
                    pcf.ProductChild = pc;
                    pcf.Name = pf.Name;
                    pcf.Comment = pf.Comment;

                    ProductChildFeatureBiz.CreateEntity(pcf);
                }
            }
        }
         
        public override IQueryable<ProductChild> GetDataToCheckDuplicateName(ProductChild productChild)
        {
            var data = base.GetDataToCheckDuplicateName(productChild);

            string ownerId = productChild.OwnerId;
            ownerId.IsNullOrWhiteSpaceThrowException("OwnerId");

            var dataForOwner = data.Where(x => x.OwnerId == ownerId);
            return dataForOwner;
        }



        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        {
            //var lstEntities = await FindAllAsync();
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Owner owner = OwnerBiz.GetOwnerForUser(UserId);
            owner.IsNullThrowException("Owner not found.");

            var lstEntities = await FindAll().Where(x => x.OwnerId == owner.Id).ToListAsync();
            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;
        }
    }
}
