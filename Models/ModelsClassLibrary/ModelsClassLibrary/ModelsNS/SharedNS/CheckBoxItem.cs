using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCheckBoxList.Models
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
            List = new List<CheckBoxItem>();
            LabelOnLeft = false;

        }
        /// <summary>
        /// You must give Id a value programatically.
        /// </summary>
        public string Id { get; set; }
        public string Label { get; set; }
        public bool IsTrue { get; set; }
        public bool LabelOnLeft { get; set; }
        public bool IsEnabled { get; set; }
        public ICollection<CheckBoxItem> List { get; set; }

    }
}