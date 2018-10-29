using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using AliKuli.Extentions;
using System.ComponentModel.DataAnnotations;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.Logs.VisitorsLogNS
{
    public class VisitorLog:CommonWithId
    {
        #region Constructor
        public VisitorLog()
        {
        }

        public void LoadVisitorLog(string controllerName, string actionName, string postOrGet, string user, string browser, string ip, string machineName, string urlReferrer)
        {
            //MetaData.Created.SetToTodaysDateStart(User);

            ControllerName = controllerName;
            ActionName = actionName;
            PostOrGet = postOrGet.ToString();
            User = user;
            Browser = browser;
            Ip = ip;
            MachineName = machineName;
            UrlReferrer = urlReferrer;
            Name = this.ToString();

            MetaData.Created.SetToTodaysDate(User);
        }
        
        #endregion
        #region Properties
        #region ControllerName

        [StringLength(100)]
        public string ControllerName { get; set; }

        #endregion
        #region ActionName

        [StringLength(100)]
        public string ActionName { get; set; }

        #endregion
        #region PostOrGet

        [StringLength(100)]
        public string PostOrGet { get; set; }

        #endregion
        #region User

        [StringLength(100)]
        public string User { get; set; }

        #endregion
        #region Browser

        [StringLength(500)]
        public string Browser { get; set; }

        #endregion
        #region MachineName

        [StringLength(100)]
        public string MachineName { get; set; }

        #endregion
        #region Ip

        [StringLength(100)]
        public string Ip { get; set; }

        #endregion
        #region UrlReferrer

        [StringLength(300)]
        public string UrlReferrer { get; set; }

        #endregion
        
        #endregion
        #region ToString, FullName
        public override string FullName()
        {
            return this.ToString();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string s = string.Format("On: {6:yyyy-MM-dd hh:mm:ss}; Page: {0}; Action: {1}; Type: {2}; User: {3}; Browser: {4}; Machine: {7}; UrlReferrer: {8}; IP: {5};",
                ControllerName,
                ActionName,
                PostOrGet.ToString(),
                User,
                Browser,
                Ip,
                this.MetaData.Created.Date,
                MachineName,
                UrlReferrer);
            return s;
        }             
        #endregion

        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);

            VisitorLog v = ic as VisitorLog;

            if(v == null)
            {
                throw new Exception("Unable to box VisitorLog. VisitorLog.UpdatePropertiesDuringModify");
            }
            LoadFrom(v);

        }
        public void LoadFrom(VisitorLog v)
        {
            ControllerName = v.ControllerName;
            ActionName = v.ActionName;
            PostOrGet = v.PostOrGet;
            User = v.User;
            Browser = v.Browser;
            MachineName = v.MachineName;
            Ip = v.Ip;
            UrlReferrer = v.UrlReferrer;

            base.LoadFrom(v as CommonWithId);
        }
    }
}