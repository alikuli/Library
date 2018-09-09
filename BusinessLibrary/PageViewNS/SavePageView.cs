using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Web.Mvc;

namespace UowLibrary.PageViewNS
{
    public partial class PageViewBiz
    {

        public void SavePageView(ActionExecutedContext filterContext, System.Web.HttpRequestBase request)
        {
            string userId = UserId ?? "";
            string userName = UserName ?? "";
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string httpMethod = request.HttpMethod;

            string urlRefererrerHost = "";
            string UserInfo = "";
            if (!request.UrlReferrer.IsNull())
            {
                urlRefererrerHost = request.UrlReferrer.Host;
                UserInfo = request.UrlReferrer.UserInfo;
            }
            string userHostName = request.UserHostName;
            string userHostAddress = request.UserHostAddress;
            string userAgent = request.UserAgent;
            string userLanguages = request.UserLanguages.ConvertToSemiColanString();
            string browserType = request.Browser.Type;

            bool isAjaxRequest = request.IsAjaxRequest();
            bool isCrawler = request.Browser.Crawler;
            bool isMobileDevice = request.Browser.IsMobileDevice;
            bool isClientWin16Based = request.Browser.Win16;
            bool isClientWin32Based = request.Browser.Win32;

            PageView pv = Factory() as PageView;
            pv.UserId = userId;
            pv.UserName = userName;
            pv.ActionName = actionName;
            pv.ControllerName = controllerName;
            pv.HttpMethod = httpMethod;
            pv.UrlRefererrerHost = urlRefererrerHost;
            pv.UserHostName = userHostName;
            pv.UserHostAddress = userHostAddress;
            pv.UserInfo = UserInfo;
            pv.UserAgent = userAgent;
            pv.UserLanguages = userLanguages;
            pv.BrowserType = browserType;

            pv.IsAjaxRequest = isAjaxRequest;
            pv.IsCrawler = isCrawler;
            pv.IsMobileDevice = isMobileDevice;
            pv.IsClientWin16Based = isClientWin16Based;
            pv.IsClientWin32Based = isClientWin32Based;

            pv.Name = pv.MakeName();

            ControllerCreateEditParameter ccep = new ControllerCreateEditParameter();
            ccep.Entity = pv as ICommonWithId;

            try
            {
                CreateAndSave(ccep);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("The PageView save failed!", System.Reflection.MethodBase.GetCurrentMethod(), e);
            }
        }
    }
}
