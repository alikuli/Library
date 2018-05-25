using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.SharedNS.Common
{
    /// <summary>
    /// This stores all the property values for an entity.
    /// </summary>
    public class PropertyValue
    {
        public PropertyValue()
        {

        }
        public PropertyValue(string name, string origValue, string currValue)
        {
            OriginalValue = origValue;
            CurrentValue = currValue;
        }
        public string Name { get; set; }
        public string OriginalValue { get; set; }
        public string CurrentValue { get; set; }
    }
}
