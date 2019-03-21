using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.MessageNS;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.PeopleMessageNS
{
    public class PeopleMessage: CommonWithId
    {
        public PeopleMessage()
        {
            if(Name.IsNullOrWhiteSpace())
                Name = MakeUniqueName();
        }
        public string PersonId { get; set; }
        public virtual Person Person {get;set;}
        
        public string MessageId {get;set;}
        public virtual Message Message {get;set;}

        public override string MakeUniqueName()
        {
            string name = Guid.NewGuid().ToString();
            return name;
        }

    }
}
