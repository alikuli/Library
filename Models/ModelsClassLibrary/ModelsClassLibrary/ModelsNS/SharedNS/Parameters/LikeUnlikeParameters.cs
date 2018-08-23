
using System.Collections.Generic;
using UserModels;
namespace ModelsClassLibrary.ModelsNS.SharedNS.Parameters
{
    public class LikeUnlikeParameter
    {
        public LikeUnlikeParameter(int likeCount, int unlikeCount, string kindOfLike, bool hasLiked, bool hasUnliked)
        {
            LikeCount = likeCount;
            UnlikeCount = unlikeCount;
            KindOfLike = kindOfLike;
            HasLiked = hasLiked;
            HasUnLiked = hasUnliked;
        }
        public int LikeCount { get; set; }
        public int UnlikeCount { get; set; }
        /// <summary>
        /// If user has been found then he has pressed the like button
        /// </summary>
        public bool HasLiked { get; set; }
        public bool HasUnLiked { get; set; }
        /// <summary>
        /// This is for debug. I send a name like MenuPath1
        /// </summary>
        public string KindOfLike { get; set; }
        public List<ParticipatingUsers> UsersWhoLikedThis { get; set; }
        public List<ParticipatingUsers> UsersWhoDidNotLikedThis { get; set; }
    }
}
