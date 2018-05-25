//using AliKuli.Exceptions;
//using AliKuli.Extentions;
//using ModelsClassLibrary.ModelsNS.MediaNS;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//

//namespace DalLibrary.Dal
//{
//    public class MovieDAL : Repositry<Movie>
//    {

//        private ApplicationDbContext db;
//        private string user;

//        public MovieDAL(ApplicationDbContext db, string user)
//            : base(db, user)
//        {
//            this.db = db;
//            this.user = user;
//        }
//        public string MakeNameForIndexMethod(string name)
//        {
//            return string.Format("{0}", name);
//        }

//        public override void Create(Movie entity)
//        {
//            //No duplicates allowed
//            //search for the name
//            var nameFound = base.SearchFor(x => x.Name.ToLower() == entity.Name.ToLower()).FirstOrDefault();

//            if (nameFound != null)
//                throw new NoDuplicateException(string.Format("The Movie '{0}' already exists. Try again",entity.Name));

//            UpdateFieldsToTitleCaseAndUpperCase(entity);

//            base.Create(entity);
//        }
//        public IEnumerable<SelectListItem> SelectList()
//        {
//            List<SelectListItem> selectList = new List<SelectListItem>();
//            var listOfCountries = base.FindAll().ToList();
//            if (listOfCountries != null)
//            {
//                if (listOfCountries.Count > 0)
//                {
//                    foreach (var item in listOfCountries)
//                    {
//                        SelectListItem sVM = new SelectListItem();
//                        sVM.Value = item.Id.ToString();
//                        sVM.Text = item.Name;
//                        selectList.Add(sVM);
//                    }
//                }
//            }
//            return selectList.OrderBy(x => x.Text).AsEnumerable();

//        }

//        public override void Update(Movie entity)
//        {
//            UpdateFieldsToTitleCaseAndUpperCase(entity);

//            base.Update(entity);
//        }

//        private static void UpdateFieldsToTitleCaseAndUpperCase(Movie entity)
//        {
//            entity.Name = entity.Name.ToTitleCase();
//        }

//        public Movie FindByName(string name)
//        {
//            if (string.IsNullOrEmpty(name))
//                return null;

//            Movie Movie= this.SearchFor(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
//            return Movie;

//        }

//    }
//}
