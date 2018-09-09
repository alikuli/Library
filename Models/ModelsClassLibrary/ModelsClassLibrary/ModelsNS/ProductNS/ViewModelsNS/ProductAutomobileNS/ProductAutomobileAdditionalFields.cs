using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AliKuli.Extentions;
using System.ComponentModel.DataAnnotations;
namespace ModelsClassLibrary.ModelsNS.ProductNS.ViewModelsNS.ProductAutomobileNS
{
    public class ProductAutomobileAdditionalFields : IAdditionalFields
    {
        public ProductAutomobileAdditionalFields()
        {
            AutomobileGearTypeEnum = AutomobileGearTypeENUM.Unknown;
            AutomobileGearTypeEnum = AutomobileGearTypeENUM.Unknown;
        }
        private void Initialize(ProductAutomobileAdditionalFields p)
        {
            Initialize(p.ModelNumber, p.EngineSize, FuelTypeEnum, p.AutomobileGearTypeEnum, p.NumberOfSeats);
        }

        private void Initialize(string modelNumber, string engineSize, FuelTypeENUM fuelTypeEnum, AutomobileGearTypeENUM automobileGearTypeEnum, int numberOfSeats) 
        {
            ModelNumber = modelNumber;
            EngineSize = engineSize;
            FuelTypeEnum = fuelTypeEnum;
            AutomobileGearTypeEnum = automobileGearTypeEnum;
            NumberOfSeats = numberOfSeats;
        }
        [Display(Name = "Model")]
        [Required(ErrorMessage = "You must select the model name.")]
        public string ModelNumber { get; set; }



        [Required(ErrorMessage = "You must select the Engine Size.")]
        [Display(Name = "Engine")]
        public string EngineSize { get; set; }

        [Display(Name = "Fuel Type")]
        public FuelTypeENUM FuelTypeEnum { get; set; }

        [Display(Name = "Gear Type")]
        public AutomobileGearTypeENUM AutomobileGearTypeEnum  { get; set; }
        
        [Display(Name = "# of Seats")]
        public int NumberOfSeats { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void Deserialize(string additionalFieldsString)
        {
            ProductAutomobileAdditionalFields p = JsonConvert.DeserializeObject<ProductAutomobileAdditionalFields>(additionalFieldsString);
            Initialize(p);

        }

        public void CheckGearType()
        {
            if (AutomobileGearTypeEnum == AutomobileGearTypeENUM.Unknown)
            {
                throw new Exception("Gear type has not been selected.");
            }
        }

        public void CheckFuelType()
        {
            if (FuelTypeEnum == FuelTypeENUM.Unknown)
            {
                throw new Exception("Fuel type has not been selected.");
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if(!ModelNumber.IsNullOrWhiteSpace())
            {
                sb.Append(" Model: " + ModelNumber);
            }

            if (!EngineSize.IsNullOrWhiteSpace())
            {
                sb.Append(" Engine: " + EngineSize);
            }

            if (FuelTypeEnum != FuelTypeENUM.Unknown)
            {
                sb.Append(" Fuel: " + FuelTypeEnum.ToString().ToTitleSentance());
            }

            if (AutomobileGearTypeEnum != AutomobileGearTypeENUM.Unknown)
            {
                sb.Append(" Gear: " + AutomobileGearTypeEnum.ToString().ToTitleSentance());
            }

            if (NumberOfSeats != 0)
            {
                sb.Append(" Seats: " + NumberOfSeats);
            }



            return sb.ToString().Trim();
        }
    }

 
}
