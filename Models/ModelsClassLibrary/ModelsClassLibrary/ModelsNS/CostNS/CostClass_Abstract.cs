using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsClassLibrary.ModelsNS.CostNS
{
    /// <summary>
    /// This lives in the Home.
    /// This is just a transition point.
    /// </summary>
    [NotMapped]
    public abstract class CostClass_Abstract : ICostClass
    {
        public CostClass_Abstract()
        {
            Heading = "If you choose to continue, you will get information to help you make a decision.";
        }
        public virtual decimal Amount { get; protected set; }
        public virtual string Heading { get; set; }
        public virtual string DetailOfInfo { get; set; }
        public virtual string ReturnUrl { get; set; }
        public virtual string GoAheadUrl { get; set; }
        public string Result { get; set; }//for debugging purposes

        public override string ToString()
        {
            string str = string.Format("Press the green button if you want to continue, or the red button to cancel. If you continue, you will be charged Rs{0:N2}. ", Amount);
            return str;
        }
    }
}
