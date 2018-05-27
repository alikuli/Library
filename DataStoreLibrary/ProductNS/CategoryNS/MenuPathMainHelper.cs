
namespace Data.MenuNS
{
    public class MenuPathMainHelper
    {
        public MenuPathMainHelper (string c1, string c2, string c3)
        {
            MenuPath1 = c1;
            MenuPath2 = c2;
            MenuPath3 = c3;

            //string s = string.Format("\"{0}\", \"{1}\", \"{2}\"", Cat1, Cat2, Cat3);
            //return "{" + s + "}, ";

        }
        public string MenuPath1 { get; set; }
        public string MenuPath2 { get; set; }
        public string MenuPath3 { get; set; }

    }
}
