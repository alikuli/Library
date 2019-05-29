
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;


namespace UowLibrary.BuySellDocNS
{
    public partial class BuySellDocBiz
    {

        public override bool Delete(string id)
        {
            BuySellDoc buySellDoc = Find(id);
            buySellDoc.IsNullThrowException("BuySellDoc not found");

            deleteItems(buySellDoc);

            
            
            return base.Delete(id);

            //Now delete the children
        }

        private void deleteItems(BuySellDoc buySellDoc)
        {
            List<BuySellItem> buySellItems = new List<BuySellItem>();
            if (!buySellDoc.BuySellItems.IsNullOrEmpty())
            {
                //List the children first
                buySellItems = buySellDoc.BuySellItems.ToList();

            }

            if (!buySellItems.IsNullOrEmpty())
            {
                foreach (var item in buySellItems)
                {
                    BuySellItemBiz.Delete(item.Id);
                }
            }
        }



        public override async System.Threading.Tasks.Task<bool> DeleteAsync(string id)
        {
            BuySellDoc buySellDoc = await FindAsync(id);
            buySellDoc.IsNullThrowException("BuySellDoc not found");

            deleteItems(buySellDoc);

            return await base.DeleteAsync(id);
        }

    }
}
