using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UserModels;

namespace ModelsClassLibrary.RightsNS
{
    /// <summary>
    /// This will be used to give users rights to CRUD. This particular user will be allowed to do whatever is allowed here.
    /// The CreateChildren will allow the user to give rights to their account.
    /// This contains everything you need to give a user his rights
    /// </summary>
    public partial class Right : CommonWithId, IRight
    {
        public Right()
        {
            Create = false;
            Retrieve = false;
            Update = false;
            Delete = false;
            DeleteActually = false;

        }
        public Right(ClassesWithRightsENUM rightsFor)
            : this(rightsFor, "")
        {

        }
        public Right(ClassesWithRightsENUM rightsFor, string userId)
        {
            UserId = userId;
            RightsFor = rightsFor;
        }


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Right;
        }


        /// <summary>
        /// This holds the name of the class which is controlled.
        /// </summary>
        public ClassesWithRightsENUM RightsFor { get; set; }

        public bool Create { get; set; }
        public bool Retrieve { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool DeleteActually { get; set; }


        public bool CreateChildren { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string MakeKey()
        {
            return RightsFor + UserId;
        }
        public string MakeKey(ClassesWithRightsENUM classesWithRightsEnum, string userId)
        {
            RightsFor = classesWithRightsEnum;
            UserId = userId;

            return MakeKey();
        }

        public override string FullName()
        {
            if (Name.IsNullOrWhiteSpace())
                return "";

            var arrayName = Name.Split(' ');

            if (arrayName.IsNullOrEmpty())
                return " ";

            string rightName = arrayName[0];
            string userName = "";

            if (arrayName.Length > 1)
                userName = arrayName[1];

            return string.Format("{0} {1}", userName, rightName); ;
        }

    }
}
