using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.ReportsNS
{
    //http://stackoverflow.com/questions/31422120/i-want-to-set-datasource-of-rdlc-report-with-model-in-mvc-4/42455071#42455071
    //http://ata2931977.blogspot.com/2012/07/rdlc-reports-in-mvc-web-application.html
    public class ReportViewModel
    {
        public enum ReportFormat { PDF = 1, Word = 2, Excel = 3 }
        public ReportViewModel()
        {
            //initation for the data set holder
            ReportDataSets = new List<ReportDataSet>();
        }

        //Name of the report
        public string Name { get; set; }

        //Language of the report
        public string ReportLanguage { get; set; }

        //Reference to the RDLC file that contain the report definition
        public string FileName { get; set; }

        //The main title for the reprt
        public string ReportTitle { get; set; }

        //The right and left titles and sub title for the report
        public string RightMainTitle { get; set; }
        public string RightSubTitle { get; set; }
        public string LeftMainTitle { get; set; }
        public string LeftSubTitle { get; set; }

        //the url for the logo, 
        public string ReportLogo { get; set; }

        //date for printing the report
        public DateTime ReportDate { get; set; }

        //the user name that is printing the report
        public string UserNamPrinting { get; set; }

        //dataset holder
        public List<ReportDataSet> ReportDataSets { get; set; }

        //report format needed
        public ReportFormat Format { get; set; }
        public bool ViewAsAttachment { get; set; }

        //an helper class to store the data for each report data set
        public class ReportDataSet
        {
            public string DatasetName { get; set; }
            public List<object> DataSetData { get; set; }
        }

        public string ReporExportFileName
        {
            get
            {
                return string.Format("attachment; filename={0}.{1}", this.ReportTitle, ReporExportExtention);
            }
        }
        public string ReporExportExtention
        {
            get
            {
                switch (this.Format)
                {
                    case ReportViewModel.ReportFormat.Word: return ".doc";
                    case ReportViewModel.ReportFormat.Excel: return ".xls";
                    default:
                        return ".pdf";
                }
            }
        }

        private string mimeType;
        public string LastmimeType 
        {
            get
            {
                return mimeType;
            }
            set
            {
                mimeType = value;
            }
        }
    }
}