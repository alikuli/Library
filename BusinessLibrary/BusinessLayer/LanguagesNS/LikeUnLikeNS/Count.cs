using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using System;
using System.Linq;

namespace UowLibrary.LikeUnlikeNS
{

    public partial class LikeUnlikeBiz : BusinessLayer<LikeUnlike>
    {
        /// <summary>
        /// This will find the first count.
        /// The return list for the like/unlike is made in addUsersWhoLikeAndDidNotLike
        /// </summary>
        /// <param name="menuPath1Id"></param>
        /// <param name="menuPath2Id"></param>
        /// <param name="menuPath3Id"></param>
        /// <param name="productId"></param>
        /// <param name="productChildId"></param>
        /// <param name="userId"></param>
        /// <param name="isLike"></param>
        /// <returns></returns>
        public LikeUnlikeParameter Count(string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId, bool oppositeWasDeleted)
        {

            LikeUnlikeParameter likeUnlikeParameter = new LikeUnlikeParameter(0, 0, "Count Default");
            if (!productChildId.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = CountProductChildLikes(productChildId, userId);
                //this is true if opposite was deleted and signals the Javascript to
                //reduce the oppoiste number by 1.
                likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

                return likeUnlikeParameter;

            }

            if (!productId.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = ProductLikes(productId, userId);
                //this is true if opposite was deleted and signals the Javascript to
                //reduce the oppoiste number by 1.
                likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

                return likeUnlikeParameter;

            }


            if (!menuPath3Id.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = CountMenuPath3Likes(menuPath3Id, userId);
                //this is true if opposite was deleted and signals the Javascript to
                //reduce the oppoiste number by 1.
                likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

                return likeUnlikeParameter;

            }


            if (!menuPath2Id.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = countMenuPath2Likes(menuPath2Id, userId);
                //this is true if opposite was deleted and signals the Javascript to
                //reduce the oppoiste number by 1.
                likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

                return likeUnlikeParameter;
            }

            if (!menuPath1Id.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = countMenuPath1Likes(menuPath1Id, userId); ;

            }

            //this is true if opposite was deleted and signals the Javascript to
            //reduce the oppoiste number by 1.
            likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

            return likeUnlikeParameter;


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

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "menuPath1Id");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

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

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "menuPath2Id");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

            addUsersWhoLikeAndDidNotLike(all, param);
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

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "menuPath3Id");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

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

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "productId");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

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

            LikeUnlikeParameter param = new LikeUnlikeParameter(likes, unlikes, "productChildId");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

            addUsersWhoLikeAndDidNotLike(all, param);

            return param;
        }
        private static void addUsersWhoLikeAndDidNotLike(IQueryable<LikeUnlike> all, LikeUnlikeParameter param)
        {
            var listOfLikeUsers = all
                .Where(x => x.IsLike == true)
                .ToList()
                .Select(x => new ParticipatingUsers(x.UserId, x.User.UserName, @"\Content\MyImages\BlankImage.jpg", x.Comment, x.MetaData.Created.Date ?? DateTime.MaxValue))
                .Distinct<ParticipatingUsers>()
                .ToList();

            var listOfUsersWhoDidNotLike = all
                .Where(x => x.IsLike == false)
                .ToList()
                .Select(x => new ParticipatingUsers(x.UserId, x.User.UserName, @"\Content\MyImages\BlankImage.jpg", x.Comment, x.MetaData.Created.Date ?? DateTime.MaxValue))
                .Distinct<ParticipatingUsers>()
                .ToList();

            param.UsersWhoLikedThis = listOfLikeUsers;
            param.UsersWhoDidNotLikedThis = listOfUsersWhoDidNotLike;
        }
    }
}
