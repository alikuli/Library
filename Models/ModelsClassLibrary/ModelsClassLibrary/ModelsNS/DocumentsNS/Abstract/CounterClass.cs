using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using DelegatesLibrary.DelegateNS;
using InterfacesLibrary.SharedNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS

{
    /// <summary>
    /// This is a class that you can use as a counter. This needs to be wired up. The method to wire
    /// it up is that you need to send a delegate to it with the signature:
    ///     public decimal Calculator (){}
    /// Once the calculation is performed the first time, IsCalculated is set to true;
    /// Amount holds the answer to the calculation. It receives it's value from the private field amount
    /// If a calculation is not performed and Amount is accessed, then Amount throws and Exception.
    /// </summary>
    public  class CounterClass : ICounterClass
    {
        public CounterClass()
        {
            IsCalculated = false;
        }
        /// <summary>
        /// If this is true, then the amount has been updated recently
        /// </summary>
        public bool IsCalculated { get; set; }
        /// <summary>
        /// this variable is used as a memory for the amount calculated
        /// </summary>
        private decimal amount = 0.00M;
        /// <summary>
        /// This is the amount that has been calculated. If the amount has not been calculated, this will throw an error.
        /// </summary>
        public decimal Amount
        {
            get
            {
                if (!IsCalculated)
                    throw new Exception("The Amount is not calculated. CounterClass");
                
                return amount;
            }
            private set
            {
                amount = value;
                IsCalculated = true;
            }
        }

        /// <summary>
        /// A delegate is assigned to this. If there is no deligate, it should throw an error.
        /// </summary>
        /// <param name="calculator">This is the delegate that will perform the calculation for the Counter Class</param>
        public void Calculator(CalculatorDelegate calculator)
        {
            if (calculator.IsNull())
                throw new Exception("Calculator Delegate not received. CounterClass.Calculator.");

            Amount = calculator();
            IsCalculated = true;
        }
    }
}



