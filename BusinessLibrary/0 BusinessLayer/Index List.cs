using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
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


        public virtual void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            //note... edit is also controlled from entity.MetaData.IsEditLocked in the entity metaData controlled through 
            //Event_LockEdit which fires during Creation.
            //indexListVM.Heading_Column = "Discount Precedence Rules ( Type-Rule-Rank)";

            indexListVM.Show.MoveUpMoveDown(false);
            indexListVM.Show.EditDeleteAndCreate = false;
            indexListVM.IsImageTiled = false;
            indexListVM.NameInput1 = "Name";
            indexListVM.NameInput2 = "";
            indexListVM.NameInput3 = "";

            indexListVM.Heading.Main = string.Format("{0}", typeof(TEntity).Name.ToSentence().ToTitleCase());
            indexListVM.Heading.Column = "All Items";
            indexListVM.Heading.Small = "List";

            //indexListVM.MainHeading = string.Format("{0}", typeof(TEntity).Name.ToSentence().ToTitleCase());
            //indexListVM.Heading_Column = "All Items";
            //indexListVM.SmallHeading = "List";
            //indexListVM.SmallHeading = string.Format("{0}", typeof(TEntity).Name.ToSentence().ToTitleCase());

            indexListVM.Heading.RecordName = indexListVM.Heading.Main; //This print as Number of Records.
            indexListVM.Heading.RecordNamePlural = indexListVM.Heading.Main + "s";



        }



    }
}
