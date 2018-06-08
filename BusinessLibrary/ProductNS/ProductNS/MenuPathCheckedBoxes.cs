using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using System.Collections.Generic;
using System.Linq;
using ModelsClassLibrary.SharedNS;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {

        //this returns a list of checked boxes marked true
        public List<CheckBoxItem> LoadMenuPathCheckedBoxes(Product product)
        {
            var allMenuPaths = MenuPathMainBiz.FindAll();

            if (allMenuPaths.IsNullOrEmpty())
                return null;

            //Now create all the check boxes
            List<CheckBoxItem> checkedboxes = new List<CheckBoxItem>();

            foreach (var menupath in allMenuPaths)
            {
                CheckBoxItem chk = new CheckBoxItem(menupath.Id, menupath.Name,true);
                checkedboxes.Add(chk);
            }

            //Now mark all the ones contained in this product as true

            if (product.MenuPathMains.IsNullOrEmpty())
                return checkedboxes;

            foreach (var menuPaths in product.MenuPathMains)
            {
                CheckBoxItem cbi = checkedboxes.FirstOrDefault(x => x.Id == menuPaths.Id);
                if (cbi.IsNull())
                    continue;
                cbi.IsTrue = true;
            }

            return checkedboxes;
        }

    }
}
