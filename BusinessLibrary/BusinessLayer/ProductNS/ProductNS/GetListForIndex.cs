

using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz
    {
        //if you set ShowUnApproved to true then only unapproved will show in the index
        //public override IList<ICommonWithId> GetListOfInActiveProductsForUser(string userId)
        //{

        //    IList <Product> lst = FindAll().Where(x => x.MetaData.IsInactive == ShowInactive).ToList() as IList<Product>;
        //    return lst.Cast<ICommonWithId>().ToList();
        //}
        //if you set this to true then only inactive will show
        public bool IsShowUnApproved { get; set; }
        public override IList<ICommonWithId> GetListForIndex()
        {

            IList<Product> lst = FindAll().Where(x => x.IsUnApproved == IsShowUnApproved).ToList() as IList<Product>;

            return lst.Cast<ICommonWithId>().ToList();
        }

        public override async Task<IList<ICommonWithId>> GetListForIndexAsync(ControllerIndexParams parameters)
        {
            IList<Product> lst = await FindAllAsync();
            lst = lst.Where(x => x.IsUnApproved == IsShowUnApproved).ToList() as IList<Product>;
            return lst.Cast<ICommonWithId>().ToList();
        }

    }
}
