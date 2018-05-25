using DalLibrary.Interfaces;
using InterfacesLibrary.SharedNS;
using System;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Reflection;
using System.Threading.Tasks;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        #region Save
        public virtual int SaveChanges()
        {
            //string msg = "";
            bool haveErrors = true;
            try
            {
                int noOfSaves = _db.SaveChanges();
                haveErrors = false;
                return noOfSaves;
            }
            catch (DbEntityValidationException e)
            {
                ErrorsGlobal.Add("DbEntityValidationException. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }

            catch (NotSupportedException e)
            {

                ErrorsGlobal.Add("NotSupportedException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }


            catch (ObjectDisposedException e)
            {

                ErrorsGlobal.Add("ObjectDisposedException. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }

            catch (InvalidOperationException e)
            {
                ErrorsGlobal.Add("InvalidOperationException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DbUpdateConcurrencyException e)
            {
                ErrorsGlobal.Add("DbUpdateConcurrencyException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DbUpdateException e)
            {
                ErrorsGlobal.Add("DbUpdateException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (EntityException e)
            {
                ErrorsGlobal.Add("EntityException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DataException e)
            {
                ErrorsGlobal.Add("DataException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (Exception e)
            {
                ErrorsGlobal.Add("Exception. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }
            finally
            {
                if (haveErrors)
                    throw new Exception(ErrorsGlobal.ToString());
            }

            return 0;
        }



        public virtual async Task<int> SaveChangesAsync()
        {
            bool haveErrors = true;
            try
            {
                var i = await _db.SaveChangesAsync();
                haveErrors = false;
                return i;
            }
            catch (DbEntityValidationException e)
            {
                ErrorsGlobal.Add("DbEntityValidationException. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }

            catch (NotSupportedException e)
            {

                ErrorsGlobal.Add("NotSupportedException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }


            catch (ObjectDisposedException e)
            {

                ErrorsGlobal.Add("ObjectDisposedException. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }

            catch (InvalidOperationException e)
            {
                ErrorsGlobal.Add("InvalidOperationException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DbUpdateConcurrencyException e)
            {
                ErrorsGlobal.Add("DbUpdateConcurrencyException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DbUpdateException e)
            {
                ErrorsGlobal.Add("DbUpdateException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (EntityException e)
            {
                ErrorsGlobal.Add("EntityException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (DataException e)
            {
                ErrorsGlobal.Add("DataException. Data not saved.", MethodBase.GetCurrentMethod(), e);
            }

            catch (Exception e)
            {
                ErrorsGlobal.Add("Exception. Data not saved.", MethodBase.GetCurrentMethod(), e);

            }
            finally
            {
                if (haveErrors)
                    throw new Exception(ErrorsGlobal.ToString());

            }
            return 0;
        }



        #endregion


    }
}