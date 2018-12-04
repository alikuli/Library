using MigraDoc.DocumentObjectModel;

namespace MigraDocLibrary.FactoryNS
{

    public partial class LetterEngine
    {

        private void ToAddress_DefineColumns()
        {
            var column = _addressTable.AddColumn("6.4cm");
            column.Format.Alignment = ParagraphAlignment.Left;


        }



    }
}
