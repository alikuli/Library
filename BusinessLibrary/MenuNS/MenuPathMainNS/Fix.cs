using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using System.Linq;
using AliKuli.Extentions;


namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz
    {

        public override void Fix(MenuPathMain entity)
        {
            base.Fix(entity);
            getMenuPaths(entity);
            fixName(entity);

        }
        private void getMenuPaths(MenuPathMain entity)
        {
            entity.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
            entity.MenuPath2Id.IsNullOrWhiteSpaceThrowException();
            entity.MenuPath3Id.IsNullOrWhiteSpaceThrowException();

            if (entity.MenuPath1.IsNull())
            {
                entity.MenuPath1 = _menupath1Biz.Find(entity.MenuPath1Id);
                entity.MenuPath1.IsNullThrowException();
            }

            if (entity.MenuPath2.IsNull())
            {
                entity.MenuPath2 = _menupath2Biz.Find(entity.MenuPath2Id);
                entity.MenuPath2.IsNullThrowException();
            }

            if (entity.MenuPath3.IsNull())
            {
                entity.MenuPath3 = _menupath3Biz.Find(entity.MenuPath3Id);
                entity.MenuPath3.IsNullThrowException();
            }

        }

        private void fixName(MenuPathMain entity)
        {
            entity.Name = entity.MakeName(entity.MenuPath1.Name, entity.MenuPath2.Name, entity.MenuPath3.Name);
        }

    }
}
