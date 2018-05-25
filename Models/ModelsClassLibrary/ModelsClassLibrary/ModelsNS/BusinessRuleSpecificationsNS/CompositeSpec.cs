using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterfacesLibrary.BusinessRuleSpecifications;

namespace ModelsClassLibrary.ModelsNS.BusinessRuleSpecifications
{
    public abstract class CompositeSpec<T>:ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T candidate);

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpec<T>(this,  other);
        }

        public ISpecification<T> Not()
        {
            return new NotSpec<T>(this);
        }
    }
}