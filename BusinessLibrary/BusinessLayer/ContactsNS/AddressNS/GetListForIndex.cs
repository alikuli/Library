﻿using AliKuli.Extentions;
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
            //throw new NotImplementedException();
            try
            {
                string personId = GetPersonIdForCurrentUser();

                var lstAsFileDoc = base.GetListForIndex().Cast<AddressMain>().ToList();

                if (lstAsFileDoc.IsNullOrEmpty())
                    return null;

                //This is incorrect. This is a many to 1 relationship where the address will have a
                //single person and the person will have many addresses
                //var lst = lstAsFileDoc.Where(x => x.People.Any(y => y.Id == personId)).ToList();
                var lst = lstAsFileDoc.Where(x => x.PersonId == personId).ToList();

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
