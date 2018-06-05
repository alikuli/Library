using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UowLibrary.Abstract;
using UowLibrary.Interface;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity> : AbstractBiz, IBusinessLayer<TEntity> where TEntity : class, ICommonWithId
    {


        #region Controller Index Methods

        public async Task<IndexListVM> IndexAsync(ControllerIndexParams parameters)
        {
            var lstEntities = await GetListForIndexAsync(parameters);

            //this is where the list is created
            IndexListVM indexListVM = CreateIndexListAndGiveNamesToColumns_Helper(parameters, lstEntities);

            return indexListVM;
        }



        //public IndexListVM Index(ControllerIndexParams parameters)
        //{
        //    return GetIndexList(parameters);

        //}



        public virtual void Event_ApplyChangesAfterCreate(TEntity entity)
        {

        }


        #endregion

        #region Create Index List for Controller



        ///// <summary>
        ///// This is the method that is called but it cannot be edited.
        ///// </summary>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        //private IndexListVM GetIndexList(ControllerIndexParams parameters)
        //{
        //    IList<ICommonWithId> lstEntities = GetListForIndex();


        //    if (lstEntities.IsNull())
        //        return null;

        //    IndexListVM indexListVM = CreateIndexListAndGiveNamesToColumns_Helper(parameters, lstEntities);

        //    return indexListVM;
        //}




        public virtual IList<ICommonWithId> GetListForIndex()
        {
            IList<ICommonWithId> lstEntities = Dal.FindAll().ToList() as IList<ICommonWithId>;

            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;
        }

        public virtual async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        {
            var lstEntities = await Dal.FindAllAsync();
            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;
        }



        /// <summary>
        /// This is the helper function for the GetIndexList and GetIndexListAsync
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="lstEntities"></param>
        /// <returns></returns>
        private IndexListVM CreateIndexListAndGiveNamesToColumns_Helper(ControllerIndexParams parameters, IList<ICommonWithId> lstEntities)
        {
            IndexListVM indexListVM = CreateIndexList(lstEntities, parameters);

            if (indexListVM.IsNull())
                return null;

            indexListVM.Load(parameters);
            
            return indexListVM;
        }


        /// <summary>
        /// This creates a list which is later used in the index method.
        /// The List is 
        ///     sorted as per the ControllerIndexParams.SortBy.
        ///     filtered as per the ControllerIndexParams.SearchFor. 
        /// </summary>
        /// <param name="lstEntities"></param>
        /// <returns></returns>
        private IndexListVM CreateIndexList(IList<ICommonWithId> lstEntities, ControllerIndexParams parameters)
        {


            //This names the sort links. They come directly from the entity
            //var dudEntity = Dal.Factory();
            
            parameters.DudEntity = Dal.Factory();
            //getMainHeadingAndSortNamesFromEntity(dudEntity);
            //getParameterInfo(parameters);

            //IndexListVM indexListVM = new IndexListVM(parameters.SortBy, parameters.SearchFor, parameters.SelectedId, dudEntity);
            IndexListVM indexListVM = new IndexListVM(parameters);

            //indexListVM.Heading.SortOrderDescription = getSortDescription(indexListVM.SortOrderEnum);

            Event_ModifyIndexList(indexListVM, parameters);

            if (lstEntities.IsNullOrEmpty())
            {
                return indexListVM;
            }

            foreach (var entity in lstEntities)
            {

                try
                {
                    string id = entity.Id;
                    string fullName = entity.FullName();
                    string input1SortString = entity.Input1SortString.IsNullOrWhiteSpace() ? "" : entity.Input1SortString; ;
                    string input2SortString = entity.Input2SortString.IsNullOrWhiteSpace() ? "" : entity.Input2SortString; ;
                    string input3SortString = entity.Input3SortString.IsNullOrWhiteSpace() ? "" : entity.Input3SortString;
                    bool isEditLocked = entity.MetaData.IsEditLocked;
                    string detailInfoToDisplayOnWebsite = entity.DetailInfoToDisplayOnWebsite.IsNullOrWhiteSpace() ? "" : entity.DetailInfoToDisplayOnWebsite;

                    //string e = string.Format("Id: {0};  fullname: {1}; Input1: {2}; Input2: {3}; Input3: {4}, isLocked {5}, detail: {6};",
                    //id,
                    //fullName,
                    //input1SortString,
                    //input2SortString,
                    //input3SortString,
                    //isEditLocked,
                    //detailInfoToDisplayOnWebsite
                    //    );

                    IndexItemVM indexItem = new IndexItemVM(
                        id,
                        fullName,
                        input1SortString,
                        input2SortString,
                        input3SortString,
                        isEditLocked,
                        detailInfoToDisplayOnWebsite);

                    Event_ModifyIndexItem(indexListVM, indexItem, entity);

                    if (AddEntryToIndex)
                        if (!indexItem.IsNull())
                            indexListVM.Add(indexItem);
                }
                catch (Exception e)
                {

                    ErrorsGlobal.Add("There was an error during creating index", MethodBase.GetCurrentMethod(), e);
                }

            }

            if (ErrorsGlobal.HasErrors)
                throw new Exception(ErrorsGlobal.ToString());


            return indexListVM;
        }

        //private void getParameterInfo(ControllerIndexParams parameters)
        //{
        //}

        //private void getMainHeadingAndSortNamesFromEntity(TEntity dudEntity)
        //{
        //    indexListVM.NameInput1 = dudEntity.NameInput1;
        //    indexListVM.NameInput2 = dudEntity.NameInput2;
        //    indexListVM.NameInput3 = dudEntity.NameInput3;
        //}



        #endregion







    }
}
