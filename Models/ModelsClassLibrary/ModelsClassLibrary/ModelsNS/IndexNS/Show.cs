
namespace ModelsClassLibrary.ViewModels
{
    public class Show
    {
        public bool EditDeleteAndCreate
        {
            set
            {
                bool isTrueOrFalse = value;

                Edit = isTrueOrFalse;
                Delete = isTrueOrFalse;
                Create = isTrueOrFalse;

                //if (isTrue)
                //{
                //    Edit = true;
                //    Delete = true;
                //    Create = true;
                //}
            }
        }

        public void MoveUpMoveDown(bool trueFalse)
        {
            MoveUp = trueFalse;
            MoveDown = trueFalse;
            MoveTop = trueFalse;
            MoveBottom = trueFalse;
        }

        public bool MoveTop { get; set; }
        public bool MoveBottom { get; set; }
        public bool MoveUp { get; set; }
        public bool MoveDown { get; set; }
        /// <summary>
        /// If true, then Edit shows
        /// </summary>
        public bool Edit { get; set; }

        /// <summary>
        /// If true then Delete shows. Remember this is also controlled by the
        /// AutoCreated in the CommonWithId. Both have to be true to allow delete.
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// if true then create shows
        /// </summary>
        public bool Create { get; set; }

        public bool ImageInList { get; set; }

        public bool VerificationIcon { get; set; }

        public bool MakeDefaultIcon { get; set; }
    }
}
