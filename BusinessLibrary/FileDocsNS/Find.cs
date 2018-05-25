using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
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
            var listOfFileDocs = (FindAll(delete) as IQueryable<FileDoc>);

            if (listOfFileDocs.IsNull())
                return null;

            if (!string.IsNullOrWhiteSpace(userId))
            {
                listOfFileDocs = listOfFileDocs.Where(x => x.UserId == userId);
            }

            return listOfFileDocs.ToList();
        }
        public async Task<List<FileDoc>> FindAllForUserAsync(string userId, bool delete = false)
        {
            var listOfFileDocs = (FindAll(delete) as IQueryable<FileDoc>);


            if (!string.IsNullOrWhiteSpace(userId))
            {
                listOfFileDocs = listOfFileDocs.Where(x => x.UserId == userId);
            }

            return await listOfFileDocs.ToListAsync();
        }
    }
}
