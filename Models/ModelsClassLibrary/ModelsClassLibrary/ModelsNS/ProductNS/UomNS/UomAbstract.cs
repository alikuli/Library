using ModelsClassLibrary.ModelsNS.ProductNS.UOM;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public abstract class UomAbstract : CommonWithId, IUom
    {

        /// <summary>
        /// Multiply the inward number with this to get the UOM. This is usually 1 but in case you
        /// need to make one yourself, you can use the multiplier to adjust the value.
        /// </summary>
        public double UnitsToMakeOneOfBase { get; set; }

        /// <summary>
        /// This is a fast and easy way to calculate the base unit.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual double NoOfBaseUnits(double value)
        {
            if (value == 0)
                return 0;

            double multiplier = 1;
            if (UnitsToMakeOneOfBase != 0)
            {
                multiplier = UnitsToMakeOneOfBase;
            }
            return value * multiplier;
        }


        //public ICollection<UploadedFile> UploadedFiles { get; set; }


        //public string UploadFileLocation
        //{
        //    get { return AliKuli.ConstantsNS.MyConstants.SAVE_LOCATION_PRODUCT_UOM_LENGTH }
        //}
    }
}