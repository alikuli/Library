using AliKuli.Extentions;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using WebLibrary.Programs;

namespace UowLibrary.StateNS
{
    public partial class StateBiz : BusinessLayer<State>
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "[Abbreviation] - State Name";
            indexListVM.Show.EditDeleteAndCreate = true;
            //indexListVM.Show.Record = "State";
            //indexListVM.Records = "States";
        }

        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithid)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithid);
            AddEntryToIndex = !icommonWithid.Name.IsNullOrWhiteSpace();
        }



    }
}
