using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;


namespace DalLibrary.DalNS
{
    public partial class Repositry<TEntity> : IRepositry<TEntity> where TEntity : class, ICommonWithId
    {

        /// <summary>
        /// This allows for finds without checking for rights
        /// </summary>
        /// <param name="dud"></param>
        private void checkRightExceptions(TEntity dud)
        {
            switch (dud.ClassNameForRights())
            {
                case ClassesWithRightsENUM.Country:
                    break;

                default:
                    canRetrieve();
                    break;
            }
        }




    }
}