
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;

namespace UowLibrary.PlayersNS.SalesmanCategoryNS
{
    public partial class SalesmanCategoryBiz
    {
        public override void InitializationData()
        {
            initializeSalesmanCategories();
        }

        /// <summary>
        /// We are getting "Super Salesman" from this basicaly.
        /// </summary>
        void initializeSalesmanCategories()
        {
            foreach (var item in Enum.GetNames(SalesmanCategoryENUM.Unknown.GetType()))
            {
                string nameFixed = item.ToTitleSentance();
                if (nameFixed.IsNullOrWhiteSpace())
                    continue;
                //see if it already exists
                SalesmanCategory sc = FindForName(nameFixed);
                if (sc.IsNull())
                {
                    sc = Factory() as SalesmanCategory;
                    sc.Name = nameFixed;
                    CreateAndSave(sc);
                }
            }
        }
    }
}
