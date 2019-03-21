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
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;

namespace MarketPlace.Web6.Controllers
{
    public class LikeUnlikesController : EntityAbstractController<LikeUnlike>
    {

        LikeUnlikeBiz _likeUnlikeBiz;

        #region Construction and initializers

        public LikeUnlikesController(LikeUnlikeBiz biz,  AbstractControllerParameters param)
            : base(biz, param) 
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
                LikeUnlikeParameters param = _likeUnlikeBiz.AddLikeAndReturnCount(menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId, isLike, comment);
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
                LikeUnlikeParameters param = _likeUnlikeBiz.Count(menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId, false);


                //UNLIKE
                if (!param.PeopleWhoDidNotLikedThis.IsNullOrEmpty())
                {
                    foreach (ParticipatingPeople pu in param.PeopleWhoDidNotLikedThis)
                    {
                        pu.UserAddressFixed = Url.Action("Edit", "Users", new { id = pu.Id });
                        if (pu.ImageLocation.IsNullOrEmpty())
                            pu.ImageLocation = Server.MapPath(AliKuli.ConstantsNS.MyConstants.DEFAULT_IMAGE_LOCATION);
                    }
                    unlikeJsonResult = Json(param.PeopleWhoDidNotLikedThis.ToArray());
                }

                //LIKE
                if (!param.PeopleWhoLikedThis.IsNullOrEmpty())
                {
                    foreach (ParticipatingPeople pu in param.PeopleWhoLikedThis)
                    {
                        pu.UserAddressFixed = Url.Action("Edit", "Users", new { id = pu.Id });
                        if (pu.ImageLocation.IsNullOrEmpty())
                            pu.ImageLocation = Server.MapPath(AliKuli.ConstantsNS.MyConstants.DEFAULT_IMAGE_LOCATION);
                    }
                    likeJsonResult = Json(param.PeopleWhoLikedThis.ToArray());
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
                JsonRequestBehavior.AllowGet);
                //JsonRequestBehavior.DenyGet);
            }
        }

    }
}
