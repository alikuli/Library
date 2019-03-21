using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
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

            if(!UserId.IsNullOrEmpty())
            {
                Person userPerson = UserBiz.GetPersonFor(UserId);
                userPerson.IsNullThrowException("userPerson");
                indexListVM.MenuManager.UserPersonId = userPerson.Id;
            }

            if (!parameters.ReturnUrl.IsNullOrWhiteSpace())
            {
                indexListVM.MenuManager.ReturnUrl = parameters.ReturnUrl;
            }


        }

        //This supplies a dummy MenuPathMain for the Back to List in the Create.
        protected IMenuManager makeMenuManager(ControllerIndexParams parm)
        {

            MenuPathMain mpm = null;
            Product p = null;
            ProductChild pc = null;

            switch (parm.Menu.MenuEnum)
            {
                case EnumLibrary.EnumNS.MenuENUM.IndexMenuPath2:
                case EnumLibrary.EnumNS.MenuENUM.IndexMenuPath3:
                case EnumLibrary.EnumNS.MenuENUM.IndexMenuProduct:

                    parm.Menu.MenuPathMainId.IsNullOrWhiteSpaceThrowException();
                    string mpmId = parm.Menu.MenuPathMainId;
                    mpm = Find(mpmId);
                    mpm.IsNullThrowException();
                    break;

                case EnumLibrary.EnumNS.MenuENUM.IndexMenuProductChild:

                    parm.Menu.ProductId.IsNullOrWhiteSpaceThrowException();
                    string pId = parm.Menu.ProductId;
                    p = ProductBiz.Find(pId);
                    p.IsNullThrowException();
                    break;

                case EnumLibrary.EnumNS.MenuENUM.IndexMenuProductChildLandingPage:

                    parm.Menu.ProductChildId.IsNullOrWhiteSpaceThrowException();
                    string productChildId = parm.Menu.ProductChildId;
                    pc = ProductChildBiz.Find(productChildId);
                    pc.IsNullThrowException();

                    //add the features
                    pc.AllFeatures = ProductChildBiz.GetAllFeatures(pc);
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

            MenuManager mm = new MenuManager(mpm, p, pc, parm.Menu.MenuEnum, parm.BreadCrumbManager, parm.LikeUnlikeCounter, UserId, parm.ReturnUrl);
            mm.BreadCrumbManager = parm.BreadCrumbManager;
            //mm.IndexMenuVariables.IsAdmin = UserBiz.IsAdmin(UserId);


            if (!UserId.IsNullOrWhiteSpace())
            {
                Person person = CashTrxBiz.PersonBiz.GetPersonForUserId(UserId);
                if (person.IsNull())
                {
                    mm.UserMoneyAccount = new UserMoneyAccount();
                }
                else
                {
                    bool isBank = BankBiz.IsBankerFor(UserId);
                    mm.UserMoneyAccount = CashTrxBiz.MoneyAccountForPerson(person.Id, isBank);
                }
            }


            return mm as IMenuManager;

        }



    }
}
