using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

            //I dont want to add the features. I will add them temporarily when it is being presented,
            //addProductFeatures(pc);

            base.Fix(parm);
        }

        /// <summary>
        /// this returns a complete list of filled features for a product child. i.e. the product child features and the product features
        /// </summary>
        /// <param name="pc"></param>
        /// <returns></returns>
        //public List<ProductChildFeature> GetAllFeatures(ProductChild pc)
        //{
        //    //get all the productFeaures from the product.
        //    HashSet<ProductChildFeature> allfeatures = getProductFeatures(pc);
        //    allfeatures = getProductChildFeatures(pc, allfeatures);
        //    return allfeatures.OrderBy(x => x.Name).ToList();

        //}

        //private HashSet<ProductChildFeature> getProductChildFeatures(ProductChild pc, HashSet<ProductChildFeature> allfeatures)
        //{
        //    pc.IsNullThrowExceptionArgument("productchild");
        //    allfeatures.IsNullThrowExceptionArgument("allfeatures");

        //    if (pc.ProductChildFeatures.IsNullOrEmpty())
        //        return allfeatures;

        //    foreach (ProductChildFeature productChildFeature in pc.ProductChildFeatures.ToList())
        //    {
        //        allfeatures.Add(productChildFeature);
        //    }
        //    return allfeatures;

        //}

        //private HashSet<ProductChildFeature> getProductFeatures(ProductChild pc)
        //{
        //    HashSet<ProductChildFeature> allfeatures = new HashSet<ProductChildFeature>();
        //    pc.Product.IsNullThrowException("Product cannot be null");

        //    //there are no product features
        //    if (pc.Product.ProductFeatures.IsNullOrEmpty())
        //        return allfeatures;

        //    List<ProductFeature> productFeaturelst = pc.Product.ProductFeatures.ToList();
        //    foreach (ProductFeature prodFeature in productFeaturelst)
        //    {
        //        FeatureAbstract featureAbstract = prodFeature as FeatureAbstract;
        //        featureAbstract.IsNullThrowException("unable to unbox Feature Abstract");
        //        ProductChildFeature pcf = featureAbstract as ProductChildFeature;
        //        pcf.IsNullThrowException("Unable to unbox pcf");
        //        allfeatures.Add(pcf);
        //    }

        //    return allfeatures;

        //}


        public override IQueryable<ProductChild> GetDataToCheckDuplicateName(ProductChild productChild)
        {
            var data = base.GetDataToCheckDuplicateName(productChild);

            string ownerId = productChild.OwnerId;
            ownerId.IsNullOrWhiteSpaceThrowException("OwnerId");

            var dataForOwner = data.Where(x => x.OwnerId == ownerId &&
                x.IdentificationNumber == productChild.IdentificationNumber &&
                x.SerialNumber == productChild.SerialNumber);

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

        public void LogPersonsVisit(string UserId, ProductChild productChild)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("UserId");
            Person person = UserBiz.GetPersonFor(UserId);
            person.IsNullThrowException("Person for user not found");

            //if (productChild.VisitorPeople.IsNull())
            //    productChild.VisitorPeople = new List<Person>();


            //if (person.VisitedProductChildren.IsNull())
            //    person.VisitedProductChildren = new List<ProductChild>();

            //productChild.VisitorPeople.Add(person);
            //person.VisitedProductChildren.Add(productChild);
            SaveChanges();

        }


        public List<ProductChildFeature> GetAllFeatures(ProductChild productChild)
        {
            productChild.IsNullThrowExceptionArgument("productChild");

            //First get all of it's MenuPaths
            //we do not know which MenuPath we have come from

            //initialize the holder list
            HashSet<ProductChildFeature> allFeatures = new HashSet<ProductChildFeature>();
            //get the product
            Product product = productChild.Product;
            product.IsNullThrowException("product");

            //Add all productChild features
            if (!productChild.ProductChildFeatures.IsNullOrEmpty())
            {
                foreach (ProductChildFeature pcf in productChild.ProductChildFeatures)
                {
                    if (!pcf.IsNull())
                        allFeatures.Add(pcf);
                }
            }

            //Add all the product features
            addProductFeatures(allFeatures, product);


            return allFeatures.OrderBy(x => x.Name).ToList();
        }

        private static void addProductFeatures(HashSet<ProductChildFeature> allFeatures, Product product)
        {
            if (!product.ProductFeatures.IsNullOrEmpty())
            {
                foreach (ProductFeature prodfea in product.ProductFeatures)
                {
                    ProductChildFeature pfa = prodfea.ToProductChildFeature();
                    if (!pfa.IsNull())
                    {   
                        bool featureNameDoesNotExist = allFeatures.FirstOrDefault(x => x.Name.ToLower() == prodfea.Name.ToLower()).IsNull();
                        if (featureNameDoesNotExist)
                            {
                                allFeatures.Add(pfa);
                            }
                        }
                }
            }
        }


    }
}
