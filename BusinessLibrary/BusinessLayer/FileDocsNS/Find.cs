using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace UowLibrary.FileDocNS
{
    public partial class FileDocBiz : BusinessLayer<FileDoc>
    {
        public List<FileDoc> FindAllForUser(string userId, bool delete = false)
        {
            userId.IsNullOrWhiteSpaceThrowArgumentException("userId");
            
            Person person = PersonBiz.GetPersonForUserId(userId);
            person.IsNullThrowException("person not found!");
            string personId = person.Id;

            var listOfFileDocs = FindAll(delete) as IQueryable<FileDoc>;
            listOfFileDocs = listOfFileDocs.Where(x => x.PersonId == personId);
            return listOfFileDocs.ToList();

        }





        public async Task<List<FileDoc>> FindAllForUserAsync(string userId, bool delete = false)
        {
            var listOfFileDocs = (FindAll(delete) as IQueryable<FileDoc>);

            Person person = PersonBiz.GetPersonForUserId(userId);
            person.IsNullThrowException("person not found!");
            string personId = person.Id;
            listOfFileDocs = listOfFileDocs.Where(x => x.PersonId == personId);

            return await listOfFileDocs.ToListAsync();
        }
    }
}
