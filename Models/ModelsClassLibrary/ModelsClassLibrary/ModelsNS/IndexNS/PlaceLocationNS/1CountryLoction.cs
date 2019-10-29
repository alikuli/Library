using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.IndexNS.PlaceLocationNS
{
    public class Country
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public List<State> States { get; set; }
    }
}
