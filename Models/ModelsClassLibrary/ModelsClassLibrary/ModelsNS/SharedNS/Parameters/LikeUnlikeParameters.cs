
using System.Collections.Generic;
using UserModels;
namespace ModelsClassLibrary.ModelsNS.SharedNS.Parameters
{
    public class LikeUnlikeParameters
    {
        public LikeUnlikeParameters(int likeCount, int unlikeCount, string kindOfLike)
        {
            LikeCount = likeCount;
            UnlikeCount = unlikeCount;
            KindOfLike = kindOfLike;
        }
        public int LikeCount { get; set; }
        public int UnlikeCount { get; set; }
        /// <summary>
        /// If user has been found to have liked this item
        /// </summary>
        public bool HasLiked { get; set; }

        /// <summary>
        /// If user has been found to have Unliked this item
        /// </summary>
        public bool HasUnLiked { get; set; }

        /// <summary>
        /// This is for debug. I send a name like MenuPath1
        /// </summary>
        /// 

        ///The opposite of this has been deleted. Eg. If we have liked, then there was an unlike which was deleted.
        public bool OppositeDeleted { get; set; }
        public string KindOfLike { get; set; }
        public List<ParticipatingPeople> PeopleWhoLikedThis { get; set; }
        public List<ParticipatingPeople> PeopleWhoDidNotLikedThis { get; set; }
    }
}
