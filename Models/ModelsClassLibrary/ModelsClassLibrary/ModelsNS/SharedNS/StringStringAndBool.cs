
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public class StringStringAndBool : StringAndBool
    {
        public StringStringAndBool()
            : base()
        {

        }

        public StringStringAndBool(string str1, string str2, bool select)
            : base(str1, select)
        {
            Str2 = str2;
        }
        public string Str2 { get; set; }

    }
}
