using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.IndexNS.PlaceLocationNS
{
    public class State
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public List<City> Cities { get; set; }

    }
}
