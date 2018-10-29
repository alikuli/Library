using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {

        public override IList<ICommonWithId> GetListForIndex()
        {

            try
            {
                var lstAsFileDoc = base.GetListForIndex().Cast<AddressWithId>().ToList();

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
