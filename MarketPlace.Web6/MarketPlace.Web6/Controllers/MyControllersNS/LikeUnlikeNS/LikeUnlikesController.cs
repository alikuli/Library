using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.Parameters;
using System;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;

namespace MarketPlace.Web6.Controllers
{
    public class LikeUnlikesController : EntityAbstractController<LikeUnlike>
    {

        LikeUnlikeBiz _likeUnlikeBiz;

        #region Construction and initializers

        public LikeUnlikesController(LikeUnlikeBiz biz, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(biz, bcm, err, pageViewBiz) 
        {
            _likeUnlikeBiz = biz;
        }

        LikeUnlikeBiz LikeUnlikeBiz
        {
            get
            {
                return _likeUnlikeBiz;
            }
        }

        #endregion



        [HttpPost]
        public ActionResult Like(string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId, bool isLike, string comment)
        {
            try
            {
                LikeUnlikeParameter param = _likeUnlikeBiz.AddLikeAndReturnCount(menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId, isLike, comment);
                if (isLike)
                {
                    //this Buton is like
                    return Json(new
                    {
                        success = true,
                        thisButtonCount = param.LikeCount,
                        otherButtonCount = param.UnlikeCount,
                        oppositeDeleted = param.OppositeDeleted
                    },
                    JsonRequestBehavior.DenyGet);

                }
                else
                {
                    //this button is unlike
                    return Json(new
                    {
                        success = true,
                        thisButtonCount = param.UnlikeCount,
                        otherButtonCount = param.LikeCount,
                        oppositeDeleted = param.OppositeDeleted
                        
                    },
                    JsonRequestBehavior.DenyGet);
                }

            }
            catch (Exception e)
            {
                string message = e.Message;

                return Json(new
                {
                    success = false,
                    likecount = 0,
                    unlikecount = 0,
                    message = e.Message
                },
                JsonRequestBehavior.DenyGet);
            }
        }


        [HttpGet]
        public ActionResult Count(string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId)
        {
            try
            {
                JsonResult unlikeJsonResult = new JsonResult(); ;
                JsonResult likeJsonResult = new JsonResult();
                //bool hasThisUserHasLiked = false;
                //bool hasThisUserHasUnliked = false;
                LikeUnlikeParameter param = _likeUnlikeBiz.Count(menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId, false);


                //UNLIKE
                if (!param.UsersWhoDidNotLikedThis.IsNullOrEmpty())
                {
                    foreach (ParticipatingUsers pu in param.UsersWhoDidNotLikedThis)
                    {
                        pu.UserAddressFixed = Url.Action("Edit", "Users", new { id = pu.Id });
                        if (pu.ImageLocation.IsNullOrEmpty())
                            pu.ImageLocation = Server.MapPath(AliKuli.ConstantsNS.MyConstants.DEFAULT_IMAGE_LOCATION);
                    }
                    unlikeJsonResult = Json(param.UsersWhoDidNotLikedThis.ToArray());
                }

                //LIKE
                if (!param.UsersWhoLikedThis.IsNullOrEmpty())
                {
                    foreach (ParticipatingUsers pu in param.UsersWhoLikedThis)
                    {
                        pu.UserAddressFixed = Url.Action("Edit", "Users", new { id = pu.Id });
                        if (pu.ImageLocation.IsNullOrEmpty())
                            pu.ImageLocation = Server.MapPath(AliKuli.ConstantsNS.MyConstants.DEFAULT_IMAGE_LOCATION);
                    }
                    likeJsonResult = Json(param.UsersWhoLikedThis.ToArray());
                }



                var response = Json(new
                {
                    success = true,
                    likeCount = param.LikeCount,
                    unLikeCount = param.UnlikeCount,
                    likedUserLst = likeJsonResult,
                    unlikedUsersLst = unlikeJsonResult,
                    oppositeDeleted = param.OppositeDeleted
                },
                JsonRequestBehavior.AllowGet);

                return response;


            }
            catch (Exception e)
            {
                string message = e.Message;

                return Json(new
                {
                    success = false,
                    likecount = 0,
                    unlikecount = 0,
                    message = e.Message,

                },
                JsonRequestBehavior.DenyGet);
            }
        }

    }
}
