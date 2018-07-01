using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.MenuNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class MenusController 
    {


        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {

            //I have made it a convention to pass the correct id in its own named Id Path. So we are going to use that.
            //MenuPathMain entity = Biz.EntityFactoryForHttpGet() as MenuPathMain;

            //This is where the dropdown boxes are loaded with their initial values in the create for Menus
            loadMp1AndMp2Ids(parm);
            loadSelectLists();


            ((MenuPathMain) parm.DudEntity).MenuManager = new MenuManager(
                "", 
                parm.DudEntity as MenuPathMain, 
                null, 
                null, 
                parm.Menu.MenuLevelEnum, 
                parm.Menu.ReturnUrl, 
                true, 
                "", 
                parm.SelectedId, 
                parm.SearchFor, 
                parm.SortBy, 
                parm.ActionNameEnum);

            return View(parm.DudEntity);


        }

        private void loadSelectLists()
        {
            ViewBag.MenuPath2SelectList = _menuBiz.MenuPath2_SelectList();
            ViewBag.MenuPath1SelectList = _menuBiz.MenuPath1_SelectList();
            ViewBag.MenuPath3SelectList = _menuBiz.MenuPath3_SelectList();
        }

        private void loadMp1AndMp2Ids(ControllerIndexParams parm)
        {
            MenuPathMain mpm = null;
            if (!parm.Menu.MenuPathMainId.IsNullOrWhiteSpace())
            {
                mpm = Biz.Find(parm.Menu.MenuPathMainId);
                mpm.IsNullThrowException();



                MenuPathMain dudMenuPathMain = parm.DudEntity as MenuPathMain;

                //Which mpm is this?
                switch (parm.Menu.MenuLevelEnum)
                {
                    case MenuLevelENUM.Level_1:
                        break;
                    case MenuLevelENUM.Level_2:

                        //This mpm contains the correct:
                        // Menupath1
                        // MenuPath2 is a dummy.
                        // MenuPath 3 is a dummy.


                        mpm.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
                        mpm.MenuPath1.IsNullThrowException();

                        dudMenuPathMain.MenuPath1 = mpm.MenuPath1;
                        dudMenuPathMain.MenuPath1Id = mpm.MenuPath1Id;
                        
                    //  Note: mpm.MenuPath2Id = string.Empty; .MenuPath3Id = string.Empty;
                        dudMenuPathMain.MenuManager.MenuPathMain = dudMenuPathMain;
                        
                        break;

                    case MenuLevelENUM.Level_3:
                        //This mpm contains the correct:
                        // Menupath1
                        // MenuPath2
                        // MenuPath 3 is a dummy.
                        mpm.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
                        mpm.MenuPath1.IsNullThrowException();

                        mpm.MenuPath2Id.IsNullOrWhiteSpaceThrowException();
                        mpm.MenuPath2.IsNullThrowException();

                        dudMenuPathMain.MenuPath1 = mpm.MenuPath1;
                        dudMenuPathMain.MenuPath1Id = mpm.MenuPath1Id;
                        
                        dudMenuPathMain.MenuPath2 = mpm.MenuPath2;
                        dudMenuPathMain.MenuPath2Id = mpm.MenuPath2Id;

                    //  Note: mpm.MenuPath2Id = string.Empty; .MenuPath3Id = string.Empty;
                        dudMenuPathMain.MenuManager.MenuPathMain = dudMenuPathMain;
                        
                        break;

                    case MenuLevelENUM.Level_4:
                    case MenuLevelENUM.Level_5:
                    case MenuLevelENUM.Level_6:
                    case MenuLevelENUM.unknown:

                    default:
                        throw new Exception("Programming error in Menu Event_CreateViewAndSetupSelectList");
                }

                //if (menupath2SelectList.IsNull())
                //    menupath2SelectList = _menuBiz.MenuPath2_SelectList();
                _menuBiz.Detach(mpm);


            }
        }

        private void loadRelatedMainPath2(MenuPathMain mpm)
        {
            if (!mpm.MenuPath2Id.IsNullOrWhiteSpace())
            {
                if (mpm.MenuPath2.IsNull())
                {
                    mpm.MenuPath2 = _menuPath2Biz.Find(mpm.MenuPath2Id);
                    mpm.MenuPath2.IsNullThrowException();

                }
            }
        }

        private void loadRelatedMainPath1(MenuPathMain mpm)
        {
            if (!mpm.MenuPath1Id.IsNullOrWhiteSpace())
            {
                if (mpm.MenuPath1.IsNull())
                {
                    mpm.MenuPath1 = _menuPath1Biz.Find(mpm.MenuPath1Id);
                    mpm.MenuPath1.IsNullThrowException();

                }
            }
        }

    }





}