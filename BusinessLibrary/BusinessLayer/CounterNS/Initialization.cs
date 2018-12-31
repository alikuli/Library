using DatastoreNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS.CounterNS;
using System;

namespace UowLibrary.CounterNS

{
    public partial class CounterBiz : BusinessLayer<Counter>
    {

        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }
        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                throw new NotImplementedException("CounterBiz GetDataForStringArrayFormat");
            }
        }


    }
}
