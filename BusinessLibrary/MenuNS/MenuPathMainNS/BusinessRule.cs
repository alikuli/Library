using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.RightsNS;
using System;
using System.Text;

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

            MenuPath1 menu1;
            MenuPath2 menu2;
            MenuPath3 menu3;
            if (!entity.MenuPath1Id.IsNullOrWhiteSpace())
                menu1 = _menupath1Biz.Find(entity.MenuPath1Id);
            else
                menu1 = entity.MenuPath1;

            if (!entity.MenuPath2Id.IsNullOrWhiteSpace())
                menu2 = _menupath2Biz.Find(entity.MenuPath2Id);
            else
                menu2 = entity.MenuPath2;

            if (!entity.MenuPath3Id.IsNullOrWhiteSpace())
                menu3 = _menupath3Biz.Find(entity.MenuPath3Id);
            else
                menu3 = entity.MenuPath3;


            menu1.IsNullThrowException();
            menu2.IsNullThrowException();
            menu3.IsNullThrowException();

            entity.Name = entity.MakeName(menu1.Name, menu2.Name, menu3.Name);
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
