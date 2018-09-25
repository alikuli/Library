using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
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
            //indexListVM.Show.Create = true;
            //we mus select the correct menu state

            IMenuManager mm = makeMenuManager(parameters);
            indexListVM.MenuManager = mm;
            indexListVM.Heading.Column = "Menu Items";
            int webClicksCount = PageViewBiz.IsNull() ? 0 : PageViewBiz.GetClickCount();
            string recordStr = (webClicksCount == 1 ? "view" : "views");
            indexListVM.MenuManager.WebClicksCount = string.Format("{0:n0} {1}",
                webClicksCount,
                recordStr);
            //indexListVM.MenuManager.MenuLevelEnum = parameters.Menu.MenuLevelEnum;

            #region Old Code
            //if (!parameters.Id.IsNullOrWhiteSpace())
            //{
            //    //in menu level 1-4 the parameters.Id that is returned is a MainMenuPath
            //    //in menu level 5, the parameters.Id that is returned is a Product.Id

            //    switch (parameters.Menu.MenuLevel)
            //    {
            //        case MenuLevelENUM.Level_1: //Menu Level 1
            //        case MenuLevelENUM.Level_2: //Menu Level 2
            //        case MenuLevelENUM.Level_3: //Menu Level 3
            //        case MenuLevelENUM.Level_4: //Product Level
            //            MenuPathMain mpm;
            //            if (!parameters.Id.IsNullOrWhiteSpace())
            //            {
            //                mpm = Find(parameters.Id);
            //            }
            //            else
            //            {

            //            }

            //            indexListVM.MenuManager = new MenuManager(mpm, null, null, parameters.Menu.MenuLevel, parameters.ReturnUrl, isMenu);
            //            break;

            //        case MenuLevelENUM.Level_5: //Product Children Level


            //            MenuPathMain mpm2 = FindAll().FirstOrDefault(x =>
            //                x.MenuPath1Id == parameters.Menu.MenuPath1Id &&
            //                x.MenuPath2Id == parameters.Menu.MenuPath2Id &&
            //                x.MenuPath3Id == parameters.Menu.MenuPath3Id
            //                );

            //            Product product = _productBiz.Find(parameters.Id);
            //            indexListVM.MenuManager = new MenuManager(mpm2, product, null, parameters.Menu.MenuLevel, parameters.ReturnUrl, isMenu);

            //            break;

            //        case MenuLevelENUM.unknown:
            //            break;
            //        default:
            //            break;
            //    }


            //}
            //else
            //{
            //    //we need to find a dummy ProductCategoryMain which will full fill the requirements of the menu level.
            //    MenuPathMain pcm = GetMenuPath(parameters);
            //    if (pcm.IsNull())
            //    {
            //        ErrorsGlobal.Add("PCM cannot be null. Programming Error", MethodBase.GetCurrentMethod());
            //        throw new Exception(ErrorsGlobal.ToString());
            //    }
            //}
            #endregion



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
                    p = _productBiz.Find(pId);
                    p.IsNullThrowException();
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

            MenuManager mm = new MenuManager(mpm, p, pc, parm.Menu.MenuEnum, parm.BreadCrumbManager, parm.LikeUnlikeCounter, UserId);
            mm.BreadCrumbManager = parm.BreadCrumbManager;
            mm.IndexMenuVariables.IsAdmin = UserBiz.IsAdmin(UserId);
            return mm as IMenuManager;

        }



    }
}
