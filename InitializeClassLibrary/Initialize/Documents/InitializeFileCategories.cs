using System;
using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeFileCategories
    {
        private static FileCategoryDAL _dal;
        private static ApplicationDbContext _db;
        private string _user;

        public InitializeFileCategories(ApplicationDbContext dbIn, string userIn)
        {
            _db = dbIn;
            _user = userIn;
            _dal = new FileCategoryDAL(dbIn, _user);
        }

        private void Add(string name)
        {

            //get list of customers
            FileCategory entity = _dal.Factory();
            entity.Name = name;




            entity.MetaData.Comment = string.Format("Added thru Initialization on {0} dated {1}, {2}",
                DateTime.UtcNow.ToLongTimeString(),
                DateTime.UtcNow.ToLongDateString(),
                entity.ToString());

            try
            {
                _dal.Create(entity);
                _dal.Save();
            }
            catch (NoDuplicateException)
            { }

            catch
            {
                throw;
            }
        }


        public void Initialize()
        {
            string names = "1-H, 2 The Mall, Assets, Banks, Business, AES, Ameritrade, Attock Feeds, Begwall, Concord College, Cosmetics, DDS, Gulkali Farms, Internet Marketing, investments, JP, Land, Laundry, Leather, Manufactuerers, Marketing, Misc, Pamir, Rolling Hills, Shaan International, Tajbagh, TB2197, Cars, Case, 110 Zubaida, 12(2) UK, 12(2) UK Custody, Aila Vs Farhad, Annul Compromise Deed dated 17.09.02, Bogus Birth Certificate, Ch. Saleem Pakistan Bar Council, Contempt PHC AKA Vs MAQ, Custody(2) Khwajah, Custody 12(2) Rubina, Custody-Execution, Custody U/S 25 AKA, Custody U/S 25 Rubina, Defamation -No2, Dissolution of Marriage, Fake Surety, Huma Vs PA etc, Judicial Tempering, Kidnapping, Ma Vs TB Owners, Maintenance of Minor, MAQ Vs Doc Etc -GK Plot, MAV Vs. PA GK Plot, Munshee Land Cases, Perjury AKA Vs Rubina Etc, Punjab Bar Council AKA Vs Mirza Etc, Recovery of Items, Restitution of Conjugal Rights Suit -LHC, Tanzeem, Tajuman-e-Punjab, U/S 25 UK, UK Contact, Wapda Meters, WP 10214/02 KH Vs GJ, WP 1293/06 Doc Vs MAQ, Lahore High Court, WP 13557/04 Sameer Vs. SHO, WP 13697/06 Khulla, WP 15045/04 Sameer Vs Ali Kuli, WP 18915/04, WP 21448/01 Misbah Vs AKA (Custody), WP4153/03 Munshee Riaz Vs FIA, WP 6211/04 Misbah Vs AKA, WP6229/04, Computer, Duplicate Files, Empty, Equipment Papers, FIA, FIA-E 065/06 Duplicate Passport, FIA E 247/04 Forigener's Act, FIA -Unused Applications, Files, FIR 006/09, FIR 046/06 PS Ghalib Market, Lahore, FIR, FIR 047/05, FIR 0641/07 PS Ghaziabad, FIR 1012/09, FIR 1034/09, FIR 1289/11 PS Islampura U/S 471/468/420, FIR 1306/08, FIR 167/07 - Fake, FIR 182 for FIR 299/05 PS Gulberg, FIR 305/09 PS Ghaziabad, PS Ghaziabad, FIR 330/05 PS Sharkee Peshawar, PS Sharkee Peshawar, FIR 355/05 PS Sharkee Peshawar, FIR 474/065 Faisel Town PA/Azhar, FIR 480/05 PS North Cantt, PS North Cantt. FIR 508/08, FIR 548/10, FIR 574/2009, FIR 611/08 PS Defence B, PS Defence B Lahore, FIR 640/07 PS Ghaziabad, PS Ghaziabad Lahore, FIR 641/07 PS Ghaziabad, FIR 656/07 MR Vs Zafar Theakaadaar, FIR 852/11 U/S 448/511 PS Ghaziabad, FIR 865/06 PS Ghaziabad -TB Kubza Attempt, FIR 870/09, FIR 969/08 PS North Cantt AKA Vs Kh/Munshi/ MAQ, FIR 969/09 North Cantt (Khizzer), Forms, GK, GK Case, Hobby, House, Investment, ISP, Law, Magazines, Maps, MAQ, MAQ: Cases, MAQ: Plots, Misc, Nadra, Notebooks, People, Ami, Amin Ahmed/Meraj Chaudry, Ch. Saleem Advocate, Doc, Farhat, Kh. Habibullah, Mohammad Afzaal (MA), Munshi riaz, Parvez Amin, Qazi Zia Advocate, Rashid, Rubina Misbah, Shaan, Zafar Amin, Personal, Articles, Clubs, Documents, Health & Medical, Telphone, Pictures, Power of Attornies, Telphone, Wapda bills";


            var namesArray = names.Split(',');

            if (!namesArray.IsNullOrEmpty())
            {
                foreach (var name in namesArray)
                {
                    Add(name);
                }
            }
        }
        public void DeleteAll()
        {
            var list = _dal.FindAll();
            foreach (var item in list)
            {
                _db.FileCategories.Remove(item);
            }
            _db.SaveChanges();

            list = _dal.FindAll(true);
            foreach (var item in list)
            {
                _db.FileCategories.Remove(item);
            }
            _db.SaveChanges();
        }

        public void Edit()
        {

            List<FileCategory> lst = _dal.FindAll().ToList();

            if (lst == null)
                throw new Exception(string.Format("No '{0}' found to edit.", _dal.GetSelfClassName()));

            foreach (var item in lst)
            {
                item.Name += "*";
                _dal.Update(item);
                try
                {
                    _dal.Save();
                }
                catch (Exception e)
                {
                    string error = ErrorHelpers.GetInnerException(e) + " - For item: " + item.Name;
                }
            }

        }

    }
}