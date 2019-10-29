using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.IndexNS.PlaceLocationNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
using UowLibrary.MenuNS.MenuStateNS;

namespace UowLibrary.MenuNS
{
    /// <summary>
    /// This is where all the data is created for the menu depending on the menu level.
    /// </summary>
    public partial class MenuBiz
    {
        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.IsImageTiled = true;
            indexListVM.Heading.Main = "Menu";


            IMenuManager mm = makeMenuManager(parameters);
            indexListVM.MenuManager = mm;
            indexListVM.Heading.Column = "Menu Items";
            int webClicksCount = PageViewBiz.IsNull() ? 0 : PageViewBiz.GetClickCount();
            string recordStr = (webClicksCount == 1 ? "view" : "views");
            indexListVM.MenuManager.WebClicksCount = string.Format("{0:n0} {1}",
                webClicksCount,
                recordStr);

            if (!UserId.IsNullOrEmpty())
            {
                Person userPerson = UserBiz.GetPersonFor(UserId);
                userPerson.IsNullThrowException("userPerson");
                indexListVM.MenuManager.UserPersonId = userPerson.Id;
            }

            if (!parameters.ReturnUrl.IsNullOrWhiteSpace())
            {
                indexListVM.MenuManager.ReturnUrl = parameters.ReturnUrl;
            }


            //indexListVM.MainLocationSelectorClass = new MainLocationSelectorClass(addressBiz.FindAll().ToList());
            indexListVM.MainLocationSelectorClass = new MainLocationSelectorClass();
            indexListVM.MainLocationSelectorClass.AddCountries(addressBiz.FindAll().ToList());
        }

        //This supplies a dummy MenuPathMain for the Back to List in the Create.
        protected IMenuManager makeMenuManager(ControllerIndexParams parm)
        {

            MenuPathMain mpm = null;
            Product p = null;
            ProductChild pc = null;

            switch (parm.MenuEnum)
            {
                case EnumLibrary.EnumNS.MenuENUM.IndexMenuPath2:
                case EnumLibrary.EnumNS.MenuENUM.IndexMenuPath3:
                case EnumLibrary.EnumNS.MenuENUM.IndexMenuProduct:

                    parm.MenuPathMainId.IsNullOrWhiteSpaceThrowException();
                    string mpmId = parm.MenuPathMainId;
                    mpm = Find(mpmId);
                    mpm.IsNullThrowException();
                    break;

                case EnumLibrary.EnumNS.MenuENUM.IndexMenuProductChild:

                    parm.ProductId.IsNullOrWhiteSpaceThrowException();
                    string pId = parm.ProductId;
                    p = ProductBiz.Find(pId);
                    p.IsNullThrowException();
                    break;

                case EnumLibrary.EnumNS.MenuENUM.IndexMenuProductChildLandingPage:

                    parm.ProductChildId.IsNullOrWhiteSpaceThrowException();
                    string productChildId = parm.ProductChildId;
                    pc = ProductChildBiz.Find(productChildId);
                    pc.IsNullThrowException();

                    //add the features
                    pc.AllFeatures = ProductChildBiz.Get_All_ProductChild_Features_For(pc);
                    break;

                case EnumLibrary.EnumNS.MenuENUM.IndexMenuPath1:
                case EnumLibrary.EnumNS.MenuENUM.EditMenuPath1:
                case EnumLibrary.EnumNS.MenuENUM.EditMenuPath2:
                case EnumLibrary.EnumNS.MenuENUM.EditMenuPath3:
                case EnumLibrary.EnumNS.MenuENUM.EditMenuPathMain:
                case EnumLibrary.EnumNS.MenuENUM.EditMenuProduct:
                case EnumLibrary.EnumNS.MenuENUM.EditMenuProductChild:
                case EnumLibrary.EnumNS.MenuENUM.CreateMenuPath1:
                case EnumLibrary.EnumNS.MenuENUM.CreateMenuPath2:
                case EnumLibrary.EnumNS.MenuENUM.CreateMenuPath3:
                case EnumLibrary.EnumNS.MenuENUM.CreateMenuPathMenuPathMain:
                case EnumLibrary.EnumNS.MenuENUM.CreateMenuProduct:
                case EnumLibrary.EnumNS.MenuENUM.CreateMenuProductChild:
                default:
                    break;
            }

            MenuManager mm = new MenuManager(mpm, p, pc, parm.MenuEnum, parm.BreadCrumbManager, parm.LikeUnlikeCounter, UserId, parm.ReturnUrl, UserName);
            mm.BreadCrumbManager = parm.BreadCrumbManager;
            //mm.IndexMenuVariables.IsAdmin = UserBiz.IsAdmin(UserId);


            if (!UserId.IsNullOrWhiteSpace())
            {
                Person person = CashTrxBiz.PersonBiz.GetPersonForUserId(UserId);
                if (person.IsNull())
                {
                    //mm.UserMoneyAccount = new UserMoneyAccount();
                }
                else
                {
                    bool isBank = BankBiz.IsBanker_User(UserId);
                    //swithched off to get rid of error. MAY NEED IT BACK!
                    //mm.UserMoneyAccount = MoneyAccountForPerson(person.Id, isBank);
                }
            }


            return mm as IMenuManager;

        }



    }
}
