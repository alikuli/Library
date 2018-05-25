using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.Logs.VisitorsLogNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class VisitorLogDAL : Repositry<VisitorLog>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public VisitorLogDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }



        public override void Fix(VisitorLog entity)
        {
            base.Fix(entity);
            entity.Name = entity.FullName();
        }

        public void CreateLog(string controllerName, string actionName, string postOrGet, string user, string browser, string ip, string machineName, string urlReferrer)
        {
            VisitorLog vLog = Factory();
            vLog.LoadVisitorLog(
                controllerName,
                actionName,
                postOrGet,
                user,
                browser,
                ip,
                machineName,
                urlReferrer);

            Create(vLog);

        }

    }
}
