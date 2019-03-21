using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
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
        public LikeUnlikeParameters Count(string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId, bool oppositeWasDeleted)
        {
            //userId.IsNullOrWhiteSpaceThrowException("User is not logged in");

            string personId = "";
            LikeUnlikeParameters likeUnlikeParameter = new LikeUnlikeParameters(0, 0, "Count Default");

            if (!userId.IsNullOrWhiteSpace())
            {
                Person person = UserBiz.GetPersonFor(userId);
                person.IsNullThrowException("Person not found");
                personId = person.Id;
                personId.IsNullOrWhiteSpaceThrowException("personId");

            }


            if (!productChildId.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = CountProductChildLikes(productChildId, personId);
                //this is true if opposite was deleted and signals the Javascript to
                //reduce the oppoiste number by 1.
                likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

                return likeUnlikeParameter;

            }

            if (!productId.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = ProductLikes(productId, personId);
                //this is true if opposite was deleted and signals the Javascript to
                //reduce the oppoiste number by 1.
                likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

                return likeUnlikeParameter;

            }


            if (!menuPath3Id.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = CountMenuPath3Likes(menuPath3Id, personId);
                //this is true if opposite was deleted and signals the Javascript to
                //reduce the oppoiste number by 1.
                likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

                return likeUnlikeParameter;

            }


            if (!menuPath2Id.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = countMenuPath2Likes(menuPath2Id, personId);
                //this is true if opposite was deleted and signals the Javascript to
                //reduce the oppoiste number by 1.
                likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

                return likeUnlikeParameter;
            }

            if (!menuPath1Id.IsNullOrWhiteSpace())
            {
                likeUnlikeParameter = countMenuPath1Likes(menuPath1Id, personId); ;

            }

            //this is true if opposite was deleted and signals the Javascript to
            //reduce the oppoiste number by 1.
            likeUnlikeParameter.OppositeDeleted = oppositeWasDeleted;

            return likeUnlikeParameter;


        }

        private LikeUnlikeParameters countMenuPath1Likes(string menuPath1Id, string personId)
        {
            var all = FindAll().Where(x => x.MenuPath1Id == menuPath1Id);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == true)).IsNull();
            bool hasUnliked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == false)).IsNull();

            LikeUnlikeParameters param = new LikeUnlikeParameters(likes, unlikes, "menuPath1Id");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

            addUsersWhoLikeAndDidNotLike(all, param);
            return param;
        }

        private LikeUnlikeParameters countMenuPath2Likes(string menuPath2Id, string personId)
        {

            var all = FindAll().Where(x => x.MenuPath2Id == menuPath2Id);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == false)).IsNull();

            LikeUnlikeParameters param = new LikeUnlikeParameters(likes, unlikes, "menuPath2Id");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

            addUsersWhoLikeAndDidNotLike(all, param);
            return param;
        }


        private LikeUnlikeParameters CountMenuPath3Likes(string menuPath3Id, string personId)
        {
            var all = FindAll().Where(x => x.MenuPath3Id == menuPath3Id);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == false)).IsNull();

            LikeUnlikeParameters param = new LikeUnlikeParameters(likes, unlikes, "menuPath3Id");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

            addUsersWhoLikeAndDidNotLike(all, param);

            return param;

        }

        private LikeUnlikeParameters ProductLikes(string productId, string personId)
        {

            var all = FindAll().Where(x => x.ProductId == productId);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == false)).IsNull();

            LikeUnlikeParameters param = new LikeUnlikeParameters(likes, unlikes, "productId");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

            addUsersWhoLikeAndDidNotLike(all, param);

            return param;
        }

        private LikeUnlikeParameters CountProductChildLikes(string productChildId, string personId)
        {

            var all = FindAll().Where(x => x.ProductChildId == productChildId);

            var likes = all.Count(x => x.IsLike == true);
            var unlikes = all.Count(x => x.IsLike == false);

            bool hasLiked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == true)).IsNull();

            bool hasUnliked = false;
            if (!hasLiked)
                hasUnliked = !(all.FirstOrDefault(x => x.PersonId == personId && x.IsLike == false)).IsNull();

            LikeUnlikeParameters param = new LikeUnlikeParameters(likes, unlikes, "productChildId");
            param.HasLiked = hasLiked;
            param.HasUnLiked = hasUnliked;

            addUsersWhoLikeAndDidNotLike(all, param);

            return param;
        }
        private static void addUsersWhoLikeAndDidNotLike(IQueryable<LikeUnlike> all, LikeUnlikeParameters param)
        {
            var listOfLikeUsers = all
                .Where(x => x.IsLike == true)
                .ToList()
                .Select(x => new ParticipatingPeople(x.PersonId, x.Person.FullName(), @"\Content\MyImages\BlankImage.jpg", x.Comment, x.MetaData.Created.Date ?? DateTime.MaxValue))
                .Distinct<ParticipatingPeople>()
                .ToList();

            var listOfUsersWhoDidNotLike = all
                .Where(x => x.IsLike == false)
                .ToList()
                .Select(x => new ParticipatingPeople(x.PersonId, x.Person.FullName(), @"\Content\MyImages\BlankImage.jpg", x.Comment, x.MetaData.Created.Date ?? DateTime.MaxValue))
                .Distinct<ParticipatingPeople>()
                .ToList();

            param.PeopleWhoLikedThis = listOfLikeUsers;
            param.PeopleWhoDidNotLikedThis = listOfUsersWhoDidNotLike;
        }
    }
}
