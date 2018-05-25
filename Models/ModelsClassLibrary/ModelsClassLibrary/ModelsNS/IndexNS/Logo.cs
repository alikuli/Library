using AliKuli.Extentions;
using System.Drawing;

namespace ModelsClassLibrary.SharedNS

{
    public class Logo
    {
        public Logo()
        {

        }
        string _a;

        public Logo(string address)
        {
            _a = address;
        }
        public string Address { get { return _a; } }
        private double Height 
        { 
            get 
            { 
                return getImageHeight(); 
            } 
        }

        
        // We increase the TopMargin to prevent the document body from overlapping the page header.
        // We have an image of 3.5 cm height in the header.
        // The default position for the header is 1.25 cm.
        // We add 0.5 cm spacing between header image and body and get 5.25 cm.
        // Default value is 2.5 cm.

        double tableHeight = 1;
        
        private double getImageHeight()
        {
            double pointToCm = 0.0352778;

            if (!_a.IsNullOrWhiteSpace())
            {
                //string a = @"C:\\Users\\ALI\\Documents\\Visual Studio 2013\\Projects\\Libraries\\MigraDocLibrary\\MigraDocLibrary\\InvoiceNS\\DataNS\\raddicco.jpg";
                Bitmap bitmap = new Bitmap(_a);
                int iWidth = bitmap.Width;
                int iHeight = bitmap.Height;

                var height = iHeight * pointToCm / 10;
                return height;
            }
            return 0;

        }
        public double TopMarginAfterLogo()
        {
            return (Height + tableHeight + 4);
        }
    }
}
