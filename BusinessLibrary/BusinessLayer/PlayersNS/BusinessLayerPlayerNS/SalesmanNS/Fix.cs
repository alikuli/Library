using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Web.Mvc;
using AliKuli.Extentions;
using System;

namespace UowLibrary.PlayersNS.SalesmanNS
{
    public partial class SalesmanBiz
    {
        public override void Fix(ModelsClassLibrary.ModelsNS.SharedNS.ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Salesman salesman = parm.Entity as Salesman;
            salesman.IsNullThrowException("Unable to unbox salesman");
            if(salesman.Name.IsNullOrWhiteSpace())
            {
                if(salesman.Person.IsNull())
                {
                    if(salesman.PersonId.IsNullOrWhiteSpace())
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

            if (salesman.SalesmanCategoryId.IsNullOrWhiteSpace())
                salesman.SalesmanCategoryId = null;

        }

    }
}
