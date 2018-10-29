using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationNS;
using System.Collections.Generic;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {
        Document _document;
        Table _headingTable;
        Table _addressTable;
        Table _footerTable;
        //Table _infoTable;
        Table _bodyTable;

        private const double FOOTER_HEIGHT = 2;

        readonly static Color Color_SeaBlue = new Color(81, 125, 192);
        readonly static Color Color_Table_LtBlue = new Color(235, 240, 249);
        readonly static Color Color_Table_Gray = new Color(242, 242, 242);
        readonly static Color Color_Red = new Color(255, 0, 0);
        readonly static Color Color_NavyBlue = new Color(0, 0, 255);
        readonly static Color Color_Green = new Color(0, 255, 0);


        //List<LineItem> _lineItems { get; set; }

        internal Document CreateNewDocument(AddressVerificationModel_Header header)
        {
            _document = new Document();
            defineStyles();
            summaryLetter_Infrastructure(header);
            foreach (var letter in header.LetterList)
            {
                letter_Infrastructure(letter, header);
            }
            footerLetter_Infrastructure(header);
            
            //fillContent(param);
            return _document;


        }




    }
}
