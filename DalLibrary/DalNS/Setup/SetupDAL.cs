using System;
using System.Linq;
using AliKuli.Extentions;
using ApplicationDbContextNS;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS;
using UserModelsLibrary.ModelsNS;


namespace DalLibrary.DalNS
{
    public class SetupDAL :
        Repositry<Setup>
    {


        public SetupDAL(ApplicationDbContext db, IErrorSet errorsGlobal)
            : base(db, errorsGlobal)
        {
            IsInitialization = false;
            ErrorsGlobal.SetLibAndClass("DalLibrary", "SetupDAL");
        }

        #region ErrorChecking


        /// <summary>
        /// This was created because we need to switch off error checks during initialization.
        /// this is made true during intialization
        /// </summary>
        public bool IsInitialization { get; set; }
        public override void ErrorCheck(Setup entity)
        {
            //base.ErrorCheck(entity);

            //Check for duplicates
            Check_ForDuplicates(entity);

            //check to see if type matches.
            //this is switched off during initialization only
            if (!IsInitialization)
            {
                Check_IsTypeMatching(entity);
            }
        }

        private static void Check_IsTypeMatching(Setup entity)
        {
            switch (entity.Type)
            {
                case EnumTypes.Boolean:
                    if (!(entity.Value.IsValidBoolean()))
                        throw new Exception("");
                    break;

                case EnumTypes.EmailAddress:
                    if (!(entity.Value.IsValidEmail()))
                        throw new Exception("The email address is not in the correct format.");
                    break;

                case EnumTypes.EmailingMethod:
                    //if (!AliKuli.Validators.MyValidators.IsValidEmailingMethod(entity.Value))
                    //    throw new Exception("The email address is not in the correct format.");
                    //break;
                    throw new NotImplementedException("SetupDAL.EnumTypes.EmailingMethod");
                case EnumTypes.FilePath:
                    if (!(entity.Value.IsValidFilePath()))
                        throw new Exception("The file path is not in the correct format.");
                    break;
                case EnumTypes.Integer:
                    if (!(entity.Value.IsValidInteger()))
                        throw new Exception("The integer is not in the correct format.");
                    break;

                case EnumTypes.Long:
                    if (!(entity.Value.IsValidLong()))
                        throw new Exception("The long number is not in the correct format.");
                    break;
                case EnumTypes.String:
                    if (!(entity.Value is string))
                        throw new Exception("The string is not in the correct format.");
                    break;

                default: break;
            }
        }

        private void Check_ForDuplicates(Setup entity)
        {
            var itemExists = this.SearchFor(x => x.Name == entity.Name &&
                x.Type == entity.Type &&
                x.Id != entity.Id)
                .FirstOrDefault();

            if (itemExists != null)
                throw new ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException(string.Format("The item '{0}' already exists! Try again.", entity.Name));
        }

        #endregion

        public void DeleteAll()
        {
            try
            {
                //we have to add ToList otherwise we get an error that 2 readers are open.
                var aliveSetup = this.FindAll().ToList();
                if (aliveSetup != null)
                {
                    foreach (var item in aliveSetup)
                    {
                        this.Delete(item);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("There was a problem while deleting. ", e);

            }
        }

    }
}