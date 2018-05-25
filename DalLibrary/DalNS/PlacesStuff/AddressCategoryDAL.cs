//using AliKuli.Utilities;
//using AliKuli.Extentions;
//
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using ModelsClassLibrary.Models.ProductNS;
//using ModelsClassLibrary.Models.AddressNS;
//using System.Web.Caching;

//namespace DalLibrary.Dal
//{
//    public class AddressCategoryDAL : Repositry<AddressCategory>
//    {


//        public AddressCategoryDAL(ApplicationDbContext db, string user)
//            : base(db, user)
//        {
//            _db = db;
//            _user = user;
//        }


//        public void InitializeFromEnumAndSave()
//        {

//            //this will create  categories for the Enums
//            AddressTypeENUM s = AddressTypeENUM.Unknown; 
//            var listOfEnums = s.ToTitleSentanceList();
//            listOfEnums.Remove(AddressTypeENUM.Unknown.ToString());

//            CreateEntitiesFromListOfStrings(listOfEnums);
//            Save();
//        }

//        public override string MakeNameForIndexMethod(AddressCategory entity)
//        {
//            return entity.FullName(); ;
//        }

//        public AddressCategory FindByName(string name)
//        {
//            if (name))
//                return null;

//            var AddressCategory= this.SearchFor(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
//            return AddressCategory;

//        }

//        public AddressCategory GetConsignToCategory()
//        {
//            //get from cache memory.
//            string cacheAddress ="ConsignToCategory";
//            var ctc = GetFromCache(cacheAddress);
//            return (ctc as AddressCategory);
//        }

//        public AddressCategory GetShipToCategory()
//        {
//            //get from cache memory.
//            string cacheAddress = "ShipToCategory";
//            var ctc = GetFromCache(cacheAddress);
//            return (ctc as AddressCategory);
//        }

//        public AddressCategory GetInformToCategory()
//        {
//            string cacheAddress = "InformToCategory";
//            var ctc = GetFromCache(cacheAddress);
//            return (ctc as AddressCategory);
//        }

//        public AddressCategory GetUnknownCategory()
//        {
//            string cacheAddress = "UnknownCategory";
//            var ctc = GetFromCache(cacheAddress);
//            return (ctc as AddressCategory);
//        }

//        private AddressCategory GetFromCache(string cacheAddress)
//        {


//            try
//            {
//                var ctc = ((AddressCategory)AliKuli.Utilities.CacheUtility.GetFromCache(cacheAddress));
//                if (ctc == null)
//                {
//                    switch (cacheAddress)
//                    {
//                        case "ConsignToCategory":
//                            ctc = FindByName(AddressTypeENUM.ConsignTo.ToString().ToSentence());
//                            AliKuli.Utilities.CacheUtility.SaveToCache(cacheAddress, ctc);
//                            return (ctc);

//                        case "ShipToCategory":
//                            ctc = FindByName(AddressTypeENUM.ShipTo.ToString().ToSentence());
//                            AliKuli.Utilities.CacheUtility.SaveToCache(cacheAddress, ctc);
//                            return (ctc);

//                        case "InformToCategory":
//                            ctc = FindByName(AddressTypeENUM.InformTo.ToString().ToSentence());
//                            AliKuli.Utilities.CacheUtility.SaveToCache(cacheAddress, ctc);
//                            return (ctc);

//                        case "unknown":
//                            ctc = FindByName(AddressTypeENUM.Unknown.ToString().ToSentence());
//                            AliKuli.Utilities.CacheUtility.SaveToCache(cacheAddress, ctc);
//                            return (ctc);

//                        default:
//                            return null;
//                    }

//                }
//                return ctc;
//            }

//            catch(Exception)
//            {
//            }
//            return null;

//        }

//    }
//}
