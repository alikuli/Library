using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UserModels;
using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.PageViewNS
{
    public class PageView : CommonWithId
    {

        //public PageView()
        //{
        //    UserId = "";
        //    UserName = "";

        //}

        [Display(Name ="User Id")]
        [MaxLength (128)]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        [MaxLength(128)]
        public string UserName { get; set; }

        [MaxLength(128)]
        [Display(Name="Action")]
        public string ActionName { get; set; }

        [MaxLength(128)]
        [Display(Name = "Controller")]
        public string ControllerName { get; set; }

        [Display(Name = "http Method")]
        [MaxLength(128)]
        public string HttpMethod { get; set; }

        [Display(Name = "User Host Name")]
        [MaxLength(128)]
        public string UserHostName { get; set; }
        [MaxLength(128)]
        public string UserHostAddress { get; set; }


        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);

            PageView c = ic as PageView;

            if (c == null)
                throw new Exception("Unable to box Page View");

            UserId = c.UserId;
            ActionName = c.ActionName;
            ControllerName = c.ControllerName;
            UserName = c.UserName;
            HttpMethod = c.HttpMethod;
            UserHostName = c.UserHostName;
            UserHostAddress = c.UserHostAddress;
            UrlRefererrerHost = c.UrlRefererrerHost;
            UserInfo = c.UserInfo;
            UserAgent = c.UserAgent;
            UserLanguages = c.UserLanguages;
            BrowserType = c.BrowserType;
            IsCrawler = c.IsCrawler;
            IsMobileDevice = c.IsMobileDevice;
            IsClientWin16Based = c.IsClientWin16Based;
            IsClientWin32Based = c.IsClientWin32Based;
            IsAjaxRequest = c.IsAjaxRequest;

        }

        [Display(Name = "url Refererrer Host")]
        [MaxLength(128)]
        public string UrlRefererrerHost { get; set; }

        [Display(Name = "User Info")]
        [MaxLength(128)]
        public string UserInfo { get; set; }
        public string UserAgent { get; set; }

        [Display(Name = "User Languages")]
        [MaxLength(128)]
        public string UserLanguages { get; set; }

        [Display(Name = "Browser Type")]
        [MaxLength(128)]
        public string BrowserType { get; set; }

        [Display(Name = "Is Crawler")]
        public bool IsCrawler { get; set; }

        [Display(Name = "Is Mobile Device")]
        public bool IsMobileDevice { get; set; }

        [Display(Name = "Is Client Win 16 Based")]
        public bool IsClientWin16Based { get; set; }


        [Display(Name = "Is Client Win 32 Based")]
        public bool IsClientWin32Based { get; set; }

        [Display(Name = "Is Ajax Request")]
        public bool IsAjaxRequest { get; set; }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.PageView;
        }

        

        public override bool DisableNameInView()
        {
            return true;

        }

        #region Make Name


        public string MakeName()
        {
            StringBuilder sb = new StringBuilder();
            
            appendUserName(sb);
            appendControllerAndAction(sb);
            appendUserHost(sb);
            appendUserInfoAgentBrowser(sb);
            appendIsCrawler(sb);
            appendIsMobileDevise(sb);
            appendIsClientWin16Based(sb);
            appendIsClientWin32Based(sb);
            appendIsAjaxRequest(sb);
            appendTime(sb);
            return sb.ToString();
        }

        
        private void appendTime(StringBuilder sb)
        {
            sb.Append(string.Format("{0} {1}",
                DateTime.UtcNow.ToLongDateString(), 
                DateTime.UtcNow.ToLongTimeString()));
        }

        private void appendIsAjaxRequest(StringBuilder sb)
        {
            if (IsAjaxRequest)
                sb.Append(string.Format("[{0}] ", "AJAX"));
        }

        private void appendIsClientWin32Based(StringBuilder sb)
        {
            if (IsClientWin32Based)
                sb.Append(string.Format("[{0}] ", "WIN32"));
        }

        private void appendIsClientWin16Based(StringBuilder sb)
        {
            if (IsClientWin16Based)
                sb.Append(string.Format("[{0}] ", "WIN16"));
        }

        private void appendIsMobileDevise(StringBuilder sb)
        {
            if (IsMobileDevice)
                sb.Append(string.Format("[{0}] ", "MOBILE"));
        }

        private void appendIsCrawler(StringBuilder sb)
        {
            if (IsCrawler)
                sb.Append(string.Format("[{0}] ", "CRAWLER"));
        }

        private void appendUserInfoAgentBrowser(StringBuilder sb)
        {
            sb.Append(string.Format("User Info(Agent)[Browser Type]: {0}({1})[{2}]; ",
                UserInfo,
                UserAgent,
                BrowserType));
        }

        private void appendUserHost(StringBuilder sb)
        {

            sb.Append(string.Format("Host Name(Addy)[url Refererrer Host]: {0}({1})[{2}]; ", 
                UserHostName, 
                UserHostAddress, 
                UrlRefererrerHost));
        }

        private void appendControllerAndAction(StringBuilder sb)
        {
            sb.Append(string.Format("Ctrl.Action:{0}.{1}.{2}; ", ControllerName, ActionName, HttpMethod));
        }

        private void appendUserName(StringBuilder sb)
        {
            sb.Append("UserName:");

            if (UserName.IsNullOrWhiteSpace())
                sb.Append("annonymous");
            else
                sb.Append(UserName);
            sb.Append("; ");
        }

        #endregion

    }

    
}
