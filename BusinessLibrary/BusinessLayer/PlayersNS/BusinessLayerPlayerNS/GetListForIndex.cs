using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UowLibrary.PlayersNS.PlayerAbstractCategoryNS
{

    public abstract partial class BusinessLayerPlayer<TEntity>
    {


        public override IList<ICommonWithId> GetListForIndex()
        {
            try
            {
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

                var lstAsFileDoc = base.GetListForIndex().Cast<TEntity>().ToList();

                if (lstAsFileDoc.IsNullOrEmpty())
                    return null;

                var lst = lstAsFileDoc.Where(x => x.UserId == UserId).ToList();

                if (lst.IsNullOrEmpty())
                    return null;

                var lstIcommonwithId = lst.Cast<ICommonWithId>().ToList();
                return lstIcommonwithId;
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Unable to continue", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }
        }
    }
}
