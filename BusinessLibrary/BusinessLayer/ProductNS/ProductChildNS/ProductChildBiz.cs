using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.Linq;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;
namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz : BusinessLayer<ProductChild>
    {

        /// <summary>
        /// All product children will be owned by Owners who will have a person who will have a user
        /// </summary>
        OwnerBiz _ownerBiz;
        ProductChildFeatureBiz _productChildFeatureBiz;
        public ProductChildBiz(IRepositry<ProductChild> entityDal, BizParameters bizParameters, OwnerBiz ownerBiz, ProductChildFeatureBiz productChildFeatureBiz)
            : base(entityDal, bizParameters)
        {
            _ownerBiz = ownerBiz;
            _productChildFeatureBiz = productChildFeatureBiz;
        }


        ProductChildFeatureBiz ProductChildFeatureBiz
        {
            get
            {
                _productChildFeatureBiz.UserId = UserId;
                _productChildFeatureBiz.UserName = UserName;
                return _productChildFeatureBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                _ownerBiz.UserId = UserId;
                _ownerBiz.UserName = UserName;
                return _ownerBiz;
            }
        }


        UserBiz UserBiz
        {
            get
            {
                return _ownerBiz.UserBiz;
            }
        }




        public override List<string> GetPictureList(IHasUploads ihasUploads)
        {
            List<string> addresses = new List<string>();

            if (ihasUploads.MiscFiles.Any(x => !x.MetaData.IsDeleted))
            {
                var lstUploadedFiles = ihasUploads.MiscFiles.Where(x => !x.MetaData.IsDeleted).ToList();
                lstUploadedFiles.IsNullOrEmptyThrowException("Something went worng. This list cannot be empty.");
                foreach (UploadedFile uploadFile in lstUploadedFiles)
                {
                    string pictureAddy = getImageAddressOf(uploadFile);
                    if (!pictureAddy.IsNullOrWhiteSpace())
                    {
                        addresses.Add(pictureAddy);

                    }
                }
            }

            if (addresses.IsNullOrEmpty())
            {
                return GetDefaultPicture(ihasUploads as ProductChild);
            }

            return addresses;
        }
        /// <summary>
        /// We need to load this because GetDefaultPicture override does not take a parameter
        /// </summary>
        //public ProductChild ProductChildForDefaultPicture { get; set; }

        public List<string> GetDefaultPicture(ProductChild productChildForDefaultPicture)
        {
            List<string> lst = new List<string>();
            productChildForDefaultPicture.IsNullThrowException("ProductChildForDefaultPicture");
            productChildForDefaultPicture.Product.IsNullThrowException("ProductChildForDefaultPicture.Product");
            Product product = productChildForDefaultPicture.Product;

            if (product.MiscFiles.IsNullOrEmpty())
            {
                base.GetDefaultPicture();

            }
            else
            {
                foreach (UploadedFile uploadedFile in product.MiscFiles)
                {
                    string pictureAddy = getImageAddressOf(uploadedFile);
                    if (pictureAddy.IsNullOrWhiteSpace())
                        continue;
                    lst.Add(pictureAddy);
                }
            }

            return lst;
        }


        public ProductChild LoadProductChildForLandingPage(string productChildId, string searchFor, string returnUrl)
        {
            //ProductChild productChild = _icrudBiz.Factory() as ProductChild;
            //productChildId.IsNullThrowExceptionArgument("Id not received. Bad Request");

            ProductChild productChild = Find(productChildId);
            productChild.IsNullThrowException("Product Child not found.");

            string productIdDud = "";
            string isandForSearchDud = "";
            string selectIdDud = "";
            string menuPathMainIdDud = "";
            string logoAddress = "";
            //string sortByDud = "";

            LikeUnlikeParameters likeUnlikeParameters = null;

            bool isMenuDud = false;
            bool isUserAdmin = false;

            if (!UserId.IsNullOrWhiteSpace())
                isUserAdmin = UserBiz.IsAdmin(UserId);

            ControllerIndexParams parms = new ControllerIndexParams(
                productChildId,
                menuPathMainIdDud,
                searchFor,
                isandForSearchDud,
                selectIdDud,
                MenuENUM.IndexMenuProductChildLandingPage,
                SortOrderENUM.Item1_Asc,
                logoAddress,
                productChild,
                productChild,
                UserId,
                UserName,
                isUserAdmin,
                isMenuDud,
                BreadCrumbManager,
                ActionNameENUM.Unknown,
                likeUnlikeParameters,
                productIdDud,
                returnUrl);

            InitializeMenuManagerForEntity(parms);

            IHasUploads hasUploadsEntity = parms.Entity as IHasUploads;
            IMenuManager menuManager = parms.Entity.MenuManager;

            menuManager.PictureAddresses = GetPictureList(hasUploadsEntity);

            productChild.AllFeatures = GetAllFeatures(productChild);


            if (UserId.IsNullOrEmpty())
            {
                //Log an annonymous user as a visitor
            }
            else
            {
                //Log user as visitor to this product child
                LogPersonsVisit(UserId, productChild);
            }

            return productChild;
        }


    }
}
