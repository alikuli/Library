

namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {
        //private List<string> lstOfIds = new List<string>();
        //private Stack<ProductChild> trailStack = new Stack<ProductChild>();

        //public override void BusinessRulesFor(ProductChild entity)
        //{
        //    base.BusinessRulesFor(entity);
        //    checkParentageIsNotCircular(entity);
        //}


        //private void checkParentageIsNotCircular(ProductChild entity)
        //{
        //    if (entity.IsChild)
        //    {
        //        if (entity.Id == entity.ParentId)
        //        {
        //            //Throw error... this will become cirular
        //        }

        //        //add the id of the entity and make sure that there are no duplicates to it.
        //        lstOfIds.Add(entity.Id);
        //        trailStack.Push(entity);
        //        traverseAllParents(entity.ParentId);
        //    }
        //}

        //private void traverseAllParents(string parentId)
        //{
        //    if (!parentId.IsNullOrWhiteSpace())
        //    {
        //        List<ProductChild> parents = FindAll().Where(x => x.ParentId == parentId).ToList();
        //        if (!parents.IsNullOrEmpty())
        //        {
        //            foreach (ProductChild p in parents)
        //            {
        //                trailStack.Push(p);
        //                if (lstOfIds.Contains(p.Id))
        //                {
        //                    //this Id is already part of the list, so there is a circular entry
        //                    string trail = makeTrailOfEntries();
        //                    ErrorsGlobal.Add(string.Format("ProductChild '{0}' is causing a circular entry", p.Name), "Traversing Parents");
        //                }
        //                lstOfIds.Add(p.Id);

        //                //this will check all the children of the current node we are on.
        //                traverseAllParents(p.Id);
        //                trailStack.Pop();
        //            }
        //        }
        //    }


        //}

        //private string makeTrailOfEntries()
        //{
        //    string s = "";

        //    if (trailStack.IsNull())
        //        return s;
        //    if (trailStack.Count == 0)
        //        return s;

        //    foreach (var item in trailStack)
        //    {
        //        s += string.Format("{0} - ", item.Name);
        //    }

        //    return s.Substring(s.Length - 2);
        //}


    }
}
