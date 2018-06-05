using AliKuli.Extentions;
using Data.MenuNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;

namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz 
    {

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }


        //public override string[] GetDataForStringArrayFormat
        //{
        //    get
        //    {
        //        return ProductCategoryMainArray.DataArray();
        //    }
        //}


        public override void AddInitData()
        {
            //get the data
            List<MenuPathMainHelper> dataList = new DatastoreNS.MenuPathMainInitilizingDataList().DataList();

            if (!dataList.IsNullOrEmpty())
            {
                foreach (var item in dataList)
                {

                    if (item.MenuPath1.IsNullOrWhiteSpace())
                    {
                        ErrorsGlobal.Add(string.Format("Menu Path 1 '{0}' not found", item.MenuPath1),MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                    }


                    MenuPath1 menu1 = _menupath1Biz.FindByName(item.MenuPath1);
                    MenuPath2 menu2 = _menupath2Biz.FindByName(item.MenuPath2);
                    MenuPath3 menu3 = _menupath3Biz.FindByName(item.MenuPath3);

                    MenuPathMain pcm = new MenuPathMain();

                    pcm.MenuPath1 = menu1;
                    pcm.MenuPath1Id = menu1.Id;

                    if (menu1.MenuPathMains.IsNull())
                        menu1.MenuPathMains = new List<MenuPathMain>();

                    menu1.MenuPathMains.Add(pcm);




                    if (menu2.IsNull())
                        continue;

                    pcm.MenuPath2 = menu2;
                    pcm.MenuPath2Id = menu2.Id;
                    if (menu2.MenuPathMains.IsNull())
                        menu2.MenuPathMains = new List<MenuPathMain>();
                    menu2.MenuPathMains.Add(pcm);

                    if (menu3.IsNull())
                        continue;

                    pcm.MenuPath3 = menu3;
                    pcm.MenuPath3Id = menu3.Id;
                    if (menu3.MenuPathMains.IsNull())
                        menu3.MenuPathMains = new List<MenuPathMain>();
                    menu3.MenuPathMains.Add(pcm);

                    CreateSave_ForInitializeOnly(pcm);
                }
                

            }
                //SaveChanges();
        }


        //private void addmenu1(MenuPathMainHelper item, MenuPathMain pcm)
        //{
        //    MenuPath1 menu1 = _menupath1Biz.FindByName(item.MenuPath1);
        //    pcm.MenuPath1 = menu1;
        //    pcm.MenuPath1Id = menu1.Id;
        //    if (menu1.MenuPathMains.IsNullOrEmpty())
        //    {
        //        menu1.MenuPathMains = new List<MenuPathMain>();
        //    }
        //    menu1.MenuPathMains.Add(pcm);
        //}



        //private void addmenu2(MenuPathMainHelper item, MenuPath3 menu3, MenuPathMain pcm)
        //{
        //    //menu2 starts here
        //    MenuPath2 menu2 = _menupath2Biz.FindByName(item.MenuPath2);
        //    if (!item.MenuPath2.IsNullOrWhiteSpace())
        //    {
        //        if (menu2.IsNull())
        //        {
        //            ErrorsGlobal.Add(string.Format("Menu Path 2 '{0}' not found", item.MenuPath2), MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());
        //        }

        //        pcm.MenuPath2 = menu2;
        //        pcm.MenuPath2Id = menu2.Id;
        //        if (menu2.MenuPathMains.IsNullOrEmpty())
        //        {
        //            menu2.MenuPathMains = new List<MenuPathMain>();
        //        }
        //        menu2.MenuPathMains.Add(pcm);

        //        addmenu3(item, menu3, pcm);
        //    }
        //}

        //private  void addmenu3(MenuPathMainHelper item, MenuPath3 menu3, MenuPathMain pcm)
        //{
        //    //Cat3 starts here....
        //    if (!item.MenuPath3.IsNullOrWhiteSpace())
        //    {
        //        if (menu3.IsNull())
        //        {
        //            ErrorsGlobal.Add(string.Format("Menu Path 3 '{0}' not found", item.MenuPath3), MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());
        //            throw new Exception(string.Format("Menu Path 3 '{0}' not found", item.MenuPath3));
        //        }
        //        pcm.MenuPath3 = menu3;
        //        pcm.MenuPath3Id = menu3.Id;
        //        if (menu3.MenuPathMains.IsNullOrEmpty())
        //        {
        //            menu3.MenuPathMains = new List<MenuPathMain>();
        //        }
        //        menu3.MenuPathMains.Add(pcm);
        //    }
        //}

    }

}

