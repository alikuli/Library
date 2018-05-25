//using AliKuli.Extentions;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FileDocsNS;
//using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;

//namespace UowLibrary.CountryNS
//{
//    public partial class FileDocOldDataBiz : BusinessLayer<OldFileData>
//    {
//        public List<OldFileData> FindAllForUser(string userId, bool delete = false)
//        {
//            var listOfFileDocs = (FindAll(delete) as IQueryable<OldFileData>);

//            if (listOfFileDocs.IsNull())
//                return null;

//            if (!string.IsNullOrWhiteSpace(userId))
//            {
//                listOfFileDocs = listOfFileDocs.Where(x => x.UserId == userId);
//            }

//            return listOfFileDocs.ToList();
//        }
//        public async Task<List<OldFileData>> FindAllForUserAsync(string userId, bool delete = false)
//        {
//            var listOfFileDocs = (FindAll(delete) as IQueryable<OldFileData>);


//            if (!string.IsNullOrWhiteSpace(userId))
//            {
//                listOfFileDocs = listOfFileDocs.Where(x => x.UserId == userId);
//            }

//            return await listOfFileDocs.ToListAsync();
//        }
//    }
//}
