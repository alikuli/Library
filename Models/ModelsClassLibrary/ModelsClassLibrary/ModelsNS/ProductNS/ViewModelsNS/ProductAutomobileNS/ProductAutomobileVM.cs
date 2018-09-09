using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS.ViewModelsNS.ProductAutomobileNS;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels
{
    [NotMapped]
    public class ProductAutomobileVM : Product
    {

        public ProductAutomobileVM()
        {
            AdditionalFields = new ProductAutomobileAdditionalFields();
        }

        [NotMapped]
        public IAdditionalFields AdditionalFields { get; set; }

        private ProductAutomobileAdditionalFields ProductAutomobileAdditionalFields
        {
            get
            {
                return AdditionalFields as ProductAutomobileAdditionalFields;
            }
        }

        /// <summary>
        /// Make the name to display. This will be created from the ProudctVM fields and stored in its correct format into the database.
        /// </summary>
        /// <returns></returns>
        public override string MakeName()
        {
            throw new NotImplementedException();
        }



        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            ProductAutomobileAdditionalFields.CheckFuelType();
            ProductAutomobileAdditionalFields.CheckGearType();
        }


        public static ProductAutomobileVM MakeThisClassFrom(Product source)
        {
            ProductAutomobileVM target = new ProductAutomobileVM();
            PropertyDescriptorCollection sourceproperties = TypeDescriptor.GetProperties(new Product());
            PropertyDescriptorCollection targetproperties = TypeDescriptor.GetProperties(new ProductAutomobileVM());

            foreach (PropertyDescriptor pd in targetproperties)
                foreach (PropertyDescriptor _pd in sourceproperties)
                    if (pd.Name == _pd.Name)
                        pd.SetValue(target, _pd.GetValue(source));
            return target;
        }


        public Product ConvertToPresistentClass()
        {
            Product target = new Product();
            PropertyDescriptorCollection sourceproperties = TypeDescriptor.GetProperties(new ProductAutomobileVM());
            PropertyDescriptorCollection targetproperties = TypeDescriptor.GetProperties(new Product());

            foreach (PropertyDescriptor pd in targetproperties)
                foreach (PropertyDescriptor _pd in sourceproperties)
                    if (pd.Name == _pd.Name)
                        pd.SetValue(target, _pd.GetValue(this));
            return target;
        }

    }


}
