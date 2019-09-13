using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity.EntityFramework;
using UserModels;

namespace ApplicationDbContextNS
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        ErrorSet _err;

        //static string[] exclusionArray = { "username", "passwordhash", "email", "phonenumber" };
        static string[] exclusionArray = { "passwordhash", "securitystamp", "id", "username", "phonenumber" };
        public ApplicationDbContext()
            : base("DefaultConnection")
        {

            _err = new ErrorSet();
            _err.SetLibAndClass(Assembly.GetCallingAssembly().GetName().Name, this.GetType().Name);

            try
            {
                //((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += ObjectContext_ObjectMaterialized;
                //((IObjectContextAdapter)this).ObjectContext.SavingChanges += ObjectContext_SavingChanges;
            }
            catch (Exception e)
            {
                _err.Add("Error while creating the Object Context", MethodBase.GetCurrentMethod(), e);
                throw new Exception(_err.ToString());
            }
        }


        //public static ApplicationDbContext Create()
        //{
        //    return new ApplicationDbContext();
        //}

        //private void ObjectContext_SavingChanges(object sender, EventArgs e)
        //{
        //    ObjectContext context = sender as ObjectContext;
        //    if (context != null)
        //        Encrypt(context);

        //}

        //public new IDbSet<TEntity> Set<TEntity>() where TEntity: class
        //{
        //    return base.Set<TEntity>();
        //}

        //private void Encrypt(ObjectContext context)
        //{
        //    //validate the state of each entry in the context
        //    foreach (ObjectStateEntry entry in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
        //    {

        //        if (entry.IsRelationship)
        //            continue;

        //        ICommonWithId iCommonWithId = entry.Entity as ICommonWithId;

        //        bool isCommonNull = iCommonWithId.IsNull();
        //        if (isCommonNull)
        //            continue;

        //        bool isEncrypted = false;
        //        bool isAdding = entry.State == EntityState.Added;

        //        if (isAdding)
        //            isEncrypted = new ConfigManagerHelper().IsEncrypted;
        //        //else
        //        //    isEncrypted = iCommonWithId.MetaData.IsEncrypted;

        //        if (!isEncrypted)
        //            continue;

        //        string theTicks = iCommonWithId.MetaData.GetCreatedTicks();

        //        //now, we want to encrypt all the string types, even the embeded ones.
        //        var allPropertiesOfEntity = entry.Entity.GetType().GetProperties();
        //        EncryptTheStrings(allPropertiesOfEntity, theTicks, entry.Entity);

        //    }
        //}

        //public void EncryptTheStrings(PropertyInfo[] allPropertiesOfEntity, string theTicks, object obj)
        //{
        //    ApplicationUser user = obj as ApplicationUser;
        //    bool isUser = user != null;

        //    if (allPropertiesOfEntity.IsNullOrEmpty())
        //        return;

        //    foreach (PropertyInfo propertyInfo in allPropertiesOfEntity)
        //    {
        //        if (isUser)
        //        {
        //            //exemptions
        //            string n = propertyInfo.Name.ToLower();
        //            if (exclusionArray.Any(x => x.ToLower() == propertyInfo.Name.ToLower()))
        //                continue;
        //        }

        //        try
        //        {
        //            string propertyValue = propertyInfo.GetValue(obj) as string;

        //            bool IsPropertyValue = !propertyValue.IsNullOrWhiteSpace();
        //            bool canWrite = propertyInfo.CanWrite;

        //            if (!IsPropertyValue || !canWrite)
        //                continue;


        //            //Now this property is a string and we are able to write to it... encrypt if required
        //            string orignalData = propertyValue;
        //            string encryptedData = (string)orignalData.Encrypt(theTicks);

        //            propertyInfo.SetValue(obj, Convert.ChangeType(encryptedData, propertyInfo.PropertyType), null);
        //        }
        //        catch (Exception e)
        //        {
        //            _err.Add("Error while encrypting", MethodBase.GetCurrentMethod(), e);
        //            throw new Exception(_err.ToString());

        //        }

        //    }//foreach (var props in allPropertiesOfEntity)
        //}



        //private void ObjectContext_ObjectMaterialized(object sender, System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs entity)
        //{

        //    ObjectContext context = sender as ObjectContext;

        //    if (context != null)
        //        Decrypt(entity);


        //}

        ////private void Decrypt(System.Data.Entity.Core.Objects.ObjectMaterializedEventArgs entity)
        //{
        //    ICommonWithId ic = entity.Entity as ICommonWithId;

        //    if (ic.IsNull())
        //        return; //No need to continue....

        //    //bool isEncrypted = ic.MetaData.IsEncrypted;

        //    //if (isEncrypted == false)
        //    //    return; //No need to continue....


        //    string theTicks = ic.MetaData.GetCreatedTicks();
        //    if (theTicks.IsNullOrWhiteSpace())
        //    {
        //        _err.Add("GetCreatedTicks has no value.", MethodBase.GetCurrentMethod());
        //        throw new Exception(_err.ToString());
        //    }


        //    //now we want to decrypt all the strings except for UserName
        //    //get all the properties

        //    var allPropertiesOfEntity = entity.Entity.GetType().GetProperties();
        //    DecryptTheStrings(allPropertiesOfEntity, theTicks, entity.Entity);
        //}

        ////private void DecryptTheStrings(PropertyInfo[] allPropertiesOfEntity, string theTicks, object o)
        //{
        //    if (!allPropertiesOfEntity.IsNullOrEmpty())
        //    {
        //        ApplicationUser user = o as ApplicationUser;
        //        foreach (PropertyInfo propertyInfo in allPropertiesOfEntity)
        //        {
        //            if (!user.IsNull())
        //            {
        //                if (exclusionArray.Any(x => x.ToLower() == propertyInfo.Name.ToLower()))
        //                    continue;
        //            }



        //            try
        //            {
        //                bool isString = propertyInfo.PropertyType.Name.ToLower() == "string";
        //                bool canWrite = propertyInfo.CanWrite;

        //                if (isString && canWrite)
        //                {
        //                    //Now this property is a string... decrypt if required
        //                    object objectDataValue = propertyInfo.GetValue(o);

        //                    if (objectDataValue.IsNull())
        //                        continue;

        //                    string orignalData = objectDataValue.ToString();
        //                    string decryptData = (string)orignalData.Decrypt(theTicks);
        //                    propertyInfo.SetValue(o, Convert.ChangeType(decryptData, propertyInfo.PropertyType), null);
        //                }

        //            }
        //            catch (Exception e)
        //            {
        //                _err.Add("Something bad happened during decryption.", MethodBase.GetCurrentMethod(), e);
        //                throw new Exception(_err.ToString());

        //            }


        //        }//foreach (var props in allPropertiesOfEntity)
        //    } //if (!allPropertiesOfEntity.IsNullOrEmpty())
        //}




    }
}