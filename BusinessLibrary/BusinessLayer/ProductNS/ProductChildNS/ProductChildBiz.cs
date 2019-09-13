using AliKuli.Extentions;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using System.Collections.Generic;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
 

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
            string buttonDud = "";
            //string sortByDud = "";
            LikeUnlikeParameters likeUnlikeParameters = null;

            bool isMenuDud = false;
            bool isUserAdmin = false;
            BuySellDocumentTypeENUM buySellDocumentTypeEnum = BuySellDocumentTypeENUM.Unknown;  //DUD
            BuySellDocStateENUM BuySellDocStateEnum = BuySellDocStateENUM.Unknown; //dud

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
                returnUrl,
                buySellDocumentTypeEnum,
                BuySellDocStateEnum,
                buttonDud);

            InitializeMenuManagerForEntity(parms);

            //IHasUploads hasUploadsEntity = parms.Entity as IHasUploads;
            //MenuManager menuManager = new MenuManager(parms.Entity.MenuManager.MenuPathMain, null, null, parms.Menu.MenuEnum, BreadCrumbManager, parms.LikeUnlikeCounter, UserId, parms.ReturnUrl, UserName);
            IMenuManager menuManager = parms.Entity.MenuManager;
            if (menuManager.IndexMenuVariables.IsNull())
                menuManager.IndexMenuVariables = new IndexMenuVariables(UserId);



            Person person = UserBiz.GetPersonFor(UserId);
            if (!person.IsNull())
            {
                string userPersonId = person.Id;
                string productChildPersonId = productChild.Owner.PersonId;
                menuManager.IndexMenuVariables.updateRequiredProperties(userPersonId, productChildPersonId);

            }



            List<string> pictureAddresses = GetPictureList(productChild);

            //if none are available get them from the product
            if (pictureAddresses.IsNullOrEmpty())
            {
                productChild.Product.IsNullThrowException();
                pictureAddresses = GetPictureList(productChild.Product);

            }

            if (pictureAddresses.IsNullOrEmpty())
            {
                pictureAddresses = GetDefaultPicture();
            }



            menuManager.PictureAddresses = pictureAddresses;

            ////also add the ProductChildperson and UserPerson
            //Person userPerson = UserBiz.GetPersonFor(UserId);

            //if (!productChild.Owner.IsNull())
            //    menuManager.IndexMenuVariables.ProductChildPersonId = productChild.Owner.PersonId;

            productChild.AllFeatures = Get_All_ProductChild_Features_For(productChild);




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



        public bool IsShowHidden { get; set; }
        public override IList<ICommonWithId> GetListForIndex()
        {

            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Owner owner = OwnerBiz.GetOwnerForUser(UserId);
            owner.IsNullThrowException("Owner not found.");

            IList<ProductChild> lst = FindAll().Where(x => x.OwnerId == owner.Id && x.Hide == IsShowHidden).ToList() as IList<ProductChild>;

            return lst.Cast<ICommonWithId>().ToList();
        }

        //public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        //{
        //    IList<ProductChild> lst = await FindAllAsync();
        //    lst = lst.Where(x => x.Hide == IsDontShowHidden).ToList() as IList<ProductChild>;
        //    return lst.Cast<ICommonWithId>().ToList();
        //}
        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        {
            //var lstEntities = await FindAllAsync();
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Owner owner = OwnerBiz.GetOwnerForUser(UserId);
            owner.IsNullThrowException("Owner not found.");

            var lstEntities = await FindAll().Where(x => x.OwnerId == owner.Id && x.Hide == IsShowHidden).ToListAsync();
            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;
        }



    }
}
