﻿using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using System;
using System.Data.Entity;
using System.Reflection;

namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        /// <summary>
        /// This procedure is used to update all ICommon records
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="oldEntity"></param>
        public void Update(TEntity entity)
        {

            if (entity == null)
            {
                ErrorsGlobal.Add("Missing entity.", MethodBase.GetCurrentMethod());
                throw new ArgumentException(ErrorsGlobal.ToString());
            }

            _db.Entry(entity).State = EntityState.Modified;
            ErrorsGlobal.AddMessage(string.Format("'{0}' Updated!", entity.Name));

        }







    }
}
