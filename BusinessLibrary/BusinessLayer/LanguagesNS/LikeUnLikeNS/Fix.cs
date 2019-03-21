using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using System.Linq;
using UserModels;

namespace UowLibrary.LikeUnlikeNS
{
    public partial class LikeUnlikeBiz 
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            LikeUnlike likeUnlike = parm.Entity as LikeUnlike;

            if (likeUnlike.MenuPath1Id.IsNullOrWhiteSpace())
                likeUnlike.MenuPath1Id = null;

            if (likeUnlike.MenuPath2Id.IsNullOrWhiteSpace())
                likeUnlike.MenuPath2Id = null;

            if (likeUnlike.MenuPath3Id.IsNullOrWhiteSpace())
                likeUnlike.MenuPath3Id = null;

            if (likeUnlike.ProductId.IsNullOrWhiteSpace())
                likeUnlike.ProductId = null;

            if (likeUnlike.PersonId.IsNullOrWhiteSpace())
                likeUnlike.PersonId = null;

        }
    }
}
