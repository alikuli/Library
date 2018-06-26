using AliKuli.Extentions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels
{
    [NotMapped]
    public class ProductAutomobileVM : Product, IProductAutomobileVM
    {
        public ProductAutomobileVM()
        {
            FuelTypeEnum = FuelTypeENUM.Unknown;
            AutomobileGearTypeEnum = AutomobileGearTypeENUM.Unknown;
        }

        #region Properties


        [Required(ErrorMessage = "You must select the brand type.")]
        public string Brand { get; set; }
        /// <summary>
        /// Eg GLX SR5
        /// </summary>
        [Display(Name = "Model")]
        [Required(ErrorMessage = "You must select the model name.")]
        public string ModelNumber { get; set; }
        /// <summary>
        /// The size of the engine
        /// </summary>

        [Required(ErrorMessage = "You must select the Engine Size.")]
        [Display(Name = "Engine")]
        public string EngineSize { get; set; }


        #region AutomobileGearTypeENUM

        [Required(ErrorMessage = "You must select the Gear type")]
        [Display(Name = "Gear Type")]
        public AutomobileGearTypeENUM AutomobileGearTypeEnum { get; set; }

        private AutomobileGearTypeENUM parseAutomobileGearTypeENUMFrom(string p)
        {
            p.IsNullThrowExceptionArgument("Autombile Gear Type String is empty");
            string fixedStr = p.RemoveAllSpaces();
            AutomobileGearTypeENUM f;
            bool success = Enum.TryParse<AutomobileGearTypeENUM>(p, out f);

            if (!success)
                throw new Exception("Unable to parse gear type Enum");

            return f;
        }

        public string AutomobileGearTypeEnumToString
        {
            get { return Enum.GetName(typeof(AutomobileGearTypeENUM), AutomobileGearTypeEnum).ToTitleSentance(); }
        }


        private AutomobileGearTypeENUM parseAutomobileGearTypeEnumFrom(string p)
        {
            p.IsNullThrowExceptionArgument("Automobile GearType String is empty");
            string fixedStr = p.RemoveAllSpaces();
            AutomobileGearTypeENUM f;
            bool success = Enum.TryParse<AutomobileGearTypeENUM>(p, out f);

            if (!success)
                throw new Exception("Unable to parse Automobile Gear Type Enum");

            return f;
        }



        #endregion


        #region FuelTypeENUM

        /// <summary>
        /// This is the vehical Fuel Type
        /// </summary>
        [Display(Name = "Fuel Type")]
        public FuelTypeENUM FuelTypeEnum { get; set; }

        /// <summary>
        /// This converts to the Fuel Type String which is stored in the Name
        /// </summary>
        public string FuelTypeEnumToString
        {
            get { return Enum.GetName(typeof(FuelTypeENUM), FuelTypeEnum).ToTitleSentance(); }
        }


        /// <summary>
        /// This gets the FuelTypeEnum from the string name.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private FuelTypeENUM parseFuelTypeEnumFrom(string p)
        {
            p.IsNullThrowExceptionArgument("Fuel Type String is empty");
            string fixedStr = p.RemoveAllSpaces();
            FuelTypeENUM f;
            bool success = Enum.TryParse<FuelTypeENUM>(p, out f);

            if (!success)
                throw new Exception("Unable to parse Fuel Type Enum");

            return f;
        }


        #endregion


        #region NumberOfSeats
        /// <summary>
        /// These are the number of seats in the vehical/Automobile
        /// </summary>
        [Display(Name = "# of Seats")]
        public int NumberOfSeats { get; set; }

        private string numberOfSeats
        {
            get
            {
                string noOfSeats = string.Format("Seats: {0}", NumberOfSeats);
                return noOfSeats;
            }
        }

        /// <summary>
        /// Extract the integer value from the string. Note it also has the word seats.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private int numberOfSeatsExtractNumber(string s)
        {
            int seats = s.ExtractNumber();
            return seats;
        }


        #endregion

        /// <summary>
        /// Any miscelaneous stuff.
        /// </summary>
        public string Misc { get; set; }

        #endregion


        /// <summary>
        /// Make the name to display. This will be created from the ProudctVM fields and stored in its correct format into the database.
        /// </summary>
        /// <returns></returns>
        public override string MakeName()
        {
            Brand.IsNullThrowExceptionArgument("Brand is null.");
            ModelNumber.IsNullOrWhiteSpaceThrowException("Model number is missing.");
            EngineSize.IsNullOrWhiteSpaceThrowException("Engine Size is missing");

            if (AutomobileGearTypeEnum == AutomobileGearTypeENUM.Unknown)
                throw new Exception("The Gear type of this model is unknown.");

            if (FuelTypeEnum == FuelTypeENUM.Unknown)
                throw new Exception("The fuel type of this model is unknown.");

            if (NumberOfSeats == 0)
                throw new Exception("The number of seats is unknown.");


            string name = string.Format("{0} {1} {2} {3} {4}",
                Brand.ToUpper(),
                ModelNumber,
                EngineSize,
                AutomobileGearTypeEnum.ToString(),
                FuelTypeEnum.ToString(),
                numberOfSeats);

            return name;

        }

        /// <summary>
        /// Save the ProductVM fields in a string format with seperators so that they can be saved to db for 
        /// retrieveal later and Makes the Name
        /// </summary>
        public override void SaveNameFields()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Brand);               //0
            sb.Append(NameFieldsSeperator);

            sb.Append(ModelNumber);         //1
            sb.Append(NameFieldsSeperator);

            sb.Append(EngineSize);          //2
            sb.Append(NameFieldsSeperator);

            sb.Append(FuelTypeEnumToString);        //3
            sb.Append(NameFieldsSeperator);

            sb.Append(numberOfSeats);       //4
            sb.Append(NameFieldsSeperator);

            sb.Append(AutomobileGearTypeEnumToString);       //5
            sb.Append(NameFieldsSeperator);

            sb.Append(Misc);       //6
            sb.Append(NameFieldsSeperator);
            NameFieldsData = sb.ToString();

            Name = MakeName();


        }

        const int BRAND = 0;
        const int MODEL_NUMBER = 1;
        const int ENGINE_SIZE = 2;
        const int FUEL_TYPE = 3;
        const int NUMBER_OF_SEATS = 4;
        const int GEAR_TYPE = 5;
        const int MISC = 6;

        /// <summary>
        /// This restores the values into the ProductVM fields for editing later.
        /// 0. Brand,       BRAND
        /// 1. ModelNumber  MODEL_NUMBER
        /// 2. EngingeSize  ENGINE_SIZE
        /// 3. FuelTypeNum  FUEL_TYPE
        /// 4. NumberOfSeats
        /// </summary>
        public void RestoreNameFields()
        {
            if (NameFieldsData.IsNullOrWhiteSpace())
                return;
            //Remember not to remove the empties because the array will get spoiled.
            string[] dataArray = NameFieldsData.Split(
                                        new string[] { NameFieldsSeperator }, StringSplitOptions.None);

            dataArray.IsNullOrEmptyThrowException("Unable to split the fields data array.");

            for (int fieldNo = 0; fieldNo < dataArray.Length; fieldNo++)
            {
                switch (fieldNo)
                {
                    case BRAND: Brand = dataArray[fieldNo];
                        break;

                    case MODEL_NUMBER: ModelNumber = dataArray[fieldNo];
                        break;

                    case ENGINE_SIZE: EngineSize = dataArray[fieldNo];
                        break;

                    case FUEL_TYPE:
                        FuelTypeEnum = parseFuelTypeEnumFrom(dataArray[fieldNo].RemoveAllSpaces());
                        break;

                    case NUMBER_OF_SEATS: NumberOfSeats = numberOfSeatsExtractNumber(dataArray[fieldNo]);
                        break;

                    case GEAR_TYPE: AutomobileGearTypeEnum = parseAutomobileGearTypeEnumFrom(dataArray[fieldNo]);
                        break;

                    case MISC: Misc = dataArray[MISC];
                        break;

                    default:
                        break;
                }
            }

        }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            checkBrand();
            checkModelNumber();
            checkEngineSize();
            checkFuelType();
            checkGearType();
        }

        #region Checks

        private void checkGearType()
        {
            if (AutomobileGearTypeEnum == AutomobileGearTypeENUM.Unknown)
            {
                throw new Exception("Gear type has not been selected.");
            }
        }

        private void checkFuelType()
        {
            if (FuelTypeEnum == FuelTypeENUM.Unknown)
            {
                throw new Exception("Fuel type has not been selected.");
            }
        }

        private void checkEngineSize()
        {
            EngineSize.IsNullOrWhiteSpaceThrowException("Engine size has not been selected.");
        }

        private void checkModelNumber()
        {
            ModelNumber.IsNullOrWhiteSpaceThrowException("Model number has not been selected.");
        }

        private void checkBrand()
        {
            Brand.IsNullOrWhiteSpaceThrowException("Brand has not been selected.");
        }
        #endregion

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


        public Product MakeProductFromThis()
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
