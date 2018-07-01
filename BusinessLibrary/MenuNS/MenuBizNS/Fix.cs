using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {

        public override void Fix(MenuPathMain entity)
        {
            base.Fix(entity);
            _menuPathMainBiz.Fix(entity);
        }

        //private void getMenuPaths(MenuPathMain entity)
        //{
        //    entity.MenuPath1Id.IsNullOrWhiteSpaceThrowException();
        //    entity.MenuPath2Id.IsNullOrWhiteSpaceThrowException();
        //    entity.MenuPath3Id.IsNullOrWhiteSpaceThrowException();

        //    if (entity.MenuPath1.IsNull())
        //    {
        //        entity.MenuPath1 = _menuPath1Biz.Find(entity.MenuPath1Id);
        //        entity.MenuPath1.IsNullThrowException();
        //    }

        //    if (entity.MenuPath2.IsNull())
        //    {
        //        entity.MenuPath2 = _menuPath2Biz.Find(entity.MenuPath2Id);
        //        entity.MenuPath2.IsNullThrowException();
        //    }

        //    if (entity.MenuPath3.IsNull())
        //    {
        //        entity.MenuPath3 = _menuPath3Biz.Find(entity.MenuPath3Id);
        //        entity.MenuPath3.IsNullThrowException();
        //    }

        //}

        //private void fixName(MenuPathMain entity)
        //{
        //    entity.Name = entity.MakeName(entity.MenuPath1.Name, entity.MenuPath2.Name, entity.MenuPath3.Name);
        //}

    }
}
