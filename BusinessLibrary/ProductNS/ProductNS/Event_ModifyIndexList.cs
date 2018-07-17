using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            //indexListVM.IsImageTiled = true;
            //indexListVM.Heading.Main = "Menu";
            //indexListVM.Show.Create = true;
            //indexListVM.MenuManager.MenuLevelEnum = parameters.Menu.MenuLevel;

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

            indexListVM.Show.EditDeleteAndCreate = true;
            indexListVM.IsImageTiled = true;

            //MenuManager mm = makeMenuManager(parameters);
            //indexListVM.MenuManager = mm;
            //indexListVM.Heading.Column = makeName(indexListVM);


        }

        ////This supplies a dummy MenuPathMain for the Back to List in the Create.
        //private MenuManager makeMenuManager(ControllerIndexParams parameters)
        //{


        //    MenuManager mm = new MenuManager(parameters.Id, null, null, null, parameters.Menu.MenuLevelEnum, parameters.Menu.ReturnUrl, true, "", parameters.SelectedId, parameters.SearchFor, parameters.SortBy,parameters.ActionNameEnum);

        //    if (!parameters.Menu.MenuPathMainId.IsNullOrWhiteSpace())
        //    {
        //        mm.MenuPathMain = _menuPathMainBiz.Find(parameters.Menu.MenuPathMainId);
        //        mm.MenuPathMain.IsNullThrowException();
        //    }

        //    if (!parameters.Menu.ProductId.IsNullOrWhiteSpace())
        //    {
        //        mm.Product = Find(parameters.Menu.ProductId);
        //        mm.Product.IsNullThrowException();
        //    }


        //    if (!parameters.Menu.ProductChildId.IsNullOrWhiteSpace())
        //    {
        //        mm.ProductChild = _productChildBiz.Find(parameters.Menu.ProductChildId);
        //        mm.ProductChild.IsNullThrowException();
        //    }





        //    return mm;


        //}



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
