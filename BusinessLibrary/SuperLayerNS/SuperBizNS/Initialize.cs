using UowLibrary.PlayersNS.SalesmanCategoryNS;

namespace UowLibrary.SuperLayerNS
{
    public partial class SuperBiz
    {
        /// <summary>
        /// This sets up the data. This is data that is expected by the system.
        /// </summary>
        public void InitializeDb()
        {
            SalesmanCategoryBiz.InitializationData();
        }
    }
}
