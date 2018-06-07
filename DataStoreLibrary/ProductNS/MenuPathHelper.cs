
namespace DatastoreNS
{
    public class MenuPathHelper
    {

        public MenuPathHelper(string menuPath1Name, string menuPath2Name, string menuPath3Name)
        {
            MenuPath1Name = menuPath1Name;
            MenuPath2Name = menuPath2Name;
            MenuPath3Name = menuPath3Name;
        }
        public string MenuPath1Name { get; set; }
        public string MenuPath2Name { get; set; }
        public string MenuPath3Name { get; set; }

    }
}
