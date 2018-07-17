//using ModelsClassLibrary.Models.DiscountNS;

using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using Newtonsoft.Json;
using System;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public partial class Product
    {



        public virtual Product SetupAndMakeProduct(IProduct iproduct)
        {
            //saveParameters(menutPath1Id);
            SaveNameFields();
            SelfErrorCheck();

            Product product = JsonConvert.DeserializeObject<Product>(JsonConvert.SerializeObject(iproduct));
            product.IsNullThrowException("Unable to cast to Product.");

            return product;
        }

        //private void saveParameters(string menutPath1Id)
        //{
        //    Menu.MenuPath1Id = menutPath1Id;
        //}

        public virtual string MakeName()
        {

            throw new NotImplementedException();
        }

        public virtual void SaveNameFields()
        {
            throw new NotImplementedException();

        }

        #region Names Field
        /// <summary>
        /// This is where the ProductVM other data.
        /// </summary>
        public string NameFieldsData { get; set; }

        public string NameFieldsSeperator
        {
            get
            {
                return "%$%#";
            }
        }

        #endregion
    }
}