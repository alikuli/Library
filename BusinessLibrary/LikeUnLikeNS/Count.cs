using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using System.Linq;

namespace UowLibrary.LikeUnlikeNS
{
    public partial class LikeUnlikeBiz : BusinessLayer<LikeUnlike>
    {
        /// <summary>
        /// This will find the first count.
        /// </summary>
        /// <param name="menuPath1Id"></param>
        /// <param name="menuPath2Id"></param>
        /// <param name="menuPath3Id"></param>
        /// <param name="productId"></param>
        /// <param name="productChildId"></param>
        /// <param name="userId"></param>
        /// <param name="isLike"></param>
        /// <returns></returns>
        public LikeUnlikeParameter Count(string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId)
        {


            if (!productChildId.IsNullOrWhiteSpace())
                return CountProductChildLikes(productChildId, userId);

            if (!productId.IsNullOrWhiteSpace())
                return ProductLikes(productId, userId);


            if (!menuPath3Id.IsNullOrWhiteSpace())
                return CountMenuPath3Likes(menuPath3Id, userId);


            if (!menuPath2Id.IsNullOrWhiteSpace())
                return countMenuPath2Likes(menuPath2Id, userId);

            if (!menuPath1Id.IsNullOrWhiteSpace())
                return countMenuPath1Likes(menuPath1Id, userId);

            return new LikeUnlikeParameter(0, 0, "Count Default", false, false);


        }

        private LikeUnlikeParameter countMenuPath1Likes(string menuPath1Id, string userId)
        {
            var all = FindAll().Where(x => x.MenuPath1Id == menuPath1Id);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == false)).IsNull();

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "menuPath1Id", hasLiked, hasUnliked);

            addUsersWhoLikeAndDidNotLike(all, param);
            return param;
        }

        private LikeUnlikeParameter countMenuPath2Likes(string menuPath2Id, string userId)
        {

            var all = FindAll().Where(x => x.MenuPath2Id == menuPath2Id);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == false)).IsNull();

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "menuPath2Id", hasLiked, hasUnliked);


            return param;
        }


        private LikeUnlikeParameter CountMenuPath3Likes(string menuPath3Id, string userId)
        {
            var all = FindAll().Where(x => x.MenuPath3Id == menuPath3Id);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == false)).IsNull();

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "menuPath3Id", hasLiked, hasUnliked);
            addUsersWhoLikeAndDidNotLike(all, param);

            return param;

        }

        private LikeUnlikeParameter ProductLikes(string productId, string userId)
        {

            var all = FindAll().Where(x => x.ProductId == productId);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == false)).IsNull();

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "productId", hasLiked, hasUnliked);
            addUsersWhoLikeAndDidNotLike(all, param);

            return param;
        }

        private LikeUnlikeParameter CountProductChildLikes(string productChildId, string userId)
        {

            var all = FindAll().Where(x => x.ProductChildId == productChildId);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.UserId == userId && x.IsLike == false)).IsNull();

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "productChildId", hasLiked, hasUnliked);
            addUsersWhoLikeAndDidNotLike(all, param);

            return param;
        }
        private static void addUsersWhoLikeAndDidNotLike(IQueryable<LikeUnlike> all, LikeUnlikeParameter param)
        {
            var listOfLikeUsers = all
                .Where(x => x.IsLike == true)
                .ToList()
                .Select(x => new ParticipatingUsers(x.UserId, x.User.UserName, @"\Content\MyImages\BlankImage.jpg", x.Comment))
                .Distinct<ParticipatingUsers>()
                .ToList();

            var listOfUsersWhoDidNotLike = all
                .Where(x => x.IsLike == false)
                .ToList()
                .Select(x => new ParticipatingUsers (x.UserId,  x.User.UserName, @"\Content\MyImages\BlankImage.jpg", x.Comment ))
                .Distinct<ParticipatingUsers>()
                .ToList();

            param.UsersWhoLikedThis = listOfLikeUsers;
            param.UsersWhoDidNotLikedThis = listOfUsersWhoDidNotLike;
        }
    }
}
