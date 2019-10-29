using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.IndexNS.PlaceLocationNS
{
    public class City
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public List<Town> Towns { get; set; }

    }
}
