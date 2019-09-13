using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Collections.Generic;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS
{
    public class BuySellDocHistory : CommonWithId
    {
        public BuySellDocHistory()
        {

        }

        public BuySellDocHistory(BuySellDoc buySellDoc)
        {
            Init(buySellDoc);
        }

        public void Init(BuySellDoc buySellDoc)
        {
            BuySellDocStateEnum = buySellDoc.BuySellDocStateEnum;
            BuySellDocStateModifierEnum = buySellDoc.BuySellDocStateModifierEnum;
            BuySellDocumentTypeEnum = buySellDoc.BuySellDocumentTypeEnum;

            BuySellDocId = buySellDoc.Id;
            BuySellDoc = buySellDoc;

            if (BuySellDoc.BuySellDocHistorys.IsNull())
                BuySellDoc.BuySellDocHistorys = new List<BuySellDocHistory>();
            BuySellDoc.BuySellDocHistorys.Add(this);
        }


        public static BuySellDocHistory UnBox(ICommonWithId icommonWithId)
        {
            BuySellDocHistory buySellDocHistory = icommonWithId as BuySellDocHistory;
            buySellDocHistory.IsNullThrowException();
            return buySellDocHistory;
        }
        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }
        public BuySellDocStateModifierENUM BuySellDocStateModifierEnum { get; set; }
        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }

        public string BuySellDocId { get; set; }
        public BuySellDoc BuySellDoc { get; set; }

        public override string MakeUniqueName()
        {
            string name = string.Format("{0} {1} By: {2} Doc: {3} State: {4} ",
                    MetaData.Created.Date_NotNull_Min.ToLongDateString(),       //0
                    MetaData.Created.Date_NotNull_Min.ToString("hh:mm:ss"),     //1
                    MetaData.Created.By,                                        //2
                    BuySellDocumentTypeEnum.ToString().ToTitleSentance(),       //3
                    BuySellDocStateEnum.ToString().ToTitleSentance());

            if (BuySellDocStateModifierEnum != BuySellDocStateModifierENUM.Unknown)
            {
                name += string.Format(" Modifier: [{0}]",
                    BuySellDocStateModifierEnum.ToString().ToTitleSentance());
            }

            return name;
        }


        public override string ToString()
        {
            return MakeUniqueName();
        }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.BuySellDocHistory;
        }
    }
}
