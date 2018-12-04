using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.SharedNS
{
    /// <summary>
    /// This works with the EditorTemplate in the View called CheckBoxItem.cshtml.
    /// </summary>
    /// 
    [NotMapped]
    public class CheckBoxItem
    {
        public CheckBoxItem()
        {

        }

        public CheckBoxItem(string id, string label, bool isOnLeft = true)
            : this()
        {
            Id = id;
            Label = label;
            LabelOnLeft = isOnLeft;
        }

        public CheckBoxItem(string id, string label, string mp1Name, string mp2Name, string mp3Name, bool isOnLeft = true)
            : this()
        {
            Id = id;
            Label = label;
            LabelOnLeft = isOnLeft;

            Mp1Name = mp1Name;
            Mp2Name = mp2Name;
            Mp3Name = mp3Name;
        }

        /// <summary>
        /// You must give Id a value programatically.
        /// </summary>
        public string Id { get; set; }
        public string Label { get; set; }
        public bool IsTrue { get; set; }
        public bool LabelOnLeft { get; set; }
        public bool IsEnabled { get; set; }

        public string Mp1Name { get; set; }
        public string Mp2Name { get; set; }
        public string Mp3Name { get; set; }


    }
}