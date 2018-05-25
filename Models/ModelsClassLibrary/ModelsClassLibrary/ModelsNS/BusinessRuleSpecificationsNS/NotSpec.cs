using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterfacesLibrary.BusinessRuleSpecifications;

namespace ModelsClassLibrary.ModelsNS.BusinessRuleSpecifications
{
    public class NotSpec<T>:CompositeSpec<T>
    {
        private ISpecification<T> _inner;

        public NotSpec(ISpecification<T> inner)
        {
            _inner = inner;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return ! _inner.IsSatisfiedBy(candidate);
        }
    }
}