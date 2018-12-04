using AliKuli.Extentions;
using ModelsClassLibrary.DelegatesNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketPlace.Web6.Models
{
    public class FeaturesListModel
    {
        public List<FeaturesModel> Features { get; set; }
        public List<FeaturesModel> FeaturesAdded { get; set; }
        public List<FeaturesModel> FeaturesUpdated { get; set; }
        public string SaveTime { get; set; }
        public void Add()
        {
            Features.Add(new FeaturesModel());

        }

        public void Delete(int index)
        {

            Features[index].IsDeleted = true;
            Features[index].NewDirtyFlag = Features[index].GetHashCode();
        }

        public void Save()
        {
            if (Features.IsNullOrEmpty())
                return;

            foreach (var item in Features)
            {
                item.NewDirtyFlag = item.GetHashCode();
            }


            var itemsToSave = Features.Where(x => x.IsChanged == true).ToList();

            if (itemsToSave.IsNull())
                return;
            
            foreach (FeaturesModel item in itemsToSave)
            {
                //save these
                switch (item.IsDeleted)
                {
                    case true:
                        //Now Delete!
                        break;
                    default:
                        switch (item.OriginalDirtyFlag)
                        {
                            case 0:
                            //This is an Add
                            default:
                                //This is an update
                                break;
                        }
                        break;
                }
                ResetDirtyFlags(item);

            }

            SaveTime = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }


        public void SetupDirtyFlags()
        {
            if (Features.IsNullOrEmpty())
                return;

            foreach (FeaturesModel item in Features)
            {
                ResetDirtyFlags(item);
            }
        }

        public static void ResetDirtyFlags(FeaturesModel item)
        {
            item.OriginalDirtyFlag = item.GetHashCode();
            item.NewDirtyFlag = item.OriginalDirtyFlag;
            item.IsUpadated = false;
        }

    }
}