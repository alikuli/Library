
using AliKuli.UtilitiesNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ModelsClassLibrary.ModelsNS.ProductNS;

using UserModels.Models;
using Microsoft.AspNet.Identity;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class ProductIdentifierTypeDAL:Repositry<ProductIdentifierType>
    {

        //private ApplicationDbContext _db;
        //private string _user;


        public ProductIdentifierTypeDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }


        public override void Delete(ProductIdentifierType entity)
        {
            if (IsExistInProduct(entity.Id))
                throw new ErrorHandlerLibrary.ExceptionsNS.ChildDataExistsException(string.Format("You cannot delete '{0}' because it is being used as an identifier for a product. Remove it as an identifier, then delete it."));
            base.Delete(entity);
        }

        private bool IsExistInProduct(Guid p)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// This will return the list of ProdctIdentifiers with products that are using this ProductIdentifierType. 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public ICollection<ProductIdentifier> ListOfProductIdentifiersWithThisIdentityType(long id)
        //{
        //    if (id == 0)
        //        return null;

        //    return ListOfProductIdentifiersWithThisIdentityType(this.FindFor(id));

        //}



        /// <summary>
        /// This will return the list of ProdctIdentifiers with products that are using this ProductIdentifierType. This is the base one.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public ICollection<ProductIdentifier> ListOfProductIdentifiersWithThisIdentityType(ProductIdentifierType entity)
        //{
        //    if (entity==null)
        //        return null;

        //    List<ProductIdentifier> listProductIdentifiersWithProducts = entity.ItemIdentifiers.ToList();

        //    return listProductIdentifiersWithProducts;
        //}



        /// <summary>
        /// This creates a SelectList for ProductIdentifierType
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<SelectListItem> SelectList()
        //{
        //    List<SelectListItem> selectList = new List<SelectListItem>();
        //    var itemIdentifierList = base.FindAll().ToList();

        //    if (itemIdentifierList != null)
        //    {
        //        if (itemIdentifierList.Count>0)
        //        {
        //            foreach (var item in itemIdentifierList)
        //            {
        //                SelectListItem sVM = new SelectListItem();
        //                sVM.Value = item.id;
        //                sVM.Text = item.Name;
        //                selectList.Add(sVM);
        //            }
        //        }
        //    }

        //    return selectList.OrderBy(x => x.Text).AsEnumerable();

        //}
    }
}
