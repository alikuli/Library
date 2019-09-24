using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;

namespace UowLibrary.PlayersNS.SalesmanNS
{
    public partial class SalesmanBiz
    {
        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Salesman salesman = parm.Entity as Salesman;
            salesman.IsNullThrowException("Unable to unbox salesman");
            add_Name(salesman);
            fix_SalesmanCategoryId(salesman);
            fix_ParentSalesmanId(salesman);


        }

        /// <summary>
        /// This gets the super salesman category.
        /// </summary>
        /// <returns></returns>
        private SalesmanCategory getSuperSalesmanCategory()
        {
            SalesmanCategory sc = SalesmanCategoryBiz.FindByName(SalesmanCategoryENUM.SuperSalesman.ToString().ToTitleSentance());
            sc.IsNullThrowException();
            return sc;
        }


        private void fix_ParentSalesmanId(Salesman salesman)
        {
            if (salesman.ParentSalesmanId.IsNullOrWhiteSpace())
                salesman.ParentSalesmanId = null;
        }

        private static void fix_SalesmanCategoryId(Salesman salesman)
        {
            if (salesman.SalesmanCategoryId.IsNullOrWhiteSpace())
                salesman.SalesmanCategoryId = null;
        }

        private void add_Name(Salesman salesman)
        {
            if (salesman.Name.IsNullOrWhiteSpace())
            {
                if (salesman.Person.IsNull())
                {
                    if (salesman.PersonId.IsNullOrWhiteSpace())
                    {
                        throw new Exception("No person added.");
                    }
                    else
                    {
                        Person person = PersonBiz.Find(salesman.PersonId);
                        salesman.Name = person.Name;
                    }
                }
                else
                {
                    salesman.Name = salesman.Person.Name;

                }
            }

        }


        public override void ErrorCheck(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.ErrorCheck(parm);
            Salesman sm = Salesman.Unbox(parm.Entity);
            cannotBeSuperSalesmanOfSelf(sm);
            checkIfParentRelated(sm);
            only_super_Salesman_Can_Be_Parent(sm);



        }

        private void only_super_Salesman_Can_Be_Parent(Salesman salesman)
        {
            //CHECK parent is Super salesman
            if (salesman.ParentSalesmanId.IsNullOrWhiteSpace())
                return;
            //check to see if Parent is a super Salesman category
            if (salesman.ParentSalesman.IsNull())
                salesman.ParentSalesman = Find(salesman.ParentSalesmanId);
            salesman.ParentSalesman.IsNullThrowException("Unable to locate Salesman");

            SalesmanCategory sc = getSuperSalesmanCategory();

            if (salesman.ParentSalesman.SalesmanCategoryId != sc.Id)
            {
                string err = string.Format("The salesman '{0}' is not a Super Salesman and therefore CANNOT be a parent.", salesman.ParentSalesman.FullName());
                throw new Exception(err);

            }

        }

        private void cannotBeSuperSalesmanOfSelf(Salesman sm)
        {
            if (sm.ParentSalesmanId == sm.Id)
                throw new Exception("Salesman cannot be Super Salesman of Self.");
        }

        void checkIfParentRelated(Salesman child)
        {
            if (child.ParentSalesmanId.IsNullOrWhiteSpace())
                return;
            if (child.ParentSalesman.IsNull())
                child.ParentSalesman = Find(child.ParentSalesmanId);

            child.ParentSalesman.IsNullThrowException();
            stackOfAncestors.Push(child);
            if (isExpectedParentRelatedToChild(child.ParentSalesman, child))
            {
                string err = string.Format("'{0}' and '{1}' are already related. {2}", child.ParentSalesman.FullName(), child.FullName(), listAncestors());
                throw new Exception(err);
            }
        }


        Stack<Salesman> stackOfAncestors = new Stack<Salesman>();
        string listAncestors()
        {
            string linkage = "";

            int noOfItems = stackOfAncestors.Count;
            int count = 0;
            if (noOfItems > 0)
            {
                foreach (Salesman salesman in stackOfAncestors)
                {
                    count++;
                    string parentName = "Error";
                    if (!salesman.ParentSalesman.IsNull())
                        parentName = salesman.ParentSalesman.FullName();
                    if(count == noOfItems)
                    {
                        if (linkage.Length > 2)
                        {
                            linkage = linkage.Substring(0, linkage.Length - 2);

                        }
                        linkage += string.Format(". Therefore, '{0}' CANNOT become Parent of '{1}'. ", parentName, salesman.FullName());
                    }
                    else
                    {
                        linkage += string.Format("'{0}' is Parent of '{1}', ", parentName, salesman.FullName());

                    }

                }
                //remove the last =>
            }
            return linkage;
        }

        private bool isExpectedParentRelatedToChild(Salesman parent, Salesman child)
        {
            stackOfAncestors.Push(parent);

            if (parent.ParentSalesmanId.IsNullOrWhiteSpace())
            {
                return false;
            }
            else
            {
                if (parent.ParentSalesmanId == child.Id)
                {
                    //we have found a relation
                    return true;
                }
                else
                {
                    //No relation
                    if (parent.ParentSalesman.IsNull())
                    {
                        parent.ParentSalesman = Find(parent.ParentSalesmanId);
                        parent.ParentSalesman.IsNullThrowException();
                    }

                    return isExpectedParentRelatedToChild(parent.ParentSalesman, child);
                }
            }

        }



    }
}
