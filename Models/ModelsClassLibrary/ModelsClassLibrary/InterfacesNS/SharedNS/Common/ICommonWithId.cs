﻿using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
namespace InterfacesLibrary.SharedNS
{
    public interface ICommonWithId
    {
        //ModelsClassLibrary.ModelsNS.SharedNS.DbBoolsStorageComplex DbBoolsStorageRules { get; set; }
        string Comment { get; set; }
        string DetailInfoToDisplayOnWebsite { get; set; }
        string FullName();
        bool IsAllowNameToBeSentanceCased { get; }
        string Id { get; set; }
        //string IdString();
        string Input1SortString { get; }
        string Input2SortString { get; }
        string Input3SortString { get; }
        string MakeUniqueName();
        MetaDataComplex MetaData { get; set; }
        string Name { get; set; }
        string NameInput1 { get; }
        string NameInput2 { get; }
        string NameInput3 { get; }
        void SelfErrorCheck();
        string ReturnUrl { get; set; }
        void UpdatePropertiesDuringModify(ICommonWithId icommonWithId);
        ClassesWithRightsENUM ClassNameForRights();
        int ClassNameForRightsVal();
        bool HideNameInView();
        string ClassNameRaw { get; }
        string ClassName { get; }
        string ClassNamePlural { get; }

    }
}
