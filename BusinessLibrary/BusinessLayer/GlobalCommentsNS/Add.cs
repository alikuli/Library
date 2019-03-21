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
    public partial class GlobalCommentBiz : BusinessLayer<GlobalComment>
    {

        public bool AddAndSaveComment(string comment, string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId)
        {
            GlobalComment gc = new GlobalComment(comment, menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId);
            return AddAndSaveComment(gc);
        }

        public bool AddAndSaveComment(GlobalComment gc)
        {
            if (gc.Comment.IsNullOrWhiteSpace())
                return false;

            gc.UserId.IsNullOrWhiteSpaceThrowException("No user is logged in!");


            if (!gc.MenuPath1Id.IsNullOrWhiteSpace())
            {
                MenuPath1 m1 = _menuPathMainBiz.MenuPath1Biz.Find(gc.MenuPath1Id);
                m1.IsNullThrowException();
                m1.GlobalComments.Add(gc);
            }


            if (!gc.MenuPath2Id.IsNullOrWhiteSpace())
            {
                MenuPath2 m2 = _menuPathMainBiz.MenuPath2Biz.Find(gc.MenuPath2Id);
                m2.IsNullThrowException();
                m2.GlobalComments.Add(gc);
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

            if (!gc.UserId.IsNullOrWhiteSpace())
            {
                ApplicationUser user = _userBiz.Find(gc.UserId);
                user.IsNullThrowException();
                //user.GlobalComments.Add(gc);
            }

            gc.Name = UserName;

            ControllerCreateEditParameter parm = new ControllerCreateEditParameter();
            parm.Entity = gc as ICommonWithId;

            CreateAndSave(parm);
            return true;

        }

        public long TotalCommentsFor(string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId)
        {
            long ttlComment = 0;

            if (productChildId.IsNullOrWhiteSpace())
            {
                return TotalCommentsForProductChild(productChildId);
            }

            if (productId.IsNullOrWhiteSpace())
            {
                return TotalCommentsForProduct(productId);
            }

            if (menuPath3Id.IsNullOrWhiteSpace())
            {
                return TotalCommentsForMenuPath3(menuPath3Id);
            }


            if (menuPath2Id.IsNullOrWhiteSpace())
            {
                return TotalCommentsForMenuPath2(menuPath2Id);
            }

            if (menuPath1Id.IsNullOrWhiteSpace())
            {
                return TotalCommentsForMenuPath1(menuPath1Id);
            }

            return ttlComment;
        }

        public long TotalCommentsForProductChild(string productChildId)
        {
            long ttlComment = 0;

            if (productChildId.IsNullOrWhiteSpace())
            {
                ttlComment = FindAll().Cast<GlobalComment>().Count(x => x.ProductChildId == productChildId);
                return ttlComment;
            }

            return ttlComment;
        }

        public long TotalCommentsForProduct(string productId)
        {
            long ttlComment = 0;

            if (productId.IsNullOrWhiteSpace())
            {
                ttlComment = FindAll().Cast<GlobalComment>().Count(x => x.ProductId == productId);
                return ttlComment;
            }

            return ttlComment;
        }

        public long TotalCommentsForMenuPath1(string menuPath1Id)
        {
            long ttlComment = 0;

            if (menuPath1Id.IsNullOrWhiteSpace())
            {
                ttlComment = FindAll().Cast<GlobalComment>().Count(x => x.MenuPath1Id == menuPath1Id);
                return ttlComment;
            }

            return ttlComment;
        }


        public long TotalCommentsForMenuPath2(string menuPath2Id)
        {
            long ttlComment = 0;

            if (menuPath2Id.IsNullOrWhiteSpace())
            {
                ttlComment = FindAll().Cast<GlobalComment>().Count(x => x.MenuPath2Id == menuPath2Id);
                return ttlComment;
            }

            return ttlComment;
        }

        public long TotalCommentsForMenuPath3(string menuPath3Id)
        {
            long ttlComment = 0;

            if (menuPath3Id.IsNullOrWhiteSpace())
            {
                ttlComment = FindAll().Cast<GlobalComment>().Count(x => x.MenuPath3Id == menuPath3Id);
                return ttlComment;
            }

            return ttlComment;
        }


    }
}
