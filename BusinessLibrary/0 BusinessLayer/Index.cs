using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
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



        public async Task<IndexListVM> IndexAsync(ControllerIndexParams parameters)
        {
            //this is where the error is.
            var lstEntities = await GetListForIndexAsync(parameters);

            //this is where the list is created
            IndexListVM indexListVM = createIndexListAndGiveNamesToColumns_Helper(parameters, lstEntities);

            return indexListVM;
        }


        public virtual void Event_ApplyChangesAfterCreate(TEntity entity)
        {

        }


        public virtual IList<ICommonWithId> GetListForIndex()
        {
            IList<ICommonWithId> lstEntities = FindAll().ToList() as IList<ICommonWithId>;

            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;
        }

        public virtual async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        {
            var lstEntities = await FindAllAsync();
            IList<ICommonWithId> lstIcom = lstEntities.Cast<ICommonWithId>().ToList();
            return lstIcom;
        }






        /// <summary>
        /// This is the helper function for the GetIndexList and GetIndexListAsync
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="lstEntities"></param>
        /// <returns></returns>
        private IndexListVM createIndexListAndGiveNamesToColumns_Helper(ControllerIndexParams parameters, IList<ICommonWithId> lstEntities)
        {
            IndexListVM indexListVM = createIndexList(lstEntities, parameters);

            if (indexListVM.IsNull())
                indexListVM = new IndexListVM(parameters);
            //else
            //    indexListVM.Load(parameters);

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
        private IndexListVM createIndexList(IList<ICommonWithId> lstEntities, ControllerIndexParams parameters)
        {

            

            //This names the sort links. They come directly from the entity
            parameters.DudEntity = Dal.Factory();

            IndexListVM indexListVM = new IndexListVM(parameters);


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

                    IndexItemVM indexItem = new IndexItemVM(
                        id,
                        fullName,
                        input1SortString,
                        input2SortString,
                        input3SortString,
                        isEditLocked,
                        detailInfoToDisplayOnWebsite);

                    //indexItem.MenuManager = indexListVM.MenuManager as IMenuManager;
                    
                    //image address is added in here.
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











    }
}
