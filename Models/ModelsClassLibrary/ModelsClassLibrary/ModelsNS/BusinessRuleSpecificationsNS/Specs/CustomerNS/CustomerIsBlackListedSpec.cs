//using ModelsClassLibrary.Models.PlayersNS;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace ModelsClassLibrary.Models.BusinessRuleSpecifications.Specs
//{
//    public class CustomerIsBlackListedSpec:CompositeSpec<Customer>
//    {

//        public override bool IsSatisfiedBy(Customer candidate)
//        {
//            if (candidate.Person == null)
//                throw new Exception("The person record was not found in IsBlackListedSpec");

//            return candidate.Person.BlackListed;
//        }

//    }
//}