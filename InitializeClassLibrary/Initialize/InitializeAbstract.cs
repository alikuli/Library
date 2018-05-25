using DbContextLibrary.ModelsNS;
using ErrorHandlerLibrary.ExceptionsNS;

namespace InitializeClassLibrary.InitializeNS
{
    public abstract class InitalizeAbstract
    {
        protected static ApplicationDbContext _db;
        protected string _user;
        protected ErrorSet _err;


        public InitalizeAbstract(ApplicationDbContext db, string user)
        {
            _db = db;
            _user = user;
            _err = new ErrorSet("InitializeClassLibrary", "InitalizeAbstract", _user);
        }


        public abstract void Add();

        //public abstract void Initialize();

        public abstract void DeleteAll();

        public abstract void Edit();


    }
}