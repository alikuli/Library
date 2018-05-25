
namespace ModelsClassLibrary.ViewModels
{
    public class Headings
    {
        public Headings()
        {
            Column = "Description/Name";
            Main = "";



        }
        public string Main { get; set; }
        public string Small { get; set; }
        public string Column { get; set; }
        public string RecordName { get; set; }
        public string RecordNamePlural { get; set; }
        public string SortOrderDescription { get; set; }

    }
}
