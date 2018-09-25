using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using System;
using System.Linq;
using System.Reflection;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;


namespace UowLibrary.MenuNS
{
    public partial class MenuPath1Biz : BusinessLayer<MenuPath1>
    {


        public MenuPath1Biz(IRepositry<MenuPath1> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
        }

        public MenuPath1ENUM ChangeToEnum(MenuPath1 mp1)
        {

            string spacesRemovedName = mp1.Name.RemoveAllSpaces();
            try
            {
                MenuPath1ENUM mp1Enum = (MenuPath1ENUM)Enum.Parse(typeof(MenuPath1ENUM), spacesRemovedName, true);
                return mp1Enum;

            }
            catch (ArgumentException a)
            {
                ErrorsGlobal.Add(string.Format("Unable to convert the enum: '{0}'", spacesRemovedName), MethodBase.GetCurrentMethod(), a);
                throw new ArgumentException(ErrorsGlobal.ToString());
            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Error converting enum: '{0}'", spacesRemovedName), MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }
        }

        public MenuPath1 FindByMenuPath1EnumFor(MenuPath1ENUM menuPath1Enum)
        {
            MenuPath1 mp1 = FindAll().FirstOrDefault(x => x.MenuPath1Enum == menuPath1Enum);
            return mp1;
        }


    }
}
