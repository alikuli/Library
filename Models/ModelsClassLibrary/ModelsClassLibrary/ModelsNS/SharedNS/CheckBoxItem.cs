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
        /// <summary>
        /// You must give Id a value programatically.
        /// </summary>
        public string Id { get; set; }
        public string Label { get; set; }
        public bool IsTrue { get; set; }
        public bool LabelOnLeft { get; set; }
        public bool IsEnabled { get; set; }

    }
}