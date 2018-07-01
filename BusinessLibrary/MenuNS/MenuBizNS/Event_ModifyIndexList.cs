using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

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
            indexListVM.Show.Create = true;
            indexListVM.MenuManager.MenuLevelEnum = parameters.Menu.MenuLevelEnum;

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

            MenuManager mm = makeMenuManager(parameters);
            indexListVM.MenuManager = mm;
            indexListVM.Heading.Column = makeName(indexListVM);


        }

        //This supplies a dummy MenuPathMain for the Back to List in the Create.
        private MenuManager makeMenuManager(ControllerIndexParams parameters)
        {


            MenuManager mm = new MenuManager(parameters.Id, null, null, null, parameters.Menu.MenuLevelEnum, parameters.Menu.ReturnUrl, true, "",parameters.SelectedId,parameters.SearchFor, parameters.SortBy,parameters.ActionNameEnum);

            if (!parameters.Menu.MenuPathMainId.IsNullOrWhiteSpace())
            {
                mm.MenuPathMain = Find(parameters.Menu.MenuPathMainId);
                mm.MenuPathMain.IsNullThrowException();
            }

            if (!parameters.Menu.ProductId.IsNullOrWhiteSpace())
            {
                mm.Product = _productBiz.Find(parameters.Menu.ProductId);
                mm.Product.IsNullThrowException();
            }


            if (!parameters.Menu.ProductChildId.IsNullOrWhiteSpace())
            {
                mm.ProductChild = _productChildBiz.Find(parameters.Menu.ProductChildId);
                mm.ProductChild.IsNullThrowException();
            }





            return mm;

            //switch (parameters.Menu.MenuLevel)
            //{
            //    case MenuLevelENUM.unknown:
            //        ErrorsGlobal.Add("Menu Level cannot be unknown here. Programming Error.", MethodBase.GetCurrentMethod());
            //        throw new Exception(ErrorsGlobal.ToString());


            //    case MenuLevelENUM.Level_1:


            //        mm = new MenuManager(parameters.Id, mpm, null, null, parameters.Menu.MenuLevel, parameters.ReturnUrl, true);
            //        return mm;



            //    case MenuLevelENUM.Level_2:
            //        //find MenuPathMain with the same productCat1
            //        //mpm = FindAll()
            //        //    .FirstOrDefault(x => x.MenuPath1Id == mpm.MenuPath1Id);
            //        mm = new MenuManager(parameters.Id, mpm, null, null, parameters.Menu.MenuLevel, parameters.ReturnUrl, true);
            //        return mm;

            //    case MenuLevelENUM.Level_3:
            //        //find MenuPathMain with the same productCat1 & productCat2
            //        //parameters.Menu.MenuPath2Id.IsNullOrWhiteSpaceThrowException("Menu Path 2 Id is null in the parameters");

            //        //mpm = FindAll().FirstOrDefault(x =>
            //        //    x.MenuPath1Id == mpm.MenuPath1Id &&
            //        //    x.MenuPath2Id == mpm.MenuPath2Id);
            //        mm = new MenuManager(parameters.Id, mpm, null, null, parameters.Menu.MenuLevel, parameters.ReturnUrl, true);
            //        return mm;

            //    case MenuLevelENUM.Level_4:
            //        //find MenuPathMain with the same productCat1 & productCat2 &  & productCat3

            //        //parameters.Menu.MenuPath2Id.IsNullOrWhiteSpaceThrowException("Menu Path 2 Id is null in the parameters");
            //        //parameters.Menu.MenuPath3Id.IsNullOrWhiteSpaceThrowException("Menu Path 3 Id is null in the parameters");

            //        //mpm = FindAll().FirstOrDefault(x =>
            //        //    x.MenuPath1Id == mpm.MenuPath1Id &&
            //        //    x.MenuPath2Id == mpm.MenuPath2Id &&
            //        //    x.MenuPath3Id == mpm.MenuPath3Id);


            //        mm = new MenuManager(parameters.Id, mpm, null, null, parameters.Menu.MenuLevel, parameters.ReturnUrl, true);
            //        return mm;

            //    case MenuLevelENUM.Level_5:
            //        throw new NotImplementedException();

            //    default:
            //        ErrorsGlobal.Add("Menu Level cannot be unknown here. Programming Error.", MethodBase.GetCurrentMethod());
            //        throw new Exception(ErrorsGlobal.ToString());
            //}
        }



        private static string makeName(IndexListVM indexListVM)
        {
            string completeName = "";
            string nameProdCat1 = "";

            if (indexListVM.MenuManager.MenuPathMain.IsNull())
                return (completeName = "Menu Items");

            if (!indexListVM.MenuManager.MenuPathMain.MenuPath1.IsNull())
                nameProdCat1 = indexListVM.MenuManager.MenuPathMain.MenuPath1.Name;

            string nameProdCat2 = "";
            if (!indexListVM.MenuManager.MenuPathMain.MenuPath2.IsNull())
                nameProdCat2 = indexListVM.MenuManager.MenuPathMain.MenuPath2.Name;

            string nameProdCat3 = "";
            if (!indexListVM.MenuManager.MenuPathMain.MenuPath3.IsNull())
                nameProdCat3 = indexListVM.MenuManager.MenuPathMain.MenuPath3.Name;


            completeName = "Menu Items";


            return completeName;
        }

    }
}
