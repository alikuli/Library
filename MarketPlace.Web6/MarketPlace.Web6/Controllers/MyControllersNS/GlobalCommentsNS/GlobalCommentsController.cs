using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.GlobalCommentsNS;
using System.Collections;
using System.Linq;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ErrorHandlerLibrary;
using UowLibrary.PageViewNS;

namespace MarketPlace.Web6.Controllers
{
    public class GlobalCommentsController : EntityAbstractController<GlobalComment>
    {

        GlobalCommentBiz _globalCommentBiz;

        #region Construction and initializers

        public GlobalCommentsController(GlobalCommentBiz biz, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(biz, bcm, err, pageViewBiz) 
        {
            _globalCommentBiz = biz;
        }

        #endregion
        public GlobalCommentBiz GlobalCommentBiz
        {
            get { return _globalCommentBiz; }
        }


        [HttpPost]
        public ActionResult AddComment(string comment, string menuPath1Id, string menuPath2Id, string menuPath3Id, string productId, string productChildId, string userId)
        {
            try
            {

                if (comment.IsNullOrEmpty())
                {
                    ErrorsGlobal.AddMessage("Comment was Empty!");
                    throw new Exception("Comment was Empty!");
                }
                _globalCommentBiz.AddAndSaveComment(comment, menuPath1Id, menuPath2Id, menuPath3Id, productId, productChildId, userId);

                return Json(new
                {
                    success = true,
                    message = string.Format("Comment Added")
                },
                JsonRequestBehavior.DenyGet);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Error. ", MethodBase.GetCurrentMethod(), e);
                return Json(new
                {
                    success = false,
                    message = string.Format((ErrorsGlobal.Messages.ToArray())[0].ToString())
                },
                JsonRequestBehavior.DenyGet);
            }
        }
    }
}
