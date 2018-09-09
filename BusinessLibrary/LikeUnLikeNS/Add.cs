using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using System.Linq;
using UserModels;

namespace UowLibrary.LikeUnlikeNS
{
    public partial class LikeUnlikeBiz : BusinessLayer<LikeUnlike>
    {
        public LikeUnlikeParameter AddLikeAndReturnCount(string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId, bool isLike, string comment)
        {
            userId.IsNullOrWhiteSpaceThrowException("No user is logged in!");

            LikeUnlike likeUnlike = Factory() as LikeUnlike;
            likeUnlike.Initialize(menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId, isLike, comment);

            if (!menuPath1Id.IsNullOrWhiteSpace())
            {
                likeUnlike.MenuPath1Id = menuPath1Id;
                MenuPath1 m1 = _menuPathMainBiz.MenuPath1Biz.Find(menuPath1Id);
                m1.IsNullThrowException();
                m1.LikeUnlikes.Add(likeUnlike);
            }


            if (!menuPath2Id.IsNullOrWhiteSpace())
            {
                likeUnlike.MenuPath2Id = menuPath2Id;
                MenuPath2 m2 = _menuPathMainBiz.MenuPath2Biz.Find(menuPath2Id);
                m2.IsNullThrowException();
                m2.LikeUnlikes.Add(likeUnlike);
            }


            if (!menuPath3Id.IsNullOrWhiteSpace())
            {
                likeUnlike.MenuPath3Id = menuPath3Id;
                MenuPath3 m3 = _menuPathMainBiz.MenuPath3Biz.Find(menuPath3Id);
                m3.IsNullThrowException();
                m3.LikeUnlikes.Add(likeUnlike);

            }


            if (!productId.IsNullOrWhiteSpace())
            {
                likeUnlike.ProductId = productId;
                Product p = _productBiz.Find(productId);
                p.IsNullThrowException();
                p.LikeUnlikes.Add(likeUnlike);
            }


            if (!productChildId.IsNullOrWhiteSpace())
            {
                likeUnlike.ProductChildId = productChildId;
                ProductChild pc = _productBiz.ProductChildBiz.Find(productChildId);
                pc.IsNullThrowException();
                pc.LikeUnlikes.Add(likeUnlike);
            }

            if (!userId.IsNullOrWhiteSpace())
            {
                likeUnlike.UserId = userId;
                ApplicationUser user = _userBiz.Find(userId);
                user.IsNullThrowException();
                user.LikeUnlikes.Add(likeUnlike);
            }
            //if it is a like delete the similar dislike. If it is a dislike delete the similar like
            bool relatedOppoisteDeleted = deleteTheRelatedLikeUnlike(menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId, isLike);
            CreateAndSave(CreateControllerCreateEditParameter(likeUnlike as ICommonWithId));

            return Count(
                menuPath1Id,
                menuPath2Id,
                menuPath3Id,
                productId,
                productChildId,
                userId, 
                relatedOppoisteDeleted);

        }

        private bool deleteTheRelatedLikeUnlike(string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId, bool isLike)
        {
            LikeUnlike dummy = Factory() as LikeUnlike;
            dummy.Initialize(menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId, !isLike, "dummy content");

            string nameId = dummy.KeyGenerator();
            LikeUnlike lu = FindAll().FirstOrDefault(x => x.Name == nameId);
            if (!lu.IsNull())
            {
                Delete(lu.Id);
                return true; //there was a related opposite which was deleted!
            }
            return false;
        }
    }
}
