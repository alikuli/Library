
using InvoiceNS;
using ModelClassLibrary.MigraDocNS;
using ModelsClassLibrary.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MigraDocLibrary.IndexNS
{
    public class IndexPdfParameter
    {
        //public IndexPdfParameter():
        //    this(
        //    new Logo(), 
        //    new PdfHeaderInfo(), 
        //    "",
        //    new List<IndexItemVM>(),
        //    new Headings(),"")
        //{
        //}

        public IndexPdfParameter(IndexPdfParameter indexPdfParameter)
            : this(
            indexPdfParameter.Logo, 
            indexPdfParameter.PdfHeaderInfo, 
            indexPdfParameter.WebCompanyName, 
            indexPdfParameter.DataSortedAndFiltered,
            indexPdfParameter.Headings,
            indexPdfParameter.SearchString,
            indexPdfParameter.Headings.SortOrderDescription,
            indexPdfParameter.DownloadFileName)
        {

        }

        public IndexPdfParameter(IndexListVM indexListVM)
            : this(
            indexListVM.Logo, 
            indexListVM.PdfHeaderInfo, 
            indexListVM.WebCompanyName, 
            indexListVM.DataSortedAndFiltered,
            indexListVM.Heading,
            indexListVM.SearchFor,
            indexListVM.Heading.SortOrderDescription,
            indexListVM.DownloadFileName)
        {


        }
        public IndexPdfParameter(
            Logo logo, 
            PdfHeaderInfo pdfHeaderInfo, 
            string webCompanyName, 
            IQueryable<IndexItemVM> dataSortedAndFiltered, 
            Headings headings, 
            string searchString, 
            string sortString, 
            string downloadfilename)
        {
            Logo = logo;
            PdfHeaderInfo = pdfHeaderInfo;
            WebCompanyName = webCompanyName;
            DataSortedAndFiltered = dataSortedAndFiltered;
            Headings = headings;
            SearchString = searchString;
            SortString = sortString;
            DownloadFileName = downloadfilename;
        }
        public Logo Logo { get; set; }

        /// <summary>
        /// Add information about the document info.
        /// </summary>
        public PdfHeaderInfo PdfHeaderInfo { get; set; }
        public string WebCompanyName { get; set; }

        public IQueryable<IndexItemVM> DataSortedAndFiltered { get; set; }

        public Headings Headings { get; set; }

        public string SearchString { get; set; }
        public string SortString { get; set; }
        public string DownloadFileName { get; set; }
    }
}
