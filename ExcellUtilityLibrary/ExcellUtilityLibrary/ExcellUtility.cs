using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using AliKuli.ExcellUtilityLibrary;

namespace AliKuli.UtilityNS.Excell
{
    public static class ExcellUtility
    {

        /// <summary>
        /// This will read in the excel file such that it will stringify the cols of each row. 
        /// Example. If there are 3 cols, the the first 3 entries will be for col 0, then 1, then Col 2,
        /// then... the 4th entry will again be col1
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<FilesDocImportVM> ImportFromExcelWithHeader(string excelFileName, string sheetName)
        {

            ExcelUtilityClass euc;
            ExcelQueryFactory excel;
            MakeExcelUtilityClass(excelFileName, out euc, out excel);

            //This creates a IQueriable<FilesDocImportVM>
            var data = from c in excel.Worksheet<FilesDocImportVM>(sheetName) select c;
            var colNames = excel.GetColumnNames(sheetName).ToArray();

            var datalist = data.ToList();

            return datalist;

        }


        //public static ExcelUtilityClass ImportFromExcelNoHeader(string excelFileName, string sheetName = "Sheet1")
        //{
        //    ExcelUtilityClass euc;
        //    ExcelQueryFactory excel;
        //    MakeExcelUtilityClass(excelFileName, out euc, out excel);


        //    var infoIn = from c in excel.WorksheetNoHeader() select c;

        //    List<string> sList = new List<string>();
        //    foreach (var row in infoIn.ToList())
        //    {

        //        foreach (var col in row)
        //        {

        //            sList.Add(col);
        //        }
        //    }

        //    euc.Data = sList;
        //    euc.ColumnNames = null;

        //    return euc;

        //}


        private static void MakeExcelUtilityClass(string excelFileName, out ExcelUtilityClass euc, out ExcelQueryFactory excel)
        {
            if (string.IsNullOrWhiteSpace(excelFileName))
                throw new Exception("No Excel File Name Passed");

            euc = new ExcelUtilityClass();
            excel = new ExcelQueryFactory(excelFileName);
        }

    }
}