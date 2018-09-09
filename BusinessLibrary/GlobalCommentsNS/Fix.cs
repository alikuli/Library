using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Linq;
using UserModels;

namespace UowLibrary.GlobalCommentsNS
{
    public partial class GlobalCommentBiz 
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            fixall(parm.Entity as GlobalComment);
            base.Fix(parm);
        }


        private void fixall(GlobalComment gc)
        {
            if (gc.Comment.IsNullOrWhiteSpace())
                return ;

            gc.UserId.IsNullOrWhiteSpaceThrowException("User is not logged in");

            if (!gc.UserId.IsNullOrWhiteSpace())
            {
                ApplicationUser user = _userBiz.Find(gc.UserId);
                user.IsNullThrowException();
                user.GlobalComments.Add(gc);
                gc.Name = UserName;
            }



            if (!gc.MenuPath1Id.IsNullOrWhiteSpace())
            {
                MenuPath1 m1 = _menuPathMainBiz.MenuPath1Biz.Find(gc.MenuPath1Id);
                m1.IsNullThrowException();
                m1.GlobalComments.Add(gc);
                return;
            }


            if (!gc.MenuPath2Id.IsNullOrWhiteSpace())
            {
                MenuPath2 m2 = _menuPathMainBiz.MenuPath2Biz.Find(gc.MenuPath2Id);
                m2.IsNullThrowException();
                m2.GlobalComments.Add(gc);
                return;

            }


            if (!gc.MenuPath3Id.IsNullOrWhiteSpace())
            {
                MenuPath3 m3 = _menuPathMainBiz.MenuPath3Biz.Find(gc.MenuPath3Id);
                m3.IsNullThrowException();
                m3.GlobalComments.Add(gc);


            }


            if (!gc.ProductId.IsNullOrWhiteSpace())
            {
                Product p = _productBiz.Find(gc.ProductId);
                p.IsNullThrowException();
                p.GlobalComments.Add(gc);
            }


            if (!gc.ProductChildId.IsNullOrWhiteSpace())
            {
                ProductChild pc = _productBiz.ProductChildBiz.Find(gc.ProductChildId);
                pc.IsNullThrowException();
                pc.GlobalComments.Add(gc);
            }

        }




    }
}
