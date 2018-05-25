using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterfacesLibrary.BusinessRuleSpecifications;

namespace ModelsClassLibrary.ModelsNS.BusinessRuleSpecifications
{
    public class AndSpec<T>:CompositeSpec<T>
    {
        private ISpecification<T> _leftSide;
        private ISpecification<T> _rightSide;

        public AndSpec(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            _leftSide = leftSide;
            _rightSide = rightSide;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return _leftSide.IsSatisfiedBy(candidate) && _rightSide.IsSatisfiedBy(candidate);
        }
    }
}