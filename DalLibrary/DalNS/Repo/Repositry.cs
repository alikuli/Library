using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using System.Data.Entity;
using UserModels;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// Always Create using factory otherwise you will have problems due to IsEncrypt.
    /// Override entity.MakeName() to change the name of the entity
    /// To create the index data for a class you will need to override
    ///    IndexListVM GetIndexList(EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc) and
    ///    Task<IndexListVM> GetIndexListAsync(EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
    /// if you want to change the headings. If the heading is empty, it will not show in the index.
    /// you will only need to change the following fields as needed 
    ///    indexListVM.NameInput1
    ///    indexListVM.NameInput2
    ///    indexListVM.NameInput3
    /// Then it the View will work inshallah.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {
        //protected static DbSet<TEntity> _dbSet;
        private readonly IErrorSet _err;
        private readonly ApplicationDbContext _db;

        private DbSet<TEntity> DbSet
        {
            get
            {
                return _db.Set<TEntity>();
            }
        }

        //private IRepositry<ApplicationUser> userDAL { get { return _userDal; } }
        ////private IRepositry<ApplicationUser> userDAL { get { return new Repositry<ApplicationUser>(_db, _err); } }
        //private IRepositry<Right> rightDAL { get { return _rightDal; } }
        //private IRepositry<Right> rightDAL { get { return new Repositry<Right>(_db, _err); } }
        //IRepositry<ApplicationUser> _userDal;
        //IRepositry<Right> _rightDal;
        public Repositry(ApplicationDbContext db, IErrorSet errorSet)
        {
            _err = errorSet;
            _db = db;
            //_userDal = userDal;
            //_rightDal = rightDal;
        }
        public ErrorSet ErrorsGlobal
        {
            get
            {
                return (ErrorSet)_err;
            }

        }

        public string UserName { get; set; }



    }
}