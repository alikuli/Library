using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UserModels;
using WebLibrary.Programs;
using System;
using ModelsClassLibrary.RightsNS;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Text;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Reflection;
using ModelsClassLibrary.MenuNS;

namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz 
    {


        public override void BusinessRulesFor(MenuPathMain menupathmain)
        {
            MakeName(menupathmain);

            base.BusinessRulesFor(menupathmain);


        }

        private void MakeName(MenuPathMain entity)
        {
            string menu1Name = "";
            string menu2Name = "";
            string menu3Name = "";

            if (!entity.MenuPath1Id.IsNullOrWhiteSpace())
            {
                var menu1 = _menupath1Biz.Find(entity.MenuPath1Id);

                if (menu1.IsNull())
                {
                    ErrorsGlobal.Add("Menu Path 1 was not found", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
                menu1Name = menu1.Name;

            }

            if (!entity.MenuPath2Id.IsNullOrWhiteSpace())
            {
                var c2 = _menupath2Biz.Find(entity.MenuPath2Id);

                if (c2.IsNull())
                {
                    entity.MakeName(menu1Name, menu2Name, menu3Name);
                    return;
                }
                menu2Name = c2.Name;

            }


            if (!entity.MenuPath3Id.IsNullOrWhiteSpace())
            {
                var c3 = _menupath3Biz.Find(entity.MenuPath3Id);

                if (!c3.IsNull())
                {
                    menu3Name = c3.Name;
                }

            }

            entity.MakeName(menu1Name, menu2Name, menu3Name);
        }




        private string MakeName(Right entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(entity.RightsFor.ToString());

            if (!entity.Id.IsNullOrWhiteSpace())
            {
                //locate the user for whom you are creating the rights for.
                var user = UserDal.FindForLightNoTracking(entity.UserId);
                if (user.IsNull())
                {
                    ErrorsGlobal.Add("User for whom Rights are being created was not found!", "Business Rules");
                    throw new Exception(ErrorsGlobal.ToString());
                }

                sb.Append(string.Format(" {0} [{1}]",
                    user.UserName, 
                    user.Id));


            }
            return sb.ToString();
        }        

    }
}
