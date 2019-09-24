using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.SuperLayerNS
{
    public partial class SuperBiz
    {
        public void Run_Daily_Task()
        {
            MakeSuperSalesmen();
        }
        public void MakeSuperSalesmen()
        {
            List<Salesman> normalSalesmen = SalesmanBiz.FindAll().Where(x => x.IsSuperSalesman == false).ToList();
            if (normalSalesmen.IsNullOrEmpty())
                return;

            int noOfBsdRequiredToBeSuperSalesman = SuperBiz.GetNumberOfBsdToBecomeSuperSalesman();
            decimal superSalesmanCommission = Salesman.CommissionPct_Owner_Super_Salesman;
            decimal superSuperSalesmanCommission = Salesman.CommissionPct_Owner_Super_Super_Salesman;
            string subject = "Congratulations! You are now a Super Salesman!";

            string body = string.Format("CONGRATULATIONS!!! You have satisfied the requirement of {0} delivered orders. You are truely a SUPER SALESMAN. Now, you can hire other salespeople and make your own sales force that will work for you. You will get a commission of {1} for every sale of theirs. Later, when they become SUPER SALESMEN themselves, you will get {2}% from each of their team sales. Remember, when building a team, if you train them properly, they will be more successful, which will end up making YOU more successful. So, take the time to train them. Help them help you. Now, you have effectively multiplied yourself. The question is now... how big a team can you build? Its all upto you.",
                noOfBsdRequiredToBeSuperSalesman,
                superSalesmanCommission,
                superSuperSalesmanCommission);

            foreach (Salesman sm in normalSalesmen)
            {
                int ttlNoBsdForSalesman = BuySellDocBiz.FindAll().Where(x => (x.CustomerSalesmanId == sm.Id ||
                    x.OwnerSalesmanId == sm.Id ||
                    x.DeliverymanSalesmanId == sm.Id) && 
                    x.BuySellDocStateEnum == EnumLibrary.EnumNS.BuySellDocStateENUM.Delivered)
                    .Count();
                
                if (ttlNoBsdForSalesman >= noOfBsdRequiredToBeSuperSalesman)
                {
                    sm.IsSuperSalesman = true;
                    SalesmanBiz.Update(sm);

                    createMessageFor(
                        CurrentUserParameter.SystemPersonId,
                        sm.PersonId,
                        subject,
                        body);

                }
            }
        }

    }



}
