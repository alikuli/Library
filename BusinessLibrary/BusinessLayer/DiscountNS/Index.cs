using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UowLibrary.DiscountPrecedenceNS
{
    public partial class DiscountPrecedenceBiz : BusinessLayer<DiscountPrecedence>
    {
        #region Main overridable methods Fix and ErrorCheck



        private void putCurrentPrecedenceInLastRank(DiscountPrecedence entity)
        {
            if (entity.IsNull())
            {
                ErrorsGlobal.Add("Discount Precedence recieved is null", "PutCurrentPrecedenceInLastRank");
            }
            int maxRankForUser = 0;
            List<DiscountPrecedence> d = new List<DiscountPrecedence>();
            if (entity.User.IsNull())
            {
                d = FindAllDiscountPrecWhereUserIsNull();
            }
            else
            {
                d = FindAllDiscountPrecWhereUserIsNotNull(entity.UserId);
            }


            if (!d.IsNullOrEmpty())
            {
                maxRankForUser = d.Max(x => x.Rank);
                entity.Rank = maxRankForUser;
            }

            entity.Rank = maxRankForUser + DiscountPrecedence.RANK_SPACING_CONST;
        }

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            //indexListVM.Heading_Column = "Discount Precedence Rules ( Type-Rule-Rank)";
            indexListVM.Show.MoveUpMoveDown(true);


        }


        #endregion


        #region Move Up, Move Down

        /// <summary>
        /// This swaps the ranks of the two items
        /// </summary>
        /// <param name="FromId"></param>
        /// <param name="ToId"></param>
        public void SwapRanks(string fromIdstr, string toIdstr)
        {
            if (fromIdstr.IsNullOrWhiteSpace() || toIdstr.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.AddMessage("Unable to swap ranks. One of the Params is empty.", MethodBase.GetCurrentMethod());
                return;
            }

            string fromId = fromIdstr;
            string toId = toIdstr;

            if (fromId.IsNullOrEmpty() || toId.IsNullOrEmpty())
            {
                ErrorsGlobal.AddMessage("Unable to swap ranks. One of the Ids is empty.", MethodBase.GetCurrentMethod());
                return;
            }

            DiscountPrecedence dpFrom = Find(fromId);
            DiscountPrecedence dpTo = Find(toId);

            if (dpTo.IsNull() || dpFrom.IsNull())
            {
                ErrorsGlobal.AddMessage("Unable to swap ranks. Unable to find one item", MethodBase.GetCurrentMethod());
                return;
            }

            int tempRank = dpFrom.Rank;
            dpFrom.Rank = dpTo.Rank;
            dpTo.Rank = tempRank;
            ControllerCreateEditParameter parmFrom = new ControllerCreateEditParameter();
            parmFrom.Entity = dpFrom;
            Update(parmFrom);

            ControllerCreateEditParameter parmTo = new ControllerCreateEditParameter();
            parmTo.Entity = dpTo;
            UpdateAndSave(parmTo);

        }

        public void MakeFirst(string idToMove)
        {

            DiscountPrecedence dpToMove = Find(idToMove);
            dpToMove.IsNullThrowException();

            int lowestRank = FindAll().Min(x => x.Rank);
            dpToMove.Rank = lowestRank - 1;

            //ControllerCreateEditParameter parmToMove = new ControllerCreateEditParameter();
            //parmToMove.Entity = dpToMove;
            UpdateAndSave(dpToMove);
            ResetRankSpacing();
            SaveChanges();
        }

        public void MakeLast(string idToMove)
        {

            idToMove.IsNullOrWhiteSpaceThrowException();

            DiscountPrecedence dpToMove = Find(idToMove);
            dpToMove.IsNullThrowException();

            int maxRank = FindAll().Max(x => x.Rank);
            dpToMove.Rank = maxRank+1;
            UpdateAndSave(dpToMove);
        }
        private void ResetRankSpacing()
        {
            var allByRank = FindAll().OrderBy(x => x.Rank).ToArray();

            if (allByRank.IsNullOrEmpty())
                return;

            int count = 0;

            foreach (var item in allByRank)
            {
                count++;
                item.Rank = count;
                Update(item);

            }
            //for (int i = 0; i < allByRank.Length; i++)
            //{
            //    //rank += DiscountPrecedence.RANK_SPACING_CONST;
            //    //var theDiscRec = Find(allByRank[i].Id);
            //    //allByRank[i].Rank = i + DiscountPrecedence.RANK_SPACING_CONST;

            //    //ControllerCreateEditParameter byRank = new ControllerCreateEditParameter();
            //    //byRank.Entity = allByRank[i];
            //    //Update(byRank);

            //}

        }

        ///// <summary>
        ///// Get the index of the item in the sorted with Id
        ///// </summary>
        ///// <param name="sortedlst"></param>
        ///// <param name="id"></param>
        ///// <returns></returns>

        //private int GetIndexOfItem(List<IndexItemVM> sortedlst, string id)
        //{
        //    if (sortedlst.IsNullOrEmpty())
        //        return -1;

        //    if (id.IsNullOrWhiteSpace())
        //        return -1;

        //    var lstArray = sortedlst.ToArray();

        //    var indexItemVM = lstArray.FirstOrDefault(x => x.Id == id.ToString());

        //    if (indexItemVM.IsNull())
        //        return -1;

        //    int indexOf_indexItemVM = Array.IndexOf(lstArray, indexItemVM);
        //    return indexOf_indexItemVM;
        //}




        //private string GetIdOfItemAtIndexNo(List<IndexItemVM> sortedlst, int indexNo)
        //{
        //    if (indexNo == -1)
        //        return string.Empty;

        //    if (sortedlst.IsNullOrEmpty())
        //        return string.Empty;

        //    var lstArray = sortedlst.ToArray();

        //    if (indexNo > lstArray.GetUpperBound(0))
        //        return string.Empty;

        //    string idstr = lstArray[indexNo].Id;

        //    return string.Parse(idstr);

        //}

        //private DiscountPrecedence GetDiscountPrecedenceAt (List<IndexItemVM> sortedlst, string id)
        //{
        //    var indexOfItem = GetIndexOfItem(sortedlst, id);
        //    string idOfItem = GetIdOfItemAtIndexNo(sortedlst, indexOfItem);
        //    return FindFor(idOfItem);

        //}


        //private DiscountPrecedence GetDiscountPrecedenceAt(List<IndexItemVM> sortedlst, int index)
        //{
        //    var indexOfItem = index;

        //    if (index > 0)
        //    {
        //        string idOfItem = GetIdOfItemAtIndexNo(sortedlst, indexOfItem);
        //        return FindFor(idOfItem);
        //    }
        //    return null;
        //}



        #endregion




    }
}
