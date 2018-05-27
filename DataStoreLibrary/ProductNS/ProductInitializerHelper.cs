
using System;
namespace DatastoreNS
{
    public class ProductInitializerHelper
    {

        public ProductInitializerHelper(string name, decimal sellPrice, decimal msrp, decimal mlpPrice, decimal cost, string cat1, string cat2, string cat3, DateTime lastOrderedDate, double height, double width, double length, string uomShipWeightName, double shipWeight, double shipVol, string uomHeightName, string uomWidthName, string uomLengthName, string uomVolumeName)
        {
            Name = name;
            //SellPrice = sellPrice;
            //MSRP = msrp;
            //MlpPrice = mlpPrice;
            //Cost = cost;
            //Cat1 = cat1;
            //Cat2 = cat2;
            //Cat3 = cat3;
            //LastOrderedDate = lastOrderedDate;
            Height = height;
            Width = width;
            Length = length;
            UomShipWeightName = uomShipWeightName;
            ShipWeight = shipWeight;
            UomHeightName = uomHeightName;
            UomWidthName = uomWidthName;
            UomLengthName = UomLengthName;
        }


        public string Name { get; set; }
        //public decimal SellPrice { get; set; }
        //public decimal MSRP { get; set; }
        //public decimal MlpPrice { get; set; }

        //public decimal Cost { get; set; }
        //public string Cat1 { get; set; }
        //public string Cat2 { get; set; }
        //public string Cat3 { get; set; }
        public string MainCategoryName { get; set; }
        public DateTime LastOrderedDate { get; set; }

        public string UomHeightName { get; set; }
        public double Height { get; set; }

        public string UomWidthName { get; set; }
        public double Width { get; set; }


        public string UomLengthName { get; set; }
        public double Length { get; set; }

        public string UomShipWeightName { get; set; }
        public double ShipWeight { get; set; }

        public string UomVolumeName { get; set; }
        public double ShipVolume { get; set; }

        public override string ToString()
        {

            string name = string.Format("\"{0}\",", Name);
            //string salePrice = string.Format("\"{0}\",", SellPrice);
            //string mlpPrice = string.Format("\"{0}\",", MlpPrice);
            //string msrp = string.Format("\"{0}\",", MSRP);
            //string cost = string.Format("\"{0}\",", Cost);
            string mainCat = string.Format("\"{0}\",", MainCategoryName);
            string lastOrderDt = string.Format("\"{0}\",", LastOrderedDate);
            string height = string.Format("\"{0}\",", Height);
            string width = string.Format("\"{0}\",", Width);
            string len = string.Format("\"{0}\",", Length);
            string uomShipWtName = string.Format("\"{0}\",", UomShipWeightName);
            string shipWt = string.Format("\"{0}\"", ShipWeight);

            string finalString = name;      //0
            //finalString += salePrice;       //1
            //finalString += msrp;            //2
            //finalString += mlpPrice;        //3
            //finalString += cost;            //4
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
