
using AliKuli.Extentions;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.SalesmanNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Linq;
namespace UowLibrary.SuperLayerNS
{


    public partial class SuperBiz
    {
        public PersonServedBySalesmanWithTotalTrxAmountAdded_Header CreateSalesmanReport_PeopleServedBySalesman(string userId)
        {
            userId.IsNullOrWhiteSpaceThrowException("Not logged in");


            int noOfDaysSalesmanKeepsPerson = GetNoOfDaysSalesmanKeepsPerson();
            int minimumAmountOfSaleRequired = GetMinimumRuppeeAmountRequiredToKeepPerson();
            DateTime beginDate = DateTime.Now.AddDays(-1 * noOfDaysSalesmanKeepsPerson);
            DateTime endDate = DateTime.Now;

            

            Person salesmanPerson = PersonBiz.GetPersonForUserId(userId);
            salesmanPerson.IsNullThrowException("Salesman not found.");

            
            PersonServedBySalesmanWithTotalTrxAmountAdded_Header header = new PersonServedBySalesmanWithTotalTrxAmountAdded_Header(userId,salesmanPerson,beginDate,endDate, minimumAmountOfSaleRequired);

            header.Detail = ListPeopleBoughtOrSoldCashFromSalesman(header);
            return header;
        }
        /// <summary>
        /// this returns a grouped list with amounts summed of people that the salesman has serviced.
        /// </summary>
        /// <param name="salesmanId"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<PersonServedBySalesmanWithTotalTrxAmountAdded> ListPeopleBoughtOrSoldCashFromSalesman(PersonServedBySalesmanWithTotalTrxAmountAdded_Header header)
        {


            List<PersonServedBySalesman> listOfPeopleWhoSoldOrBoughtCashFromSalesman = getListOfPeopleOwnedBySalesmanFor(header.SalesmanPersonId, header.BeginDate, header.EndDate);

            //now group the people and add up their amounts. Note, we do not diffrentiate between Reciepts and payments. We want to see the
            //maximum transaction amount

            if (listOfPeopleWhoSoldOrBoughtCashFromSalesman.IsNullOrEmpty())
                return null;

            //var query = (from t in Transactions
            //             group t by new { t.MaterialID, t.ProductID }
            //                 into grp
            //                 select new
            //                 {
            //                     grp.Key.MaterialID,
            //                     grp.Key.ProductID,
            //                     Quantity = grp.Sum(t => t.Quantity)
            //                 }).ToList();
            //https://stackoverflow.com/questions/847066/group-by-multiple-columns

            List<PersonServedBySalesmanWithTotalTrxAmountAdded> peopleWhoBoughtOrSoldCashToSalesman = (from p in listOfPeopleWhoSoldOrBoughtCashFromSalesman
                                                                                                       group p by p.PersonId
                                                                                                           into grp
                                                                                                           select new PersonServedBySalesmanWithTotalTrxAmountAdded
                                                                                                           {
                                                                                                               PersonId = grp.Key,
                                                                                                               Amount = grp.Sum(t => t.Amount),
                                                                                                               FirstDate = grp.Min(t => t.Date),
                                                                                                               LastDate = grp.Max(t => t.Date),
                                                                                                           })
                                                                               .ToList();

            if (peopleWhoBoughtOrSoldCashToSalesman.IsNullOrEmpty())
                return null;

            foreach (PersonServedBySalesmanWithTotalTrxAmountAdded personAdded in peopleWhoBoughtOrSoldCashToSalesman)
            {
                PersonServedBySalesman personServedBySalesman = listOfPeopleWhoSoldOrBoughtCashFromSalesman.FirstOrDefault(x => x.PersonId == personAdded.PersonId);

                personServedBySalesman.IsNullThrowException("person Served By Salesman cannot be null");
                personServedBySalesman.Person.IsNullThrowException("person cannot be null");
                personAdded.Person = personServedBySalesman.Person;
            }

            return peopleWhoBoughtOrSoldCashToSalesman;

        }

        private List<PersonServedBySalesman> getListOfPeopleOwnedBySalesmanFor(string personId, DateTime beginDate, DateTime endDate)
        {
            //get the window
            //get the salesmanPerson

            //get the cashTrx within the dates where salesman is either a From or a to.
            List<CashTrx> cashTrxs = CashTrxBiz
                .FindAll()
                .Where(x => x.PersonFromId == personId || x.PersonToId == personId)
                .ToList();

            if (cashTrxs.IsNullOrEmpty())
                return null;

            List<PersonServedBySalesman> listOfPeopleOwnedBySalesman = new List<PersonServedBySalesman>();
            foreach (CashTrx cashTrx in cashTrxs)
            {
                PersonServedBySalesman personOwned = new PersonServedBySalesman();
                if (cashTrx.PersonToId != personId)
                {
                    personOwned.PersonId = cashTrx.PersonToId;
                    personOwned.Person = cashTrx.PersonTo;
                    personOwned.IsReceivingMoney = true;

                }
                else
                {

                    personOwned.PersonId = cashTrx.PersonFromId;
                    personOwned.Person = cashTrx.PersonFrom;
                }

                personOwned.Amount = cashTrx.Amount;
                listOfPeopleOwnedBySalesman.Add(personOwned);
                personOwned.Date = cashTrx.MetaData.Created.Date_NotNull_Min;

            }

            return listOfPeopleOwnedBySalesman;
        }


    }
}
