
using AliKuli.UtilitiesNS;
using System.Linq;
using ModelsClassLibrary.ModelsNS.ProductNS;
using Microsoft.AspNet.Identity;
using UserModels.Models;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// </summary>
    public class ProductIdentifierDAL:Repositry<ProductIdentifier>
    {

        //private ApplicationDbContext _db;
        //private string _user;


        public ProductIdentifierDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }

        public override void ErrorCheck(ProductIdentifier entity)
        {
            base.ErrorCheck(entity);
            var found = this.SearchFor(x => x.ProductIdentifierTypeId == entity.ProductIdentifierTypeId && x.ProductId == entity.ProductId).FirstOrDefault();

            if (found != null)
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException("Duplicate Identifier. The same one exists.");

            base.Create(entity);
        }

        public override void Fix(ProductIdentifier entity)
        {
            base.Fix(entity);

            entity.Name = entity.ProductIdentifierType.FullName();
        }

        

    }
}
