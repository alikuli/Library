using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Linq;


namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        private List<string> lstOfIds = new List<string>();
        private Stack<Product> trailStack = new Stack<Product>();

        public override void BusinessRulesFor(ControllerCreateEditParameter parm)
        {
            base.BusinessRulesFor(parm);
            checkParentageIsNotCircular(parm.Entity as Product);
            GetDataFromMenuPathCheckBoxes(parm.Entity as Product);
        }


        private void checkParentageIsNotCircular(Product entity)
        {
            if (entity.IsChild)
            {
                if (entity.Id == entity.ParentId)
                {
                    //Throw error... this will become cirular
                }

                //add the id of the entity and make sure that there are no duplicates to it.
                lstOfIds.Add(entity.Id);
                trailStack.Push(entity);
                traverseAllParents(entity.ParentId);
            }
        }

        private void traverseAllParents(string parentId)
        {
            if (!parentId.IsNullOrWhiteSpace())
            {
                List<Product> parents = FindAll().Where(x => x.ParentId == parentId).ToList();
                if (!parents.IsNullOrEmpty())
                {
                    foreach (Product p in parents)
                    {
                        trailStack.Push(p);
                        if (lstOfIds.Contains(p.Id))
                        {
                            //this Id is already part of the list, so there is a circular entry
                            string trail = makeTrailOfEntries();
                            ErrorsGlobal.Add(string.Format("Product '{0}' is causing a circular entry", p.Name), "Traversing Parents");
                        }
                        lstOfIds.Add(p.Id);

                        //this will check all the children of the current node we are on.
                        traverseAllParents(p.Id);
                        trailStack.Pop();
                    }
                }
            }


        }

        private string makeTrailOfEntries()
        {
            string s = "";

            if (trailStack.IsNull())
                return s;
            if (trailStack.Count == 0)
                return s;

            foreach (var item in trailStack)
            {
                s += string.Format("{0} - ", item.Name);
            }

            return s.Substring(s.Length - 2);
        }


    }
}
