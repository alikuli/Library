using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.RightsNS;
using System;
using System.Collections.Generic;
using System.Linq;
using UserModels;
using System.Data.Entity;

namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {




        /// <summary>
        /// Use this to give special rights to specific classes
        /// </summary>
        /// <param name="classesWithRightsENUM"></param>
        /// <returns></returns>
        private IRight getDefaultRights(ClassesWithRightsENUM classesWithRightsENUM)
        {
            switch (classesWithRightsENUM)
            {
                case ClassesWithRightsENUM.Country:
                    return (IRight)new _R_Right(classesWithRightsENUM);

                default:
                    return (IRight)new NoRights(classesWithRightsENUM);
            }


        }

        



    }
}
