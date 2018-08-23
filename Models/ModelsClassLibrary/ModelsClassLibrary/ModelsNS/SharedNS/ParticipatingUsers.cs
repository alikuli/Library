using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [NotMapped]
    public class ParticipatingUsers
    {
        public ParticipatingUsers()
        {

        }
        /// <summary>
        /// this class is used for showing the users who have comments, clicked etc
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="imageLocation"></param>
        public ParticipatingUsers(string id, string name, string imageLocation, string userComment)
        {
            Id = id;
            Name = name;
            ImageLocation = imageLocation;
            UserComment = userComment;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        public string UserAddressFixed { get; set; }
        public string UserComment { get; set; }
    }
}
