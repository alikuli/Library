
using ModelsClassLibrary.MenuNS;
using System;
using System.Collections.Generic;
namespace DatastoreNS
{
    public class ProductInitializerHelper
    {

        public ProductInitializerHelper(
            string name,
            double height,
            double width,
            double length,
            string uomShipWeightName,
            double shipWeight,
            double shipVol,
            string uomLengthName,
            string uomVolumeName,
            string uomPurchaseName,
            string uomSaleName,
            string uomWeightListedName,
            double weightListed,
            decimal mrsp,
            decimal mlsp,
            List<MenuPathHelper> menupaths)
        {
            Name = name;
            Height = height;
            Width = width;
            Length = length;
            UomShipWeightName = uomShipWeightName;
            ShipWeight = shipWeight;
            UomLengthName = uomLengthName;
            UomVolumeName = uomVolumeName;
            UomPurchaseName = uomPurchaseName;
            UomSaleName = uomSaleName;
            UomWeightListedName = uomWeightListedName;
            UomVolumeName = uomVolumeName;
            WeightListed = weightListed;
            Menupaths = menupaths;
            //UomWeightActualName = uomWeightActual;
            //ImageRelativeAddress = imageRelativeAddress;
        }
        public List<MenuPathHelper> Menupaths { get; set; }

        //public string ImageRelativeAddress { get; set; }
        public string UomPurchaseName { get; set; }
        public string UomSaleName { get; set; }
        public string UomWeightListedName { get; set; }
        public double WeightListed { get; set; }
        public string Name { get; set; }

        public string MainCategoryName { get; set; }
        public DateTime LastOrderedDate { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }


        public string UomLengthName { get; set; }
        public double Length { get; set; }

        public string UomShipWeightName { get; set; }
        public double ShipWeight { get; set; }

        public string UomVolumeName { get; set; }
        public double ShipVolume { get; set; }
        public string UomStock { get; set; }

        public decimal MLSP { get; set; }
        public decimal MRSP { get; set; }
        public override string ToString()
        {

            string name = string.Format("\"{0}\",", Name);
            string mainCat = string.Format("\"{0}\",", MainCategoryName);
            string lastOrderDt = string.Format("\"{0}\",", LastOrderedDate);
            string height = string.Format("\"{0}\",", Height);
            string width = string.Format("\"{0}\",", Width);
            string len = string.Format("\"{0}\",", Length);
            string uomShipWtName = string.Format("\"{0}\",", UomShipWeightName);
            string shipWt = string.Format("\"{0}\"", ShipWeight);

            string finalString = name;      //0
            finalString += mainCat;         //5
            finalString += LastOrderedDate; //6
            finalString += height;          //7
            finalString += width;           //8
            finalString += len;             //9
            finalString += uomShipWtName;   //10
            finalString += shipWt;          //11

            return finalString;



        }
    }
}
